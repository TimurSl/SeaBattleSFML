using SeaBattleSFML.Account.Types;

namespace SeaBattleSFML.Account.Interfaces;

public interface IAccountProvider
{
	public Types.Account GetAccount(string login, string password);
	public Types.Account ModifyStats(string login, string password, JsonStats newJsonStats);
}