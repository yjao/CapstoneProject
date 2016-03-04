using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableObject : MonoBehaviour
{
	#region ENUMS

	public enum InteractionType
	{
		NONE, DIALOG, ITEM, MOVE
	};

	public enum Dialogue_ID_Type
	{
		SINGLE_DIALOGUE_ID, DIALOGUE_MIN_MAX, MULTI_DIALOGUE_ID
	};

	#endregion
	
	public int iD;

	public NPC npc;
	public CharacterAnimations characterAnimations;

    protected GameManager gameManager;
    protected Player player;
    private bool colliding = false;

    public InteractionType interactionType;

	[Header("Choose one type of Dialogue")]
    public Dialogue_ID_Type dialogueIDType;
    public int dialogueIDSingle;
    public int dialogueIDMin;
    public int dialogueIDMax;
    public List<int> dialogueIDMulti;

	[Header("How many sets/distinct time blocks?")]
    public Parameters[] parameter;
    [Header("Boolean to check if object is disabled")]
    public string disableBool;
    [Header("Is this NPC also an item?")]
    public bool isItem;

    public void Init()
    {
        gameManager = GameManager.instance;
        player = Player.Instance;
    }

    void Start()
    {
        Init();
        if (interactionType == InteractionType.ITEM || isItem)
        {
            disableBool = gameManager.allItems[iD].Name;
        }
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
    }

    private void CallDialogue()
    {
        int newIndex = dialogueIDSingle;
        switch (dialogueIDType)
        {
            case Dialogue_ID_Type.SINGLE_DIALOGUE_ID:
                newIndex = dialogueIDSingle;
                break;
            case Dialogue_ID_Type.MULTI_DIALOGUE_ID:
                int multiIndex = dialogueIDMulti.IndexOf(dialogueIDSingle);
                if ((multiIndex < 0) || (multiIndex >= dialogueIDMulti.Count))
                    multiIndex = 0;
                dialogueIDSingle = dialogueIDMulti[multiIndex];
                newIndex = (multiIndex + 1 >= dialogueIDMulti.Count) ? dialogueIDMulti[0] : dialogueIDMulti[multiIndex + 1];
                break;
            case Dialogue_ID_Type.DIALOGUE_MIN_MAX:
                if ((dialogueIDSingle <= dialogueIDMin) || (dialogueIDSingle > dialogueIDMax))
                    dialogueIDSingle = dialogueIDMin;
                newIndex = dialogueIDSingle + 1;
                break;
        }
		if (gameManager.allObjects[iD].dialogues[dialogueIDSingle].TypeIsChoice() || gameManager.allObjects[iD].dialogues[dialogueIDSingle].Action != null)
		{
			EventManager.OnDialogChoiceMade += HandleOnDialogChoiceMade;
		}
		gameManager.DBox(iD, dialogueIDSingle);
        dialogueIDSingle = newIndex;
    }

    public void Interact()
    {
        npc.TurnNpcAndPlayer();
        
        switch (interactionType)
        {       
        case InteractionType.DIALOG:
            if (gameManager.gameMode != GameManager.GameMode.DIALOGUE)
			{

                // Call QuestList and check if the quest requirements are met with this interactable object. Change dialogue if necessasry
               
                QuestList ql = GameManager.instance.GetComponent<QuestList>();
                ql.CheckQuest(this);
                CallDialogue();
			}
            break;
		default:
			Debug.Log("Interaction type is: " + interactionType);
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

    public void CheckAndInteract()
    {
        if (colliding && Input.GetKeyDown(KeyCode.Space) && gameManager.gameMode == GameManager.GameMode.PLAYING)
        {
			// Cooldown procedure
			//if (!gameManager.CheckKeyCooldown()) { return; } else { gameManager.SetKeyCooldown(); }

            // colliding = false; //troublesome without this line...
            if (interactionType == InteractionType.DIALOG)
                Interact();
            else if (interactionType == InteractionType.ITEM)
			{
				GameEventArgs args = new GameEventArgs() { IDNum = iD, ThisGameObject = gameObject };
				InteractItem(this, args);
			}
			//else if (InteractionType == TYPE.MOVE)
                //InteractMove(1, 0);
        }
    }

    public void HandleOnDialogChoiceMade(object sender, GameEventArgs args)
    {
		EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
        args.ThisGameObject = gameObject;
        if (args.ChoiceAction != null)
        {
            args.ChoiceAction(this, args);
        }
    }

    public static void HandleTutorial(object sender, GameEventArgs args)
    {
        EventManager.OnDialogChoiceMade -= HandleTutorial;
        if (args.ChoiceAction != null)
        {
            args.ChoiceAction(args.DialogueBox, args);
        }
    }

    void Update()
    {
        CheckAndInteract();
    }

    [System.Serializable]
    public class Parameters
    {
		[Header("Specify the time frames that this set takes effect")]
        public List<int> timeBlocks;

		[Header("InteractableObject dialogue information")]
        public InteractableObject.Dialogue_ID_Type dialogueIDType;
        public int dialogueIDSingle;
        public int dialogueIDMin;
        public int dialogueIDMax;
        public List<int> dialogueIDMulti;

		[Header("NPC CharacterAnimations")]
        public CharacterAnimations.States startingAnimationState;
        public bool turnOnInteract = true;
		public float animationSpeed;
        public float wanderDistanceX;
        public int wanderDirectionX;
        public float wanderDistanceY;
        public int wanderDirectionY;

		private int m_npcID = -1;
		public int NpcID
		{
			set
			{
				m_npcID = value;
			}
			get
			{
				return m_npcID;
			}
		}

		private string m_summary = "";
		public string Summary
		{
			set
			{
				m_summary = value;
			}
			get
			{
				return m_summary;
			}
		}
    }
}
