using DartBot.Player;
using DartSimulator.Controller;

namespace DartSimulator.Factory
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly ISimulationController _controller;
		private readonly IPlayerHand playerHand;

		public ViewModelFactory()
		{
			playerHand = new PlayerHand();
			IPlayerService player = new PlayerService(playerHand);
			_controller = new SimulationController(player);
		}
		public MainViewModel CreateMainViewModel()
		{
			return new MainViewModel(_controller, playerHand);
		}
	}
}
