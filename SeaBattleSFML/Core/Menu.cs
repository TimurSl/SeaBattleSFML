using SeaBattleSFML.Account.Providers;
using SeaBattleSFML.Core.Types;
using SeaBattleSFML.Input;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings.Types;

namespace SeaBattleSFML.Core;

public class Menu
{
	private List<Player> players = new List<Player>();

	public void OpenMenu()
	{
		WelcomeMessage ();
		PlayerSetup ();
		Game game = new Game(new GameLaunchParams () { Players = players });
		game.Run ();
	}
	
	public List<Player> GetPlayers()
	{
		return players;
	}

	private void PlayerSetup()
	{
		int playersCount = 2;

		for (int i = 0; i < playersCount; i++)
		{
			AskForPlayer (i);
		}
	}

	private void AskForPlayer(int number)
	{
		Player player;
		Console.WriteLine($"Select player type (Player {number}):");
		foreach (PlayerType type in Enum.GetValues(typeof(PlayerType)))
		{
			Console.WriteLine($"{(int) type} - {type}");
		}
		Console.Write("Player type (default: 1): ");
		string playerT = Console.ReadLine () ?? "1";
		if (playerT == "")
			playerT = "1";
		
		PlayerType playerType = (PlayerType) int.Parse(playerT);
		
		Console.Write("Player name: ");
		string playerName = Console.ReadLine ();
		if (string.IsNullOrEmpty(playerName))
			playerName = "Player " + (number + 1);
		
		// check if player with this name already exists
		if (players.Exists (p => p.Name == playerName))
		{
			Console.WriteLine("Player with this name already exists!");
			AskForPlayer (number);
			return;
		}

		Account.Types.Account account = Account.Types.Account.GetAccount(new XMLProvider (), playerName, "2314");
		Console.WriteLine($"Welcome, {account.Login}!");
		Console.WriteLine($"Your stats: {account.Stats.Wins} wins, {account.Stats.MMR} MMR");

		
		
		if (playerType == PlayerType.Bot)
		{
			Console.WriteLine("Select bot difficulty:");
			foreach (BotDifficulties diff in Enum.GetValues(typeof(BotDifficulties)))
			{
				Console.WriteLine($"{(int) diff} - {diff}");
			}
			Console.Write("Bot difficulty (default: 1): ");
			string difficulty = Console.ReadLine () ?? "1";
			if (difficulty == "")
				difficulty = "1";
			
			BotDifficulties botDifficulty = (BotDifficulties) int.Parse(difficulty);
			IInput input = null;
			switch (botDifficulty)
			{
				case BotDifficulties.Easy:
					input = new EasyBotInput ();
					break;
				case BotDifficulties.Medium:
					input = new MediumBotInput ();
					break;
				case BotDifficulties.Hard:
					input = new HardBotInput ();
					break;
				default:
					input = new EasyBotInput ();
					break;
			}
			player = new Player(playerName, input);
		}
		else
		{
			player = new Player(playerName, new PlayerInput ());
			
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
		players.Add(player);
		Console.Clear();
	}

	private static void WelcomeMessage()
	{
		Console.WriteLine(Figgle.FiggleFonts.Doom.Render("Sea Battle"));

		Console.WriteLine("Welcome to Sea Battle!");
		Console.WriteLine();
	}

}