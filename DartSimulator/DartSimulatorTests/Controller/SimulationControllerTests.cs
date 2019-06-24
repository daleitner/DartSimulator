using ApprovalTests;
using ApprovalTests.Reporters;
using DartBot;
using DartBot.Player;
using DartSimulator.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartSimulatorTests.Controller
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class SimulationControllerTests
	{
		private Mock<IPlayerService> playerMock;

		[TestInitialize]
		public void Setup()
		{
			playerMock = new Mock<IPlayerService>();
		}

		[TestMethod]
		public void WhenStartSimulation_ThenResultShouldHaveTenLegs()
		{
			var legs = 10;
			playerMock.Setup(x => x.PlayLeg(false)).Returns(new Leg());
			var controller = new SimulationController(playerMock.Object);
			var result = controller.StartSimulation(legs, 100, 100, false, false);
			Approvals.Verify("Amount of Legs: " + result.Legs.Count);
		}

		[TestMethod]
		public void WhenInitializeRoundCounts_ThenVerifyRoundCounts()
		{
			var controller = new SimulationController(playerMock.Object);
			Approvals.VerifyAll(controller.InitializeRoundCounts(), "RoundCounts");
		}
	}
}
