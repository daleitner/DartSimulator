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
	public class LegTests
	{
		[TestMethod]
		public void WhenLastRoundHasThreeDarts_ThenAmountShouldBeTwelve()
		{
			var dartBoard = DartBoard.GetInstance();
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty,Dart2 = ttwenty,Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = dtwenty},
					new Round {Dart1 = outside, Dart2 = outside, Dart3 = dtwenty}
				}
			};

			Approvals.Verify("Anzahl Darts: " + leg.AmountDarts);
		}

		[TestMethod]
		public void WhenLastRoundHasTwoDarts_ThenAmountShouldBeEleven()
		{
			var dartBoard = DartBoard.GetInstance();
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty,Dart2 = ttwenty,Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = dtwenty},
					new Round {Dart1 = outside, Dart2 = dtwenty}
				}
			};

			Approvals.Verify("Anzahl Darts: " + leg.AmountDarts);
		}

		[TestMethod]
		public void WhenLastRoundHasOneDart_ThenAmountShouldBeTen()
		{
			var dartBoard = DartBoard.GetInstance();
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty,Dart2 = ttwenty,Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = dtwenty},
					new Round {Dart1 = dtwenty}
				}
			};

			Approvals.Verify("Anzahl Darts: " + leg.AmountDarts);
		}
	}
}
