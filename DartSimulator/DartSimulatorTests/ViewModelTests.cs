using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator;
using DartSimulator.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace DartSimulatorTests
{
	[TestClass]
	public class ViewModelTests
	{
		private Mock<ISimulationController> controllerMock = new Mock<ISimulationController>();
		[TestMethod]
		public void WhenCreatingNewViewModel_LegsShouldBeTenThousand()
		{
			var viewModel = new MainViewModel(this.controllerMock.Object);
			viewModel.AmountLegs.ShouldEqual(10000);
		}

		[TestMethod]
		public void WhenClickSimulateButton_ThenControllerShouldGetTriggered()
		{
			var viewModel = new MainViewModel(this.controllerMock.Object);
			viewModel.StartCommand.Execute(null);
			this.controllerMock.Verify(x => x.StartSimulation(), Times.Once, "Start Simulation was not triggered");
		}
	}
}
