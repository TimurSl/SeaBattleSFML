using Newtonsoft.Json;

namespace SeaBattle.Account.Providers;

public class JsonAccount
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("login")]
	public string Login { get; set; }

	[JsonProperty("password")]
	public string Password { get; set; }

	[JsonProperty("stats")]
	public string Stats { get; set; }

	[JsonProperty("register_date")]
	public string RegisterDate { get; set; }
}