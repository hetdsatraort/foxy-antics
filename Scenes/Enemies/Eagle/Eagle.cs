using Godot;
using System;
using System.Threading;

public partial class Eagle : EnemyBase
{
	private Vector2 FLY_SPEED = new Vector2(35.0f, 15.0f);
	private Vector2 _flyDirection = Vector2.Zero;
	[Export] private Shooter _shooter;
	[Export] private RayCast2D _rayCast2D;

	public override void _PhysicsProcess(double delta)
    {
		Velocity = _flyDirection;
		TriggerShot();
		MoveAndSlide();
    }

	private void TriggerShot()
	{
		if(_rayCast2D.IsColliding())
		{
			_shooter.Shoot(GlobalPosition.DirectionTo(_playerRef.GlobalPosition));
		}
		
	}

	
	protected override void OnScreenEntered()
	{
		base.OnScreenEntered();
		_animatedSprite2D.Play("eagle");
		FlyToPlayer();
	}
	protected override void OnTimeout()
	{
		FlyToPlayer();
	}

	private void FlyToPlayer()
	{
		FlipMe();
		float xDir = _animatedSprite2D.FlipH == true ? 1.0f : -1.0f;
		float yDir = _playerRef.GlobalPosition.Y > GlobalPosition.Y ? 1.0f : -1.0f;
		_flyDirection = new Vector2(xDir * FLY_SPEED.X, yDir * FLY_SPEED.Y );
	}
}
