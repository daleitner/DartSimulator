using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Player
{
	public interface IPlayerService
	{
		Leg PlayLeg();
		Round GetRound(int leftScore, int index);
		Round GetNoScore();
		Field ValidateTarget(int leftScore, int leftDarts);
		void AssignQuotes(int singleQuote, int doubleQuote, int tripleQuote);
	}
}
