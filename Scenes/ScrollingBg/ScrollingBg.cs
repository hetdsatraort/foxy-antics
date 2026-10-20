using Godot;
using System;

public partial class ScrollingBg : Node2D
{
	[Export] private ScrollingBgImages _scrollingImages;
	[Export] private Vector2 baseSize = new Vector2(1920f, 1080f);
	[Export] private float targetScale = 1.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (_scrollingImages == null) 
		{
			QueueFree();
			return;
		}
		else
		{
			Setup();
			Scale = new Vector2(targetScale, targetScale);
			Position -= new Vector2(0f, baseSize.Y * targetScale);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void AddLayer(float currentScrollScale, Texture2D image)
	{
		Parallax2D parallax2D = new Parallax2D();
		parallax2D.RepeatSize = new Vector2(baseSize.X, 0.0f);
		parallax2D.ScrollScale = new Vector2(currentScrollScale, 1.0f);

		Sprite2D sprite = new Sprite2D();
		sprite.Texture = image;
		sprite.Centered = false;

		parallax2D.AddChild(sprite);
		AddChild(parallax2D);
	}

	private void Setup()
	{
		float _scrollGap = 1.0f / _scrollingImages.Images.Count;
		float currentScrollScale = 0.0f;

		for (int i = 0; i < _scrollingImages.Images.Count; i++)
		{
			AddLayer(currentScrollScale, _scrollingImages.Images[i]);
			currentScrollScale += _scrollGap;
		}
	}
}
