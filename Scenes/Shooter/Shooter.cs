using Godot;
using System;

public partial class Shooter : Node2D
{
	[Export] private PackedScene _bulletScene;
	[Export] private float _speed;
	[Export] private float _shootDelay;
	[Export] private Timer _timer;
	[Export] private AudioStreamPlayer2D _audioStream;
	private bool _canShoot = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Timeout += OnTimeout;
		_timer.WaitTime = _shootDelay;
	}

    private void OnTimeout()
	{
		_canShoot = true;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
	}

	public void Shoot(Vector2 direction)
	{
		if(_canShoot)
		{
			SignalHub.EmitOnCreateBullet(GlobalPosition, direction, _speed, _bulletScene);
			_audioStream.Play();
			_canShoot = false;
		}
	}
}
