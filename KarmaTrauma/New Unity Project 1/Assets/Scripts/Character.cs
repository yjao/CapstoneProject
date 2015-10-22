using UnityEngine;
using System.Collections;

public class Character
{
	public string Name;
	public string[] Dialogue;
	public int LastDialogueDisplayed;

	public Character(string name, string[] dialogue)
	{
		LastDialogueDisplayed = -1;
		Name = name;
		Dialogue = dialogue;
	}
}
