using Godot;
using System;
using System.Collections.Generic;

public partial class InputMapDisplay : Label
{
	// The exact name of the action from your Project Settings -> Input Map
	[Export]
	public Godot.Collections.Array<string> _actions { get; set; } = ["shoot", "jump", "quit"];

	public override void _Ready()
	{
		UpdateDisplay();
	}

	public void UpdateDisplay()
	{
		foreach (var ActionName in _actions)
		{
			// 1. Get the list of all keys/buttons assigned to this action
			var events = InputMap.ActionGetEvents(ActionName);

			if (events.Count > 0)
			{
				// 2. Grab the first event (usually the primary key bind)
				InputEvent firstEvent = events[0];

				// 3. Clean up the text formatting (removes trailing "(Physical)")
				string cleanText = firstEvent.AsText().Split(" (")[0];

				// 4. Update the Label text
				Text += $"Press [{cleanText}] to {ActionName}\n";
			}
			else
			{
				Text += $"Action [{ActionName}] has no key bound!\n";
			}

		}

		Text += "\nPRESS SHOOT TO START";
	}
}
