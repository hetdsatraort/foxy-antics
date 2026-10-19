using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
    private const string SCORE_FILE_PATH = "user://foxy.res";


	// Called when the node enters the scene tree for the first time.
	public HighScoreCollection HighScoreCollection { get; private set; } = new HighScoreCollection();
	public override void _EnterTree()
	{
		// MakeScores();
		LoadHighScore();
	}

	public override void _ExitTree()
	{
		UpdateHighScore();
	}

	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LogInfo(string msg)
	{
		GD.Print($"ScoreManager {msg}");
	}

	private void MakeScores()
	{
		Random rng = new Random();
		for (int i = 0; i < 12; i++)
		{
			var randNum = rng.Next(10, 200);
			HighScoreCollection.AddNewScore(randNum);

			LogInfo($"Added new high score: {randNum}");
		}

		foreach(var score in HighScoreCollection.HighScores)
		{
			LogInfo($"Score: {score.Score} at: {score.DateScored}");
		}
	}

	private void LoadHighScore()
    {
        if (ResourceLoader.Exists(SCORE_FILE_PATH))
        {
            var loadedResource = ResourceLoader.Load<HighScoreCollection>(SCORE_FILE_PATH);
            if (loadedResource != null)
            {
                HighScoreCollection = loadedResource;
            }
        }
    }

	public void AddScore(int score)
	{
		HighScoreCollection.AddNewScore(score);
		UpdateHighScore();		
	}

	public void UpdateHighScore()
    {
        var highScoreResource = new HighScoreCollection();
        highScoreResource = HighScoreCollection;
        Error err = ResourceSaver.Save(highScoreResource, SCORE_FILE_PATH);

		if(err == Error.Ok)
		{
			LogInfo($"Scores Saved Ok, {highScoreResource.HighScores.Count} total");
		}
		else
		{
			LogInfo($"Scores Saved Not Ok, {highScoreResource.HighScores.Count} total");
		}
    }
}
