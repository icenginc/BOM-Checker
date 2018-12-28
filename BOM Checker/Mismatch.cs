using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BOM_Checker
{
	class part_mismatch
	{
		private string type;
		public string partno;
		public string mrp_value, edif_value; //also called 'comment' in altium
		public string mrp_package, edif_package;
		public string mrp_footprint, edif_footprint;
		public string mrp_aux, edif_aux; //also called 'wattage/voltage'in altium
		public string mrp_instances, edif_instances;
		public string mrp_temp, edif_temp; //also called 'rating' in mrp
		public string mrp_modelno, edif_modelno;
		public List<string> mrp_instance_names = new List<string>(), edif_instance_names = new List<string>();
		public string name;
		public string error;

		component edif;
		component bom;
		DataRow partmast;

		public part_mismatch(string error_msg)
		{
			error = error_msg;
			assign_type();
		}

		public part_mismatch(component component, DataRow row, string error_msg)
		{
			error = error_msg;
			edif = component;
			partmast = row;
			assign_members();
		}

		public part_mismatch(component component_edif, component component_bom, string error_msg)
		{
			error = error_msg;
			edif = component_edif;
			bom = component_bom;
			assign_members_bom();
		}

		public void assign_members()
		{
			assign_type();
			if(type == "Aux")
				assign_aux();
			if(type == "Footprint")
				assign_footprint();
			if (type == "Value")
				assign_value();
			if (type == "Package")
				assign_package();
			if (type == "Temperature")
				assign_temp();
			if (type == "ModelNo")
				assign_modelno();

			name = edif.name;
			partno = edif.partno;
		}

		private void assign_members_bom()
		{
			assign_type();
			if (type == "Instance")
				assign_instance();
			if (type == "Instance Names")
				assign_instance_names();

			name = edif.name;
			partno = edif.partno;
		}

		private void assign_type()
		{
			var split = error.Split(' ');
			if (split.Count() == 2)
				type = split[0];
			else if
				(split.Count() == 3)
				type = "Instance Names"; //only type that contains a space
		}

		private void assign_aux()
		{
			edif_aux = edif.value;
			if (edif.type == 'C' || edif.type == 'F')
				mrp_aux = partmast[1].ToString();
			else if (edif.type == 'R')
				mrp_aux = Form1.remove_whitespace(partmast[2].ToString());
		}

		private void assign_footprint()
		{
			edif_footprint = edif.footprint;
			mrp_footprint = Form1.remove_whitespace(partmast[3].ToString());
		}

		private void assign_value()
		{
			edif_value = edif.value;
			mrp_value = Form1.remove_whitespace(partmast[4].ToString());
		}

		private void assign_package()
		{
			edif_package = edif.package;
			mrp_package = Form1.remove_whitespace(partmast[5].ToString());
		}

		private void assign_temp()
		{
			edif_temp = edif.temp;
			mrp_temp = Form1.remove_whitespace(partmast[6].ToString());
		}

		private void assign_modelno()
		{
			edif_modelno = edif.modelno;
			mrp_modelno = Form1.remove_whitespace(partmast[7].ToString());
		}

		private void assign_instance()
		{
			edif_instances = edif.instances.ToString();
			mrp_instances = bom.instances.ToString();
		}

		private void assign_instance_names()
		{
			edif_instance_names = edif.instance_names;
			mrp_instance_names = bom.instance_names;
		}

		public string string_mismatch()
		{
			string msg = string.Empty;
			string[] errors = new string[8] { "Aux", "Footprint", "Value", "Package", "Temperature", "ModelNo", "Instance", "Instance Names" };

			if (type == errors[2])
				msg = "MRP:" + mrp_value + " | " + "EDIF:" + edif_value;
			else if (type == errors[3])
				msg = "MRP:" + mrp_package + " | " + "EDIF:" + edif_package;
			else if (type == errors[1])
				msg = "MRP:" + mrp_footprint + " | " + "EDIF:" + edif_footprint;
			else if (type == errors[0])
				msg = "MRP:" + mrp_aux + " | " + "EDIF:" + edif_aux;
			else if (type == errors[6])
				msg = "MRP:" + mrp_instances + " | " + "EDIF:" + edif_instances;
			else if (type == errors[7])
			{
				string mrp_names = "", edif_names = "";

				foreach (string name in mrp_instance_names)
					mrp_names += (name + " ");
				foreach (string name in edif_instance_names)
					edif_names += (name + " ");

				msg = "MRP:" + mrp_names + " | " + "EDIF:" + edif_names;
			}
			else if (type == errors[4])
				msg = "MRP:" + mrp_temp + " | " + "EDIF:" + edif_temp;
			else if (type == errors[5])
				msg = "MRP:" + mrp_modelno + " | " + "EDIF:" + edif_modelno;

			return msg;
		} //return based on what is populated
	}
}