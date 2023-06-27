namespace SeaBattle.Types;

public struct IntegerVector2
{
	public int X;
	public int Y;

	public IntegerVector2(int x, int y)
	{
		X = x;
		Y = y;
	}
	
	public static bool operator ==(IntegerVector2 a, IntegerVector2 b)
	{
		return a.X == b.X && a.Y == b.Y;
	}
	
	public static bool operator !=(IntegerVector2 a, IntegerVector2 b)
	{
		return !(a == b);
	}
	
	public static IntegerVector2 operator +(IntegerVector2 a, IntegerVector2 b)
	{
		return new IntegerVector2(a.X + b.X, a.Y + b.Y);
	}
	
	public static IntegerVector2 operator +(IntegerVector2 a, int b)
	{
		return new IntegerVector2(a.X + b, a.Y + b);
	}
	
	public static IntegerVector2 operator *(IntegerVector2 a, int b)
	{
		return new IntegerVector2(a.X * b, a.Y * b);
	}
}