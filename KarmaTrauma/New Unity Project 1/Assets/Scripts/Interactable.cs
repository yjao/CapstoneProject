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
    public ChoiceEventArgs CEA;
    public string setbool;
    public Dialogue(int _id, string _text, Choice[] _choices = null, ChoiceEventArgs _CEA = null, string b = null)
    {
        ID = _id;
        text = _text;
        choices = _choices;
        setbool = b;
        if (_CEA != null)
        {
            CEA = _CEA;
            Action += _CEA.ChoiceAction;
        }
    }
}

[Serializable]
public class Choice
{
	public string option;
	public Interactable.Action Action;
    public ChoiceEventArgs CEA;
    public Choice (string _option, ChoiceEventArgs _CEA = null)
    {
        option = _option;
        if (_CEA != null)
        {
            Action += _CEA.ChoiceAction;
            CEA = _CEA;
        }
    }
}
