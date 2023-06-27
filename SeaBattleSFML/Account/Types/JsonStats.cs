using Newtonsoft.Json;

namespace SeaBattle.Account.Providers;

public class JsonStats
{
	[JsonProperty("wins")]
	public string Wins = "0";
	
	[JsonProperty("mmr")]
	public string Mmr = "0";
}