using Godot;
using System;

public partial class BulletBase : Area2D
{
	protected Vector2 _direction = Vector2.Right;
	[Export] protected CollisionShape2D _collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
    {
        if(body is TileMapLayer)
		{
			QueueFree();
		}
    }


    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
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
