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
			this.playerMock = new Mock<IPlayerService>();
		}

		[TestMethod]
		public void WhenStartSimulation_ThenResultShouldHaveTenLegs()
		{
			var legs = 10;
			this.playerMock.Setup(x => x.PlayLeg()).Returns(new Leg());
			var controller = new SimulationController(this.playerMock.Object);
			var result = controller.StartSimulation(legs, 100, 100, 100);
			Approvals.Verify("Amount of Legs: " + result.Legs.Count);
		}
	}
}
