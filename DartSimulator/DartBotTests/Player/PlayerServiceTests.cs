using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using DartBot;
using DartBot.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartBotTests.Player
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class PlayerServiceTests
	{
		private Mock<IPlayerHand> playerHandMock;

		[TestInitialize]
		public void Setup()
		{
			this.playerHandMock = new Mock<IPlayerHand>();
		}
		[TestMethod]
		public void WhenPlayLeg_ThenLegShouldHaveFiveHundretOnePoints()
		{
			var player = new PlayerService(new PlayerHand());
			var leg = player.PlayLeg();
			Approvals.Verify("Points: " + leg.Points);
		}

		[TestMethod]
		public void WhenPlayLeg_ThenLastDartShouldBeADoubleField()
		{
			var player = new PlayerService(new PlayerHand());
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
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetNoScore();
			Approvals.Verify("Dart1: " + round.Dart1.Type + "\nDart2: " + round.Dart2.Type + "\nDart3: " + round.Dart3.Type );
		}

		[TestMethod]
		public void WhenLeftScoreIsGreaterThanHundretAndAllDartsLeft_ThenTargetShouldBeSixty()
		{
			var leftScore = new List<int> {171, 111, 100, 130, 167, 107};
			var leftDarts = 3;
			var player = new PlayerService(this.playerHandMock.Object);
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
			var player = new PlayerService(this.playerHandMock.Object);
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
			var player = new PlayerService(this.playerHandMock.Object);
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
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 61, 65, 66, 77, 80, 82, 87, 90, 92, 93, 99 };
			var leftDarts = 3;
			var player = new PlayerService(this.playerHandMock.Object);
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
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 61, 65, 66, 77, 80, 82, 87, 90, 92, 93, 99 };
			var leftDarts = 2;
			var player = new PlayerService(this.playerHandMock.Object);
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
			var leftScore = new List<int> { 3, 39, 49, 50, 55, 61, 65, 66, 77, 80, 82, 87, 90, 92, 93, 99 };
			var leftDarts = 1;
			var player = new PlayerService(this.playerHandMock.Object);
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenLeftScoreIsCheck_ValidateTargets()
		{
			var leftScore = new List<int> {2, 32, 38, 40};
			var leftDarts = 3;
			var player = new PlayerService(this.playerHandMock.Object);
			var targets = new List<string>();
			foreach (var i in leftScore)
			{
				targets.Add("left score: " + i + "; " + player.ValidateTarget(i, leftDarts));
			}
			Approvals.VerifyAll(targets, "Target");
		}

		[TestMethod]
		public void WhenGetRoundAndScoring_ThenRoundShouldHaveThreeDarts()
		{
			this.playerHandMock.Setup(x => x.ThrowDart(It.IsAny<Field>())).Returns<Field>(x => x);
			var leftScore = 501;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify(GetDartsFilledString(round) + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenGetRoundAndFirstDartIsCheckDart_ThenDart2AndDart3ShouldBeNull()
		{
			this.playerHandMock.Setup(x => x.ThrowDart(It.IsAny<Field>())).Returns<Field>(x => x);
			var leftScore = 32;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenGetRoundAndSecondDartIsCheckDart_ThenDart3ShouldBeNull()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.SetupSequence(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetOutside())
				.Returns(dartBoard.GetDoubleField(32));
			var leftScore = 32;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenBust_ThenRoundShouldBeNoScoreRound()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.Setup(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetTripleField(60));
			var leftScore = 32;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenLeftScoreIsOne_ThenRoundShouldBeNoScoreRound()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.SetupSequence(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetTripleField(60))
				.Returns(dartBoard.GetOutside())
				.Returns(dartBoard.GetOutside());
			var leftScore = 61;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenHaveFirstAndThridDartCheckdarts_ThenTriesShouldBeTwo()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.SetupSequence(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetSingleOutField(5))
				.Returns(dartBoard.GetSingleOutField(1))
				.Returns(dartBoard.GetDoubleField(4));
			var leftScore = 10;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenCheckWithLastDart_ThenTriesShouldBeThree()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.SetupSequence(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetOutside())
				.Returns(dartBoard.GetOutside())
				.Returns(dartBoard.GetDoubleField(4));
			var leftScore = 4;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		[TestMethod]
		public void WhenCheckHundretSeventy_ThenTriesShouldBeOne()
		{
			var dartBoard = DartBoard.GetInstance();
			this.playerHandMock.SetupSequence(x => x.ThrowDart(It.IsAny<Field>()))
				.Returns(dartBoard.GetTripleField(60))
				.Returns(dartBoard.GetTripleField(60))
				.Returns(dartBoard.GetDoubleBull());
			var leftScore = 170;
			var player = new PlayerService(this.playerHandMock.Object);
			var round = player.GetRound(leftScore, 1);
			Approvals.Verify("Round: " + round + "\nTries To Double: " + round.Tries);
		}

		private string GetDartsFilledString(Round round)
		{
			return "Dart1: " + (round.Dart1 == null ? "null" : "filled") +
			       "\nDart2: " + (round.Dart2 == null ? "null" : "filled") +
			       "\nDart3: " + (round.Dart3 == null ? "null" : "filled");
		}
	}
}
