using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableObject : MonoBehaviour
{
    public int ID;
    public GameObject Npc;

    protected GameManager gameManager;
    protected Player player;
    private bool colliding = false;

    public enum TYPE
    {
        NONE, DIALOG, ITEM
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
    public int[] BranchID;
    public string[] BranchBool;

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

        /*if (!player.CollidingWithID.Contains(ID))
            player.CollidingWithID.Add(ID);*/
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            colliding = false;

        /*if (player.CollidingWithID.Contains(ID))
            player.CollidingWithID.Remove(ID);*/
    }

    private void CallDialogue()
    {
        for (int branches = 0; branches < BranchID.Length; branches++)
        {
            if ((gameManager.GetData(BranchBool[branches]) == true) || (gameManager.HasItem(BranchBool[branches])))
            {
                ID = BranchID[branches];
            }
        }
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
                newIndex = (multiIndex + 1 >= DialogueIDMulti.Count) ? DialogueIDMulti[0] : DialogueIDMulti[multiIndex + 1];
                break;
            case Dialogue_ID_Type.DIALOGUE_MIN_MAX:
                if ((DialogueIDSingle <= DialogueIDMin) || (DialogueIDSingle > DialogueIDMax))
                    DialogueIDSingle = DialogueIDMin;
                newIndex = DialogueIDSingle + 1;
                break;
        }
        gameManager.DBox(ID, DialogueIDSingle);
        DialogueIDSingle = newIndex;
    }

    public void Interact()
    {
        EventManager.NotifyNPC(this, new GameEventArgs() { ThisGameObject = gameObject });

        switch (InteractionType)
        {
            case TYPE.DIALOG:
                if (gameManager.GameMode != GameManager.MODE.DIALOGUE)
                    CallDialogue();
                break;
        }
    }

    public void InteractItem()
    {
        EventManager.NotifyItemTaken(this, new GameEventArgs() { IDNum = ID });
        GameObject.Destroy(gameObject);
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
        if (colliding && Input.GetKeyDown(KeyCode.E))
        {
            // colliding = false; //troublesome without this line...
            if (InteractionType == TYPE.DIALOG)
                Interact();
            else if (InteractionType == TYPE.ITEM)
                InteractItem();
        }
    }

    void Update()
    {
        CheckAndInteract();
    }
}
