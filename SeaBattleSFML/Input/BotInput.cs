using SeaBattle.Core;
using SeaBattle.Settings;
using SeaBattle.Types;
using SeaBattleSFML.Objects;

namespace SeaBattleSFML.Input;

public class BotInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	private IntegerVector2 lastPoint = new IntegerVector2(0, 0);
	
	public void UpdateInput()
	{
		// pick random point
		IntegerVector2 point = new IntegerVector2(Game.Random.Next(0, Configuration.size), Game.Random.Next(0, Configuration.size));	
		
		lastPoint = point;
		
		ControlledPlayer.AttackMap.MoveCursor(point);
		
		ControlledPlayer.Attack(Game.Instance.CurrentDefender, point);
	}
}