using SeaBattleSFML.Settings;
using SFML.Graphics;

namespace SeaBattleSFML.Core.Types;

public static class UIConfiguration
{
	public static Font Font = new Font(Path.Combine(Directory.GetCurrentDirectory (), "Fonts", "Pusia.otf"));
	
	public static Color CursorColor = new Color(229, 68, 109);
	public static Color LastHitColor = new Color(108, 166, 193);
	
	public static Dictionary<Configuration.CellType, Color> CellColors = new Dictionary<Configuration.CellType, Color>()
	{
		{Configuration.CellType.Nothing, new Color(47, 48, 97)},
		{Configuration.CellType.Ship, new Color(230, 225, 197)},
		{Configuration.CellType.Hit, new Color(194, 1, 20)},
		{Configuration.CellType.Miss, new Color(17, 17, 34)},
	};
}