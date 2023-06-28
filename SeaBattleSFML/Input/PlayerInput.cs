using SeaBattleSFML.Core;
using SeaBattleSFML.Objects;
using SeaBattleSFML.Settings;
using SeaBattleSFML.Types;
using SFML.Window;

namespace SeaBattleSFML.Input;

public class PlayerInput : IInput
{
	public Player ControlledPlayer { get; set; }
	
	public Dictionary<PlayerInputKey, Configuration.Direction> KeyMap { get; set; } = new Dictionary<PlayerInputKey, Configuration.Direction>();

	public Dictionary<PlayerInputKey, Action> ActionMap { get; set; } = new Dictionary<PlayerInputKey, Action> ();
	public PlayerInput()
	{
		ActionMap.Add(new PlayerInputKey(Keyboard.Key.Space), Attack);
		ActionMap.Add(new PlayerInputKey(Keyboard.Key.Enter), Attack);

		KeyMap.Add(new PlayerInputKey(Keyboard.Key.W), Configuration.Direction.Up);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.S), Configuration.Direction.Down);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.A), Configuration.Direction.Left);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.D), Configuration.Direction.Right);
		
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.Up), Configuration.Direction.Up);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.Down), Configuration.Direction.Down);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.Left), Configuration.Direction.Left);
		KeyMap.Add(new PlayerInputKey(Keyboard.Key.Right), Configuration.Direction.Right);
	}


	public void UpdateInput()
	{
		foreach (var key in KeyMap.Keys)
		{
			if (key.GetKeyDown ())
			{
				MoveCursor(KeyMap[key]);
			}
		}
		
		foreach (var key in ActionMap.Keys)
		{
			if (key.GetKeyDown())
			{
				ActionMap[key]();
			}
		}
	}
	
	public void MoveCursor(Configuration.Direction direction)
	{
		IntegerVector2 vector = direction switch {
			Configuration.Direction.Up => new IntegerVector2(0, -1),
			Configuration.Direction.Down => new IntegerVector2(0, 1),
			Configuration.Direction.Left => new IntegerVector2(-1, 0),
			Configuration.Direction.Right => new IntegerVector2(1, 0),
		};
		
		ControlledPlayer.AttackMap.MoveCursor(vector);
	}
	
	public void Attack()
	{
		ControlledPlayer.Attack(Game.Instance.CurrentDefender, ControlledPlayer.AttackMap.map.cursorPosition);
	}
}