using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Player
{
	public class PlayerService : IPlayerService
	{
		public Leg PlayLeg()
		{
			var leg = new Leg();
			while (leg.Points != 501)
			{
				var newRound = GetRound();
				leg.Runden.Add(newRound);
			}
			return leg;
		}

		private Round GetRound()
		{
			var dartBoard = DartBoard.GetInstance();
			return new Round {Dart1 = dartBoard.GetTripleField(60), Dart2 = dartBoard.GetTripleField(57), Dart3 = dartBoard.GetDoubleBull()};
		}
	}
}
