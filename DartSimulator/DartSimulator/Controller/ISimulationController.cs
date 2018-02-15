using System.Collections.ObjectModel;

namespace DartSimulator.Controller
{
	public interface ISimulationController
	{
		Result StartSimulation(int legs, int singleQuote, int doubleQuote, int tripleQuote);
		ObservableCollection<RoundCount> InitializeRoundCounts();
		ObservableCollection<RoundCount> FillRoundCounts(ObservableCollection<RoundCount> roundCounts, Result result);
	}
}
