using SeaBattleSFML.Cells;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;

namespace SeaBattleSFML.MapCreators.Types;

public class Map
{
	public Cell[,] Grid;

	public IntegerVector2 cursorPosition = new IntegerVector2(0, 0);
	public IntegerVector2 lastHit = new IntegerVector2(-1, -1);

	public bool showCursor;
	public bool useLastHit;
	
	private LevelCreationType levelType;

	public Map(LevelCreationType levelType = LevelCreationType.Random, bool showCursor = false, bool useLastHit = false)
	{
		CreateGrid (levelType);
		
		this.levelType = levelType;
		this.showCursor = showCursor;
		this.useLastHit = useLastHit;
		
		Random r = new Random();
	}

	private void CreateGrid(LevelCreationType levelType)
	{
		Grid = levelType switch
		{
			LevelCreationType.Empty => LevelGenerator.MakeEmptyMap (),
			LevelCreationType.Random => LevelGenerator.GenerateLevel (),
			_ => Grid
		};
	}

	public bool HasShips()
	{
		for (int x = 0; x < Grid.GetLength(0); x++)
		{
			for (int y = 0; y < Grid.GetLength(1); y++)
			{
				if (Grid[x, y].CellType == Configuration.CellType.Ship)
				{
					return true;
				}
			}
		}

		return false;
	}

	public void ResetMap()
	{
		// regenerate the map
		CreateGrid(levelType);
	}
	
	
}