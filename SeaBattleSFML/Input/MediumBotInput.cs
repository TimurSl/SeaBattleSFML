using SeaBattleSFML.Core;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;

namespace SeaBattleSFML.Input;

public class MediumBotInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	private IntegerVector2 lastPoint = new IntegerVector2(0, 0);
	public IntegerVector2 MediumGetTarget(Player attacker, Player enemy)
	{
		Random random = new Random();
		int x = random.Next(Configuration.size);
		int y = random.Next(Configuration.size);
		// try to find already hit (not miss) cell, and then try to find a cell around it (if no cells hit, then just random)
		int attempts = 10;
		bool found = false;
		while (attacker.AttackMap.map.Grid[x,y].CellType != Configuration.CellType.Hit && attempts > 0 && !attacker.AttackMap.map.Grid[x,y].InBounds ())
		{
			x = random.Next(Configuration.size);
			y = random.Next(Configuration.size);
			attempts--;
			if (enemy.DefenseMap.map.Grid[x,y].CellType == Configuration.CellType.Hit)
			{
				found = true;
			}
		}

		if (found)
		{
			int x1 = x + random.Next(-1, 2);
			int y1 = y + random.Next(-1, 2);
			
			// check if it is in bounds
			if (!attacker.AttackMap.map.Grid[x1,y1].IsAlreadyHit () && attacker.AttackMap.map.Grid[x1,y1].InBounds ())
			{
				x = x1;
				y = y1;
			}
		}
		return new IntegerVector2(x, y);
	}
	


	public void UpdateInput()
	{
		IntegerVector2 point = MediumGetTarget(ControlledPlayer, Game.Instance.CurrentDefender);

		lastPoint = point;
		
		ControlledPlayer.AttackMap.SetCursor(lastPoint);
		
		ControlledPlayer.Attack(Game.Instance.CurrentDefender, ControlledPlayer.AttackMap.map.cursorPosition);

	}
}