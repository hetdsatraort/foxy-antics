using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float GRAVITY = 690.0f;
	private const float RUN_SPEED = 120.0f;
	private const float JUMP_SPEED = -400.0f;
	private const float MAX_FALL = 300.0f;
	private bool _jumped = false;
	public bool IsStill { get { return Mathf.IsZeroApprox(Velocity.X); } }
	public bool IsFalling { get { return Velocity.Y > 0; } }
	public bool OnFloor { get { return IsOnFloor(); } }
	[Export] private Label _debugLabel;
	[Export] private AudioStreamPlayer2D _jumpSound;
	[Export] private Sprite2D _playerSprite;
	public override void _Ready()
	{
	}
	public override void _EnterTree()
	{
		AddToGroup("Player"); //GameConstants.GROUP_PLAYER; --> 
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		if(@event.IsActionPressed("jump"))
		{
			if(_jumped) return;

			_jumped = true;
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
		velocity.X = Input.GetAxis("left", "right") * RUN_SPEED;
		// if(IsOnFloor() && _jumped)
		if(_jumped)
		{
			velocity.Y = JUMP_SPEED;
			_jumpSound.Play();
		    _jumped = false;
		}

		if(!Mathf.IsZeroApprox(velocity.X))
		{
			_playerSprite.FlipH = velocity.X < 0;
		}

		return velocity;
	}
}
