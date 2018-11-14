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

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			var table = GetYourData();
			var dataRow = table.Rows[0];
			for(int i = 0; i < table.Columns.Count; i++)
			{
				Console.WriteLine(table.Columns[i]);
				/*
				foreach (var item in dataRow.ItemArray)
				{
					Console.WriteLine(item);
				}
				*/
			}

			foreach (DataRow row in table.Rows)
			{
				Console.WriteLine(row["partno"]);
			}

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
	}
}
