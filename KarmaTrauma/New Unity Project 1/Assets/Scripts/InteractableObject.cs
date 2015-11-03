using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableObject : MonoBehaviour
{
	public int ID;

	protected GameManager gameManager;
	protected Player player;
	private bool colliding = false;

	public enum TYPE
	{
		NONE, DIALOG
	};
	public TYPE InteractionType;

	public enum Dialogue_ID_Type
	{
		SINGLE_DIALOGUE_ID, DIALOGUE_MIN_MAX, MULTI_DIALOGUE_ID
	};
	public Dialogue_ID_Type DialogueIDType;
	public int DialogueIDSingle;
	public int DialogueIDMin;
	public int DialogueIDMax;
	public List<int> DialogueIDMulti;

	public void Init()
	{
		gameManager = GameManager.Instance;
		player = Player.Instance;
	}

	void Start()
	{
		Init();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        if (c.gameObject.tag == "Player")
			colliding = true;

		if (!player.CollidingWithID.Contains(ID))
			player.CollidingWithID.Add(ID);
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
			colliding = false;

		if (player.CollidingWithID.Contains(ID))
			player.CollidingWithID.Remove(ID);
	}

	private void CallDialogue()
	{
		int newIndex = DialogueIDSingle;
		switch (DialogueIDType)
		{
		case Dialogue_ID_Type.SINGLE_DIALOGUE_ID:
			newIndex = DialogueIDSingle;
			break;
		case Dialogue_ID_Type.MULTI_DIALOGUE_ID:
			int multiIndex = DialogueIDMulti.IndexOf(DialogueIDSingle);
			if ((multiIndex < 0) || (multiIndex >= DialogueIDMulti.Count))
				multiIndex = 0;
			DialogueIDSingle = DialogueIDMulti[multiIndex];
			newIndex = (multiIndex+1 >= DialogueIDMulti.Count)? DialogueIDMulti[0] : DialogueIDMulti[multiIndex+1];
			break;
		case Dialogue_ID_Type.DIALOGUE_MIN_MAX:
			if ((DialogueIDSingle <= DialogueIDMin) || (DialogueIDSingle > DialogueIDMax))
				DialogueIDSingle = DialogueIDMin;
			newIndex = DialogueIDSingle+1;
			break;
		}
		gameManager.DBox(ID, DialogueIDSingle);
		DialogueIDSingle = newIndex;
	}

	public void Interact()
	{
		switch (InteractionType)
		{
		case TYPE.DIALOG:
			if (gameManager.GameMode != GameManager.MODE.DIALOGUE)
				CallDialogue();
			break;
		}
	}

	/*public void CheckAndTurnCharacter()
	{
		if (this.transform.position.x > player.gameObject.transform.position.x)
		{
			animator.SetInteger(animationState, leftIdle);
		}
		else if (this.transform.position.x < player.gameObject.transform.position.x)
		{
			animator.SetInteger(animationState, rightIdle);
		}
	}*/

	public void CheckAndInteract()
	{
		if (colliding && Input.GetKey(KeyCode.Space))
		{
			colliding = false; //troublesome without this line...
			Debug.Log("Interacting");
			Interact();
		}
	}

    void Update()
    {
		CheckAndInteract();
    }
}
