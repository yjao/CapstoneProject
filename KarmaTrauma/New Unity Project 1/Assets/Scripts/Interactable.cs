﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Interactable
{
	public delegate void Action(object sender, GameEventArgs args);
	public enum ObjectType { NPC, OBJECT };

	public int iD;
	public string name;
	public string strangerName = "???";
	public ObjectType objectType;

	public Dialogue[] dialogues;
	public int lastDialogueDisplayed;

	public Interactable()
	{
		dialogues = new Dialogue[]{};
	}

	public Interactable(string _name, string _strangerName, Dialogue[] _dialogues)
	{
		lastDialogueDisplayed = -1;
		name = _name;
		if (_strangerName.Length > 0)
		{
			strangerName = _strangerName;
		}
		dialogues = _dialogues;
	}
}

[Serializable]
public class Dialogue
{
	public int iD;
	public string text;
	public Choice[] choices;
	public Interactable.Action Action;
    public ChoiceEventArgs CEA;
    public string setbool;
	public string setDayBool;
    
	public Dialogue(int _id, string _text, Choice[] _choices = null, ChoiceEventArgs _CEA = null, string b = null, string db = null)
    {
        iD = _id;
        text = _text;
        choices = _choices;
        setbool = b;
		setDayBool = db;
        if (_CEA != null)
        {
            CEA = _CEA;
            Action += _CEA.ChoiceAction;
        }
    }

	public bool TypeIsChoice()
	{
		return choices != null;
	}
}

[Serializable]
public class Choice
{
	public string option;
	public Interactable.Action Action;
    public ChoiceEventArgs CEA;
    public string setbool;
	public string setDayBool;
    public string checkbool;
    public string checkitem;
    public string removeitem;
    public string[] tutorialcontinue;
    public Choice (string _option, ChoiceEventArgs _CEA = null, string setb = null, string checkb = null, string removeItem = null, string[] tutorialContinue = null, string checki = null, string setdb = null)
    {
        option = _option;
        if (_CEA != null)
        {
            Action += _CEA.ChoiceAction;
            CEA = _CEA;
        }
		setbool = setb;
		setDayBool = setdb;
		checkbool = checkb;
        checkitem = checki;
        removeitem = removeItem;
        tutorialcontinue = tutorialContinue;
    }
}
