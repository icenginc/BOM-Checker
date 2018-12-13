using System;
using System.Collections.Generic;

class part_mismatch
{
	public string partno;
	public string mrp_value, edif_value; //also called 'comment' in altium
	public string mrp_package, edif_package;
	public string mrp_footprint, edif_footprint;
	public string mrp_aux, edif_aux; //also called 'wattage/voltage'in altium
	public string mrp_instances, edif_instances;
	public List<string> mrp_instance_names, edif_instance_names = new List<string>();
	public string name;
	public string error;

	public part_mismatch(string error_msg)
	{
		error = error_msg;
	}
}