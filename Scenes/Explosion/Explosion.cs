using Godot;
using System;

public partial class Explosion : AnimatedSprite2D
{
	[Export] private AudioStreamPlayer2D _audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_audio.Play();
		AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished()
	{
		CallDeferred(MethodName.QueueFree);
	}
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
