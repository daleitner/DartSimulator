using System.Collections.ObjectModel;

namespace DartSimulator.Controller
{
	public interface ISimulationController
	{
		Result StartSimulation(int legs, int my, int sigma, bool score19);
		ObservableCollection<RoundCount> InitializeRoundCounts();
		ObservableCollection<RoundCount> FillRoundCounts(ObservableCollection<RoundCount> roundCounts, Result result);
		void ShowCanvas();
	}
}
