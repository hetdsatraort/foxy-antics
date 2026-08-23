using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; set; }
	[Signal] public delegate void OnCreateBulletEventHandler(Vector2 pos, Vector2 dir, float speed, PackedScene scene);
	[Signal] public delegate void OnCreateExplosionEventHandler(Vector2 pos);
	[Signal] public delegate void OnCreatePickupEventHandler(Vector2 pos);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void EmitOnCreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
	{
		Instance.EmitSignal(SignalName.OnCreateBullet, pos, dir, speed, scene);
	}

	public static void EmitOnCreateExplosion(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.OnCreateExplosion, pos);
	}

	public static void EmitOnCreatePickup(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.OnCreatePickup, pos);
	}
}
