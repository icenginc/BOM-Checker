using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{

		private List<string> read_edif_file(string path)
		{
			List<string> file_contents = new List<string>();

			try
			{
				using (StreamReader read = new StreamReader(path))
				{
					string line;
					while ((line = read.ReadLine()) != null)
						file_contents.Add(line);
				}
			}
			catch
			{
				Console.WriteLine("Error in reading EDIF file");
				MessageBox.Show("Error reading EDIF file, not valid!");
				stop = true;
				this.Invoke((MethodInvoker)delegate
				{
					button_clear.PerformClick(); //do a clear
				});
			}
			return file_contents;
		} //read EDIF file into a string list

		private List<component> filter_edif_file(List<string> edif_file)
		{
			List<component> components = new List<component>();
			for (int i = 0; i < edif_file.Count; i++)
			{
				string line = edif_file[i];
				if (line.Contains("(Instance "))
				{
					string raw_text = string.Empty;
					raw_text += line; //add in teh instance name line -> but wait! causes fail in raw text match
					for (int j = 0; j < 25; j++)
					{
						line = edif_file[i + j];
						if (valid_line(line))
							raw_text += line;
					}

					if (!raw_text.Contains("DNI")) //dont save if its DNI
					{
						component addition = new component(raw_text); //auto populate the members
						if(addition.type != '\0') //if valid component
							components.Add(addition); //adding it to the list
					}
					/*
					var instance = return_object(raw_text);

					if (instance is component)
					{
						components = add_component(components, (component)instance); //if reistor or capacitor
					}
					*/ //this is if we want to differentiate component objects, decided to use the same


				} //save next 25 lines if so, then save the object
			}
			return components;
		}

		private bool valid_line(string line)
		{
			bool valid = false;

			if (!line.Contains("Property"))
				return false;
			if (!line.Contains("String"))
				return false;
			if (line.Contains("UniqueId"))
				return false;
			if (line.Contains("PhysicalPath")) //causes problems bc of cells
				return false;

			if (line.Contains("Instance"))
				valid = true;
			if (line.Contains("Comment"))
				valid = true;
			if (line.Contains("Footprint"))
				valid = true;
			if (line.Contains("voltage") || line.Contains("VOLTAGE") || line.Contains("Voltage"))
				valid = true;
			if (line.Contains("wattage") || line.Contains("WATTAGE") || line.Contains("Wattage"))
				valid = true;
			if (line.Contains("PACKAGE") || line.Contains("Package") || line.Contains("package"))
				valid = true;
			if (line.Contains("TEMPERATURE"))
				valid = true;
			if (line.Contains("MODELNO"))
				valid = true;
			if (line.Contains("PARTNO"))
				valid = true;

			return valid;
		}

		private List<component> consolidate_edif_file(List<component> filtered_list)
		{
			for (int i = 0; i < filtered_list.Count; i ++)
			{
				string str1 = filtered_list[i].raw_text.Substring(filtered_list[i].raw_text.IndexOf("Property"));
				for (int j = 0; j < filtered_list.Count; j++)
				{
					string str2 = filtered_list[j].raw_text.Substring(filtered_list[j].raw_text.IndexOf("Property"));
					if (j > i)//pick the first one then compare all else 
					{
						if (str1 == str2)
						{
							string instance = filtered_list[j].name;
							filtered_list.RemoveAt(j);
							j--;
							filtered_list[i].instances++;
							if(!filtered_list[i].instance_names.Contains(instance)) //prevent duplicates
								filtered_list[i].instance_names.Add(instance);
						}//removal routine
					}//compare in here, if identical then delete and increment instances member
				}
			}

			return filtered_list;
		}
	}
}