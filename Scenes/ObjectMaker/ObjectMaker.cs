using Godot;
using System;

public partial class ObjectMaker : Node
{
	[Export] private PackedScene _explosion;
	[Export] private PackedScene _fruitPickup;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
		SignalHub.Instance.OnCreateExplosion += OnCreateExplosion;
		SignalHub.Instance.OnCreatePickup += OnCreatePickup;
	}

    private void OnCreateExplosion(Vector2 pos)
	{
		Explosion explode = _explosion.Instantiate<Explosion>();
		explode.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, explode);

		FruitPickup fp = _fruitPickup.Instantiate<FruitPickup>();
		fp.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, fp);
	}

	private void OnCreatePickup(Vector2 pos)
	{
		FruitPickup fp = _fruitPickup.Instantiate<FruitPickup>();
		fp.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, fp);
	}

    private void OnCreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
	{
		BulletBase pb = scene.Instantiate<BulletBase>();
		pb.Setup(pos, dir, speed);
		CallDeferred(MethodName.AddObject, pb);
	}

	private void AddObject(Node node)
	{
		AddChild(node);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
