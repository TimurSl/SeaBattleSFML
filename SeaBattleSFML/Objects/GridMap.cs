using SeaBattle.Cells;
using SeaBattle.MapCreators;
using SeaBattle.MapCreators.Types;
using SeaBattle.Settings;
using SeaBattle.Types;
using SFML.Graphics;
using SFML.System;
using ZenisoftGameEngine.Interfaces;
using ZenisoftGameEngine.Types;

namespace SeaBattleSFML.Objects;

public class GridMap : BaseObject, IDrawable
{
	public int ZIndex { get; set; }
	
	private RectangleShape[,] Grid { get; set; } = new RectangleShape[Configuration.size, Configuration.size];
	private Map map;
	
	private float cellSize = 50f;
	
	public Vector2f offset = new Vector2f(0, 0);

	public GridMap(LevelCreationType type, bool showCursor, bool useLastHit)
	{
		map = new Map(type, showCursor, useLastHit);
		
		for (int x = 0; x < Configuration.size; x++)
		{
			for (int y = 0; y < Configuration.size; y++)
			{
				Grid[x, y] = new RectangleShape(new Vector2f(cellSize, cellSize));
				Grid[x, y].FillColor = Color.Blue;
				Grid[x, y].Position = new Vector2f(x * cellSize + offset.X, y * cellSize + offset.Y);
			}
		}
	}
	public void Draw(RenderTarget target)
	{
		for (int x = 0; x < Configuration.size; x++)
		{
			for (int y = 0; y < Configuration.size; y++)
			{
				target.Draw(Grid[x, y]);
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
				switch (grid[x,y].CellType)
				{
					case Configuration.CellType.Nothing:
						Grid[x, y].FillColor = Color.Blue;
						break;
					case Configuration.CellType.Ship:
						Grid[x, y].FillColor = Color.Green;
						break;
					case Configuration.CellType.Miss:
						Grid[x, y].FillColor = Color.Red;
						break;
					case Configuration.CellType.Hit:
						Grid[x, y].FillColor = Color.Yellow;
						break;
					default:
						Grid[x, y].FillColor = Color.White;
						break;
				}
				
				Grid[x, y].Position = new Vector2f(x * cellSize + offset.X, y * cellSize + offset.Y);
				Grid[x, y].Size = new Vector2f(cellSize, cellSize);
				
				Grid[x, y].OutlineColor = Color.Black;
				Grid[x, y].OutlineThickness = 1;

				if (new IntegerVector2(x,y) == map.cursorPosition && map.showCursor)
				{
					Grid[x, y].FillColor += new Color(100,100,100,0);
				}
				
				if (new IntegerVector2(x, y) == map.lastHit && map.useLastHit)
				{
					Grid[x, y].FillColor = new Color(20,100,0,255);
				}
			}
		}
	}
}