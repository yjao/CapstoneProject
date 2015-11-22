using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Interactable
{
	public delegate void Action(object sender, GameEventArgs args);
	public enum ObjectType { NPC, OBJECT };

	public int ID;
	public string Name;
	public string StrangerName = "???";
	public ObjectType objectType;

	public Dialogue[] Dialogue;
	public int LastDialogueDisplayed;

	public Interactable()
	{
		Dialogue = new Dialogue[]{};
	}

	public Interactable(string _name, string _strangerName, Dialogue[] _dialogues)
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

[Serializable]
public class Dialogue
{
	public int ID;
	public string text;
	public Choice[] choices;
	public Interactable.Action Action;
    public Dialogue(int _id, string _text, Choice[] _choices = null)
    {
        ID = _id;
        text = _text;
        choices = _choices;
    }
}

[Serializable]
public class Choice
{
	public string option;
	public Interactable.Action Action;
    public GameEventArgs GEA;
    public Choice (string _option, GameEventArgs _GEA = null)
    {
        option = _option;
        if (_GEA != null)
        {
            Action += _GEA.ChoiceAction;
            GEA = _GEA;
        }
    }
}
