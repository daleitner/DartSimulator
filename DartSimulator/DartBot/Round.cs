using Dart.Base;

namespace DartBot
{
	public class Round
	{
		public Field Dart1;
		public Field Dart2;
		public Field Dart3;
		public int Sum => (this.Dart1?.Value ?? 0) + (this.Dart2?.Value ?? 0) + (this.Dart3?.Value ?? 0);
		public int Tries { get; set; }
		public int Rest { get; set; }
		public int Index { get; set; }

		public override string ToString()
		{
			if (this.Dart1 == null)
				return "";
			var ret = this.Index + ". " + this.Rest + "\t" + this.Sum + " (" + this.Dart1;
			if (this.Dart2 != null)
			{
				ret += " " + this.Dart2;
				if (this.Dart3 != null)
					ret += " " + this.Dart3;
			}
			ret += ")";
			return ret;
		}
	}
}
