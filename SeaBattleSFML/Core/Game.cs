using SeaBattle.Core.Types;
using ZenisoftGameEngine.Types;

namespace SeaBattle.Core;

public class Game : BaseGame
{
	public static Game Instance { get; private set; }
	
	public Game(GameLaunchParams @params)
	{
		Instance = this;
	}

	public override void Initialize()
	{
		base.Initialize ();
	}
	
	
	protected override void OnFrameEnd()
	{
		
	}
	
}