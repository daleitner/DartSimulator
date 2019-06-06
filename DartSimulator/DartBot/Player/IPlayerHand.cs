using Dart.Base;
using System.Drawing;

namespace DartBot.Player
{
	public interface IPlayerHand
	{
		Point HitPoint { get; }
		Field ThrowDart(Field target);
		void AssignPreferredTarget(bool score19);
		void AssignHitQuotes(double my, double sigma);
		bool Score19 { get; }
	}
}
