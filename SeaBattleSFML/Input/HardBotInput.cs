using SeaBattleSFML.Core;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;

namespace SeaBattleSFML.Input;

public class HardBotInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	private IntegerVector2 lastPoint = new IntegerVector2(0, 0);
	
	public IntegerVector2 HardGetTarget(Player attacker, Player enemy)
	{
		Random random = new Random();
		// get random point in enemy defense, get random ship with 10% chance, get random cell of this ship
		int x = random.Next(Configuration.size);
		int y = random.Next(Configuration.size);
		if (random.Next(0, 100) <= 48)
		{
			while (enemy.DefenseMap.map.Grid[x,y].CellType != Configuration.CellType.Ship)
			{
				x = random.Next(Configuration.size);
				y = random.Next(Configuration.size);
			}
		}
		
		return new IntegerVector2(x, y);
	}

	public void UpdateInput()
	{
		IntegerVector2 point = HardGetTarget(ControlledPlayer, Game.Instance.CurrentDefender);

		lastPoint = point;
		
		ControlledPlayer.AttackMap.SetCursor(lastPoint);
		
		ControlledPlayer.Attack(Game.Instance.CurrentDefender, ControlledPlayer.AttackMap.map.cursorPosition);
	}
}