using SeaBattle.Settings;
using SeaBattle.Types;

namespace SeaBattle.Cells;

public class Ship : Cell
{
	public Cell[] ShipCells;
	private Configuration.ShipType shipType;
	
	public Ship(IntegerVector2 position, Configuration.ShipType ship) : base(position)
	{
		base.CellType = Configuration.CellType.Ship;
		shipType = ship;
		base.Position = position;
	}
	
	public override void ProcessDefenseHit()
	{
		base.CellType = Configuration.CellType.Hit;
	}
	
	public bool IsAlive()
	{
		return ShipCells.Any(cell => cell.CellType == Configuration.CellType.Ship);
	}

	public override char GetCellChar()
	{
		if (CellType == Configuration.CellType.Hit)
		{
			return Configuration.PixelMap[(int) Configuration.CellType.Hit].Char;
		}
		return Configuration.ShipPixelMap[(int) shipType].Char;
	}

	public override ConsoleColor GetCellColor()
	{
		if (CellType == Configuration.CellType.Hit)
		{
			return Configuration.PixelMap[(int) Configuration.CellType.Hit].Color;
		}
		return Configuration.ShipPixelMap[(int) shipType].Color;
	}
	
	
}