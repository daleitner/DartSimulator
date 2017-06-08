using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Player
{
	public interface IPlayerHand
	{
		Field ThrowDart(Field target);
		void AssignHitQuotes(int singleQuote, int doubleQuote, int tripleQuote);
	}
}
