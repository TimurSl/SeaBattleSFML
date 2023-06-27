using SFML.Graphics;
using ZenisoftGameEngine.Interfaces;
using ZenisoftGameEngine.Types;

namespace SeaBattleSFML.Objects;

public class Player : BaseObject, IUpdatable, IDrawable
{
	public int ZIndex { get; set; }

	public GridMap AttackMap { get; set; }
	public GridMap DefenseMap { get; set; }
	
	public void Update()
	{
		
	}

	public void Draw(RenderTarget target)
	{
		
	}
}