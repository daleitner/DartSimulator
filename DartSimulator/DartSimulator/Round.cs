using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public class Round
	{
		public Field Dart1;
		public Field Dart2;
		public Field Dart3;
		public int Sum => (this.Dart1?.Value ?? 0) + (this.Dart2?.Value ?? 0) + (this.Dart3?.Value ?? 0);
		public int Tries { get; set; }

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
