using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		private void Compare_worker_DoWork(object sender, DoWorkEventArgs e)
		{
			Console.WriteLine("Hello Worker");
			foreach (component component in edif_list)
			{
				bool[] compare = Enumerable.Repeat<bool>(false, 4).ToArray(); //by default, each element does not match
				string partno = component.partno;
				var datarow = partmast_data.Select("partno = '" + partno + "'")[0];

				//pass both edif and partmast values into unit parse then compare
				if (component.value.Contains("V"))
					compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[1].ToString())); 
				else if (component.value.Contains("W"))
					compare[0] = compare_values(unit_parse(component.value), unit_parse(datarow[2].ToString()));

				compare[1] = compare_values(unit_parse(component.footprint), unit_parse(datarow[3].ToString())); //footprint mrp and footprint edif
				compare[2] = compare_values(unit_parse(component.comment), unit_parse(datarow[4].ToString())); //value mrp and comment edif
				compare[3] = compare_values(unit_parse(component.package), unit_parse(datarow[4].ToString())); //packtype mrp and package edif

				for (int i = 0; i < compare.Length; i++)
				{
					if (compare[0] == false)
					{
						//new part_mismatch(
						//implement in here
					}
				}//make this scan and then point out the non matching part. create new instance of non match?
			}
		}

		private bool compare_values(float edif, float dbf)
		{
			bool result;

			if (edif == dbf)
				result = true;
			else
				result = false;

			return result;
		}


		private float unit_parse(string input)
		{
			float value;
			string striped = new String(input.Where(char.IsDigit).ToArray());
			if (!float.TryParse(striped, out value))
				throw new System.FormatException();

			if (input.Contains("m"))//milli
				value /= 1000;
			if (input.Contains("n"))//nano
				value /= 1000000000;
			if (input.Contains("u"))//micro
				value /= 1000000;
			if (input.Contains("p"))//pico
				value /= 1000000000000;
			if (input.Contains("K"))//kilo
				value *= 1000;
			if (input.Contains("M"))//mega
				value *= 1000000;
			if (input.Contains("/"))//fraction
				value = fraction_parse(striped);
			return value;
		}

		private float fraction_parse(string input)
		{
			string[] split = input.Split(new char[] { ' ', '/' });
			float value = -1;
			if (split.Length == 2 || split.Length == 3)
			{
				int a, b;

				if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
				{
					return a / b;
				}
			}
			return value;
		}
	}
}