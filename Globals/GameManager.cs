using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
    private PackedScene _levelBaseScene = GD.Load<PackedScene>("res://Scenes/LevelBase/LevelBase.tscn");
    private PackedScene _nextScene;
    public PackedScene SimpleMainScene { get { return _mainScene; } }
    private int _currentLevel = -1;
    private List<PackedScene> _levelScenes = new List<PackedScene>
    {
        GD.Load<PackedScene>("res://Scenes/LevelBase/Level1.tscn"),
        GD.Load<PackedScene>("res://Scenes/LevelBase/Level2.tscn"),
        GD.Load<PackedScene>("res://Scenes/LevelBase/Level3.tscn"),
        GD.Load<PackedScene>("res://Scenes/LevelBaseOld/LevelBaseOld.tscn")
    };

    public int CurrentLevel { get { return _currentLevel; } }
    public PackedScene NextScene { get { return _nextScene; } set { _nextScene = value; } }

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

    private void LoadMainMenu()
    {
        _currentLevel = -1;
        NextScene = SimpleMainScene;
        GetTree().ChangeSceneToPacked(SimpleMainScene);
    }

    private void LoadNextLevel()
    {
        _currentLevel++;
        if (_currentLevel >= _levelScenes.Count)
        {
            LoadMainMenu();
        }
        NextScene = _levelScenes[_currentLevel];
        GetTree().ChangeSceneToPacked(NextScene);
    }

    private void ReloadLevel()
    {
        if(_currentLevel == -1) return;
        NextScene = _levelScenes[_currentLevel];
        GetTree().ChangeSceneToPacked(NextScene);
    }

    public static void GoToMainMenu()
    {
        if (Instance != null)
        {
            Instance.LoadMainMenu();
        }
    }


    public static void LoadNextLLevel()
    {
        if (Instance != null)
        {
            Instance.LoadNextLevel();
        }
    }

    public static void ReloadCurrentLevel()
    {
        if (Instance != null)
        {
            Instance.ReloadLevel();
        }
    }
}
