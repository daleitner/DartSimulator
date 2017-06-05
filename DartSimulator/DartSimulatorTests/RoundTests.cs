using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using DartSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartSimulatorTests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class RoundTests
	{
		[TestMethod]
		public void VerifyRoundWithOneDart()
		{
			var round = new Round
			{
				Dart1 = new Field(20, FieldEnum.SingleOut)
			};
			Approvals.Verify(round);
		}

		[TestMethod]
		public void VerifyRoundWithTwoDarts()
		{
			var round = new Round
			{
				Dart1 = new Field(20, FieldEnum.SingleOut),
				Dart2 = new Field(60, FieldEnum.Triple)
			};
			Approvals.Verify(round);
		}

		[TestMethod]
		public void VerifyRoundWithThreeDarts()
		{
			var round = new Round
			{
				Dart1 = new Field(20, FieldEnum.SingleOut),
				Dart2 = new Field(60, FieldEnum.Triple),
				Dart3 = new Field(60, FieldEnum.Triple)
			};
			Approvals.Verify(round);
		}

		[TestMethod]
		public void WhenThrowThreeTimesTripleTwenty_ThenSumShouldBeHundretEighty()
		{
			var round = new Round
			{
				Dart1 = new Field(60, FieldEnum.Triple),
				Dart2 = new Field(60, FieldEnum.Triple),
				Dart3 = new Field(60, FieldEnum.Triple)
			};

			Approvals.Verify(round.Sum);
		}

		[TestMethod]
		public void WhenThrowSingleOneDoubleTwoTripleThree_ThenSumShouldBeFourteen()
		{
			var round = new Round
			{
				Dart1 = new Field(1, FieldEnum.SingleOut),
				Dart2 = new Field(4, FieldEnum.Double),
				Dart3 = new Field(9, FieldEnum.Triple)
			};

			Approvals.Verify(round.Sum);
		}

		[TestMethod]
		public void WhenThrowSingleBullOutsideDoubleBull_ThenSumShouldBeSeventyFive()
		{
			var dartBoard = DartBoard.GetInstance();
			var round = new Round
			{
				Dart1 = dartBoard.GetSingleBull(),
				Dart2 = dartBoard.GetOutside(),
				Dart3 = dartBoard.GetDoubleBull()
			};

			Approvals.Verify(round.Sum);
		}


		[TestMethod]
		public void WhenThrowThirtyTwoWithFirstDart_ThenSumShouldBeThirtyTwo()
		{
			var dartBoard = DartBoard.GetInstance();
			var round = new Round
			{
				Dart1 = dartBoard.GetDoubleField(32),
				Dart2 = dartBoard.GetOutside(),
				Dart3 = dartBoard.GetOutside()
			};

			Approvals.Verify(round.Sum);
		}

		[TestMethod]
		public void WhenThrowThirtyTwoWithLast_ThenSumShouldBeThirtyTwo()
		{
			var dartBoard = DartBoard.GetInstance();
			var round = new Round
			{
				Dart1 = dartBoard.GetOutside(),
				Dart2 = dartBoard.GetOutside(),
				Dart3 = dartBoard.GetDoubleField(32)
			};

			Approvals.Verify(round.Sum);
		}
	}
}
