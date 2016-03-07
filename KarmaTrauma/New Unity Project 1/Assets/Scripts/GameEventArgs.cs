﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GameEventArgs
{
    public string DialogChoice { get; set; }
    public Interactable.Action ChoiceAction { get; set; }//or function reference
    public string Testing { get; set; }
    public int ChoiceCaller { get; set; }
    public GameObject ThisGameObject { get; set; }
    public Textbox DialogueBox { get; set; }

    public string String { get; set; }
    public int Integer { get; set; }
    public Vector2 Position { get; set; }
    public int ShoveX { get; set; }
    public int IDNum { get; set; }
    public int DialogueID { get; set; }
    public string[] TutorialDialogues { get; set; }
    public int TutorialDialogueCounter { get; set; }
    public string CoroutineName { get; set; }

    public CharacterAnimations.States AnimationState { get; set; }

    public void ConvertChoiceEventArgs(ChoiceEventArgs CEA)
    {
        ChoiceAction = CEA.ChoiceAction;
        Testing = CEA.Testing;
        ShoveX = CEA.ShoveX;
        IDNum = CEA.IDNum;
        DialogueID = CEA.DialogueID;
		String = CEA.String;
        TutorialDialogues = CEA.TutorialDialogues;
        TutorialDialogueCounter = CEA.TutorialDialogueCounter;
        CoroutineName = CEA.CoroutineName;
    }
}
