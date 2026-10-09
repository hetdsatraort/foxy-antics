using Godot;
using System;

public partial class AnimatableMovingPlatform : AnimatableBody2D
{
	[Export] private Marker2D _from;
	[Export] private Marker2D _to;
	[Export] float _speed = 100.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(_from == null || _to == null) QueueFree();
		GlobalPosition = _from.GlobalPosition;
		MoveToTarget(_to.GlobalPosition);
	}

	private void MoveToTarget(Vector2 target)
	{
		float totalTime = GlobalPosition.DistanceTo(target)/_speed;

		Tween tween = CreateTween();
		tween.TweenProperty(this, Node2D.PropertyName.GlobalPosition.ToString(), target, totalTime);

		tween.Finished += () => 
		{
			Vector2 next = target == _to.GlobalPosition ? _from.GlobalPosition : _to.GlobalPosition; 
			MoveToTarget(next); 
		};
	}
}
