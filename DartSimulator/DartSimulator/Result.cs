using System;
using System.Collections.Generic;
using System.Linq;
using DartBot;

namespace DartSimulator
{
	public class Result
	{
		public Result()
		{
			this.Legs = new List<Leg>();
		}
		public List<Leg> Legs { get; set; }
		public double DoubleQuote => CalculateDoubleQuote();
		public int HundretEighties => GetHundretEighties();
		public int HundretFourties => GetHundretFourties();
		public int Hundrets => GetHundrets();
		public int WorstLeg => GetWorstLeg();
		public int BestLeg => GetBestLeg();
		public double DartAverage => GetDartAverage();
		public double Average => GetAverage();
		public int Hits => this.Legs.Count;
		public int Tries => GetTries();

		private int GetHundretEighties()
		{
			int amount = this.Legs.Sum(leg => leg.Runden.Count(round => round.Sum == 180));
			return amount;
		}

		private int GetHundretFourties()
		{
			int amount = this.Legs.Sum(leg => leg.Runden.Count(round => round.Sum >= 140 && round.Sum < 180));
			return amount;
		}

		private int GetHundrets()
		{
			int amount = this.Legs.Sum(leg => leg.Runden.Count(round => round.Sum >= 100 && round.Sum < 140));
			return amount;
		}

		private double CalculateDoubleQuote()
		{
			return this.Tries == 0 ? 0 : (double)this.Hits / this.Tries * 100;
		}

		private int GetWorstLeg()
		{
			if (this.Legs == null || this.Legs.Count == 0)
				return 0;
			return this.Legs.Select(x => x.AmountDarts).Max();
		}

		private int GetBestLeg()
		{
			if (this.Legs == null || this.Legs.Count == 0)
				return 0;
			return this.Legs.Select(x => x.AmountDarts).Min();
		}

		private double GetDartAverage()
		{
			if (this.Legs.Count == 0)
				return 0.0;
			return Math.Round(this.Legs.Select(x => x.AmountDarts).Average(), 2);
		}

		private double GetAverage()
		{
			var points = this.Legs.Select(x => x.Points).Sum();
			var darts = this.Legs.Select(x => x.AmountDarts).Sum();
			if (darts == 0)
				return 0.0;
			return Math.Round((double)points/darts*3, 2);
		}

		private int GetTries()
		{
			if (this.Legs.Count == 0)
				return 0;
			return this.Legs.Select(x => x.Tries).Sum();
		}

		public override string ToString()
		{
			var ret = "";
			ret += "Legs (Count=" + Legs.Count + "):";
			foreach (var leg in this.Legs)
			{
				ret += "\n\t" + leg;
			}
			ret += "\n3-Dart-Average: " + this.Average;
			ret += "\nDoppelquote: " + this.DoubleQuote;
			ret += "\nBest Leg: " + this.BestLeg;
			ret += "\nWorst Leg: " + this.WorstLeg;
			ret += "\nDart-Average: " + this.DartAverage;
			ret += "\n100er: " + this.Hundrets;
			ret += "\n140er: " + this.HundretFourties;
			ret += "\n180er: " + this.HundretEighties;
			ret += "\nHits: " + this.Hits;
			ret += "\nTries: " + this.Tries;
			return ret;
		}
	}
}
