using Godot;
using System;
using System.Linq;

public partial class HighScoreCollection : Resource
{
	const int MAX_SCORES = 10;
	[Export] public Godot.Collections.Array<HighScore> HighScores {get; set;} = new();

	private void SortScores()
	{
		var lsHighScores = HighScores.ToList();
		lsHighScores.Sort((a, b) => b.Score.CompareTo(a.Score)); //descending sort
		HighScores = new(lsHighScores);
	}

	private void AddNewScore(int score)
	{
		HighScore hs = new HighScore();
		hs.Score = score;

		HighScores.Add(hs);
		
		SortScores();

		if(HighScores.Count > MAX_SCORES)
		HighScores.Resize(MAX_SCORES);
	}

}
