using SeaBattle.Settings;

namespace SeaBattle.Types;

public static class IntegerVector2Extensions
{
	public static bool InBounds(this IntegerVector2 vector)
	{
		return vector.X >= 0 && vector.X < Configuration.size && vector.Y >= 0 && vector.Y < Configuration.size;
	}
}