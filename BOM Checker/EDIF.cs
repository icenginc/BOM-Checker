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

		private List<component> filter_edif_file(List<string> edif_file)
		{
			List<component> components = new List<component>();
			for (int i = 0; i < edif_file.Count; i++)
			{
				string line = edif_file[i];
				if (line.Contains("(Instance C") || line.Contains("(Instance R"))
				{
					component instance = new component();
					instance.raw_text += line;
					for (int j = 0; j < 25; j++)
					{
						line = edif_file[i + j];
						if (line.Contains("Property") && line.Contains("String"))
							instance.raw_text += line;
					}

					components.Add(instance);
				} //save next 25 lines if so, then save the object
			}
			return components;
		}
	}
}