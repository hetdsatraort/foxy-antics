using Godot;
using System;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export] private PackedScene _highScoreScene;
	[Export] private GridContainer _gridContainer;
	public override void _Ready()
	{
		GetTree().Paused = false;
		ScoreManager.Instance.CachedScore = 0;		
		SetUpScores();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			GameManager.LoadNextLLevel();
		}
		if(Input.IsActionJustPressed("quit") || Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
	}

	private void SetUpScores()
	{
		foreach(var score in ScoreManager.Instance.HighScoreCollection.HighScores)
		{
			var highScoreDisplay = _highScoreScene.Instantiate<HighScoreDisplay>();
			highScoreDisplay.SetHighScore(score);
			_gridContainer.AddChild(highScoreDisplay);
		}
	}
}
