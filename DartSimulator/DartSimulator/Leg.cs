using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public class Leg
	{
		public Leg()
		{
			this.Runden = new List<Round>();
		}
		public List<Round> Runden;
		public int AmountDarts => GetAmountDarts();

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
	}
}
