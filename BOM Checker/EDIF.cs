using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
			}
			return file_contents;
		} //read EDIF file into a string list

		private List<component_edif> filter_edif_file(List<string> edif_file)
		{
			List<component_edif> components = new List<component_edif>();
			for (int i = 0; i < edif_file.Count; i++)
			{
				string line = edif_file[i];
				if (line.Contains("(Instance C") || line.Contains("(Instance R"))
				{
					component_edif instance = new component_edif();
					instance.assign_name(line); //put the name in
					for (int j = 0; j < 25; j++)
					{
						line = edif_file[i + j];
						if (line.Contains("Property") && line.Contains("String") && !line.Contains("UniqueId"))
							instance.raw_text += line;
					}

					components.Add(instance);
				} //save next 25 lines if so, then save the object
			}
			return components;
		}

		private List<component_edif> consolidate_edif_file(List<component_edif> filtered_list)
		{
			for (int i = 0; i < filtered_list.Count; i ++)
			{
				for(int j = 0; j < filtered_list.Count; j++)
				{
					if (j > i)//pick the first one then compare all else 
					{
						if (filtered_list[i].raw_text == filtered_list[j].raw_text)
						{
							filtered_list.RemoveAt(j);
							j--;
							filtered_list[i].instances++;
						}//removal routine
					}//compare in here, if identical then delete and increment instances member
				}
			}

			return filtered_list;

		}

		private List<component_edif> assign_members(List<component_edif> consolidated_list)
		{
			foreach (component_edif current in consolidated_list)
				current.assign_members();

			return consolidated_list;
		}
	}
}