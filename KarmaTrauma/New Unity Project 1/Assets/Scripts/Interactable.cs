using UnityEngine;
using System.Collections;

public class Interactable
{
	public string Name;
	public string StrangerName = "???";
	public enum OBJECT_TYPE { NPC, OBJECT };
	public OBJECT_TYPE Type;
	public int ID;
	public string[] Dialogue;
	public int LastDialogueDisplayed;

	public Interactable(string name, string strangerName, string[] dialogue)
	{
		LastDialogueDisplayed = -1;
		Name = name;
		if (strangerName.Length > 0)
			StrangerName = strangerName;
		Dialogue = dialogue;
	}
}
