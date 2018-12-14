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

		private DataTable get_partmast_data(List<component> edif_list)
		{
			DataTable results = new DataTable();
			List<string> part_nums = return_part_nums(edif_list);
			string part_list = return_part_list(part_nums).ToUpper();

			OleDbConnection connection = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=C:\temp\pcmrpw\PARTMAST.dbf"); //need to install this provider

			connection.Open();

			if (connection.State == ConnectionState.Open)
			{
				string mySQL = "SELECT `partno`, `aux1`, `aux2`, `footprint`, `value`, `packtype` FROM PARTMAST WHERE `partno` IN (" + part_list + ")";  // dbf table + columns
				OleDbCommand MyQuery = new OleDbCommand(mySQL, connection);
				OleDbDataAdapter DA = new OleDbDataAdapter(MyQuery);

				DA.Fill(results);

				connection.Close();
			}

			//CreateCSVFile(ref results, "C:\\temp\\pcmrpw\\Excel Export\\partmast.csv"); //for visual tool

			return results;
		}

		private DataTable get_bom_data(string bomno)
		{
			if (bomno == "" || !bomno.Contains("-"))
			{
				MessageBox.Show("Invalid BOM Number entry!");
				throw new InvalidDataException();
			}
				

			DataTable results = new DataTable();
			//List<string> part_nums = return_part_nums(edif_list);
			//string part_list = return_part_list(part_nums);

			OleDbConnection connection = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=C:\temp\pcmrpw\BOM.dbf"); //need to install this provider

			connection.Open();

			if (connection.State == ConnectionState.Open)
			{
				string mySQL = "SELECT `bomno`, `partno`, `qty`, `refdesmemo` FROM BOM WHERE `bomno`='" + bomno + "';";  // dbf table + columns
				OleDbCommand cmd = new OleDbCommand(mySQL, connection);
				OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

				DA.Fill(results);

				connection.Close();
			}

			//CreateCSVFile(ref results, "C:\\temp\\pcmrpw\\Excel Export\\bom.csv"); //for visual tool

			return results;
		}

		private List<component> build_bom_list(DataTable bom)
		{
			List<component> bom_list = new List<component>();

			foreach (DataRow row in bom.Rows)
			{
				component new_component = new component(row);

				new_component.assign_members_bom();
				string temp = new_component.instance_names[0]; //first part name from each row -> to make next lien more readable
				if ((temp.StartsWith("R") || temp.StartsWith("C")) && Char.IsDigit(temp[1])) //if starts with C or R, and has a number after that
					bom_list.Add(new_component);
			}//loop through all entries in db

			return bom_list;
		}

		private List<string> return_part_nums(List<component> edif_list)
		{
			List<string> part_nums = new List<string>();

			foreach (component component in edif_list)
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
		} //this function returns a string list of the parts in the edif component list

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
		} //checks that each part from the edif file is found within bom or partmast database

		private void handle_not_found(List<string> not_found_partmast, List<string> not_found_bom)
		{
			if (not_found_partmast.Count == 0)
				Console.WriteLine("EDIF partno's all found in PartMast DB");
			else
			{
				string error = "";
				foreach (string part in not_found_partmast)
					error += part + " ";
				MessageBox.Show("Part number(s) not found in PartMast DB: " + error);
			} //if there are any entries that are not found in the partmast database, notify user

			if (not_found_bom.Count == 0)
				Console.WriteLine("EDIF partno's all found in BOM DB");
			else
			{
				string error = "";
				foreach (string part in not_found_bom)
					error += part + " ";
				MessageBox.Show("Part number(s) not found in BOM DB: " + error);
			} //if there are any entries that are not found in the bom database, notify user
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