using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour
{
	public static QuestManager Instance;
	private GameManager gameManager;
    
	//temp public
	public ArrayList quest_log;

	void Start()
	{
        quest_log = new ArrayList();
		// Singleton/Persistent
		if ((Instance != null) && (Instance != this))
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(this);

		EventManager.OnNPC += HandleNPC;
		gameManager = GameManager.instance;
	}

	void OnDestroy()
	{
		EventManager.OnNPC -= HandleNPC;
	}

	public void HandleNPC(object sender, GameEventArgs args)
	{
	}

	/*public void Old_HandleNPC(object sender, GameEventArgs args)
	{
		if (args.ThisGameObject == null)
		{
			return;
		}

		// What time is it?
		Debug.Log("QM: The time now is " + GameManager.instance.GetTime());

		// Who is the NPC?
		Interactable intr = GameManager.instance.GetInteractableByID(args.ThisGameObject.GetComponent<InteractableObject>().iD);
		Debug.Log("QM: Hello, " + intr.name);
		bool requiredNpc = false;

		// Any required/trigger items?
		bool requiredItem = false;
		// Check boolean values.
		bool requiredBools = false;
		// Overwrite NPC's Interact()

		switch (intr.name)
		{
		case "Mr. Test":
			if (gameManager.dayData.GetQuest("Quest1"))
			{
				Change("MrLy", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 5);
				break;
			}
			requiredNpc = (intr.name == "Mr. Test");
			requiredItem = gameManager.HasItem("Jewel");
			//Debug.Log("QM: Checking to see if you have Jewel. You do" + (requiredItem? "!" : " NOT!"));
			requiredBools = gameManager.dayData.GetBool("GimmeJewel");
			Debug.Log("QM: Checking to see if he wanted Jewel. " + (requiredBools? "He does!" : "NOT yet!"));
			//Debug.Log("QuestManager says: No Interact() has been modified.");
			if (requiredNpc && requiredItem && requiredBools)
			{
				Change("MrLy", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 3);
			}
			break;
		case "Random Woman":
			if (gameManager.playerData.GetBool("AlfredName_Learned"))
			{
				GameManager.instance.allObjects[73].dialogues[0].choices = null;
				Change("Jeney", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 0);
			}
			requiredItem = gameManager.HasItem("Bacon and Eggs");
			requiredBools = gameManager.dayData.GetBool("JeneyHungry");
			if (requiredItem && requiredBools)
			{
				Change("Jeney", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 2);
				gameManager.playerData.SetBool("AlfredName_Learned");
			}
			break;
		case "Mr.Ly":
			if (gameManager.playerData.GetBool("AlfredName_Learned"))
			{
				GameManager.instance.allObjects[65].dialogues[4].choices = new Choice[]
				{
					DataLoader.Instance.AddChoice("Say nothing"),
					DataLoader.Instance.AddChoice("Poor Alfred...", DataLoader.ChoiceAction.CONTINUE, 65, 6)
				};
			}
			else
			{
				Change("MrLy", InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID, 4);
			}
			break;
		}
	}*/

	// temp method. really hardcoded
	private void Change(string objName, InteractableObject.Dialogue_ID_Type dialogueIDType, int dialogueID)
	{
		//InteractableObject npc = GameObject.Find(objName).GetComponent<InteractableObject>();
		InteractableObject npc = GameObject.Find(objName).transform.GetChild(0).GetComponent<InteractableObject>();
		npc.dialogueIDType = dialogueIDType;
		npc.dialogueIDSingle = dialogueID;
	}

	public void AddQuestToLog(int index)
	{
		if (!quest_log.Contains(index))
		{
			quest_log.Add(index);
		}
	}

	public void RemoveQuestFromLog(int index)
	{
		if (quest_log.Contains(index))
		{
			quest_log.Remove(index);
		}
	}

    public ArrayList Quest_log()
    {
        return quest_log;
    }

}
