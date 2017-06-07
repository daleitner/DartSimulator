using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using DartSimulator.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartSimulatorTests.Player
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class PlayerServiceTests
	{
		[TestMethod]
		public void WhenPlayLeg_ThenLegShouldHaveFiveHundretOnePoints()
		{
			var player = new PlayerService();
			var leg = player.PlayLeg();
			Approvals.Verify("Points: " + leg.Points);
		}

		[TestMethod]
		public void WhenPlayLeg_ThenLastDartShouldBeADoubleField()
		{
			var player = new PlayerService();
			var leg = player.PlayLeg();
			var last = leg.Runden.Last().Dart1;
			if(leg.AmountDarts % 3 == 2)
				last = leg.Runden.Last().Dart2;
			else if(leg.AmountDarts % 3 == 0)
				last = leg.Runden.Last().Dart3;
			Approvals.Verify("Letzter Dart: " + last.Type);
		}

		[TestMethod]
		public void WhenGetNoScore_ThenThreeDartsShouldBeOutside()
		{
			var player = new PlayerService();
			var round = player.GetNoScore(1);
			Approvals.Verify("Dart1: " + round.Dart1.Type + "\nDart2: " + round.Dart2.Type + "\nDart3: " + round.Dart3.Type + "\nTries: " + round.Tries );
		}

		[TestMethod]
		public void WhenLeftScoreIsGreaterThanHundretAndAllDartsLeft_ThenTargetShouldBeSixty()
		{
			var leftScore = new List<int> {171, 111, 100, 130, 167, 107};
			var leftDarts = 3;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsGreaterThanHundretTwoDartsLeft_ValidateTarget()
		{
			var leftScore = new List<int> { 107, 104, 101, 103, 140 };
			var leftDarts = 2;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsGreaterThanHundretAndOneDartLeft_ValidateTarget()
		{
			var leftScore = new List<int> { 107, 104, 101, 100, 499 };
			var leftDarts = 1;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsNoCheckAndThreeDartsLeft_ValidateTargets()
		{
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 65, 77, 80, 87, 92, 93, 99 };
			var leftDarts = 3;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsNoCheckAndTwoDartsLeft_ValidateTargets()
		{
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 65, 77, 80, 87, 92, 93, 99 };
			var leftDarts = 2;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsNoCheckAndOneDartLeft_ValidateTargets()
		{
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 65, 77, 80, 87, 92, 93, 99 };
			var leftDarts = 1;
			var player = new PlayerService();
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}
	}
}
