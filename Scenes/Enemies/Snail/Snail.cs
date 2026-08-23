using Godot;
using System;

public partial class Snail : EnemyBase
{
	[Export] private RayCast2D _floorDetect;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = ApplyGravity(delta);
		if(IsOnFloor())
		{
			velocity.X = _animatedSprite2D.FlipH ? _speed : -_speed;
		}
		Velocity = velocity;
		MoveAndSlide();
		FlipMe();
	}

	protected override void FlipMe()
	{
		if (!_floorDetect.IsColliding() || IsOnWall())
		{
			_animatedSprite2D.FlipH = !_animatedSprite2D.FlipH;
			_floorDetect.Position = new Vector2(
				_floorDetect.Position.X * -1,
				_floorDetect.Position.Y
			);
		}
	}
}
