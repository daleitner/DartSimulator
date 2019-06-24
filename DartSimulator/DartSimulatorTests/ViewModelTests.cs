using System.Collections.Generic;
using System.Collections.ObjectModel;
using ApprovalTests;
using ApprovalTests.Reporters;
using Dart.Base;
using DartBot;
using DartBot.Player;
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
		private Mock<IPlayerHand> _playerHand;

		[TestInitialize]
		public void Setup()
		{
			controllerMock = new Mock<ISimulationController>();
			_playerHand = new Mock<IPlayerHand>();
		}
		[TestMethod]
		public void WhenCreatingNewViewModel_LegsShouldBeTenThousand()
		{
			var viewModel = new MainViewModel(controllerMock.Object, _playerHand.Object);
			viewModel.AmountLegs.ShouldEqual(10000);
		}

		[TestMethod]
		public void WhenCreatingNewViewModel_VerifyRoundCounts()
		{
			var viewModel = new MainViewModel(controllerMock.Object, _playerHand.Object);
			controllerMock.Verify(x => x.InitializeRoundCounts(), Times.Once, "RoundCounts wasn't initialized");
		}

		[TestMethod]
		public void WhenClickSimulateButton_ThenControllerShouldGetTriggered()
		{
			var viewModel = new MainViewModel(controllerMock.Object, _playerHand.Object);
			viewModel.StartCommand.Execute(null);
			controllerMock.Verify(x => x.StartSimulation(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once, "Start Simulation was not triggered");
			controllerMock.Verify(x => x.FillRoundCounts(It.IsAny<ObservableCollection<RoundCount>>(), It.IsAny<Result>()), Times.Once, "FillRoundCounts was not triggered");
		}

		[TestMethod]
		public void WhenStartSimulation_ThenAllPropertiesShouldBeFilled()
		{
			var dartBoard = DartBoard.Instance;
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
			controllerMock.Setup(x => x.StartSimulation(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(result);
			controllerMock
				.Setup(x => x.FillRoundCounts(It.IsAny<ObservableCollection<RoundCount>>(), It.IsAny<Result>()))
				.Returns(new ObservableCollection<RoundCount>(){new RoundCount("15")});
			var viewModel = new MainViewModel(controllerMock.Object, _playerHand.Object);
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
