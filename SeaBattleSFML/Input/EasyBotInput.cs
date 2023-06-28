using SeaBattleSFML.Core;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;
using Timer = System.Timers.Timer;

namespace SeaBattleSFML.Input;

public class EasyBotInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	private IntegerVector2 lastPoint = new IntegerVector2(0, 0);


	public void UpdateInput()
	{
		IntegerVector2 point = new IntegerVector2(Game.Random.Next(0, Configuration.size), Game.Random.Next(0, Configuration.size));

		lastPoint = point;
		
		ControlledPlayer.AttackMap.SetCursor(lastPoint);
		
		ControlledPlayer.Attack(Game.Instance.CurrentDefender, lastPoint);
	}
}