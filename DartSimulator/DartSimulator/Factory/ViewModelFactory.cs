using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator.Controller;

namespace DartSimulator.Factory
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly ISimulationController controller;
		public ViewModelFactory()
		{
			this.controller = new SimulationController();
		}
		public MainViewModel CreateMainViewModel()
		{
			return new MainViewModel(this.controller);
		}
	}
}
