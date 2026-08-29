using Godot;
using System;

public partial class GameHud : Control
{
	[Export] private AudioStreamPlayer _youreWinnerSound;
	[Export] private AudioStreamPlayer _gameOverSound;
	[Export] private Label _gameOverLabel;
	[Export] private Label _pressShotLabel;
	[Export] private Timer _timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnLevelComplete += OnLevelComplete;
		_timer.Timeout += () =>
		{
			_pressShotLabel.Visible = true;
			GetTree().Paused = false;
		};
		
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if(_pressShotLabel.Visible && @event.IsActionPressed("shoot"))
		{
			GameManager.GoToMainMenu();
		}
    }

    private void OnLevelComplete()
    {
		_gameOverLabel.Text = "Level Complete";
		_gameOverLabel.Visible = true;
		_youreWinnerSound.Play();
		_timer.Start();
		GetTree().Paused = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
