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
		string path = "\\\\backup-server\\Assembly Drawings\\TEST1077\\EDIF\\TEST1077_Schematic.EDF";

		public Form1()
		{
			InitializeComponent();
		}

		private List<string> read_edif_file(string path)
		{
			List<string> file_contents = new List<string>();

			try
			{
				using (StreamReader read = new StreamReader(path))
				{
					string line;
					while((line = read.ReadLine()) != null)
						file_contents.Add(line);
				}
			}
			catch
			{
				Console.WriteLine("Error in reading EDIF file");
			}
			return file_contents;
		}

		public DataTable GetYourData()
		{
			DataTable YourResultSet = new DataTable();

			OleDbConnection yourConnectionHandler = new OleDbConnection(
				@"Provider=VFPOLEDB.1;Data Source=C:\temp\pcmrpw\PARTMAST.dbf");

			// if including the full dbc (database container) reference, just tack that on
			//      OleDbConnection yourConnectionHandler = new OleDbConnection(
			//          "Provider=VFPOLEDB.1;Data Source=C:\\SomePath\\NameOfYour.dbc;" );


			// Open the connection, and if open successfully, you can try to query it
			yourConnectionHandler.Open();

			if (yourConnectionHandler.State == ConnectionState.Open)
			{
				string mySQL = "select * from PARTMAST";  // dbf table name

				OleDbCommand MyQuery = new OleDbCommand(mySQL, yourConnectionHandler);
				OleDbDataAdapter DA = new OleDbDataAdapter(MyQuery);

				DA.Fill(YourResultSet);

				yourConnectionHandler.Close();
			}

			return YourResultSet;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				CommonOpenFileDialog dialog = new CommonOpenFileDialog();
				dialog.InitialDirectory = "\\\\backup-server\\Assembly Drawings\\";
				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					textbox_edif.Text = dialog.FileName;
					path = dialog.FileName;
				}
			}
			catch
			{
				MessageBox.Show("Unable to open Assembly Drawings");

			}
			//path.Replace("\\", "/");
			/*
			if (!Form1.edit_lock)
				textbox_lotreports.Text = path + "\\Lot_Reports\\";
			*/
		}

		private void button_parse_Click(object sender, EventArgs e)
		{
			var file_contents = read_edif_file(path);
		}

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
		}
	}

	class component
	{
		public string partno;
		public string footprint;
		public string value; //wattage or voltage
		public string comment; //value
		public string package;
	}

}
