using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using DartSimulator;
using DartSimulator.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace DartSimulatorTests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class ViewModelTests
	{
		private readonly Mock<ISimulationController> controllerMock = new Mock<ISimulationController>();
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

		[TestMethod]
		public void WhenStartSimulation_ThenAllPropertiesShouldBeFilled()
		{
			var result = new Result
			{
				Average = 50.5,
				BestLeg = 18,
				DartAverage = 27,
				DoubleQuote = 25.2,
				Hundrets = 7,
				HundretFourties = 3,
				HundretEighties = 1,
				WorstLeg = 36
			};
			this.controllerMock.Setup(x => x.StartSimulation()).Returns(result);
			var viewModel = new MainViewModel(this.controllerMock.Object);
			viewModel.StartCommand.Execute(null);
			Approvals.Verify(GetResultProperties(viewModel));
		}

		private string GetResultProperties(MainViewModel viewModel)
		{
			string ret = "";
			ret += "Anzahl Legs: " + viewModel.Legs.Count;
			ret += "\nSelectedLeg: " + viewModel.SelectedLeg;
			ret += "\n3-Dart-Average: " + viewModel.Average;
			ret += "\nDoppelquote: " + viewModel.SimulatedDoubleQuote;
			ret += "\nBest Leg: " + viewModel.BestLeg;
			ret += "\nWorst Leg: " + viewModel.WorstLeg;
			ret += "\nDart-Average: " + viewModel.DartAverage;
			ret += "\n100er: " + viewModel.Hundrets;
			ret += "\n140er: " + viewModel.HundretFourties;
			ret += "\n180er: " + viewModel.HundretEighties;
			return ret;
		}
	}
}
