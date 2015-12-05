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
        NONE, DIALOG, ITEM, MOVE
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
    public Parameters[] Parameter;

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

    void OnDestroy()
    {
        //EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
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
                newIndex = (multiIndex + 1 >= DialogueIDMulti.Count) ? DialogueIDMulti[0] : DialogueIDMulti[multiIndex + 1];
                break;
            case Dialogue_ID_Type.DIALOGUE_MIN_MAX:
                if ((DialogueIDSingle <= DialogueIDMin) || (DialogueIDSingle > DialogueIDMax))
                    DialogueIDSingle = DialogueIDMin;
                newIndex = DialogueIDSingle + 1;
                break;
        }

		if (gameManager.AllObjects[ID].Dialogue[DialogueIDSingle].TypeIsChoice())
		{
			EventManager.OnDialogChoiceMade += HandleOnDialogChoiceMade;
		}
		gameManager.DBox(ID, DialogueIDSingle);
        DialogueIDSingle = newIndex;
    }

    public void Interact()
    {
        EventManager.NotifyNPC(this, new GameEventArgs() { ThisGameObject = gameObject });
        //EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
        
        switch (InteractionType)
        {
            case TYPE.DIALOG:
                if (gameManager.GameMode != GameManager.MODE.DIALOGUE)
				{

                    CallDialogue();
                    // Call QuestList and check if the quest requirements are met with this interactable object. Change dialogue if necessasry
                    GameObject QL = GameObject.Find("QuestList");
                    QuestList ql = QL.GetComponent<QuestList>();
                    ql.CheckQuest(this);
				}
                break;
        }
    }

    public static void InteractItem(object sender, GameEventArgs args)
    {
		if (args.Testing != null) {Debug.Log(args.Testing);}
        EventManager.NotifyItemTaken(sender, args);
        GameObject.Destroy(args.ThisGameObject);
    }

    public static void InteractMove(object sender, GameEventArgs args)
    {
        args.ThisGameObject.transform.Translate(args.ShoveX, 0, 0);
    }

    public static void InteractDestroy(object sender, GameEventArgs args)
    {
        GameObject.Destroy(args.ThisGameObject);
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
			{
				GameEventArgs args = new GameEventArgs() { IDNum = ID, ThisGameObject = gameObject };
				InteractItem(this, args);
			}
			//else if (InteractionType == TYPE.MOVE)
                //InteractMove(1, 0);
        }
    }

    void HandleOnDialogChoiceMade(object sender, GameEventArgs args)
    {
		EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
        args.ThisGameObject = gameObject;
        if (args.ChoiceAction != null)
        {
            args.ChoiceAction(this, args);
        }
        //EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
    }

    void Update()
    {
        CheckAndInteract();
    }

    [System.Serializable]
    public class Parameters
    {
        public List<int> Time;
        public InteractableObject.Dialogue_ID_Type DialogueIDType;
        public int DialogueIDSingle;
        public int DialogueIDMin;
        public int DialogueIDMax;
        public List<int> DialogueIDMulti;
        public CharacterAnimations.States StartingAnimationState;
        public float wanderDistanceX;
        public int wanderDirectionX;
        public float wanderDistanceY;
        public int wanderDirectionY;
    }
}
