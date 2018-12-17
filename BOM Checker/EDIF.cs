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
				if (line.Contains("(Instance C") || line.Contains("(Instance R"))
				{
					component instance = new component();
					instance.assign_name(line); //put the name in
					for (int j = 0; j < 25; j++)
					{
						line = edif_file[i + j];
						if (line.Contains("Property") && line.Contains("String") && !line.Contains("UniqueId"))
							instance.raw_text += line;
					}
					instance.raw_text = instance.raw_text.ToLower();
					if(!instance.raw_text.Contains("dni")) //dont save if its a DNI
						components.Add(instance);
				} //save next 25 lines if so, then save the object
			}
			return components;
		}

		private List<component> consolidate_edif_file(List<component> filtered_list)
		{
			for (int i = 0; i < filtered_list.Count; i ++)
			{
				for(int j = 0; j < filtered_list.Count; j++)
				{
					if (j > i)//pick the first one then compare all else 
					{
						if (filtered_list[i].raw_text == filtered_list[j].raw_text)
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

		private List<component> assign_members(List<component> consolidated_list)
		{
			foreach (component current in consolidated_list)
				current.assign_members();

			return consolidated_list;
		}
	}
}