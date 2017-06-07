using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private Mock<ISimulationController> controllerMock;

		[TestInitialize]
		public void Setup()
		{
			this.controllerMock = new Mock<ISimulationController>();
		}
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
			this.controllerMock.Verify(x => x.StartSimulation(It.IsAny<int>()), Times.Once, "Start Simulation was not triggered");
		}

		[TestMethod]
		public void WhenStartSimulation_ThenAllPropertiesShouldBeFilled()
		{
			var dartBoard = DartBoard.GetInstance();
			var twenty = dartBoard.GetSingleOutField(20);
			var ttwenty = dartBoard.GetTripleField(60);
			var leg1 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = twenty, Dart2 = ttwenty, Dart3 = twenty},
					new Round {Dart1 = ttwenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = twenty, Dart2 = ttwenty, Dart3 = dartBoard.GetTripleField(57)},
					new Round {Dart1 = ttwenty, Dart2 = dartBoard.GetOutside(), Dart3 = dartBoard.GetDoubleField(24), Tries = 2}
				},
				Index = 1
			};

			var leg2 = new Leg
			{
				Runden = new List<Round>
				{
					new Round {Dart1 = twenty, Dart2 = ttwenty, Dart3 = ttwenty},
					new Round {Dart1 = twenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = twenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = twenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = twenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = twenty, Dart2 = twenty, Dart3 = twenty},
					new Round {Dart1 = dartBoard.GetSingleBull(), Dart2 = dartBoard.GetOutside(), Dart3 = dartBoard.GetDoubleField(36), Tries = 2}
				},
				Index = 2
			};
			var result = new Result
			{
				Legs = new List<Leg> { leg1, leg2 }
			};
			this.controllerMock.Setup(x => x.StartSimulation(It.IsAny<int>())).Returns(result);
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
