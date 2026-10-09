using Godot;
using System;

public partial class BallSpikes : PathFollow2D
{
	[Export] double _speed = 100.0f;
	[Export] double _spinSpeed = 300.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Progress += (float)(_speed * delta);
		RotationDegrees += (float)(_spinSpeed * delta);
	}
}
