namespace DartSimulator.Controller
{
	public interface ISimulationController
	{
		Result StartSimulation(int legs, int singleQuote, int doubleQuote, int tripleQuote);
	}
}
