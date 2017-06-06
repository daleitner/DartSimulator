using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator.Controller;
using DartSimulator.Player;

namespace DartSimulator.Factory
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly IPlayerService player;
		private readonly ISimulationController controller;
		public ViewModelFactory()
		{
			this.player = new PlayerService();
			this.controller = new SimulationController(player);
		}
		public MainViewModel CreateMainViewModel()
		{
			return new MainViewModel(this.controller);
		}
	}
}
