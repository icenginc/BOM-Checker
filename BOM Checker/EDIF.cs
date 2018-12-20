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
						if (line.Contains("Property") && line.Contains("String") && !line.Contains("UniqueId"))
							raw_text += line;
					}

					var instance = return_object
						(raw_text);

					if (instance is component)
						components = add_component(components, (component)instance); //if reistor or capacitor
					
				} //save next 25 lines if so, then save the object
			}
			return components;
		}

		private List<component> consolidate_edif_file(List<component> filtered_list)
		{
			for (int i = 0; i < filtered_list.Count; i ++)
			{
				string str1 = filtered_list[i].raw_text;

				for (int j = 0; j < filtered_list.Count; j++)
				{
					string str2 = filtered_list[j].raw_text;
					if (j > i)//pick the first one then compare all else 
					{
						if (str1.Substring(str1.IndexOf("Property")) == str2.Substring(str2.IndexOf("Property")))
						{
							string instance = filtered_list[j].name;
							filtered_list.RemoveAt(j);
							j--;
							filtered_list[i].instances++;
							filtered_list[i].instance_names.Add(instance);
						}//removal routine
					}//compare in here, if identical then delete and increment instances member
				}
			}

			return filtered_list;

		}

		private Object return_object(string raw_text)
		{
			int index = raw_text.IndexOf("PARTNO") + 16; //skips the PACKAGE (String "
			int quote_index = raw_text.IndexOf('\"', index);
			string partno = raw_text.Substring(index, quote_index - index);

			if (partno.Length > 3) //found a partno
				partno = partno.Substring(0, 3); //get prefix
			else
				return -1;

			if (partno[0] == '3')
				return new component(raw_text); //capactior
			else if (partno[0] == '4')
				return new component(raw_text); //resistor
			else
				return -1; //not found
		}

		private List<component> add_component(List<component> components, component instance)
		{
			instance.assign_name(instance.raw_text); //put the name in

			instance.raw_text = Regex.Replace(instance.raw_text, "voltage", "voltage", RegexOptions.IgnoreCase);
			instance.raw_text = Regex.Replace(instance.raw_text, "wattage", "wattage", RegexOptions.IgnoreCase);
			instance.raw_text = Regex.Replace(instance.raw_text, "package", "package", RegexOptions.IgnoreCase);

			if (!instance.raw_text.Contains("DNI")) //dont save if its a DNI
				components.Add(instance);

			return components;
		} //take the component list, modify isntance, add to component list, return

		private List<component> assign_members(List<component> consolidated_list)
		{
			foreach (component current in consolidated_list)
				current.assign_members();

			return consolidated_list;
		}
	}
}