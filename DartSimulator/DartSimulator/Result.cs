using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public class Result
	{

	//	private const int doubleFactor = 8;
	//	private const int singleOutFactor = 46;
	//	private const int singleInFactor = 28;
	//	private const int tripleFactor = 5;
	//	private const int bullFactor = 9;
	//	private const int doubleBullFactor = 3;
	//	private static DartBoard dartBoard;
	//	public static int GetFactor(FieldEnum type)
	//	{
	//		switch (type)
	//		{
	//			case FieldEnum.SingleIn:
	//				return singleInFactor;
	//			case FieldEnum.SingleOut:
	//				return singleOutFactor;
	//			case FieldEnum.Double:
	//				return doubleFactor;
	//			case FieldEnum.Triple:
	//				return tripleFactor;
	//			case FieldEnum.SingleBull:
	//				return bullFactor;
	//			case FieldEnum.DoubleBull:
	//				return doubleBullFactor;
	//		}
	//		return 0;
	//	}

	//	public static DartBoard GetDartBoard()
	//	{
	//		if (dartBoard != null)
	//			return dartBoard;
	//		else
	//		{
	//			CreateDartBoard();
	//			return dartBoard;
	//		}
	//	}

	//	private static void CreateDartBoard()
	//	{
			
	//	}
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
		public double Average { get; set; }
		public int Hits { get; set; }
		public int Tries { get; set; }

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
			return this.Tries == 0 ? 0 : this.Hits / this.Tries * 100;
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
			return this.Legs.Select(x => x.AmountDarts).Average();
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
			return ret;
		}
	}
}
