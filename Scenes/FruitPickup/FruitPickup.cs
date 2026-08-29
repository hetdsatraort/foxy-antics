using Godot;
using System;
using System.Linq;

public partial class FruitPickup : Area2D
{
	[Export] AnimatedSprite2D _animatedSprite;
	[Export] AudioStreamPlayer2D _audioStream;
	[Export] int _points = 2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayRandomAnimation();
		AreaEntered += OnAreaEntered;
		_audioStream.Finished += QueueFree;
	}

    private void OnAreaEntered(Area2D area)
    {
        _audioStream.Play();
		SignalHub.EmitOnPointsScored(_points);
		Hide();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    private void PlayRandomAnimation()
	{
		var arrAnimations = _animatedSprite.SpriteFrames.GetAnimationNames();
		if(arrAnimations.Length > 0)
		{
			var reqIndex = new Random().Next(arrAnimations.Length - 1);
			string randName = arrAnimations[reqIndex];
			_animatedSprite.Play(randName);
		}
	}
}
