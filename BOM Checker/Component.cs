using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
class component
{
	public string name;
	public string partno;
	public string footprint;
	public string value; //wattage or voltage
	public string comment; //value
	public string package;
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
	}

	public component() { } //empty constructor

	public void assign_members()
	{
		assign_partno();
		assign_footprint();
		assign_value();
		assign_comment();
		assign_package();
		assign_temp();
	}

	public void assign_members_bom()
	{	
		partno = new String(row["partno"].ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
		assign_instances(row["qty"].ToString());
		assign_instance_names(row["refdesmemo"].ToString());
	}

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
				instance_names.Add(new String(ref_des.Where(ch => !char.IsWhiteSpace(ch)).ToArray()));
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

	public void assign_name(string line)
	{
		int index = line.IndexOf("Instance") + 9; //skips the (Instance "
		
		name = new String(line.Substring(index, line.IndexOf("(Property") - index).Where(c => !char.IsWhiteSpace(c)).ToArray());
		instance_names.Add(name); //add itself to the instance names
	}

	private void assign_package()
	{
		int index;
			index = raw_text.IndexOf("package") + 17; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		package = raw_text.Substring(index, quote_index - index);
	}

	private void assign_comment()
	{
		int index = raw_text.IndexOf("Comment") + 17; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		comment = raw_text.Substring(index, quote_index - index);
		
	}

	private void assign_value() //either watts or volts depending on if R or C
	{
		string key = "";
		if (name.Contains("R"))
				key = "wattage";
		if (name.Contains("C"))
				key = "voltage";

		int index = raw_text.IndexOf(key) + 17; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		value = raw_text.Substring(index, quote_index - index);
	}

	private void assign_footprint()
	{
		int index = raw_text.IndexOf("Footprint") + 19; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		footprint = raw_text.Substring(index, quote_index - index);
	}

	private void assign_temp()
	{
		int index = raw_text.IndexOf("TEMPERATURE") + 20; //skips the TEMPERATURE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		footprint = raw_text.Substring(index, quote_index - index);
	}

	private void assign_partno()
	{
		int index = raw_text.IndexOf("PARTNO") + 16; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		partno = raw_text.Substring(index, quote_index - index);
	}
}