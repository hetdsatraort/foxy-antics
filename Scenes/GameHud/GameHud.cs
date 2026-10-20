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
	[Export] public Node2D Player;
	private List<TextureRect> _hearts = new List<TextureRect>();
	private int _points = 0;
	private bool _complete = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetTree().Paused)
		{
			GetTree().Paused = false;
		}

		if (ScoreManager.Instance.CachedScore != 0)
		{
			_points = ScoreManager.Instance.CachedScore;
			_pointsLabel.Text = _points.ToString("D4");
		}
		_levelLabel.Text = $"Level: {GameManager.Instance.CurrentLevel + 1}";
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
		if (_complete && @event.IsActionPressed("shoot"))
		{
			GameManager.LoadNextLLevel();
		}
		else if (_pressShotLabel.Visible && @event.IsActionPressed("shoot"))
		{
			GameManager.GoToMainMenu();
		}
		if (@event.IsActionPressed("reload"))
		{
			GameManager.ReloadCurrentLevel();
		}
		if (@event.IsActionPressed("next"))
		{
			GameManager.LoadNextLLevel();
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
		ScoreManager.Instance.CachedScore += points;
	}

	private void OnLevelComplete(bool complete)
	{
		_complete = complete;
		if (complete)
		{
			_gameOverLabel.Text = "Level Complete";
			_youreWinnerSound.Play();
			if (GameManager.Instance.CurrentLevel + 1 >= GameManager.Instance._levelScenes.Count)
			{
				ScoreManager.Instance.AddScore(_points);
			}
		}
		else
		{
			_gameOverSound.Play();
			ScoreManager.Instance.AddScore(_points);
		}
		_gameOverLabel.Visible = true;
		_timer.Start();
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
		if (!_gameOverLabel.Text.Contains("GAME OVER"))
		{
			GetTree().Paused = false;
		}
		_gameOverLabel.Text = string.Empty;
		_pressShotLabel.Visible = true;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
