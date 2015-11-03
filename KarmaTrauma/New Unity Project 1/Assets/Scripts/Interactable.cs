using UnityEngine;
using System.Collections;

public class Interactable
{
	public string Name;
	public enum OBJECT_TYPE { NPC, OBJECT };
	public OBJECT_TYPE Type;
	public int ID;
	public string[] Dialogue;
	public int LastDialogueDisplayed;

	public Interactable(string name, string[] dialogue)
	{
		LastDialogueDisplayed = -1;
		Name = name;
		Dialogue = dialogue;
	}
}
