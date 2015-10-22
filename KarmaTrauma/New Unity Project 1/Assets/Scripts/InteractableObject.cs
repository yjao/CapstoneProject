using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
	public int ID;
	protected GameManager gameManager;
    private bool colliding = false;

	public enum TYPE
	{
		NONE, DIALOG
	};
	public TYPE InteractionType;

	void Start()
	{
		gameManager = GameManager.Instance;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        if (c.gameObject.tag == "Player")
		{
			colliding = true;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			colliding = false;
		}
	}

	public void Interact()
	{
		switch (InteractionType)
		{
		case TYPE.DIALOG:
			if (gameManager.GameMode != GameManager.MODE.DIALOGUE)
				gameManager.DBox(ID, true);
			break;
		}
	}

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
