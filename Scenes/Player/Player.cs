using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	private const float GRAVITY = 690.0f;
	private const float RUN_SPEED = 120.0f;
	private const float JUMP_SPEED = -400.0f;
	private const float MAX_FALL = 300.0f;
	private Vector2 HurtJumpVelocity = new Vector2(0, -330.0f);
	private bool _jumped = false;
	private bool _hurt = false;
	private bool _invincible = false;
	public bool IsStill { get { return Mathf.IsZeroApprox(Velocity.X); } }
	public bool IsFalling { get { return Velocity.Y > 0; } }
	public bool OnFloor { get { return IsOnFloor(); } }
	public bool IsHurt { get { return _hurt; } }
	[Export] private Label _debugLabel;
	[Export] private AudioStreamPlayer2D _jumpSound;
	[Export] private AudioStreamPlayer2D _hurtSound;
	[Export] private Sprite2D _playerSprite;
	[Export] private Shooter _shooter;
	[Export] protected HitBox _hitBox;
	[Export] protected Timer _hurtTimer;
	[Export] private int _lives = 3;
	[Export] private int _invincibilityTimer = 16;

	[Export] private AnimationPlayer _invincibleAnimation;
	private PackedScene _bulletScene = GD.Load<PackedScene>("res://Scenes/BulletBase/PlayerBullet.tscn");
	public List<Area2D> _currentDamageAreas = new List<Area2D>();
	private int _invincibilityTimerRec = 16;

	public override void _Ready()
	{
		GoInvincible();
		_invincibilityTimerRec = _invincibilityTimer;
		_hitBox.AreaEntered += OnHBAreaEntered;
		_hitBox.AreaExited += OnHBAreaExited;
		_hurtTimer.Timeout += HurtTimeoutCallback;
		_invincibleAnimation.AnimationFinished += OnAnimationInvFinished;
	}

    private void OnAnimationInvFinished(StringName animName)
	{
		if(!_invincible) return;
		
		_invincibilityTimer -= 2;
		if (_invincibilityTimer <= 0)
		{
			_invincible = false;
			_invincibleAnimation.Play("RESET");
			_invincibilityTimer = _invincibilityTimerRec;
			if(_currentDamageAreas.Count > 0)
			{
				CallDeferred(MethodName.ApplyHit);
			}
		}
		else
		{
			GoInvincible();
		}
	}

	private void HurtTimeoutCallback()
	{
		_hurt = false;
		if (_lives == 0)
		{
			QueueFree();
		}
	}

    private void OnHBAreaEntered(Area2D area)
	{
		if(area is HitBox && !_currentDamageAreas.Contains(area))
		{
			_currentDamageAreas.Add(area);
		}
		CallDeferred(MethodName.ApplyHit);
	}

    private void OnHBAreaExited(Area2D area)
    {
        if(_currentDamageAreas.Contains(area))
		{
			_currentDamageAreas.Remove(area);
		}
    }

	private void ApplyHit()
	{
		if(_invincible) return;

		ApplyHurt();
		GoInvincible();
	}
	private void ApplyHurt()
	{
		if(_hurt) return;
		
		_hurt = true;
		// SignalHub.EmitOnCreateExplosion(GlobalPosition);
		_lives--;
		_hurtSound.Play();
		GD.Print($"Lives Left: {_lives}");
		Velocity = HurtJumpVelocity;
		_hurtTimer.Start();
		
	}

	private void GoInvincible()
	{
		if (!_invincible)
		{
			_invincible = true;
			_jumpSound.Play();
		}
		_invincibleAnimation.Play("invincible");		
	}

    public override void _EnterTree()
	{
		AddToGroup("Player"); //GameConstants.GROUP_PLAYER; --> 
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		if(_hurt) return;
		
		if (@event.IsActionPressed("jump"))
		{
			if (_jumped) return;

			_jumped = true;
		}
		if (@event.IsActionPressed("shoot"))
		{
			var direction = _playerSprite.FlipH ? Vector2.Left : Vector2.Right;
			_shooter.Shoot(direction);

			// SignalHub.EmitOnCreateBullet(Position, direction, 30.0f, _bulletScene);

			// BulletBase pb = _bulletScene.Instantiate<BulletBase>();
			// pb.Setup(direction, 300.0f);
			// CallDeferred(MethodName.AddChild, pb);
		}
		if (@event.IsActionPressed("test3"))
		{
			_invincibilityTimer = 1000;
			GoInvincible();
			_invincibilityTimer = _invincibilityTimerRec;
		}
		if (@event.IsActionPressed("test2"))
		{
			_invincible = false;
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;
		velocity = GetInput(velocity);

		velocity.Y = Mathf.Clamp(velocity.Y, JUMP_SPEED, MAX_FALL);
		Velocity = velocity;

		_debugLabel.Text = $"Position: {GlobalPosition.X}, {GlobalPosition.Y}, Velocity: {Velocity.X}, {Velocity.Y}";

		MoveAndSlide();

	}

	private Vector2 GetInput(Vector2 velocity)
	{
		if(_hurt) return velocity;

		velocity.X = Input.GetAxis("left", "right") * RUN_SPEED;
		// if(IsOnFloor() && _jumped)
		if (_jumped)
		{
			velocity.Y = JUMP_SPEED;
			_jumpSound.Play();
			_jumped = false;
		}

		if (!Mathf.IsZeroApprox(velocity.X))
		{
			_playerSprite.FlipH = velocity.X < 0;
		}

		return velocity;
	}
}
