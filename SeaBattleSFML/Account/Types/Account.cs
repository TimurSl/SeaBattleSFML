namespace SeaBattle.Account.Providers;

public struct Account
{
	public string Login { get; set; }
	public string Password { get; set; }
	public Stats Stats { get; set; }

	private IAccountProvider AccountProvider;

	public Account(string login, string password, Stats stats, IAccountProvider provider)
	{
		Login = login;
		Password = password;
		Stats = stats;
		AccountProvider = provider;
	}

	public static Account GetAccount(IAccountProvider provider, string login, string password)
	{
		Account account = provider.GetAccount(login, password);
		return account;
	}
	
	public void UpdateStats(JsonStats newJsonStats)
	{
		Stats = new Stats(int.Parse(newJsonStats.Wins), int.Parse(newJsonStats.Mmr));
		AccountProvider.ModifyStats(Login, Password, newJsonStats);
	}
	
	public static bool operator ==(Account a, Account b)
	{
		return a.Login == b.Login && a.Password == b.Password;
	}

	public static bool operator !=(Account a, Account b)
	{
		return !(a == b);
	}
	
	public void AddWin()
	{
		Stats stats = new Stats(Stats.Wins + 1, Stats.MMR);
		Stats = stats;
		AccountProvider.ModifyStats(Login, Password, stats.ToStatsData());
	}
}