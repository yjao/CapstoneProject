using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour
{
	public static QuestManager Instance;

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

		// Who is the NPC?
		Interactable intr = GameManager.Instance.GetInteractableByID(args.ThisGameObject.GetComponent<InteractableObject>().ID);
		Debug.Log("QM: Hello, " + intr.Name);
		bool requiredNpc = (intr.Name == "Mr. Test");

		// What time is it?
		Debug.Log("QM: The time now is " + GameManager.Instance.GetTime());

		// Any required/trigger items?
		bool requiredItem = GameManager.Instance.HasItem("Jewel");
		Debug.Log("QM: Checking to see if you have Jewel. You do" + (requiredItem? "!" : " NOT!"));

		// Check boolean values.
		bool requiredBool = GameManager.Instance.Data.GetBool("GimmeJewel");
		Debug.Log("QM: Checking to see if he wanted Jewel. " + (requiredBool? "He does!" : "NOT yet!"));

		// Overwrite NPC's Interact()
		Debug.Log("QuestManager says: No Interact() has been modified.");
		if (requiredNpc && requiredItem && requiredBool)
		{
			// MAKE ME INTO A FUNCTION
			//or a UpdateScene()
			//GameManager.Instance.CreateMessage("You cleared the game!");
			InteractableObject mrly = GameObject.Find("MrLy").GetComponent<InteractableObject>();
			mrly.DialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID;
			mrly.DialogueIDSingle = 3;
		}
	}
}
