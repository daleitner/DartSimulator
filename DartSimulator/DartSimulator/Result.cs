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
			this.Legs = new ObservableCollection<Leg>();
		}
		public ObservableCollection<Leg> Legs { get; set; }
		public double DoubleQuote { get; set; }
		public int HundretEighties { get; set; }
		public int HundretFourties { get; set; }
		public int Hundrets { get; set; }
		public int WorstLeg { get; set; }
		public int BestLeg { get; set; }
		public double DartAverage { get; set; }
		public double Average { get; set; }
	}
}
