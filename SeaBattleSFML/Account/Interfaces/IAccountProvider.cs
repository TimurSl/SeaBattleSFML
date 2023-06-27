namespace SeaBattle.Account.Providers;

public interface IAccountProvider
{
	public Account GetAccount(string login, string password);
	public Account ModifyStats(string login, string password, JsonStats newJsonStats);
}