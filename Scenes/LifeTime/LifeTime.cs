using Godot;
using System;

public partial class LifeTime : Node
{
	[Export] private Timer _Timer;
	[Export] private float _waitTime = 10.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Timer.Start(_waitTime);
		_Timer.Timeout += OnTimeout;
	}

	protected void OnTimeout()
	{
		GetParent().QueueFree();
	}
}
