using SeaBattle.MapCreators.Types;
using SeaBattle.Settings;
using SeaBattle.Types;
using static SeaBattle.Settings.Configuration;

namespace SeaBattle.Cells;

public class Cell
{
	public IntegerVector2 Position;
	public CellType CellType { get; set; }

	public Cell(IntegerVector2 position, CellType cellType)
	{
		Position = position;
		CellType = cellType;
	}

	public Cell(IntegerVector2 position)
	{
		Position = position;
		CellType = CellType.Nothing;
	}

	public virtual void ProcessDefenseHit()
	{
		CellType = CellType.Miss;
	}
	
	public bool IsAlreadyHit()
	{
		return CellType == CellType.Hit || CellType == CellType.Miss;
	}
	
	public bool InBounds()
	{
		return Position.X >= 0 && Position.X < Configuration.size && Position.Y >= 0 && Position.Y < Configuration.size;
	}
	
	public virtual char GetCellChar()
	{
		return PixelMap[(int) CellType].Char;
	}
	
	public virtual ConsoleColor GetCellColor()
	{
		return PixelMap[(int) CellType].Color;
	}

	public virtual void ProcessAttackHit(Map map, IntegerVector2 coords)
	{
		Cell cell = map.Grid[coords.X, coords.Y];
		if (cell.CellType == CellType.Ship)
		{
			Console.WriteLine("Hit!");
			CellType = CellType.Hit;
		}
		else
		{
			Console.WriteLine("Miss!");
			CellType = CellType.Miss;
		}
	}
	
}