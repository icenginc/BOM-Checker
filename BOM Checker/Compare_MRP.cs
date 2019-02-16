using System;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		private void Compare_worker_DoWork(object sender, DoWorkEventArgs e)
		{
			//now doing EDIF file

			Console.WriteLine("Reading EDIF file...");
			var file_contents = read_edif_file(edif_path);  //read in the file into memory
			if (!stop)
			{
				Console.WriteLine("Parsing EDIF file...");
				var filtered_file = filter_edif_file(file_contents);  //pick out the instances and add values 
				Console.WriteLine("Consolidating part instances...");
				//var consolidated_list
				edif_list = consolidate_edif_file(filtered_file); //merge identical instances into one
				//Console.WriteLine("Parsing text into values...");
				//edif_list = assign_members(consolidated_list); //fill out class objects from raw text
				Console.WriteLine("Discovered " + edif_list.Count + " unique parts from EDIF file." + Environment.NewLine);
			}
			//now doing DB reads
			if (!stop)
			{
				Console.WriteLine("Accessing PCMRP partmast database...");
				partmast_data = get_partmast_data(edif_list);
				Console.WriteLine("Accessing PCMRP bom database...");
				bom_data = get_bom_data(bomno);
				bom_component_list = build_bom_list(bom_data);
				Console.WriteLine("Checking against EDIF entries...");
				var not_found_partmast = check_datatable(return_part_nums(edif_list), partmast_data); //check that we have matching number of entries, returns list of non-matching parts
				var not_found_bom = check_datatable(return_part_nums(edif_list), bom_data); //check this too
				handle_not_found(not_found_partmast, not_found_bom); //if there is a non-matching partno, then tell user here
			}
			//now doing comparisons
			if (!stop)
			{
				Console.WriteLine("Comparing each entry in EDIF file to partmast db values.. ");
				partmast_compare();
				Console.WriteLine("Comparing each entry in EDIF file to bom db values.. ");
				bom_compare();
				Console.WriteLine("Resolving cascading errors.. ");
				error_compare();

				Console.WriteLine("Done creating error list.");
			}

			stop = false;
		}

		private void bom_compare()
		{
			foreach (component component_edif in edif_list)
			{
				//compare edif value with value from bom db (match based on partno, then qty and ref des')
				int[] compare = new int[2];
				foreach (component component_bom in bom_component_list)
				{
					if (component_edif.partno == component_bom.partno)
					{
						component_bom.instance_names.Sort();
						component_edif.instance_names.Sort();

						if (component_edif.checks[6])
						{
							compare[0] = compare_values(component_edif.instances, component_bom.instances); //compare number
							compare[1] = Convert.ToInt32(component_edif.instance_names.SequenceEqual(component_bom.instance_names));
						} //filter based on checks 
						else
							compare = new int[] { 1, 1};

						if (compare.Contains(-1) || compare.Contains(0))
							Console.WriteLine(component_edif.name);

						build_error_list(compare, component_edif, component_bom);

					}//do comparison in here
				}//find the matching bom component, then compare the values

			}//loop through edif list
			
		} //compare bom part numbers and names with edif names/instances of components

		private void partmast_compare()
		{
			foreach (component component in edif_list)
			{
				int[] compare = new int[6];
				string partno = component.partno.ToUpper();
				DataRow datarow = partmast_data.NewRow(); //placeholder row to use conditional below
				if (partno != "")
					datarow = partmast_data.Select("partno = '" + partno + "'")[0];
				else
				{
					string error = "Partno missing from entry \"" + component.name + "\" in EDIF file.";
					Console.WriteLine(error);
					part_mismatch name_missing = new part_mismatch(error);
				}


				//pass both edif and partmast values into unit parse then compare
				if (component.checks[0])
				{
					if (component.type == 'C' || component.type == 'F')
						compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[1].ToString()));
					else if (component.type == 'R')
						compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[2].ToString()));
				}
				else
					compare[0] = 1; //if it is not the type with an aux, then skip over it
				if (component.checks[1])
					if (datarow[3].ToString().Contains(';'))
					{
						var split = datarow[3].ToString().Split(';');
						int one = compare_values(component.footprint, remove_whitespace(split[0]));
						int two = compare_values(component.footprint, remove_whitespace(split[1]));

						if (one == 1 || two == 1)
							compare[5] = 1;
						else if (one == -1 && two == -1)
							compare[5] = -1;
						else
							compare[5] = 0;
					}
					else
						compare[1] = compare_values(component.footprint, datarow[3].ToString()); //footprint mrp and footprint edif
				else
					compare[1] = 1;
				if (component.checks[2])
					compare[2] = compare_values(unit_parse(component.comment), unit_parse(datarow[4].ToString())); //value mrp and comment edif
				else
					compare[2] = 1;
				if (component.checks[3])
					compare[3] = compare_values(component.package, datarow[5].ToString()); //packtype mrp and package edif
				else
					compare[3] = 1;
				if (component.checks[4])
					compare[4] = compare_values(component.temp, datarow[6].ToString()); //rating mrp and temperature edif
				else
					compare[4] = 1;
				if (component.checks[5])
				{
					if (component.type == 'S')
					{
						int one = compare_values(component.modelno, datarow[7].ToString());
						int two = compare_values(component.modelno, datarow[3].ToString());
						if (one == 1 || two == 1)
							compare[5] = 1;
						else
							compare[5] = one;
					}//socket has to compare modleno edif to modelno mrp OR footprint mrp
					else
						compare[5] = compare_values(component.modelno, datarow[7].ToString()); //modelno mrp and modelno edif -> normal
				}
				else
					compare[5] = 1;

				if (compare.Contains(-1) || compare.Contains(0))
					Console.WriteLine(component.name);

				build_error_list(compare, component, datarow); //scans through the bools, and adds to the error list if necessary.
			}
		} //compare partmast values with edif values of components

		private void error_compare()
		{
			for (int i = 0; i < error_list.Count; i++)
			{
				for (int j = 0; j < error_list.Count; j++)
				{
					if (i < j && error_list[i].name == error_list[j].name)
					{
						if (error_list[i].error.Contains("mismatch") && error_list[j].error.Contains("Instance") && !error_list[i].error.Contains("Instance"))
						{
							Console.WriteLine("Found cascading error: " + error_list[j].name);
							error_list[j].error += " [Warning]";
						}
					}//if an error of the same part, but not the same exact error
				}//2nd iterator
			}//1st iterator
		}//goal of this is to mark cascaded instance errors as warnings

		private void build_error_list(int[] compare, component edif, DataRow partmast) //overload for pcmrp
		{
			string[] errors = new string[6] { "Aux ", "Footprint ", "Value ", "Package ", "Temperature ", "ModelNo "};

			for (int i = 0; i < compare.Length; i++)
			{
				if (compare[i] != 1)
				{
					string error = errors[i] + generate_error(compare[i]);
					part_mismatch mismatch = new part_mismatch(edif, partmast, error);
					error_list.Add(mismatch);
				} //if its wrong
			} //go through compare list
		} //if there is a false then adds to error_list and fills the data.

		private void build_error_list(int[] compare, component edif, component bom) //overload for bom 
		{
			string[] errors = new string[2] { "Instance ", "Instance Names "};

			for (int i = 0; i < compare.Length; i++)
			{
				if (compare[i] != 1)
				{
					string error = errors[i] + generate_error(compare[i]);
					part_mismatch mismatch = new part_mismatch(edif, bom, error);
					error_list.Add(mismatch);
				} //if its wrong
			} //go through compare list
		}//if there is a false then adds to error_list and fills the data.

		private string generate_error(int input)
		{
			if (input == -1)
				return "missing";
			if (input == 1)
				return "correct";
			if (input == 0)
				return "mismatch";

			return "error";
		}

		private int compare_values(float edif, float dbf)
		{
			if (edif == -1 || dbf == -1)
				return -1; // data missing, make sure to flag

			if (edif == -2)
				return 1; //dont check, it is not supposed to be 

			if (edif == -3 || dbf == -3)
				return 0; //invalid data, make sure to flag

			return (Convert.ToInt32(edif == dbf)); //otherwise normal 0 and 1
		}

		private int compare_values(string edif, string dbf)
		{
			if (edif == null || dbf == null)
				return -1; //missing
			
			edif = new String(edif.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLower();
			dbf = new String(dbf.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLower();
			//remove whitespace and make lowercase

			if (edif == "" || dbf == "")
				return -1; //missing

			if (edif == dbf)
				return 1;
			else
				return 0;
		}


		private float unit_parse(string input)
		{
			/*
			if (input == null)
				return -2; //if not supposed to be tested
				*/
			float value;
			string striped = new String(input.Where(character => char.IsDigit(character) || char.IsPunctuation(character)).ToArray());
			
			if (input.Replace(" ", "") == "")
				return -1; //if missing
			else if (!float.TryParse(striped.Replace("/", ""), out value) && input != "")
				return 0; //if cant parse, because invalid format, then make sure it is wrong.
				//throw new System.FormatException();
				
			if (input.Contains("m"))//milli
				value /= 1000;
			if (input.Contains("n"))//nano
				value /= 1000000000;
			if (input.Contains("u"))//micro
				value /= 1000000;
			if (input.Contains("p"))//pico
				value /= 1000000000000;
			if (input.Contains("K"))//kilo
				value *= 1000;
			if (input.Contains("M"))//mega
				value *= 1000000;
			if (input.Contains("/"))//fraction
				value = fraction_parse(striped);
			return value;
		}

		private float fraction_parse(string input)
		{
			string[] split = input.Split(new char[] { ' ', '/' });
			float value = -1;
			if (split.Length == 2 || split.Length == 3)
			{
				int a, b;

				if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
				{
					return (float)a / b;
				}
			}
			return value;
		}

		public static string remove_whitespace(string input)
		{
			return new String(input.Where(ch => !char.IsWhiteSpace(ch)).ToArray());
		}
	}
}
/*
		private part_mismatch assign_error_members(int i, DataRow partmast, component edif, part_mismatch mismatch)
		{
			switch (i)
			{
				case 0:
					mismatch.edif_aux = edif.value;
					if (edif.type == 'C' || edif.type == 'F')
						mismatch.mrp_aux = remove_whitespace(partmast[1].ToString());
					else if (edif.type == 'R')
						mismatch.mrp_aux = remove_whitespace(partmast[2].ToString());
					break;
				case 1:
					mismatch.edif_footprint = edif.footprint;
					mismatch.mrp_footprint = remove_whitespace(partmast[3].ToString());
					break;
				case 2:
					mismatch.edif_value = edif.value;
					mismatch.mrp_value = remove_whitespace(partmast[4].ToString());
					break;
				case 3:
					mismatch.edif_package = edif.package;
					mismatch.mrp_package = remove_whitespace(partmast[5].ToString());
					break;
				case 4:
					mismatch.edif_temp = edif.temp;
					mismatch.mrp_temp = remove_whitespace(partmast[6].ToString());
					break;
				case 5:
					mismatch.edif_modelno = edif.modelno;
					mismatch.mrp_modelno = remove_whitespace(partmast[7].ToString());
					break;
			}
			mismatch.name = edif.name;
			mismatch.partno = edif.partno;

			return mismatch;
		} //(generic assignments put into this funct, error specific ones left in build_error_list)
		*/

