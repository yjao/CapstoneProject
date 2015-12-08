using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLoader
{
	private GameManager gameManager;

    public enum ChoiceAction{ITEM, MOVE, CONTINUE, DESTROY, NONE}

	// temporary
	public static DataLoader Instance;
	public void Init()
	{
		Instance = this;
	}


	#region GAME CHARACTER DATA
	
	private void LoadOldData()
	{
		string[] chelsey = new string[]
		{
			"\"Um... my name is Chelsey. I just moved in town recently. I like helping people, and.. yeah.\"",
			"\"Sure!  Let's go~ furendddd.\"",
			"\"I live on Maple Street.\"",
			"\"What just happened? That man... He looks familiar. What a strange night.\"",
			"\"...First day of school?.\"",
			"\"??\"",
			"*Didn't this happen yesterday?...*",
			"\"Um...\"",
			"*What is happening today??  Is everyone playing a trick on me?*",
			"!!!The old man!!",
            "\"...\""
		};
		AddNpc(1, "Chelsey", "", chelsey);

		gameManager.allObjects[1].dialogues[7].choices = new Choice[1] { new Choice("Maybe not today.") };

        string[] alfred = new string[]
        {
            "\"Help me...\"",
            "[You told Alfred his son loves him now]"

        };
        AddNpc(2, "???", "Alfred", alfred);

		string[] mom = new string[]
		{
			/*0*/ "\"Chelsey! Wake up! You don't want to be late on your first day of school!\"",
			/*1*/ "\"The breakfast's on the table. Bacon and eggs, your favorite!\"",
			/*2*/ "\"Come on, you're going to be late!\"",
			/*3*/ "\"Hi Chelsey, hope you had a great time at school today!\"",
			/*4*/ "\"It's late, go to bed!\""
        };

		AddNpc(21, "Mom", "Mom", mom);

		string[] mrly = new string[]
		{
			/*0*/ "\"Good morning class, we have a new student today. Chelsey, why don't you introduce yourself?\"",
			/*1*/ "\"Thank you Chelsey. Take a seat at the empty desk over there. Now, everybody, get out your textbooks and turn to page 42...\"",
			/*2*/ "\"That's all for today.  Remember to do the exercises on page 61.\"",
			/*3*/ "\"Go on, don't be shy and introduce yourself.\"",
			/*4*/  "\"Why did he do it?\"",
			/*5*/ "\"You don't even know him, anyway. It's too sad.\"",
			/*6*/ "\"Yup. Great cop, wasn't he? But that was all in the past. Ask his ex-coworkers, they all know! That poor, poor soul.\""
		};
		AddNpc(65, "Mr.Ly", "Teacher", mrly);
		gameManager.allObjects[65].dialogues[4].choices = new Choice[]
		{
			AddChoice("Say nothing"),
			AddChoice("Who is he?", ChoiceAction.CONTINUE, 65, 5)
		};
		//GameManager.Instance.AllObjects[65].Dialogue[5].CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.continueDialogue, IDNum = 65, DialogueID = 4};
		/*GameManager.Instance.AllObjects[65].Dialogue[6].choices = new Choice[]
		{
			addChoice("Say nothing"),
			addChoice("What happened?", ChoiceAction.CONTINUE, 65, 7)
		};*/
		gameManager.allObjects[65].dialogues[6].choices = new Choice[]
		{
			AddChoice("Okay.", boolname:"Quest2")
		};
		//GameManager.Instance.AllObjects[65].Dialogue[6].setbool = "Quest2";

		string[] kelly = new string[]
		{
			"\"Hey, new kid! Your name is Chelsey right? I'm Kelly.\"",
			"\"Want me to show you around?\"",
			"\"This is the mall.  I got my belly piercing here.\"",
			"\"Whoa, it's getting late. Where do you live?\"",
			"\"Just head down from Main Street and you should be able to reach Maple Street. I'll see you tomorrow!\"",
            "\"Ok then.  See ya!\""
        };
        AddNpc(66, "Kelly", "", kelly);

        string[] frost = new string[]
		{
            "\"Welcome to Frost's Pizza.  Today's special is Hwaiian Combo. It's only $2 a piece!\"",
            "\"Here's your pizza.\""
        };
        AddNpc(67, "Frost", "", frost);

		string[] parkdude = new string[]
		{
			"\"Hey kid, have you seen a brown dog around here somewhere?\"",
			"\"I've lost him a little while ago, and I am worried sick about him.\"",
			"\"Thank you!\""
        };
        AddNpc(71, "Park Dude", "", parkdude);

        string[] stylish_guy = new string[]
		{
			"\"He just suddenly jumped off the building!\"",
			"\"How am I supposed to know?\""
        };
        AddNpc(72, "Stylish Guy", "", stylish_guy);

        string[] jeney = new string[]
		{
			/*0*/ "[Jeney tells you she saw the crying person at the mall around 4PM]",
			/*1*/ "\"Welcome to Jeney's Donut Shop!\"",
			/*2*/ "\"   \""
        };
        AddNpc(73, "Random Woman", "Jeney", jeney);

        string[] alex = new string[]
		{
			/*0*/ "[A crying person tells you that's his father]",
			/*1*/ "[Dialogue. This person does not trust you]",
			/*2*/ "[He refuses to believe that Alfred is going to jump off a building today]",
			/*3*/ "[You showed him the jewel and now he trusts you]"
        };
        AddNpc(74, "???", "", alex);

        /*
		GameManager.Instance.AllObjects[73].Dialogue[0].choices = new Choice[]
		{
			addChoice("Say nothing"),
			addChoice("Who is he?", ChoiceAction.CONTINUE, 73, 1)
		};
		GameManager.Instance.AllObjects[73].Dialogue[1].setbool = "JeneyHungry";
        */
		string[] baconandeggs = new string[]
		{
			"A delicious floating egg on a magical bacon."
		};

        string[] monologue = new string[]
        {
            "\"Ooh, money on the floor!\"",
            "\"This is where I found $2\""
        };
        AddNpc(111, "Chelsey", "Chelsey", monologue);

        AddNpc(171, "Bacon and Eggs", "Food???", baconandeggs);
		gameManager.allObjects[71].dialogues[0].choices = new Choice[2]
		{
			new Choice("I guess I'll eat it"),
			new Choice("Nah...")
		};

        string[] jewel = new string[]
        {
            "hi",
            "continue",
            "more"
        };
        AddNpc(150, "Jewel", "Jewel", jewel);
		gameManager.allObjects[150].dialogues[0].choices = new Choice[]
		{
            AddChoice("Talk to the jewel", ChoiceAction.CONTINUE, 150, 1),
            AddChoice("Take the jewel", ChoiceAction.ITEM, 150),
            AddChoice("Destroy the jewel", ChoiceAction.DESTROY),
            AddChoice("Do nothing")
		};

        string[] jewel2 = new string[]
        {
            "Bool is false"
        };
        AddNpc(151, "Jewel", "Jewel", jewel2);

        string[] jewel3 = new string[]
        {
            "Bool is true"
        };
        AddNpc(152, "Jewel", "Jewel", jewel3);

        //string[] alfred = new string[]
        //{
        //    "A man who jumped off of the building."
        //};
        //AddNpc(31, "Alfred", "", alfred);
        //GameManager.Instance.AllObjects[31].Dialogue[0].choices = new Choice[2]
        //{
        //    new Choice("Should I talk to him?"),
        //    new Choice("Nah...")
        //};

		string[] jewel4 =
		{
			"Are you worthy enough to take the Debugger's Jewel?"
		};
		AddNpc(153, "Jewel", "", jewel4);
		gameManager.allObjects[153].dialogues[0].choices = new Choice[2]
		{
			new Choice("You bet!", new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = 150 }),
			new Choice("Nah, Idk Ruby.")
		};
	}

    private void LoadFallDemoData()
    {
        // ================ JENEY ================ //
        string[] jeney = new string[]
		{
			/*0*/ "\"Oh... Poor Alfred. This must have a toll on Alex.\"",
            /*1*/ "\"Alex was just at my donut shop at 4PM today to talk about his father and now he's gone...\"",
        };
        AddNpc(73, "Random Woman", "Jeney", jeney);
        gameManager.allObjects[73].dialogues[0].setbool = "AlfredName_Learned";



        // ================ MANNY ================ //
        string[] manny = new string[]
		{
			/*0*/ "\"Hello. Who are you visiting today?\"",
			/*1*/ "\"What is your name?\"",
			/*2*/ "\"Oh, Alex! Your father talked about how much he misses you. Right now, he is busy, but you are permitted to wait for him here. ...I could've sworn he had a son.\"",
            /*3*/ "\"Sorry, you are not on the visitors list. Please leave.\"",
            /*4*/ "\"Please leave if you're not here to see someone\"",
            /*5*/ "\"How did I let him out of my sight? I'm going to be fired...\""
        };
        AddNpc(74, "Guard", "Manny", manny);
        gameManager.allObjects[74].dialogues[0].choices = new Choice[]
		{
			AddChoice("Alfred", ChoiceAction.CONTINUE, 74, 1),
			AddChoice("Uh...", ChoiceAction.CONTINUE, 74, 4)
		};
        gameManager.allObjects[74].dialogues[1].choices = new Choice[]
		{
			AddChoice("Alex", ChoiceAction.CONTINUE, 74, 2),
            AddChoice("Chelsey", ChoiceAction.CONTINUE, 74, 3)
		};



        // ================ ALEX ================ //
        string[] alex = new string[]
		{
			/*0*/ "\"What do you want?\"",
            /*1*/ "\"I'm Alex. Welcome to our mundane little town.\"",
            /*2*/ "\"Yes, he is. How do you even know this?\"",
            /*3*/ "\"Please, he never cared about me so why should I care about him? If you want me to come with you, prove to me that he still cares and I'll consider going with you to visit him.\"",
            /*4*/ "\"He... He still has this? I guess the old man really does care. I'll go with you.\"",
            /*5*/ "\"Stop wasting my time.\"",
            /*6*/ "\"Weirdo...\""
        };
        AddNpc(75, "Kid", "Alex", alex);
        gameManager.allObjects[75].dialogues[0].choices = new Choice[]
		{
			AddChoice("Tell him you're new in town and want to make friends", ChoiceAction.CONTINUE, 75, 1),
			AddChoice("Say nothing", ChoiceAction.CONTINUE, 75, 6)
		};
        gameManager.allObjects[75].dialogues[1].choices = new Choice[]
		{
			AddChoice("Ask him if his father is the cop in the Mental Hospital", ChoiceAction.CONTINUE, 75, 2)
		};
        gameManager.allObjects[75].dialogues[2].choices = new Choice[]
		{
			AddChoice("Tell him that he needs to come with you and visit his father", ChoiceAction.CONTINUE, 75, 3)
		};
        gameManager.allObjects[75].dialogues[3].choices = new Choice[]
		{
			AddChoice("Show him the Jewel", ChoiceAction.CONTINUE, 75, 4),
            AddChoice("Do nothing", ChoiceAction.CONTINUE, 75, 5),
		};


		// ================ ITEMS & OBJECTS ================ //

        string[] jewel = new string[]
		{
			/*0*/ "\"Hello.\""
			/*1*/ 
			/*2*/ 
            /*3*/ 
        };
        AddNpc(2, "Jewel", "Jewel", jewel);

        //GameManager.Instance.AllObjects[74].Dialogue[1].setbool = "JeneyHungry";

		string[] bed = new string[]
		{
			/*0*/ "\"Sleep and end the day?\""
		};
		AddNpc(100, "Bed", "Bed", bed);
		gameManager.allObjects[100].dialogues[0].choices = new Choice[]
		{
			new Choice("Good Night!", new ChoiceEventArgs() { ChoiceAction = GameManager.UseBed }),
			AddChoice("I ain't weak!")
		};
    }

	private void LoadTestData()
	{
		string[] test = new string[] 
		{
			"Oh um, hey, I didn't see you there. ...Go back to class!",
			"Shh kid, can you find me one of those debugger jewels?",
			"Did you bring the Jewel?",
			"O.M.G.!!! You actually brought it. Gimme!",
			"Well whatever, you don't get to clear the game.",
			"Thanks kid."
		};
		AddNpc(999, "Mr. Test", "", test);
		gameManager.allObjects[999].dialogues[1].setbool = "GimmeJewel";
		gameManager.allObjects[999].dialogues[3].choices = new Choice[]
		{
			new Choice("Never!", new ChoiceEventArgs() { ChoiceAction =  Textbox.continueDialogue, IDNum = 999, DialogueID = 4}),
			//new Choice("I guess...")
            AddChoice("I guess...", boolname:"Quest1")
		};

		string[] test2 = new string[] 
		{
			""
		};
		AddNpc(998, "Mom", "", test2);
	}

	private void LoadMiniTestData()
	{
		string[] test = new string[] 
		{
			"I wanted to tell you I absolutely LOVE #PB#. Bring it to me at #5AM#!!"
		};
		AddNpc(1000, "Mr. Test", "", test);
	}

	#endregion

	private void LoadQuestTerms()
	{
		gameManager.questTerms = new Dictionary<string, string>()
		{
			{ "PB" , "peanut butter" },
			{ "NM" , "name" },
			{ "" , "" }
		};
	}

	private void LoadSceneData()
	{
		string sceneName = "";

		#region EMPTY TEMPLATE
		// EMPTY TEMPLATE
		/*
		InteractableObject.Parameters emptyTemplate = new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() {  },
			
			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
			dialogueIDSingle = 0,
			dialogueIDMin = 0,  dialogueIDMax = 0,
			dialogueIDMulti = new List<int>() {  },
			
			// NPC CharacterAnimations
			startingAnimationState = CharacterAnimations.States.DOWN_IDLE,
			animationSpeed = 0f,
			wanderDistanceX = 0f,  wanderDirectionX = 0,
			wanderDistanceY = 0f,  wanderDirectionY = 0,
			
			// Getter/Setter variables, NpcID is required
			Summary = "",
			NpcID = -1
		};
		AddParameters(sceneName, emptyTemplate);
		*/
		#endregion
        
        sceneName = SceneManager.SCENE_HOUSE;
        
        // ================ MOM ================ //
		InteractableObject.Parameters mom = new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 6 },

			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
			dialogueIDSingle = 2,

			// NPC CharacterAnimations
			startingAnimationState = CharacterAnimations.States.DOWN_STRETCH,
			animationSpeed = 0.2f,

			// Getter/Setter variables, NpcID is required
			Summary = "mom says wakeup in the morning",
			NpcID = 21
		};
		AddParameters(sceneName, mom);

		InteractableObject.Parameters mom2 = new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 16, 18, 20 },
			
			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
			dialogueIDSingle = 3,
			
			// NPC CharacterAnimations
			startingAnimationState = CharacterAnimations.States.DOWN_IDLE,
			animationSpeed = 0.05f,
			wanderDistanceX = 30f,  wanderDirectionX = 1,

			// Getter/Setter variables, NpcID is required
			Summary = "mom walks around after school",
			NpcID = 21
		};
		AddParameters(sceneName, mom2);

		InteractableObject.Parameters mom3 = new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 22 },
			
			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
			dialogueIDSingle = 3,
			dialogueIDMin = 3,  dialogueIDMax = 4,

			// NPC CharacterAnimations
			startingAnimationState = CharacterAnimations.States.DOWN_IDLE,
			
			// Getter/Setter variables, NpcID is required
			Summary = "mom stands and says go sleep",
			NpcID = 21
		};
		AddParameters(sceneName, mom3);


        sceneName = SceneManager.SCENE_HOSPITAL;
        
        // ================ MANNY ================ //
        InteractableObject.Parameters manny = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 6, 8, 10, 12, 14, 16, 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "guard talks to you",
            NpcID = 74
        };
        AddParameters(sceneName, manny);


        sceneName = SceneManager.SCENE_MAINSTREET;

        // ================ JENEY ================ //
        InteractableObject.Parameters jeney2 = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "jeney tells you about alfred and his son",
            NpcID = 73
        };
        AddParameters(sceneName, jeney2);


	}

	public DataLoader()
	{
		gameManager = GameManager.instance;

		Init(); //delete me one day

		Debug.Log("Loading Game playerData");
		
		LoadOldData();
		LoadMiniTestData();
		LoadFallDemoData();

		LoadSceneData();
		LoadQuestTerms();


		Debug.Log("Loading Complete");
		gameManager.SaveGameData();
	}

    private ChoiceEventArgs ChoiceInteractItem(int id)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = id };
        return CEA;
    }

    private ChoiceEventArgs ChoiceContinueDialog(int id, int dialogueID)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.continueDialogue, IDNum = id, DialogueID = dialogueID };
        return CEA;
    }

	// temporarily making it public for hardcoding purposes
    public Choice AddChoice(string text, ChoiceAction CA = ChoiceAction.NONE, int id = -1, int subID = -1, string boolname = null)
    {
        ChoiceEventArgs CEA;
        if (CA == ChoiceAction.ITEM)
        {
            CEA = ChoiceInteractItem(id);
        }
        else if (CA == ChoiceAction.CONTINUE)
        {
            CEA = ChoiceContinueDialog(id, subID);
        }
        else if (CA == ChoiceAction.DESTROY)
        {
            CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractDestroy };
        }
        else
        {
            CEA = new ChoiceEventArgs();
        }
        return new Choice(text, CEA, boolname);
    }

	private void AddNpc(int ID, string name, string strangerName, string[] strings)
	{
		Dialogue[] dialogues = new Dialogue[strings.GetLength(0)];
		for (int i = 0; i < strings.GetLength(0); i++)
		{
			Choice[] choices = null;
			dialogues[i] = new Dialogue(i, strings[i], choices);
		}

		gameManager.allObjects[ID] = new Interactable(name, strangerName, dialogues);
	}

	private void AddParameters(string sceneName, InteractableObject.Parameters parameters)
	{
		List<InteractableObject.Parameters> paramList = null;
		if (gameManager.sceneParameters.ContainsKey(sceneName))
		{
			paramList = gameManager.sceneParameters[sceneName];
		}

		if (paramList == null)
		{
			gameManager.sceneParameters[sceneName] = new List<InteractableObject.Parameters>();
		}

		gameManager.sceneParameters[sceneName].Add(parameters);
	}

}
