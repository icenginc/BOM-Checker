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

		private DataTable get_partmast_data(List<component_edif> edif_list)
		{
			DataTable results = new DataTable();
			List<string> part_nums = return_part_nums(edif_list);
			string part_list = return_part_list(part_nums);

			OleDbConnection yourConnectionHandler = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=C:\temp\pcmrpw\PARTMAST.dbf"); //need to install this provider

			yourConnectionHandler.Open();

			if (yourConnectionHandler.State == ConnectionState.Open)
			{
				string mySQL = "SELECT `partno`, `aux1`, `aux2`, `footprint`, `value`, `packtype` FROM PARTMAST WHERE `partno` IN (" + part_list + ")";  // dbf table + columns
				OleDbCommand MyQuery = new OleDbCommand(mySQL, yourConnectionHandler);
				OleDbDataAdapter DA = new OleDbDataAdapter(MyQuery);

				DA.Fill(results);

				yourConnectionHandler.Close();
			}

			//CreateCSVFile(ref results, "C:\\temp\\pcmrpw\\Excel Export\\partmast.csv");

			return results;
		}

		private List<string> return_part_nums(List<component_edif> edif_list)
		{
			List<string> part_nums = new List<string>();

			foreach (component_edif component in edif_list)
				part_nums.Add(component.partno);

			return part_nums;
		}

		private string return_part_list(List<string> part_nums)
		{
			string part_list = "";

			foreach (string part_num in part_nums)
				part_list += ("'" + part_num + "', ");

			part_list = part_list.Substring(0, part_list.Length - 2); //cut off the last ", "

			return part_list;
		}

		private List<string> check_datatable(List<string> part_nums, DataTable results)
		{
			List<string> not_found = new List<string>();

			foreach (string part in part_nums)
			{
				DataRow[] selected = results.Select("partno = '" + part + "'");
				if (selected.Length < 0)
					not_found.Add(part);
			}

			return not_found;
		}

		private void handle_not_found(List<string> not_found)
		{
			if (not_found.Count == 0)
				Console.WriteLine("EDIF partno's all found in DB");
			else
			{
				string error = "";
				foreach (string part in not_found)
					error += part + " ";
				MessageBox.Show("Part number(s) not found in database: " + error);
			} //if there are any entries that are not found in the database, notify user
		}
		/** For debug use only **/

		public void CreateCSVFile(ref DataTable dt, string strFilePath) //<- for development aid
		{
			try
			{
				// Create the CSV file to which grid data will be exported.
				StreamWriter sw = new StreamWriter(strFilePath, false);
				// First we will write the headers.
				//DataTable dt = m_dsProducts.Tables[0];
				int iColCount = dt.Columns.Count;
				for (int i = 0; i < iColCount; i++)
				{
					sw.Write(dt.Columns[i]);
					if (i < iColCount - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);

				// Now write all the rows.

				foreach (DataRow dr in dt.Rows)
				{
					for (int i = 0; i < iColCount; i++)
					{
						if (!Convert.IsDBNull(dr[i]))
						{
							sw.Write(dr[i].ToString());
						}
						if (i < iColCount - 1)
						{
							sw.Write(",");
						}
					}

					sw.Write(sw.NewLine);
				}
				sw.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}