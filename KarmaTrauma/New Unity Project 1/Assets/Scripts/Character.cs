using UnityEngine;
using System.Collections;

public class Character
{
	public string Name;
	public string[] Dialogue;

	public Character(string name, string[] dialogue)
	{
		Name = name;
		Dialogue = dialogue;
	}
}
