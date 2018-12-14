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
			Console.WriteLine("Parsing EDIF file...");
			var filtered_file = filter_edif_file(file_contents);  //pick out the instances
			Console.WriteLine("Consolidating part instances...");
			var consolidated_list = consolidate_edif_file(filtered_file); //merge identical instances into one
			Console.WriteLine("Parsing text into values...");
			edif_list = assign_members(consolidated_list); //fill out class objects from raw text
			Console.WriteLine("Discovered " + edif_list.Count + " unique parts from EDIF file." + Environment.NewLine);

			//now doing DB reads

			Console.WriteLine("Accessing PCMRP partmast database...");
			partmast_data = get_partmast_data(edif_list);
			Console.WriteLine("Accessing PCMRP bom database...");
			bom_data = get_bom_data(bomno);
			bom_component_list = build_bom_list(bom_data);
			Console.WriteLine("Checking against EDIF entries...");
			var not_found_partmast = check_datatable(return_part_nums(edif_list), partmast_data); //check that we have matching number of entries, returns list of non-matching parts
			var not_found_bom = check_datatable(return_part_nums(edif_list), bom_data); //check this too
			handle_not_found(not_found_partmast, not_found_bom); //if there is a non-matching partno, then tell user here

			//now doing comparisons

			Console.WriteLine("Comparing each entry in EDIF file to partmast db values.. ");
			partmast_compare();
			Console.WriteLine("Comparing each entry in EDIF file to bom db values.. ");
			bom_compare();

			Console.WriteLine("Done creating error list.");
		}

		private void bom_compare()
		{
			foreach (component component_edif in edif_list)
			{
				//compare edif value with value from bom db (match based on partno, then qty and ref des')
				bool[] compare = Enumerable.Repeat<bool>(false, 2).ToArray(); //default false
				foreach (component component_bom in bom_component_list)
				{
					if (component_edif.partno == component_bom.partno)
					{
						compare[0] = compare_values(component_edif.instances, component_bom.instances); //compare number

						component_bom.instance_names.Sort();
						component_edif.instance_names.Sort();

						compare[1] = component_edif.instance_names.SequenceEqual(component_bom.instance_names);

						if (compare.Contains(false))
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
				bool[] compare = Enumerable.Repeat<bool>(false, 4).ToArray(); //by default, each element does not match
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
				if (component.value.Contains("v"))
					compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[1].ToString()));
				else if (component.value.Contains("w"))
					compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[2].ToString()));

				compare[1] = compare_values(component.footprint, datarow[3].ToString()); //footprint mrp and footprint edif
				compare[2] = compare_values(unit_parse(component.comment), unit_parse(datarow[4].ToString())); //value mrp and comment edif
				compare[3] = compare_values(unit_parse(component.package), unit_parse(datarow[5].ToString())); //packtype mrp and package edif

				if (compare.Contains(false))
					Console.WriteLine(component.name);

				build_error_list(compare, component, datarow); //scans through the bools, and adds to the error list if necessary.
			}
		} //compare partmast values with edif values of components

		private void build_error_list(bool[] compare, component edif, DataRow partmast) //overload for pcmrp
		{
			if (!compare[0])
			{
				part_mismatch auxs = new part_mismatch("aux mismatch");
				auxs.edif_aux = edif.value;
				if (edif.value.Contains("v"))
					auxs.mrp_aux = new String(partmast[1].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				else if (edif.value.Contains("w"))
					auxs.mrp_aux = new String(partmast[2].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, auxs);
			}//if aux(V or W) rating of component doesn't match (wattage or voltage)
			if (!compare[1])
			{
				part_mismatch footprints = new part_mismatch("footprint mismatch");
				footprints.edif_footprint = edif.footprint;
				footprints.mrp_footprint = new String(partmast[3].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, footprints);
			}//if footprint doesn't match
			if (!compare[2])
			{
				part_mismatch values = new part_mismatch("value mismatch");
				values.edif_value = edif.value;
				values.mrp_value = new String(partmast[4].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, values);
			}// if value of component don't match
			if (!compare[3])
			{
				part_mismatch packages = new part_mismatch("package mismatch");
				packages.edif_package = edif.package;
				packages.mrp_package = new String(partmast[5].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
				assign_error_name(edif, packages);
			}// if package of component doesnt match
		} //if there is a false then adds to error_list and fills the data.

		private void build_error_list(bool[] compare, component edif, component bom) //overload for bom 
		{
			if (!compare[0])
			{
				part_mismatch instances = new part_mismatch("instance mismatch");
				instances.edif_instances = edif.instances.ToString();
				instances.mrp_instances = bom.instances.ToString();
				assign_error_name(edif, instances);
			}//if instances of component doesn't match
			if (!compare[1])
			{
				part_mismatch instance_names = new part_mismatch("instance_names mismatch");
				instance_names.edif_instance_names = edif.instance_names;
				instance_names.mrp_instance_names = bom.instance_names;
				assign_error_name(edif, instance_names);
			}//if instance names doesn't match

		}//if there is a false then adds to error_list and fills the data.

		private void assign_error_name(component component, part_mismatch mismatch)
		{
			mismatch.name = component.name;
			mismatch.partno = component.partno;
			error_list.Add(mismatch);
		} //(generic assignments put into this funct, error specific ones left in build_error_list)

		private bool compare_values(float edif, float dbf)
		{
			bool result;

			if (edif == dbf)
				result = true;
			else
				result = false;

			return result;
		}

		private bool compare_values(string edif, string dbf)
		{
			edif = new String(edif.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLower();
			dbf = new String(dbf.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLower();
			//remove whitespace and make lowercase

			bool result;

			if (edif == dbf)
				result = true;
			else
				result = false;

			return result;
		}


		private float unit_parse(string input)
		{
			float value;
			string striped = new String(input.Where(character => char.IsDigit(character) || char.IsPunctuation(character)).ToArray());
			if (!float.TryParse(striped.Replace("/", ""), out value) && input != "")
				throw new System.FormatException();

			if (input.Contains("m"))//milli
				value /= 1000;
			if (input.Contains("n"))//nano
				value /= 1000000000;
			if (input.Contains("u"))//micro
				value /= 1000000;
			if (input.Contains("p"))//pico
				value /= 1000000000000;
			if (input.Contains("k") || input.Contains("K"))//kilo
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
	}
}