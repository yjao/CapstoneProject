using UnityEngine;
using System.Collections;

public class DataLoader
{
	public DataLoader()
	{
		Debug.Log("Loading Game Data");
		
		Interactable intr = new Interactable();
		intr.ID = 65;
		intr.Name = "Mr. Ly";
		intr.StrangerName = "???";
		intr.objectType = Interactable.ObjectType.NPC;
		string[] testDialog = new string[] {"Oh. Um, I didn't see you there. You should be in class, not here playing with jewels.",
			"I'm telling you, go back to class!"};
		intr.Dialogue = new Dialogue[10];
		for (int i = 0; i < testDialog.GetLength(0); i++)
		{
			Dialogue dialogue = new Dialogue(i, testDialog[i]);
			//dialogue.text = testDialog[i];
			
			intr.Dialogue[i] = dialogue;
		}
		GameManager.Instance.AllObjects[intr.ID] = intr;
		
		Debug.Log("Loading Complete");
		GameManager.Instance.SaveGameData();
	}
}
