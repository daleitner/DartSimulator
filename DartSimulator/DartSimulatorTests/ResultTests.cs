using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using DartBot;
using DartSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartSimulatorTests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class ResultTests
	{
		[TestMethod]
		public void VerifyDefaultResult()
		{
			var result = new Result();
			Approvals.Verify(result);
		}
		[TestMethod]
		public void WhenThrowFiveHundretEighties_ThenHundretEightiesShouldBeFive()
		{
			var dartBoard = DartBoard.GetInstance();
			var ttwenty = dartBoard.GetTripleField(60);
			var tnineteen = dartBoard.GetTripleField(57);
			var dtwelve = dartBoard.GetDoubleField(24);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = tnineteen, Dart3 = dtwelve},
				}
			};
			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = tnineteen},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = dtwelve},
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg, leg, leg2}
			};

			Approvals.Verify("Anzahl 180er = " + result.HundretEighties);
		}

		[TestMethod]
		public void WhenThrowNoHundretEighties_ThenHundretEightiesShouldBeZero()
		{
			var dartBoard = DartBoard.GetInstance();
			var ttwenty = dartBoard.GetTripleField(60);
			var tnineteen = dartBoard.GetTripleField(57);
			var dfifteen = dartBoard.GetDoubleField(30);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = tnineteen, Dart3 = tnineteen},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = tnineteen},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = dfifteen},
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg, leg, leg }
			};

			Approvals.Verify("Anzahl 180er = " + result.HundretEighties);
		}

		[TestMethod]
		public void WhenThrowThreeHundretFourties_ThenHundretFourtiesShouldBeThree()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dten = dartBoard.GetDoubleField(20);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 =  one, Dart2 = ttwenty, Dart3 = dten}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg }
			};

			Approvals.Verify("Anzahl 140er = " + result.HundretFourties);
		}

		[TestMethod]
		public void WhenThrowTwoHundretFourtiesAndOneHundretEighty_ThenHundretFourtiesShouldBeTwo()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dten = dartBoard.GetDoubleField(20);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 =  one, Dart2 = twenty, Dart3 = dten}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg }
			};

			Approvals.Verify("Anzahl 140er = " + result.HundretFourties);
		}

		[TestMethod]
		public void WhenThrowTwoHundretFourtiesAndOneHundretSeventySeven_ThenHundretFourtiesShouldBeThree()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dten = dartBoard.GetDoubleField(20);
			var tnineteen = dartBoard.GetTripleField(57);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = tnineteen},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 =  one, Dart2 = one, Dart3 = one},
					new Round {Dart1 =  one, Dart2 = twenty, Dart3 = dten}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg }
			};

			Approvals.Verify("Anzahl 140er = " + result.HundretFourties);
		}

		[TestMethod]
		public void WhenThrowFiveHundrets_ThenHundretsShouldBeFive()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg }
			};

			Approvals.Verify("Anzahl 100er = " + result.Hundrets);
		}

		[TestMethod]
		public void WhenThrowHundretHundretFourtyHundretEighty_ThenHundretsShouldBeOne()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg }
			};

			Approvals.Verify("Anzahl 100er = " + result.Hundrets);
		}

		[TestMethod]
		public void WhenPlayTwoLegs_ThenVerifyWorstLeg()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg1 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = outside, Dart2 = outside, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg1, leg2 }
			};

			Approvals.Verify("Worst Leg= " + result.WorstLeg);
		}

		[TestMethod]
		public void WhenPlayTwoLegs_ThenVerifyBestLeg()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg1 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = outside, Dart2 = outside, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg1, leg2 }
			};

			Approvals.Verify("Best Leg= " + result.BestLeg);
		}

		[TestMethod]
		public void WhenPlayTwoLegs_ThenVerifyDartAverage()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg1 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = dtwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = outside, Dart2 = outside, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg1, leg2 }
			};

			Approvals.Verify("Dart Average= " + result.DartAverage);
		}

		[TestMethod]
		public void WhenThrowFiveHundrets_ThenVerifyAverage()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var one = dartBoard.GetSingleOutField(1);
			var dtwenty = dartBoard.GetDoubleField(40);
			var outside = dartBoard.GetOutside();
			var leg = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = dtwenty}
				}
			};

			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = one, Dart3 = outside},
					new Round {Dart1 = dtwenty}
				}
			};

			var result = new Result
			{
				Legs = new List<Leg> { leg, leg2 }
			};

			Approvals.Verify("3 Dart Average = " + result.Average);
		}

		[TestMethod]
		public void WhenFiveLegs_ThenHitsShouldBeFive()
		{
			var result = new Result
			{
				Legs = new List<Leg>
				{
					new Leg(),
					new Leg(),
					new Leg(),
					new Leg(),
					new Leg()
				}
			};

			Approvals.Verify("Hits = " + result.Hits);
		}

		[TestMethod]
		public void WhenTwoLegsWith5Tries_ThenTriesShouldBeTen()
		{
			var result = new Result
			{
				Legs = new List<Leg>
				{
					new Leg {Runden = new List<Round> {new Round {Tries=3}, new Round {Tries=2} } },
					new Leg {Runden = new List<Round> {new Round {Tries=3}, new Round {Tries=2} } }
				}
			};

			Approvals.Verify("Tries = " + result.Tries);
		}

		[TestMethod]
		public void WhenHitsTwoAndTriesFour_ThenDoubleQuoteShouldBeFifty()
		{
			var result = new Result
			{
				Legs = new List<Leg>
				{
					new Leg {Runden = new List<Round> {new Round {Tries=2} } },
					new Leg {Runden = new List<Round> {new Round {Tries=2} } }
				}
			};

			Approvals.Verify("DoubleQuote = " + result.DoubleQuote + "%");
		}
	}
}
