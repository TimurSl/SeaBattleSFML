using SeaBattle.Settings;
using SeaBattleSFML.Objects;

namespace SeaBattle.Core;

public class RoundManager
{
	private int currentRound = 1;
	public Dictionary<Player, int> scores = new();
	
	public void InitializeScores(List<Player> players)
	{
		foreach (Player player in players)
		{
			scores.Add(player, 0);
		}
	}

	public void NextRound(Player winner)
	{
		scores[winner]++;
		currentRound++;
	}
	
	public bool IsGameOver()
	{
		foreach (KeyValuePair<Player, int> score in scores)
		{
			if (score.Value == Configuration.roundsToWin)
			{
				return true;
			}
		}
		return false;
	}

	public Player GetWinner()
	{
		foreach (KeyValuePair<Player, int> score in scores)
		{
			if (score.Value == Configuration.roundsToWin)
			{
				return score.Key;
			}
		}
		return null;
	}
	
	public bool CanContinue()
	{
		foreach (KeyValuePair<Player, int> score in scores)
		{
			if (score.Value >= Configuration.roundsToWin || currentRound > Configuration.rounds)
			{
				return false;
			}
		}
		return currentRound < Configuration.rounds;
	}
	
	public void PrintRound()
	{
		Console.WriteLine($"Round {currentRound} of {Configuration.rounds}");
	}
}