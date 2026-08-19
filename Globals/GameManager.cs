using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
	private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
    private PackedScene _levelBaseScene = GD.Load<PackedScene>("res://Scenes/LevelBase/LevelBase.tscn");
    private PackedScene _nextScene;
	public PackedScene SimpleMainScene  { get { return _mainScene;}}
    public PackedScene SimpleLevelBaseScene  { get { return _levelBaseScene;}}
    public PackedScene NextScene { get { return _nextScene;} set { _nextScene = value;}}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		ProcessMode = ProcessModeEnum.Always;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void GoToMainMenu()
    {
        if (Instance != null)
        {
            Instance.NextScene = Instance.SimpleMainScene;
            Instance.GetTree().ChangeSceneToPacked(Instance.SimpleMainScene);
        }
    }

    public static void StartGame()
    {
        if (Instance != null)
        {
            Instance.NextScene = Instance.SimpleLevelBaseScene;
            Instance.GetTree().ChangeSceneToPacked(Instance.SimpleLevelBaseScene);
        }
    }
}
