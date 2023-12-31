using SFML.Graphics;
using SFML.Window;
using ZenisoftGameEngine.Config;
using ZenisoftGameEngine.Interfaces;

namespace ZenisoftGameEngine.Types;

public abstract class BaseGame
{
	protected Engine Engine { get; init; }

	protected BaseGame()
	{
		Engine = new Engine();
		
		Engine.OnFrameStart += OnFrameStart;
		Engine.OnFrameEnd += OnFrameEnd;
		
		Engine.Window.Closed += OnWindowClosed;
	}
	
	public virtual void Run()
	{
		Initialize();
		
		Engine.Run();
	}
	
	public virtual void Initialize()
	{
		Engine.DestroyAll();
		
	}

	protected virtual void OnFrameStart()
	{
		
	}

	protected virtual void OnFrameEnd()
	{
		
	}
	
	protected virtual void OnWindowClosed(object sender, EventArgs args)
	{
		Window window = (Window) sender;
		EngineConfiguration.Save();
		
		window.Close();
	}
	
	public void RegisterUpdatable(IUpdatable updatable)
	{
		if (updatable == null)
		{
			Console.WriteLine("Updatable is null");
			return;
		}
		Engine.updatables.Add(updatable);
	}
	
	public void RegisterDrawable(IDrawable drawable)
	{
		Engine.drawables.Add(drawable);
	}
	
	public BaseObject RegisterActor(BaseObject actor)
	{
		return Engine.RegisterActor(actor);
	}

	public RenderTarget GetDrawTarget()
	{
		return Engine.Window;
	}

}