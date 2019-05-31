﻿using System.Collections.Generic;
using System.Drawing;

namespace DartBot.Player
{
	public interface IPlayerService
	{
		Leg PlayLeg();
		Round GetRound(int leftScore, int index);
		Round GetNoScore();
		Field ValidateTarget(int leftScore, int leftDarts);
		void AssignQuotes(int singleQuote, int doubleQuote, int tripleQuote, double my, double sigma);
		void AssignPreferredTarget(bool score19);
		List<Point> HitPoints { get; }
	}
}
