using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Controller
{
	public interface ISimulationController
	{
		Result StartSimulation(int legs, int singleQuote, int doubleQuote, int tripleQuote);
	}
}
