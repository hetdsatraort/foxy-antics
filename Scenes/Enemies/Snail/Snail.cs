using Godot;
using System;

public partial class Snail : EnemyBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		Velocity = velocity;
		GD.Print(velocity.Y.ToString("F2"));

		MoveAndSlide();

	}
}
