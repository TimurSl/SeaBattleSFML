﻿using SeaBattle.Account.Providers;
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
	}

	public void Update()
	{
		AttackMap.IsInitialized = Game.Instance.CurrentAttacker == this;
		DefenseMap.IsInitialized = Game.Instance.CurrentAttacker == this;
		if (Game.Instance.CurrentAttacker == this && CanAttack)
		{
			Input.UpdateInput();
		}
	}
	
	public void Attack(Player target, IntegerVector2 coordinates)
	{
		if (!Game.Instance.CanGameRun())
			return;
		
		if (coordinates.X < 0 || coordinates.X >= Configuration.size || coordinates.Y < 0 || coordinates.Y >= Configuration.size)
		{
			Console.WriteLine("Invalid coordinates");
			return;
		}
		Cell targetCell = target.DefenseMap.map.Grid[coordinates.X, coordinates.Y];
		
		if (targetCell.IsAlreadyHit ())
			return;
		
		AttackMap.map.Grid[coordinates.X, coordinates.Y].ProcessAttackHit (target.DefenseMap.map, coordinates);
		
		targetCell.ProcessDefenseHit ();

		// Console.WriteLine($"Attacking {target.Name} at {coordinates.X}, {coordinates.Y}, new status: {targetCell.CellType}");

		if (targetCell is Ship ship)
		{
			if (!ship.IsAlive ())
			{
				AttackMap.OutlineShip(ship);
			}
			// IsStreak = true;
		}
		// else
		// {
		// 	IsStreak = false;
		// }
		
		target.DefenseMap.map.lastHit = coordinates;
		
		Game.Instance.NextTurn();
	}
}