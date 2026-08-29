using Godot;
using System;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	[Export] private Shooter _shooter;
	[Export] private Node2D _visuals;
	[Export] private HitBox _hitbox;
	[Export] private int _lives = 3;
	[Export] private int _points = 6;
	protected Player _playerRef;
	private AnimationNodeStateMachinePlayback _animationState;
	private Vector2 _visualsPosition;
	private bool _invincible = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if (_playerRef == null)
		{
			GD.PrintErr("Player Reference Not Found");
			QueueFree();
		}
		_trigger.AreaEntered += OnTriggered;
		_hitbox.AreaEntered += OnHBAreaEnetered;
		_animationState = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_visualsPosition = _visuals.Position;
		_animationTree.AnimationFinished += OnAnimationInvFinished;
	}
	
	private void OnAnimationInvFinished(StringName animName)
	{
		if(!_invincible) return;
		
		if (animName == "hit")
		{
			_invincible = false;
		}
	}

	private void GoInvincible()
	{
		if (!_invincible)
		{
			_invincible = true;
		}
	}

    private void OnHBAreaEnetered(Area2D area)
    {
        OnHit(area);
    }


    private void OnHit(Area2D area)
	{
		if(area is BulletBase && !_invincible)
		{
			GoInvincible();
			_animationState.Travel("hit");
			TweetHit();
			ReduceLives();
		}
	}

	private void ReduceLives()
	{
		_lives--;
		
		if(_lives <= 0)
		{
			SignalHub.EmitOnBossKilled();
			SignalHub.EmitOnPointsScored(_points);
			QueueFree();
		}
	}

	private void TweetHit()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(
			_visuals,
			Node2D.PropertyName.Position.ToString(),
			_visualsPosition,
			1.8f
		);
	}

    private void OnTriggered(Area2D area)
    {
		_animationTree.Set("parameters/conditions/on_trigger", true);
		// _hitbox.Activate(true);
		_trigger.AreaEntered -= OnTriggered;
    }

	private void TriggerShot()
	{
		_shooter.Shoot(_visuals.GlobalPosition.DirectionTo(_playerRef.GlobalPosition));
	}

	private void ActivateHB()
	{
		_hitbox.Activate(true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Shoot()
	{
		TriggerShot();
		GD.Print("shooted");
	}
}
