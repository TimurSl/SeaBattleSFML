using System.Xml.Serialization;
using SeaBattleSFML.Account.Interfaces;
using SeaBattleSFML.Account.Types;

namespace SeaBattleSFML.Account.Providers;

public class XMLProvider : IAccountProvider
{
	string path_to_profiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SeaBattle", "Profiles");
	public Types.Account GetAccount(string login, string password)
	{
		if (!Directory.Exists(path_to_profiles))
		{
			Directory.CreateDirectory(path_to_profiles);
		}
		// combine absolute path to profile file, from disk root to current folder
		
		var path = Path.Combine(path_to_profiles, $"{login}.xml");


		if (!File.Exists(path) || new FileInfo(path).Length == 0)
		{
			Types.Account account = CreateAccountWithStats(login, password, new JsonStats() { Wins = "0", Mmr = "0" });
			return account;
		}
		else
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Types.Account));
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
			{
				Types.Account accountData = (Types.Account) serializer.Deserialize(fs);
				return new Types.Account(accountData.Login, password, accountData.Stats, this);
			}
		}
	}
	

	public Types.Account ModifyStats(string login, string password, JsonStats newJsonStats)
	{
		var path = Path.Combine(path_to_profiles, $"{login}.xml");

		if (!File.Exists(path))
		{
			return CreateAccountWithStats (login, password, newJsonStats);
		}
		
		if (new FileInfo(path).Length == 0)
		{
			return CreateAccountWithStats (login, password, newJsonStats);
		}

		XmlSerializer serializer = new XmlSerializer(typeof(Types.Account));
		using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
		{
			Types.Account accountData = (Types.Account) serializer.Deserialize(fs);
			Stats stats = new Stats ();
				
			stats.Wins = int.Parse(newJsonStats.Wins);
			stats.MMR = int.Parse(newJsonStats.Mmr);
			// accountData.Stats = JsonConvert.SerializeObject(stats);
			
			fs.SetLength(0);
			serializer.Serialize(fs, accountData);
				
			return new Types.Account(accountData.Login, password, stats, this);
		}

	}

	private Types.Account CreateAccountWithStats(string login, string password, JsonStats newJsonStats)
	{
		string path = Path.Combine(path_to_profiles, $"{login}.xml");
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(Types.Account));
		using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
		{
			Types.Account accountData = new Types.Account ();
			accountData.Login = login;
			// accountData.Password = password;
			// serialize stats to XML, not JSON

			Stats stats = new Stats(int.Parse(newJsonStats.Wins), int.Parse(newJsonStats.Mmr));
			accountData.Stats = stats;
			// accountData.Stats = JsonConvert.SerializeObject(stats);
			xmlSerializer.Serialize(fs, accountData);

			return new Types.Account(login, password, stats, this);
		}
	}
}