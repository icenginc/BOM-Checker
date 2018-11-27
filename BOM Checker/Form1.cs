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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		string path = "\\\\backup-server\\Assembly Drawings\\TEST1077\\EDIF\\TEST1077_Schematic.EDF"; //temrporary hardcode

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) //find edif file button
		{
			try
			{
				CommonOpenFileDialog dialog = new CommonOpenFileDialog(); //run in Nuget -> Install-Package Microsoft.WindowsAPICodePack-Shell -Version 1.1.0
				dialog.InitialDirectory = "\\\\backup-server\\Assembly Drawings\\";
				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					textbox_edif.Text = dialog.FileName;
					path = dialog.FileName;
				} //set path to variables
			}
			catch
			{
				MessageBox.Show("Error in file select");
			}
		}

		private void button_parse_Click(object sender, EventArgs e)
		{
			var file_contents = read_edif_file(path);  //read in the file into memory
			var filtered_file = filter_edif_file(file_contents);  //pick out the instances
			var consolidated_list = consolidate_edif_file(filtered_file); //merge identical instances into one
			var complete_list = assign_members(consolidated_list); //fill out class objects from raw text
		}//parse edif file

		private void button_db_Click(object sender, EventArgs e)
		{
			var table = GetYourData();
			var dataRow = table.Rows[0];
			for (int i = 0; i < table.Columns.Count; i++)
			{
				Console.WriteLine(table.Columns[i]);
			}

			foreach (DataRow row in table.Rows)
			{
				Console.WriteLine(row["partno"]);
			}
		} //read the pcmrp db
	}

	

}
