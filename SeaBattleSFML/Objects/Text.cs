using SeaBattleSFML.Core.Types;
using SFML.System;
using ZenisoftGameEngine.Interfaces;

namespace SeaBattleSFML.Objects;

public class Text : IDrawable
{
	public int ZIndex { get; set; } = 999;
	public SFML.Graphics.Text TextClass;
	
	public Text(string message, uint size, SFML.Graphics.Color color, Vector2f position)
	{
		TextClass = new SFML.Graphics.Text(message, UIConfiguration.Font, size);
		TextClass.Position = position;
		TextClass.FillColor = color;
		TextClass.OutlineColor = SFML.Graphics.Color.Black;
		TextClass.OutlineThickness = 1f;
		TextClass.Origin = new Vector2f(TextClass.GetLocalBounds().Width / 2, TextClass.GetLocalBounds().Height / 2);
	}

	public void Draw(SFML.Graphics.RenderTarget target)
	{
		target.Draw(TextClass);
	}
	
	public void SetPosition(Vector2f position)
	{
		TextClass.Position = position;
	}
	
	public void SetMessage(string message)
	{
		TextClass.DisplayedString = message;
		TextClass.Origin = new Vector2f(TextClass.GetLocalBounds().Width / 2, TextClass.GetLocalBounds().Height / 2);
	}
	
	public void SetColor(SFML.Graphics.Color color)
	{
		TextClass.FillColor = color;
	}
	
	public string GetMessage()
	{
		return TextClass.DisplayedString;
	}
	
	public void Hide()
	{
		TextClass.FillColor = SFML.Graphics.Color.Transparent;
		TextClass.OutlineColor = SFML.Graphics.Color.Transparent;
	}
	
	public void Show()
	{
		TextClass.FillColor = SFML.Graphics.Color.White;
		TextClass.OutlineColor = SFML.Graphics.Color.Black;
	}
}