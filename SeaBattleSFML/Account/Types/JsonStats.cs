using Newtonsoft.Json;

namespace SeaBattleSFML.Account.Types;

public class JsonStats
{
	[JsonProperty("wins")]
	public string Wins = "0";
	
	[JsonProperty("mmr")]
	public string Mmr = "0";
}