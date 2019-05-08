namespace DartBot.Player
{
	public interface IPlayerService
	{
		Leg PlayLeg();
		Round GetRound(int leftScore, int index);
		Round GetNoScore();
		Field ValidateTarget(int leftScore, int leftDarts);
		void AssignQuotes(int singleQuote, int doubleQuote, int tripleQuote);
		void AssignPreferredTarget(bool score19);
	}
}
