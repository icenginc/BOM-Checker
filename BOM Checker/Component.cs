using System;

class component_edif
{
	public string name;
	public string partno;
	public string footprint;
	public string value; //wattage or voltage
	public string comment; //value
	public string package;
	public int instances = 1;
	public string raw_text;

	public void assign_members()
	{
		assign_partno();
		assign_footprint();
		assign_value();
		assign_comment();
		assign_package();
	}

	public void assign_name(string line)
	{
		int index = line.IndexOf("Instance") + 9; //skips the PACKAGE (String "
		
		name = line.Substring(index, line.Length - index);
	}

	private void assign_package()
	{
		int index;
		if(raw_text.Contains("package"))
			index = raw_text.IndexOf("package") + 17; //skips the PACKAGE (String "
		else 
			index = raw_text.IndexOf("PACKAGE") + 17; //skips the PACKAGE (String "
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
		{
			if (raw_text.Contains("WATTAGE"))
				key = "WATTAGE";
			if (raw_text.Contains("wattage"))
				key = "wattage";
		}
		if (name.Contains("C"))
		{
			if (raw_text.Contains("VOLTAGE"))
				key = "VOLTAGE";
			if (raw_text.Contains("voltage"))
				key = "voltage";
		}

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

	private void assign_partno()
	{
		int index = raw_text.IndexOf("PARTNO") + 16; //skips the PACKAGE (String "
		int quote_index = raw_text.IndexOf('\"', index);
		partno = raw_text.Substring(index, quote_index - index);
	}
}