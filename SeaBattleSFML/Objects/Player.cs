using SeaBattle.Account.Providers;
using SeaBattle.Cells;
using SeaBattle.Core;
using SeaBattle.MapCreators.Types;
using SeaBattle.Settings;
using SeaBattle.Types;
using SeaBattleSFML.Input;
using SFML.Graphics;
using SFML.System;
using ZenisoftGameEngine.Interfaces;
using ZenisoftGameEngine.Types;
using Text = agar.io.Game.Objects.Text;
using Timer = System.Timers.Timer;

namespace SeaBattleSFML.Objects;

public class Player : BaseObject, IUpdatable
{
	public int ZIndex { get; set; }
	
	public GridMap AttackMap { get; set; }
	public GridMap DefenseMap { get; set; }
	
	public IInput Input { get; set; }
	
	public Account Account { get; set; }
	public string Name { get; set; } = "Player";
	
	public Text ScoreText { get; set; }
	
	public bool CanAttack { get; set; } = false;
	
	public bool IsStreak = false;
	
	private Timer waitTimer = new Timer(1000);
	
	public Player(string name = "Player", IInput input = null)
	{
		AttackMap = new GridMap(LevelCreationType.Empty, true, false);
		DefenseMap = new GridMap(LevelCreationType.Random, false, true);
		DefenseMap.offset = new Vector2f(700, 100);
		AttackMap.offset = new Vector2f(100, 100);

		ScoreText = new Text("", 30, Color.White, new Vector2f(200, 50));
		
		Name = name;
		if (input == null)
		{
			Input = new BotInput();
		}
		else
		{
			Input = input;
		}
		
		Input.ControlledPlayer = this;

		Account = new XMLProvider ().GetAccount(Name, "123");
		
		waitTimer.AutoReset = false;
		
		waitTimer.Elapsed += (sender, args) =>
		{
			Game.Instance.NextTurn();
		};
	}

	public void Update()
	{
		AttackMap.IsInitialized = Game.Instance.CurrentAttacker == this && Input is not BotInput;
		DefenseMap.IsInitialized = Game.Instance.CurrentAttacker == this && Input is not BotInput;
		if (Game.Instance.CurrentAttacker == this && CanAttack)
		{
			Input.UpdateInput();
		}
	}
	
	public void Attack(Player target, IntegerVector2 coordinates)
	{
		if (!Game.Instance.CanGameRun())
			return;
		
		if (!coordinates.InBounds ())
		{
			Console.WriteLine("Invalid coordinates");
			return;
		}
		Cell targetCell = target.DefenseMap.map.Grid[coordinates.X, coordinates.Y];

		if (targetCell.IsAlreadyHit ())
			return;

		AttackMap.map.Grid[coordinates.X, coordinates.Y].ProcessAttackHit (target.DefenseMap.map, coordinates);
		targetCell.ProcessDefenseHit ();

		CheckShipsAndOutline (targetCell);
		
		target.DefenseMap.map.lastHit = coordinates;

		CanAttack = IsStreak && Input is not BotInput;
		
		if (!CanAttack)
		{
			if (Input is not BotInput)
				waitTimer.Start ();
			else
				Game.Instance.NextTurn ();
		}
	}

	private void CheckShipsAndOutline(Cell targetCell)
	{
		if (targetCell is Ship ship)
		{
			if (!ship.IsAlive ())
			{
				AttackMap.OutlineShip(ship);
			}

			IsStreak = true;
		}
		else
		{
			IsStreak = false;
		}
	}
}