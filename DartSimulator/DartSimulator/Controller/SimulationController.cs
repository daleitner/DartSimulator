using DartBot.Player;

namespace DartSimulator.Controller
{
	public class SimulationController : ISimulationController
	{
		private readonly IPlayerService player;

		public SimulationController(IPlayerService playerService)
		{
			this.player = playerService;
		}
		public Result StartSimulation(int legs, int singleQuote, int doubleQuote, int tripleQuote)
		{
			this.player.AssignQuotes(singleQuote, doubleQuote, tripleQuote);
			var result = new Result();
			for (int i = 0; i < legs; i++)
			{
				var leg = this.player.PlayLeg();
				leg.Index = i + 1;
				result.Legs.Add(leg);
			}
			return result;
		}
	}
}
