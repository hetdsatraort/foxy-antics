using Godot;
using System;

public partial class ObjectMaker : Node
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
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
