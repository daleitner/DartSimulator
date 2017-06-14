namespace DartBot.Player
{
	public interface IPlayerHand
	{
		Field ThrowDart(Field target);
		void AssignHitQuotes(int singleQuote, int doubleQuote, int tripleQuote);
	}
}
