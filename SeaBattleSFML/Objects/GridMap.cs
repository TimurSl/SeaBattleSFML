using SeaBattleSFML.Cells;
using SeaBattleSFML.Core.Types;
using SeaBattleSFML.MapCreators.Types;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;
using SFML.Graphics;
using SFML.System;
using ZenisoftGameEngine.Interfaces;
using ZenisoftGameEngine.Sound;
using ZenisoftGameEngine.Types;

namespace SeaBattleSFML.Objects;

public class GridMap : BaseObject, IDrawable
{
	public int ZIndex { get; set; }
	
	private RenderCell[,] Grid { get; set; } = new RenderCell[Configuration.size, Configuration.size];
	public Map map;
	
	private float cellSize = 50f;
	
	public Vector2f offset = new Vector2f(0, 0);
	
	public GridMap(LevelCreationType type, bool showCursor, bool useLastHit)
	{
		map = new Map(type, showCursor, useLastHit);
		
		for (int x = 0; x < Configuration.size; x++)
		{
			for (int y = 0; y < Configuration.size; y++)
			{
				Grid[x,y] = new RenderCell(new Vector2f(x * cellSize + offset.X, y * cellSize + offset.Y));
				Grid[x,y].Animation.Start();
			}
		}
	}
	
	public void Draw(RenderTarget target)
	{
		UpdateGrid ();
		for (int x = 0; x < Configuration.size; x++)
		{
			for (int y = 0; y < Configuration.size; y++)
			{
				target.Draw(Grid[x, y].Shape);
			}
		}
	}

	public void UpdateGrid()
	{
		Cell[,] grid = map.Grid;
		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				Grid[x, y].Shape.FillColor = UIConfiguration.CellColors[grid[x, y].CellType];
				
				Grid[x, y].Shape.Position = new Vector2f(x * cellSize + offset.X, y * cellSize + offset.Y);
				Grid[x, y].Shape.Size = new Vector2f(cellSize, cellSize);
				
				Grid[x, y].Shape.OutlineColor = Color.Black;
				Grid[x, y].Shape.OutlineThickness = 1;

				if (new IntegerVector2(x,y) == map.cursorPosition && map.showCursor)
				{
					Grid[x, y].Shape.FillColor += UIConfiguration.CursorColor;
				}
				
				if (new IntegerVector2(x, y) == map.lastHit && map.useLastHit)
				{
					Grid[x, y].Shape.FillColor = UIConfiguration.LastHitColor;
				}
				Grid[x, y].UpdateCellState(grid[x, y].CellType);

			}
		}
	}
	
	public void MoveCursor(IntegerVector2 direction)
	{
		if (map.cursorPosition.X + direction.X < 0 || map.cursorPosition.X + direction.X >= Configuration.size || 
		    map.cursorPosition.Y + direction.Y < 0 || map.cursorPosition.Y + direction.Y >= Configuration.size)
		{
			return;
		}
		map.cursorPosition += direction;
	}
	
	public bool HasShips()
	{
		return map.HasShips();
	}
	public void OutlineShip(Ship ship)
	{
		// get all cells around the ship
		Cell[] cells = ship.ShipCells;

		foreach (Cell cell in cells)
		{
			OutlineCell(cell);
		}
		
		UpdateGrid ();
	}
	
	private void OutlineCell(Cell cell)
	{
		for (int x = cell.Position.X - 1; x <= cell.Position.X + 1; x++)
		{
			for (int y = cell.Position.Y - 1; y <= cell.Position.Y + 1; y++)
			{
				if (x >= 0 && x < Grid.GetLength(0) && y >= 0 && y < Grid.GetLength(1))
				{
					if (map.Grid[x, y].CellType == Configuration.CellType.Nothing)
					{
						Cell cell1 = map.Grid[x, y];
						cell1.CellType = Configuration.CellType.Miss;
					}
				}
			}
		}
	}
}