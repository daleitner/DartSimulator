using System.Collections.Generic;
using System.Drawing;
using Dart.Base;

namespace DartBot.Player
{
	public interface IPlayerService
	{
		Leg PlayLeg(bool withOpponent);
		Round GetRound(int leftScore, int index);
		Round GetNoScore();
		Field ValidateTarget(int leftScore, int leftDarts);
		void AssignQuotes(double my, double sigma);
		void AssignPreferredTarget(bool score19);
		List<Point> HitPoints { get; }
	}
}
