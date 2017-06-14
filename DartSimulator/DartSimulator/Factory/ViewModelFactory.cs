using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartBot.Player;
using DartSimulator.Controller;

namespace DartSimulator.Factory
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly ISimulationController controller;

		public ViewModelFactory()
		{
			IPlayerHand playerHand = new PlayerHand();
			IPlayerService player = new PlayerService(playerHand);
			this.controller = new SimulationController(player);
		}
		public MainViewModel CreateMainViewModel()
		{
			return new MainViewModel(this.controller);
		}
	}
}
