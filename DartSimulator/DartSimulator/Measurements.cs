using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public static class Measurements
	{
		private const int doubleFactor = 8;
		private const int singleOutFactor = 46;
		private const int singleInFactor = 28;
		private const int tripleFactor = 5;
		private const int bullFactor = 9;
		private const int doubleBullFactor = 3;
		private static DartBoard dartBoard;
		public static int GetFactor(FieldEnum type)
		{
			switch (type)
			{
				case FieldEnum.SingleIn:
					return singleInFactor;
				case FieldEnum.SingleOut:
					return singleOutFactor;
				case FieldEnum.Double:
					return doubleFactor;
				case FieldEnum.Triple:
					return tripleFactor;
				case FieldEnum.SingleBull:
					return bullFactor;
				case FieldEnum.DoubleBull:
					return doubleBullFactor;
			}
			return 0;
		}

		public static DartBoard GetDartBoard()
		{
			if (dartBoard != null)
				return dartBoard;
			else
			{
				CreateDartBoard();
				return dartBoard;
			}
		}

		private static void CreateDartBoard()
		{
			
		}
	}
}
