using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BOM_Checker
{
	class component
	{
		public char type;
		public List<bool> checks = new List<bool>();
		public string name;
		public string partno;
		public string footprint;
		public string value; //wattage or voltage
		public string comment; //value
		public string package;
		public string modelno;
		public string temp; //rating in mrp
		public int instances = 1;
		public List<string> instance_names = new List<string>();
		public string raw_text;
		public DataRow row;

		public component(DataRow input_row)
		{
			row = input_row;
		}

		public component(string text)
		{
			raw_text = text;
			replace_text();

			assign_name();
			assign_partno();
			assign_type();
			assign_footprint();
			assign_package();
			assign_temp();
			assign_checks();
			assign_members(); //do value and comment for some, modelno for some
		}

		public component() { } //empty constructor

		private void replace_text()
		{
			raw_text = Regex.Replace(raw_text, "voltage", "voltage", RegexOptions.IgnoreCase);
			raw_text = Regex.Replace(raw_text, "wattage", "wattage", RegexOptions.IgnoreCase);
			raw_text = Regex.Replace(raw_text, "package", "package", RegexOptions.IgnoreCase);
		}

		private void assign_members() //res, cap, fuse
		{
			if (type == 'R' || type == 'C' || type == 'F')
			{
				assign_value();
				assign_comment();
			}
			if (type == 'I' || type == 'S' || type == 'T')
			{
				assign_modelno();
			}
		}

		private void assign_checks()
		{
			foreach (CheckBox check in Form1.checks)
				checks.Add(check.Checked);

			if (type == 'R' || type == 'C' || type == 'F')
				checks[5] = false; //modelno

			if (type == 'I' || type == 'S' || type == 'T')
			{
				checks[0] = false; //aux
				checks[2] = false; //value
			}
		}

		public void assign_members_bom()
		{
			partno = Form1.remove_whitespace(row["partno"].ToString());
			assign_instances(row["qty"].ToString());
			assign_instance_names(row["refdesmemo"].ToString());
		}

		private void assign_type()
		{
			int index = raw_text.IndexOf("PARTNO") + 16; //skips the PACKAGE (String "
			int quote_index = raw_text.IndexOf('\"', index);
			string partno = raw_text.Substring(index, quote_index - index);

			if (partno.Length == 8) //found a partno
			{
				partno = partno.Substring(0, 3); //get prefix
				if (partno[0] == '2')
					type = 'I';
				else if (partno[0] == '3')
					type = 'C';
				else if (partno[0] == '4')
					type = 'R';
				else if (partno[0] == '5')
					type = 'F';
				else if (partno[0] == '6')
				{
					if (partno[1] == '1')
						type = 'S';
					else if (partno[1] == '3')
						type = 'T';
				}
				else
					type = '\0';
			}
			else
				type = '\0';
		}//use partnum to do type

		private void assign_instance_names(string input)
		{
			input = input.Remove(0, 11); //remove "Per board: "
			var ref_dess = input.Split(',');
			foreach (string ref_des in ref_dess)
			{
				if (ref_des.Contains('-'))
				{
					var range = ref_des.Split('-');
					string begin = new string(range[0].Where(c => char.IsDigit(c)).ToArray());
					string end = new string(range[1].Where(c => char.IsDigit(c)).ToArray());
					Int32.TryParse(begin, out int one);
					Int32.TryParse(end, out int two);
					int span = (two - one) + 1; //this is how many in between, plus the one itself

					for (int i = 0; i < span; i++)
					{
						string name = new string(range[0].Where(c => Char.IsLetter(c)).ToArray());
						instance_names.Add(name + (one + i));
					}

				}//if there is a range
				else
					instance_names.Add(Form1.remove_whitespace(ref_des));
			}
		}

		private void assign_instances(string qty)
		{
			float temp;
			if (!float.TryParse(qty, out temp)) //if this fails, set to 0
				instances = 0;
			else if (!Int32.TryParse(temp.ToString(), out instances)) //if above doesnt fail, then this fails, set to 0
				instances = 0;
		}//string to float to int conversion (bom database gives string based float)

		private void assign_name()
		{
			int offset = 9;
			int index = raw_text.IndexOf("Instance") + offset; //skips the (Instance "

			name = Form1.remove_whitespace(raw_text.Substring(index, raw_text.IndexOf("(Property") - index));
			instance_names.Add(name); //add itself to the instance names
		}

		private void assign_package()
		{
			int quote_index, offset = 17;
			int index = raw_text.IndexOf("package") + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			package = raw_text.Substring(index, quote_index - index);
		}

		private void assign_comment()
		{
			int offset = 17;
			int quote_index, index = raw_text.IndexOf("Comment") + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			comment = raw_text.Substring(index, quote_index - index);

		}

		private void assign_value() //either watts or volts depending on if R or C
		{
			string key = "";
			if (name.Contains("R"))
				key = "wattage";
			if (name.Contains("C"))
				key = "voltage";

			int offset = 17;
			int quote_index, index = raw_text.IndexOf(key) + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			value = raw_text.Substring(index, quote_index - index);
		}

		private void assign_footprint()
		{
			int offset = 19;
			int quote_index, index = raw_text.IndexOf("Footprint") + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			footprint = raw_text.Substring(index, quote_index - index);
		}

		private void assign_temp()
		{
			int offset = 19;
			int quote_index, index = raw_text.IndexOf("TEMPERATURE") + offset; //skips the TEMPERATURE (String "
			if (index - offset > 0) //this makes it so if the key is not found, then dont go in here
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			temp = raw_text.Substring(index, quote_index - index);
		}

		private void assign_partno()
		{
			int offset = 16;
			int quote_index, index = raw_text.IndexOf("PARTNO") + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			partno = raw_text.Substring(index, quote_index - index);
		}

		private void assign_modelno()
		{
			int offset = 17;
			int quote_index, index = raw_text.IndexOf("MODELNO") + offset; //skips the PACKAGE (String "
			if (index - offset > 0)
				quote_index = raw_text.IndexOf('\"', index);
			else
				quote_index = offset - 1;
			modelno = raw_text.Substring(index, quote_index - index);
		}
	}
}