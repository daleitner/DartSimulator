using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator.Player;

namespace DartSimulator.Controller
{
	public class SimulationController : ISimulationController
	{
		private readonly IPlayerService player;

		public SimulationController(IPlayerService playerService)
		{
			this.player = playerService;
		}
		public Result StartSimulation(int legs)
		{
			var result = new Result();
			for (int i = 0; i < legs; i++)
			{
				result.Legs.Add(this.player.PlayLeg());
			}
			return result;
		}
	}
}
