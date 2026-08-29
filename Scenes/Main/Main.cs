using Godot;
using System;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			GameManager.StartGame();
		}
		if(Input.IsActionJustPressed("quit") || Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
	}
}
