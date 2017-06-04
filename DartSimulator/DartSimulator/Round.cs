using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public class Round
	{
		public Field Dart1;
		public Field Dart2;
		public Field Dart3;
		public override string ToString()
		{
			if (this.Dart1 == null)
				return "";
			var ret = this.Dart1.ToString();
			if (this.Dart2 == null)
				return ret;
			ret += " " + this.Dart2;
			if (this.Dart3 == null)
				return ret;
			ret += " " + this.Dart3;
			return ret;
		}
	}
}
