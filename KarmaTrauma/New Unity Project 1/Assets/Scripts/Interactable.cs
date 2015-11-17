using UnityEngine;
using System.Collections;
using System;

public class Interactable
{
	public delegate void Action(object sender, GameEventArgs args);
	public enum ObjectType { NPC, OBJECT };

	public int ID;
	public string Name;
	public string StrangerName = "???";
	public ObjectType objectType;

	public string[] Dialogue;
	public int LastDialogueDisplayed;

	public Interactable(string _name, string _strangerName, string[] _dialogues)
	{
		LastDialogueDisplayed = -1;
		Name = _name;
		if (_strangerName.Length > 0)
		{
			StrangerName = _strangerName;
		}
		Dialogue = _dialogues;
	}
}

public class Dialogue
{
	public int ID;
	public string text;
	public Choices[] choices;
	public Interactable.Action Action;
}

public class Choice
{
	public string option;
	public Interactable.Action Action;
}
