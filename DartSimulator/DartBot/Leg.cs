using System.Collections.Generic;
using System.Linq;

namespace DartBot
{
	public class Leg
	{
		public Leg()
		{
			this.Runden = new List<Round>();
		}
		public List<Round> Runden;
		public int AmountDarts => GetAmountDarts();
		public int Points => GetPoints();
		public int Tries => this.Runden.Select(x => x.Tries).Sum();
		public int Index { get; set; }

		private int GetAmountDarts()
		{
			if (this.Runden.Count == 0)
				return 0;
			if (this.Runden.Last().Dart3 != null)
				return this.Runden.Count*3;
			if(this.Runden.Last().Dart2 != null)
				return this.Runden.Count*3 - 1;
			return this.Runden.Count*3 - 2;
		}

		private int GetPoints()
		{
			if (this.Runden.Count == 0)
				return 0;
			return this.Runden.Select(x => x.Sum).Sum();
		}

		public override string ToString()
		{
			return this.Index + ".Leg: " + this.AmountDarts;
		}
	}
}
