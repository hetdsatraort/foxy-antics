using Godot;
using System;

public partial class MinimapViewport : SubViewport
{
	private Camera2D _minimapCamera;

	private Player _player;
    public override void _Ready()
    {
        _minimapCamera = GetNode<Camera2D>("MiniMapCamera");
        // Force this viewport to use the main game's 2D world data
        World2D = GetTree().Root.World2D;
    }

    public override void _Process(double delta)
    {
        // The player lives in the level, not under the HUD, so look it up by group
        if (!IsInstanceValid(_player))
        {
            _player = GetTree().GetFirstNodeInGroup("Player") as Player;
            if (_player == null) return;
        }

        // Follow the player, but stay inside the same limits as the player cam
        _minimapCamera.GlobalPosition = ClampToLimits(_player.GlobalPosition, _player.PlayerCam);
    }

    private Vector2 ClampToLimits(Vector2 target, Camera2D limitsCam)
    {
        // Half of the world area the minimap shows (viewport size scaled by zoom)
        Vector2 halfView = (Vector2)Size / _minimapCamera.Zoom / 2.0f;

        return new Vector2(
            ClampAxis(target.X, limitsCam.LimitLeft, limitsCam.LimitRight, halfView.X),
            ClampAxis(target.Y, limitsCam.LimitTop, limitsCam.LimitBottom, halfView.Y)
        );
    }

    private static float ClampAxis(float value, float min, float max, float halfView)
    {
        // Level is smaller than the minimap view on this axis, so keep it centred
        if (max - min <= halfView * 2.0f) return (min + max) / 2.0f;

        return Mathf.Clamp(value, min + halfView, max - halfView);
    }
}
