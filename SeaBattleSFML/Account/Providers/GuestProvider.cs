using SeaBattleSFML.Account.Interfaces;
using SeaBattleSFML.Account.Types;

namespace SeaBattleSFML.Account.Providers;

public class GuestProvider : IAccountProvider
{
	public Types.Account GetAccount(string login, string password)
	{
		return new Types.Account(login, password, new Stats(0), this);
	}

	public Types.Account ModifyStats(string login, string password, JsonStats newJsonStats)
	{
		return new Types.Account(login, password, new Stats(0), this);
	}
}