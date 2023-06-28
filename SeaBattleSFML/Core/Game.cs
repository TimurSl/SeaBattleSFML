using SeaBattleSFML.Core.Types;
using SeaBattleSFML.Input;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SFML.System;
using ZenisoftGameEngine.Types;
using Timer = System.Timers.Timer;

namespace SeaBattleSFML.Core;

public class Game : BaseGame
{
	public static Game Instance { get; private set; }
	public static Random Random { get; } = new Random();
	
	public RoundManager roundManager = new();
	
	public List<Player> Players { get; set; }
	
	public Queue<Player> PlayerQueue { get; set; }
	
	public Player CurrentAttacker;
	public Player CurrentDefender;
	public Text LogText { get; set; }
	
	private Timer timer = new Timer(1000);

	
	public Game(GameLaunchParams @params)
	{
		Instance = this;
		
		Players = new List<Player>(@params.Players);

	}

	public override void Initialize()
	{
		base.Initialize ();
		
		List<Player> registeredPlayers = new List<Player>();

		foreach(Player player in Players)
		{
			Player registeredPlayer = (Player) RegisterActor(player);
			RegisterActor(registeredPlayer.AttackMap);
			RegisterActor(registeredPlayer.DefenseMap);
			RegisterDrawable(registeredPlayer.ScoreText);
			registeredPlayers.Add(registeredPlayer);
		}
		
		Players = registeredPlayers;
		
		PlayerQueue = new Queue<Player>(registeredPlayers);
		
		LogText = new Text("", 40, SFML.Graphics.Color.White, new Vector2f(ZenisoftGameEngine.Engine.Window.Size.X / 2, ZenisoftGameEngine.Engine.Window.Size.Y - 100));
		
		RegisterDrawable(LogText);

		roundManager.InitializeScores(Players);
		
		NextTurn ();
		
		timer.AutoReset = false;
		timer.Elapsed += (sender, args) =>
		{
			CurrentAttacker.CanAttack = true;
			CurrentDefender.CanAttack = false;
		};
	}

	public void NextTurn()
	{
		if (!CanGameRun ())
		{
			return;
		}

		CurrentAttacker = PlayerQueue.Dequeue();
		CurrentDefender = PlayerQueue.Peek();
		
		PlayerQueue.Enqueue(CurrentAttacker);

		if (IsBvB ())
		{
			timer.Start();
		}
		else
		{
			CurrentAttacker.CanAttack = true;
			CurrentDefender.CanAttack = false;
		}
		
		LogText.SetMessage(CurrentAttacker.Name + " is attacking " + CurrentDefender.Name);
		UpdateScoreTexts ();

	}

	private void UpdateScoreTexts()
	{
		CurrentAttacker.ScoreText.SetMessage(GetPlayerString(CurrentAttacker));
		CurrentDefender.ScoreText.SetMessage(GetPlayerString(CurrentDefender));

		CurrentDefender.ScoreText.SetPosition(new Vector2f(ZenisoftGameEngine.Engine.Window.Size.X - 350, 50));
		CurrentAttacker.ScoreText.SetPosition(new Vector2f(350, 50));
	}
	
	private string GetPlayerString(Player player)
	{
		string playerType = player.Input.GetType() == typeof(BotInput) ? "Bot" : "Player";
		return $"{player.Name} ({playerType})      Score: {roundManager.scores[player]} / {Configuration.roundsToWin}\nWins: {player.Account.Stats.Wins}";
	}

	public bool CanGameRun()
	{
		if (roundManager.IsGameOver ())
		{
			Player winner = roundManager.GetWinner ();
			winner.Account.AddWin ();
			
			LogText.SetMessage($"Player {winner.Name} has won the game!");
			
			return false;
		}
		var hasShips = GetAllPlayersThatHasShips ();
		
		if (hasShips.Length == 1)
		{
			if (roundManager.CanContinue ())
			{
				LogText.SetMessage($"Player {hasShips[0].Name} has won the round! He has {roundManager.scores[hasShips[0]]} points!");
				
				roundManager.NextRound(hasShips[0]);
				
				PlayerQueue.Clear ();
				PlayerQueue = new Queue<Player>(Players);

				ResetMaps();
				
				NextTurn ();
			}
		}
		return true;
	}
	

	private Player[] GetAllPlayersThatHasShips()
	{
		List<Player> playersWithShips = new List<Player>();
		foreach (Player player in Players)
		{
			if (player.DefenseMap.HasShips())
			{
				playersWithShips.Add(player);
			}
		}
		return playersWithShips.ToArray();
	}

	private void ResetMaps()
	{
		foreach (Player player in Players)
		{
			player.AttackMap.map.ResetMap();
			player.DefenseMap.map.ResetMap();
		}
	}

	public bool IsBvB()
	{
		return CurrentAttacker.Input is BotInput && CurrentDefender.Input is BotInput;
	}
	
}