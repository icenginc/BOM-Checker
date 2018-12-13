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
		string bomno = "814-1077"; //temporary hardcode
		List<component> edif_list, bom_component_list = new List<component>();
		DataTable partmast_data, bom_data = new DataTable();
		List<part_mismatch> error_list = new List<part_mismatch>(); //to store errors in

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
			BackgroundWorker edif_worker = new BackgroundWorker();
			edif_worker.DoWork += edif_worker_DoWork;
			edif_worker.RunWorkerAsync();
		}//parse edif file

		

		private void button_db_Click(object sender, EventArgs e)
		{
			BackgroundWorker db_worker = new BackgroundWorker();
			db_worker.DoWork += db_worker_DoWork;
			db_worker.RunWorkerAsync();
		} //read the pcmrp db

		private void db_worker_DoWork(object sender, DoWorkEventArgs e)
		{
			Console.WriteLine("Accessing PCMRP partmast database...");
			partmast_data = get_partmast_data(edif_list);
			Console.WriteLine("Accessing PCMRP bom database...");
			bom_data = get_bom_data(bomno);
			bom_component_list = build_bom_list(bom_data);
			Console.WriteLine("Checking against EDIF entries...");
			var not_found_partmast = check_partmast_datatable(return_part_nums(edif_list), partmast_data); //check that we have matching number of entries, returns list of non-matching parts
			var not_found_bom = check_bom_datatable(return_part_nums(edif_list), bom_data); //check this too
			handle_not_found(not_found_partmast, not_found_bom); //if there is a non-matching partno, then tell user here
		}

		private void edif_worker_DoWork(object sender, DoWorkEventArgs e)
		{
			Console.WriteLine("Reading EDIF file...");
			var file_contents = read_edif_file(path);  //read in the file into memory
			Console.WriteLine("Parsing EDIF file...");
			var filtered_file = filter_edif_file(file_contents);  //pick out the instances
			Console.WriteLine("Consolidating part instances...");
			var consolidated_list = consolidate_edif_file(filtered_file); //merge identical instances into one
			Console.WriteLine("Parsing text into values...");
			edif_list = assign_members(consolidated_list); //fill out class objects from raw text
			Console.WriteLine("Discovered " + edif_list.Count + " unique parts from EDIF file." + Environment.NewLine);
		}

		private void textbox_bomno_textchanged(object sender, EventArgs e)
		{
			bomno = textbox_bomno.Text;
		}

		private void button_compare_Click(object sender, EventArgs e)
		{
			BackgroundWorker compare_worker = new BackgroundWorker();
			compare_worker.DoWork += Compare_worker_DoWork;
			compare_worker.RunWorkerAsync();
			foreach (part_mismatch error in error_list)
				Console.WriteLine(error);
		}
	}
}
