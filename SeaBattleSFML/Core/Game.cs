using System.Timers;
using agar.io.Game.Objects;
using SeaBattle.Core.Types;
using SeaBattle.Settings;
using SeaBattleSFML.Input;
using SeaBattleSFML.Objects;
using SFML.System;
using ZenisoftGameEngine.Types;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SeaBattle.Core;

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
		
		float CenterX = ZenisoftGameEngine.Engine.Window.Size.X / 2;
		float BottomY = ZenisoftGameEngine.Engine.Window.Size.Y - 100;
		LogText = new Text("", 40, SFML.Graphics.Color.White, new Vector2f(CenterX, BottomY));
		
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
			LogText.SetMessage("Game is over!");
			return;
		}
		
		Player newAttacker = PlayerQueue.Dequeue();
		PlayerQueue.Enqueue(newAttacker);
		
		Player newDefender = PlayerQueue.Peek();

		CurrentAttacker = newAttacker;
		CurrentDefender = newDefender;

		if (CurrentAttacker.Input is BotInput && CurrentDefender.Input is BotInput)
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
		CurrentAttacker.ScoreText.SetMessage(
			$"{CurrentAttacker.Name}      Score: {roundManager.scores[CurrentAttacker]} / {Configuration.roundsToWin}");
		CurrentDefender.ScoreText.SetMessage(
			$"{CurrentDefender.Name}      Score: {roundManager.scores[CurrentDefender]} / {Configuration.roundsToWin}");

		CurrentDefender.ScoreText.SetPosition(new Vector2f(ZenisoftGameEngine.Engine.Window.Size.X - 400, 50));
		CurrentAttacker.ScoreText.SetPosition(new Vector2f(400, 50));
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
	
}