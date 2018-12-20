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
		assign_name();
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

	private void assign_name()
	{
		int offset = 9;
		int index = raw_text.IndexOf("Instance") + offset; //skips the (Instance "
		
		name = new String(raw_text.Substring(index, raw_text.IndexOf("(Property") - index).Where(c => !char.IsWhiteSpace(c)).ToArray());
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
}