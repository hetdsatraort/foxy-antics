using Godot;
using System;

public partial class BulletBase : Area2D
{
	protected Vector2 _direction = Vector2.Right;
	[Export] protected CollisionShape2D _collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print($"Position: {Position}");
		GD.Print($"Global Position: {GlobalPosition}");
		GD.Print($"Bullet Position Breakpoint");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += _direction * (float)delta;
	}

	public void Setup(Vector2 pos, Vector2 dir, float speed)
	{
		Position = pos;
		_direction = dir * speed;
	}
}
