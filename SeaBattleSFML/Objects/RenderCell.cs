using agar.io.Game.Animations;
using SeaBattleSFML.Core;
using SeaBattleSFML.Settings;
using SFML.Graphics;
using SFML.System;

namespace SeaBattleSFML.Objects;

public class RenderCell
{
	public RectangleShape Shape { get; set; }
	
	public Animation Animation { get; set; }
	private Texture ShipTexture;
	private Texture FireTexture;

	public RenderCell(Vector2f position, float cellSize = 50f)
	{
		Shape = new RectangleShape(new Vector2f(cellSize, cellSize));
		Shape.FillColor = Color.Blue;
		Shape.Position = position;
		
		Animation = new Animation(Shape, true);
		Animation.Loop = true;
		
		string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory (), "AnimationFrames", "Wave"), "*.png");
		Random random = new Random();
		files = files.OrderBy(x => random.Next()).ToArray();
		
		for (var i = 0; i < files.Length; i++)
		{
			Texture texture = new Texture(files[i]);
			Animation.KeyFrames.Add(AnimationKeyFrameBuilder.CreateKeyFrame(i * 0.1f).SetTexture(texture));
		}
		
		ShipTexture = new Texture(Path.Combine(Directory.GetCurrentDirectory (), "AnimationFrames", "ShipTexture.png"));
		FireTexture = new Texture(Path.Combine(Directory.GetCurrentDirectory (), "AnimationFrames", "fire.png"));
	}
	
	public void UpdateCellState(Configuration.CellType cellType)
	{
		switch (cellType)
		{
			case Configuration.CellType.Nothing:
				break;
			case Configuration.CellType.Ship:
				Shape.Texture = ShipTexture;
				break;
			case Configuration.CellType.Miss:
				break;
			case Configuration.CellType.Hit:
				Shape.FillColor = Color.White;

				Shape.Texture = FireTexture;
				break;
			default:
				break;
		}
	}
}