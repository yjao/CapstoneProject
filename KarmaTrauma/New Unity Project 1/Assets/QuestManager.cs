using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour
{
	public static QuestManager Instance;
	private GameManager gameManager;

	void Start()
	{
		// Singleton/Persistent
		if ((Instance != null) && (Instance != this))
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(this);

		EventManager.OnNPC += HandleNPC;
		gameManager = GameManager.Instance;
	}

	void OnDestroy()
	{
		EventManager.OnNPC -= HandleNPC;
	}

	public void HandleNPC(object sender, GameEventArgs args)
	{
		if (args.ThisGameObject == null)
		{
			return;
		}

		// What time is it?
		Debug.Log("QM: The time now is " + GameManager.Instance.GetTime());

		// Who is the NPC?
		Interactable intr = GameManager.Instance.GetInteractableByID(args.ThisGameObject.GetComponent<InteractableObject>().ID);
		Debug.Log("QM: Hello, " + intr.Name);
		bool requiredNpc;

		// Any required/trigger items?
		bool requiredItem;
		// Check boolean values.
		bool requiredBools;
		// Overwrite NPC's Interact()


		switch (intr.Name)
		{
		case "Mr. Test":
			requiredNpc = (intr.Name == "Mr. Test");
			requiredItem = gameManager.HasItem("Jewel");
			Debug.Log("QM: Checking to see if you have Jewel. You do" + (requiredItem? "!" : " NOT!"));
			requiredBools = gameManager.dayData.GetBool("GimmeJewel");
			Debug.Log("QM: Checking to see if he wanted Jewel. " + (requiredBools? "He does!" : "NOT yet!"));
			Debug.Log("QuestManager says: No Interact() has been modified.");
			if (requiredNpc && requiredItem && requiredBools)
			{
				//GameManager.Instance.CreateMessage("You cleared the game!");
				Change("MrLy", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 3);
			}
			break;
		case "Mom":
			requiredNpc = (intr.Name == "Mom");
			break;
		}
	}

	private void Change(string objName, InteractableObject.Dialogue_ID_Type dialogueIDType, int dialogueID)
	{
		InteractableObject npc = GameObject.Find(objName).GetComponent<InteractableObject>();
		npc.DialogueIDType = dialogueIDType;
		npc.DialogueIDSingle = dialogueID;
	}
}
