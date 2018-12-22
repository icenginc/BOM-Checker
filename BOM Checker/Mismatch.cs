﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BOM_Checker
{
	class part_mismatch
	{
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

		int[] compare;
		component edif;
		component bom;
		DataRow partmast;

		public part_mismatch(string error_msg)
		{
			error = error_msg;
		}

		public part_mismatch(int[] array, component component, DataRow row, string error_msg)
		{
			compare = array;
			error = error_msg;
			edif = component;
			partmast = row;
			assign_members();
		}

		public part_mismatch(int[] array, component component_edif, component component_bom, string error_msg)
		{
			compare = array;
			error = error_msg;
			edif = component_edif;
			bom = component_bom;
			assign_members_bom();
		}

		public void assign_members()
		{
			if(compare[0] != 1)
				assign_aux();
			if(compare[1] != 1)
				assign_footprint();
			if (compare[2] != 1)
				assign_value();
			if (compare[3] != 1)
				assign_package();
			if (compare[4] != 1)
				assign_temp();
			if (compare[5] != 1)
				assign_modelno();

			name = edif.name;
			partno = edif.partno;
		}

		private void assign_members_bom()
		{
			if (compare[0] != 1)
				assign_instance();
			if (compare[1] != 1)
				assign_instance_names();

			name = edif.name;
			partno = edif.partno;
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

		}

		private void assign_instance_names()
		{

		}

		public string string_mismatch()
		{
			string msg = string.Empty;

			if (mrp_value != null && edif_value != null)
				msg = "MRP:" + mrp_value + " | " + "EDIF:" + edif_value;
			else if (mrp_package != null && edif_package != null)
				msg = "MRP:" + mrp_package + " | " + "EDIF:" + edif_package;
			else if (mrp_footprint != null && edif_footprint != null)
				msg = "MRP:" + mrp_footprint + " | " + "EDIF:" + edif_footprint;
			else if (mrp_aux != null && edif_aux != null)
				msg = "MRP:" + mrp_aux + " | " + "EDIF:" + edif_aux;
			else if (mrp_instances != null && edif_instances != null)
				msg = "MRP:" + mrp_instances + " | " + "EDIF:" + edif_instances;
			else if (mrp_instance_names.Count != 0 && edif_instance_names.Count != 0)
			{
				string mrp_names = "", edif_names = "";

				foreach (string name in mrp_instance_names)
					mrp_names += (name + " ");
				foreach (string name in edif_instance_names)
					edif_names += (name + " ");

				msg = "MRP:" + mrp_names + " | " + "EDIF:" + edif_names;
			}
			else if (mrp_temp != null && edif_temp != null)
				msg = "MRP:" + mrp_temp + " | " + "EDIF:" + edif_temp;

			return msg;
		} //return based on what is populated
	}
}