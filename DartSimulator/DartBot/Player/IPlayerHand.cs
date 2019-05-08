namespace DartBot.Player
{
	public interface IPlayerHand
	{
		Field ThrowDart(Field target);
		void AssignHitQuotes(int singleQuote, int doubleQuote, int tripleQuote);
		void AssignPreferredTarget(bool score19);
		bool Score19 { get; }
	}
}
