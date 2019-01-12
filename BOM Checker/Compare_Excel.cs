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
		private void Compare_worker_DoWork_Excel(object sender, DoWorkEventArgs e)
		{
			//now doing EDIF file

			Console.WriteLine("Reading EDIF file...");
			var file_contents = read_edif_file(edif_path2);  //read in the file into memory
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
			//now doing Excel read
			if (!stop)
			{
				var excel_file = read_excel_file(excel_path);
			}
		}
	}
}