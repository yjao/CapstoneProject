using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ChoiceEventArgs
{
    public string DialogChoice { get; set; }
    public Interactable.Action ChoiceAction { get; set; }//or function reference
    public string Testing { get; set; }
    public int ChoiceCaller { get; set; }

    public string String { get; set; }
    public int Integer { get; set; }
    public int ShoveX { get; set; }
    public int IDNum { get; set; }
    public int DialogueID { get; set; }
}
