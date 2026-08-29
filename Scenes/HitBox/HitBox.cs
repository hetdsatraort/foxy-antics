using Godot;
using System;

[Tool]
public partial class HitBox : Area2D
{
	[Export]
	public Shape2D Shape
	{
		get
		{
			return _shape;
		}
		set
		{
			_shape = value;
			if (Engine.IsEditorHint() && _collisionShape2D != null)
			{
				_collisionShape2D.Shape = _shape;
				_collisionShape2D.QueueRedraw();
			}
		}
	}
	[Export] private CollisionShape2D _collisionShape2D;

	private Shape2D _shape;
	public override void _Ready()
	{
		_collisionShape2D.Shape = _shape;
	}

	public override void _Process(double delta)
	{
	}

	public void Activate(bool isOn)
	{
		SetDeferred(Area2D.PropertyName.Monitoring, isOn);
		SetDeferred(Area2D.PropertyName.Monitorable, isOn);
	}
}
