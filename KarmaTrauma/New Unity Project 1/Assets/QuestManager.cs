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
		Debug.Log("QuestManager says: Hello, " + intr.Name);

		// What time is it?
		Debug.Log("QuestManager says: The time now is " + GameManager.Instance.GetTime());

		// Any required/trigger items?
		Debug.Log("QuestManager says: Faye told me to not check your bags yet.");
		bool requiredItem = GameManager.Instance.HasItem("Jewel");

		// Check boolean values.
		Debug.Log("QuestManager says: No boolean values to report.");

		// Overwrite NPC's Interact()
		Debug.Log("QuestManager says: No Interact() has been modified.");
		if ((intr.Name == "Frost") && (requiredItem))
		{
			GameManager.Instance.CreateMessage("You cleared the game!");
		}
	}
}
