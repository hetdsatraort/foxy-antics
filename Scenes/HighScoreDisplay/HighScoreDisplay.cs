using Godot;
using System;

public partial class HighScoreDisplay : VBoxContainer
{
	
	[Export] private Label _scoreLabel;
	[Export] private Label _dateLabel;

	private HighScore _highScore;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(_highScore == null) QueueFree();
		_scoreLabel.Text = _highScore.Score.ToString("D5");
		_dateLabel.Text = _highScore.DateScored;

	}

	public void SetHighScore(HighScore score)
	{
		_highScore = score;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
