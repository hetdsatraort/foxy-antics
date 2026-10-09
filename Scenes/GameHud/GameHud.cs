using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameHud : Control
{
	[Export] private AudioStreamPlayer _youreWinnerSound;
	[Export] private AudioStreamPlayer _gameOverSound;
	[Export] private Label _gameOverLabel;
	[Export] private Label _pressShotLabel;
	[Export] private Label _levelLabel;
	[Export] private Label _pointsLabel;
	[Export] private Timer _timer;
	[Export] private HBoxContainer _hbHearts;
	private List<TextureRect> _hearts = new List<TextureRect>();
	private int _points = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hearts = _hbHearts.GetChildren().OfType<TextureRect>().ToList();
		SignalHub.Instance.OnLevelComplete += OnLevelComplete;
		SignalHub.Instance.OnPointsScored += OnPointsScored;
		SignalHub.Instance.OnPlayerHit += OnPlayerHit;
		_timer.Timeout += HUDTimeout;
	}
	public override void _ExitTree()
	{
		SignalHub.Instance.OnLevelComplete -= OnLevelComplete;
		SignalHub.Instance.OnPointsScored -= OnPointsScored;
		SignalHub.Instance.OnPlayerHit -= OnPlayerHit;
		_timer.Timeout -= HUDTimeout;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (_pressShotLabel.Visible && @event.IsActionPressed("shoot"))
		{
			GameManager.GoToMainMenu();
		}
		if (@event.IsActionPressed("quit"))
		{
			GameManager.GoToMainMenu();
		}
	}

	private void OnPointsScored(int points)
	{
		_points += points;
		_pointsLabel.Text = _points.ToString("D4");
	}

	private void OnLevelComplete(bool complete)
	{
		if(complete)
		{
			_gameOverLabel.Text = "Level Complete";
			_youreWinnerSound.Play();
		}
		else
		{
			_gameOverSound.Play();
		}
		_gameOverLabel.Visible = true;
		_timer.Start();
		GetTree().Paused = true;
	}

	private void OnPlayerHit(int lives, bool shake)
	{
		for (int i = 0; i < _hearts.Count; i++)
		{
			_hearts[i].Visible = lives > i;
		}
		if (lives <= 0)
		{
			OnLevelComplete(false);
		}
	}

	private void HUDTimeout()
	{
		_pressShotLabel.Visible = true;
		GetTree().Paused = false;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
