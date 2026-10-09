using Godot;
using System;

public partial class MovingPlatformPf : PathFollow2D
{
	[Export] double _speed = 40.0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Progress += (float)(_speed * delta);
	}
}
