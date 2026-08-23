using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] protected AnimatedSprite2D _animatedSprite2D;
	[Export] protected HitBox _hitBox;

	[Export] protected float _speed = 30.0f;
	[Export] protected float _fallenOffY = 200.0f;

	[Export] protected Timer _movementTimer;

	protected Player _playerRef;
	protected float _gravity = 800.0f;

	public override void _Ready()
	{
		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if (_playerRef == null)
		{
			GD.PrintErr("Player Reference Not Found");
			QueueFree();
		}
		_screenNotifier.ScreenEntered += OnScreenEntered;
		_movementTimer.Timeout += OnTimeout;
		_hitBox.AreaEntered += OnHBAreaEntered;
	}

    private void OnHBAreaEntered(Area2D area)
	{
		SignalHub.EmitOnCreateExplosion(GlobalPosition);
		QueueFree();
	}

    public override void _Process(double delta)
	{
		CallDeferred(MethodName.FallenOff);
	}

	private void FallenOff()
	{
		if(GlobalPosition.Y > _fallenOffY)
		{
			QueueFree();
		}
	}

	protected Vector2 ApplyGravity(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		
		return velocity;
	}

	protected virtual void FlipMe()
	{
		_animatedSprite2D.FlipH = _playerRef.GlobalPosition.X > GlobalPosition.X;
	}

	protected virtual void OnScreenEntered()
	{
		_movementTimer.Start();
		_screenNotifier.ScreenEntered -= OnScreenEntered;
	}
	protected virtual void OnTimeout()
	{
		// GD.Print("Timer has restarted, No Timeout Override on this loser enemy");
	}
}
