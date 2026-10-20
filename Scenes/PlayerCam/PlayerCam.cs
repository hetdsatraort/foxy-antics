using Godot;
using System;

public partial class PlayerCam : Camera2D
{
	[Export] private double _shakeAmount = 5.0f;
	[Export] private double _shakeTime = 0.3f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnPlayerHit += OnPlayerHit;
		SetProcess(false);
	}

	public override void _ExitTree()
	{
		SignalHub.Instance.OnPlayerHit -= OnPlayerHit;
	}

    private async void OnPlayerHit(int lives, bool shake)
    {
		if (!shake) return;

		SetProcess(true);
		
		await ToSignal(GetTree().CreateTimer(_shakeTime), Timer.SignalName.Timeout);
		
		SetProcess(false);
		Offset = Vector2.Zero;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Offset = new Vector2(
			(float)GD.RandRange(-_shakeAmount, _shakeAmount),
			(float)GD.RandRange(-_shakeAmount, _shakeAmount)
		);
	}
}
