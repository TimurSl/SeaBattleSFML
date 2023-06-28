namespace SeaBattleSFML.Account.Types;

[Serializable]
public struct Stats
{
	public int Wins { get; set; }
	public int MMR { get; set; }
	
	public Stats(int wins)
	{
		Wins = wins;
	}
	
	public Stats(int wins, int mmr)
	{
		Wins = wins;
	}
	
	public JsonStats ToStatsData()
	{
		JsonStats jsonStats = new JsonStats();
		jsonStats.Wins = Wins.ToString();
		jsonStats.Mmr = MMR.ToString();
		return jsonStats;
	}
}