/*
	private part_mismatch assign_error_members(int i, component bom, component edif, part_mismatch mismatch)
	{
		switch (i)
		{
			case 0:
				mismatch.edif_instances = edif.instances.ToString();
				mismatch.mrp_instances = bom.instances.ToString();
				break;
			case 1:
				mismatch.edif_instance_names = edif.instance_names;
				mismatch.mrp_instance_names = bom.instance_names;
				break;
		}
		mismatch.name = edif.name;
		mismatch.partno = edif.partno;

		return mismatch;
	}
	*/

/*
			if (compare[0] != 1)
			{
				string error = "Aux " + generate_error(compare[0]);
				part_mismatch auxs = new part_mismatch(error);

				auxs.edif_aux = edif.value;
				if (edif.type == 'C' || edif.type == 'F')
					auxs.mrp_aux = new String(partmast[1].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				else if (edif.type == 'R')
					auxs.mrp_aux = new String(partmast[2].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());

				assign_error_name(edif, auxs);
			}//if aux(V or W) rating of component doesn't match (wattage or voltage)
			if (compare[1] != 1)
			{
				string error = "Footprint " + generate_error(compare[1]);
				part_mismatch footprints = new part_mismatch(error);

				footprints.edif_footprint = edif.footprint;
				footprints.mrp_footprint = new String(partmast[3].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, footprints);
			}//if footprint doesn't match
			if (compare[2] != 1)
			{
				string error = "Value " + generate_error(compare[2]);
				part_mismatch values = new part_mismatch(error);

				values.edif_value = edif.value;
				values.mrp_value = new String(partmast[4].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, values);
			}// if value of component don't match
			if (compare[3] != 1)
			{
				string error = "Package " + generate_error(compare[3]);
				part_mismatch packages = new part_mismatch(error);

				packages.edif_package = edif.package;
				packages.mrp_package = new String(partmast[5].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, packages);
			}// if package of component doesnt match
			if (compare[4] != 1)
			{
				string error = "Temperature " + generate_error(compare[4]);
				part_mismatch temperatures = new part_mismatch(error);

				temperatures.edif_temp = edif.temp;
				temperatures.mrp_temp = new String(partmast[6].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, temperatures);
			}//if temperatuer of component doesnt match
			if (compare[5] != 1)
			{
				string error = "ModelNo " + generate_error(compare[4]);
				part_mismatch modelno = new part_mismatch(error);

				modelno.edif_temp = edif.temp;
				modelno.mrp_temp = new String(partmast[6].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, modelno);
			}
			*/ //old way, explicit instead of looping




/*
if (compare[0] != 1)
{
	part_mismatch instances = new part_mismatch("Instance mismatch");
	instances.edif_instances = edif.instances.ToString();
	instances.mrp_instances = bom.instances.ToString();
	assign_error_name(edif, instances);
}//if instances of component doesn't match
if (compare[1] != 1)
{
	part_mismatch instance_names = new part_mismatch("Instance Names mismatch");
	instance_names.edif_instance_names = edif.instance_names;
	instance_names.mrp_instance_names = bom.instance_names;
	assign_error_name(edif, instance_names);
}//if instance names doesn't match
*/ //rewrite this in general loop fashion