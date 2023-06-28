using SeaBattleSFML.Objects;

namespace SeaBattleSFML.Input;

public interface IInput
{
	public Player ControlledPlayer { get; set; }
	
	public void UpdateInput();
}