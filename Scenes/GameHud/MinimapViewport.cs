using Godot;
using System;

public partial class MinimapViewport : SubViewport
{
	private Camera2D _minimapCamera;

	private Node2D _player;
    public override void _Ready()
    {
        _minimapCamera = GetNode<Camera2D>("Camera2D");
        _player = GetNode<Player>("Player");
        // Force this viewport to use the main game's 2D world data
        World2D = GetTree().Root.World2D;
    }

    public override void _Process(double delta)
    {

        // Godot instances are checked using IsInstanceValid to prevent errors if freed
        if (IsInstanceValid(_player))
        {
            // Keep the minimap camera centered on the player
            _minimapCamera.GlobalPosition = _player.GlobalPosition;
        }
    }
}
