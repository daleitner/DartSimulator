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
	}
}
