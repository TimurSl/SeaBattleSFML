using SeaBattleSFML.Core;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;
using Timer = System.Timers.Timer;

namespace SeaBattleSFML.Input;

public class BotInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	private IntegerVector2 lastPoint = new IntegerVector2(0, 0);
	
	private Timer timer = new Timer(1000);

	public BotInput()
	{
		timer.Elapsed += (sender, args) =>
		{
			ControlledPlayer.Attack(Game.Instance.CurrentDefender, lastPoint);
		};
		timer.AutoReset = false;
	}
	
	public void UpdateInput()
	{
		// pick random point
		IntegerVector2 point = new IntegerVector2(Game.Random.Next(0, Configuration.size), Game.Random.Next(0, Configuration.size));	
		
		lastPoint = point;
		
		ControlledPlayer.AttackMap.MoveCursor(lastPoint);
		if (Game.Instance.IsBvB ())
			timer.Start();
		else
			ControlledPlayer.Attack(Game.Instance.CurrentDefender, lastPoint);
	}
}