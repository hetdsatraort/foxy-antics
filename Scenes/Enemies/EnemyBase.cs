using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private HitBox _hitBox;

	[Export] protected float _speed = 30.0f;
	protected float _gravity = 800.0f;

	public override void _Ready()
	{
	}
}
