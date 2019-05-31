using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using DartBot;
using DartBot.Player;
using DartSimulator.CanvasDialog;
using static System.Int32;

namespace DartSimulator.Controller
{
	public class SimulationController : ISimulationController
	{
		private readonly IPlayerService player;
		private List<Point> points;
		public SimulationController(IPlayerService playerService)
		{
			this.player = playerService;
		}
		public Result StartSimulation(int legs, int singleQuote, int doubleQuote, int tripleQuote, bool score19)
		{
			this.player.AssignQuotes(singleQuote, doubleQuote, tripleQuote, singleQuote, doubleQuote);
			player.AssignPreferredTarget(score19);
			var result = new Result();
			points = new List<Point>();
			for (int i = 0; i < legs; i++)
			{
				var leg = this.player.PlayLeg();
				points.AddRange(player.HitPoints);
				leg.Index = i + 1;
				result.Legs.Add(leg);
			}
			return result;
		}

		public ObservableCollection<RoundCount> InitializeRoundCounts()
		{
			var roundCounts = new ObservableCollection<RoundCount>();
			for(var i = 3; i<16; i++)
				roundCounts.Add(new RoundCount(i.ToString()));
			roundCounts.Add(new RoundCount(">15"));
			return roundCounts;
		}

		public ObservableCollection<RoundCount> FillRoundCounts(ObservableCollection<RoundCount> roundCounts, Result result)
		{
			var list = new List<RoundCount>(roundCounts);
			foreach (var roundCount in list)
			{
				if(roundCount.Round != ">15")
					roundCount.Count = result.Legs.Count(x => x.AmountDarts <= Parse(roundCount.Round) * 3 && x.AmountDarts >= Parse(roundCount.Round) * 3 - 2);
			}
			list.Last().Count = result.Legs.Count(x => x.AmountDarts > 45);
			var maxCount = list.Select(x => x.Count).Max();
			double maxHeight = 160;
			foreach (var roundCount in list)
			{
				roundCount.Height = (int) (maxHeight / maxCount * roundCount.Count);
			}
			return new ObservableCollection<RoundCount>(list);
		}

		public void ShowCanvas()
		{
			var viewModel = new TargetViewModel();
			viewModel.SelectedDart.Y = DartBoard.GetInstance().GetTripleField(60).Target.Y;
			viewModel.SelectedDart.X = DartBoard.GetInstance().GetTripleField(60).Target.X;
			foreach (var point in points)
			{
				viewModel.NextCommand.Execute(null);
				viewModel.SelectedDart.Y = point.Y < 0 ? 0 : point.Y > 339 ? 339 : point.Y;
				viewModel.SelectedDart.X = point.X < 0 ? 0 : point.X > 339 ? 339 : point.X;
			}
			viewModel.NextCommand.Execute(null);
			var window = new TargetWindow { DataContext = viewModel };
			window.Show();
		}
	}
}
