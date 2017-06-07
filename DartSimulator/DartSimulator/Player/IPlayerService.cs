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
		Round GetRound();
		Round GetNoScore(int triesToDouble);
		Field ValidateTarget(int leftScore, int leftDarts);
	}
}
