namespace SeaBattle.Account.Providers;

public class GuestProvider : IAccountProvider
{
	public Account GetAccount(string login, string password)
	{
		return new Account(login, password, new Stats(0), this);
	}

	public Account ModifyStats(string login, string password, JsonStats newJsonStats)
	{
		return new Account(login, password, new Stats(0), this);
	}
}