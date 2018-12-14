using System;
using System.Collections.Generic;
using System.Linq;
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

	public string string_mismatch()
	{
		string msg = string.Empty;

		if (mrp_value != null && edif_value != null)
			msg = "MRP:" + mrp_value + " | " + "EDIF:" + edif_value;
		else if (mrp_package != null && edif_package != null)
			msg = "MRP:" + mrp_package + " | " + "EDIF:" + edif_package;
		else if(mrp_footprint != null && edif_footprint != null)
			msg = "MRP:" + mrp_footprint + " | " + "EDIF:" + edif_footprint;
		else if(mrp_aux != null && edif_aux != null)
			msg = "MRP:" + mrp_aux + " | " + "EDIF:" + edif_aux;
		else if(mrp_instances != null && edif_instances != null)
			msg = "MRP:" + mrp_instances + " | " + "EDIF:" + edif_instances;
		else if(mrp_instance_names.Count != 0 && edif_instance_names.Count != 0)
			msg = "MRP:" + mrp_instances + " | " + "EDIF:" + edif_instances;

		return msg;
	} //return based on what is populated
}