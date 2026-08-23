using Godot;
using System;

public partial class Frog : EnemyBase
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private bool _jump = false;

	private readonly Vector2 JUMP_VEL_R = new Vector2(100.0f, -150.0f);
	private readonly Vector2 JUMP_VEL_L = new Vector2(-100.0f, -150.0f);
	public override void _Ready()
	{
		base._Ready();
		_movementTimer.OneShot = true;
		_movementTimer.WaitTime = GD.RandRange(2.0f, 4.0f);
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = ApplyGravity(delta);
		Velocity = velocity;
		ApplyJump();
		MoveAndSlide();
        FlipMe();

		if(IsOnFloor())
		{
			_animatedSprite2D.Play("frog_idle");
			Velocity = Vector2.Zero;
		}
	}

	protected override void OnTimeout()
	{
		_jump = true;
	}

	private void ApplyJump()
	{
		if(IsOnFloor() && _jump)
		{
			_animatedSprite2D.Play("frog_jumping");
			Velocity = _animatedSprite2D.FlipH ? JUMP_VEL_R : JUMP_VEL_L;
			_jump = false;
			_movementTimer.Start(GD.RandRange(2.0f, 4.0f));
		}
	}
}
