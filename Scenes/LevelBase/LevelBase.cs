using Godot;
using System;

public partial class LevelBase : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnLevelComplete += OnLevelComplete;
	}

    private void OnLevelComplete()
	{
		GD.Print("Level Complete");
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public override void _UnhandledInput(InputEvent @event)
    {
        if(Input.IsActionJustPressed("quit"))
        {
            GameManager.GoToMainMenu();
        }
    }
}
