﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_WinterPlayTest : DataLoader
{
    public Data_WinterPlayTest()
    {
        LoadNpcData();
        LoadQuestTerms();
        LoadQuestData();
        LoadSceneData();
        LoadOutcomeData();
    }

    private void LoadNpcData()
    {
        // ================ Chelsey ================ //
        string[] chelsey = new string[]
		{
			/*0*/ "\"Hey, I'm Chelsey. Do you also go to Pinewood High School?.\"",
			/*1*/ "\"That old guy in the hospital, his name is Alfred I think, he's your dad right?\"",
			/*2*/ "\"Come with me! Your dad is going to die soon!  He might change his mind if you visit him!\"",
			/*3*/ "\"Come with me! Your dad is going to die soon!\"",
		};
        AddNpc(72, "", "Chelsey", chelsey);



        // ================ JENEY ================ //
        string[] jeney = new string[]
		{
			/*0*/ "\"Oh... Poor Alfred. This must have a toll on Alex.\"",
			/*1*/ "\"Alex was just at my #donut shop at 4PM# today to talk about his father and now he's gone...\"",
			/*2*/ "\"Hi! Welcome to my humble donut shop. Unfortunately, we're out of stock. Please visit tomorrow. Sorry!\"",
		};
        AddNpc(73, "Random Woman", "Jeney", jeney);
        AddBooleanToDialogue(73, 0, "AlfredName_Learned");
        AddToDialogue(73, 0, ChoiceContinueDialog(73, 1));

        // ================ MANNY ================ //
        string[] manny = new string[]
		{
			/*0*/ "\"Hello. Who are you visiting today?\"",
			/*1*/ "\"What is your name?\"",
			/*2*/ "\"Oh, Alex! Your father talked about how much he misses you. Right now, he is busy, but you are permitted to wait for him inside.\"", 
			/*3*/ "\"...I could've sworn he had a son.\"",
			/*4*/ "\"Sorry, you are not on the visitors list. Please leave.\"",
			/*5*/ "\"Please leave if you're not here to see someone\""
		};
        AddNpc(74, "Guard", "Manny", manny);
        gameManager.allObjects[74].dialogues[0].choices = new Choice[]
		{
			AddChoice("Alfred", ChoiceAction.CONTINUE, 74, 1),
			AddChoice("Uh...", ChoiceAction.CONTINUE, 74, 5)
		};
        gameManager.allObjects[74].dialogues[1].choices = new Choice[]
		{
			AddChoice("Alex", ChoiceAction.CONTINUE, 74, 2),
			AddChoice("Chelsey", ChoiceAction.CONTINUE, 74, 4)
		};
        gameManager.allObjects[74].dialogues[2].choices = new Choice[]
		{
			AddChoice("Thank you", ChoiceAction.CONTINUE, 74, 3)
		};
        AddToDialogue(74, 3, new ChoiceEventArgs() { ChoiceAction = GameManager.UnlockDoor, String = "DoorToAlfredRoom" });


        // ================ ALEX ================ //
        string[] alex = new string[]
		{
			/*0*/ "\"What do you want?\"",
			/*1*/ "\"Yeah, I'm Alex. Welcome to our little town.\"",
			/*2*/ "\"Yes, he is. How do you even know this?\"",
			/*3*/ "\"Please, he never cared about me so why should I care about him? If you want me to come with you, prove to me that he still cares and I'll consider going with you to visit him.\"",
			/*4*/ "\"He... He still has this? I guess the old man really does care. I'll go with you.\"",
			/*5*/ "\"Stop wasting my time.\"",
			/*6*/ "\"Weirdo...\""  ,
			/*7*/ "\"Do you have anything else to say?\""  ,
			/*8*/ "\"Do you have anything else to say?\"" , 
			/*9*/ "\"Dad...why did you do it? *sob*\""  ,
			/*10*/ "\"Dad... you still have the #jewel#. You really did care about me.\""  ,
			/*11*/ "\"I'm still mad at you, but Chelsey showed me this jewel.  You've been keeping it with you since then.\""  
			
		};

        AddNpc(75, "Kid", "Alex", alex);
        gameManager.allObjects[75].dialogues[0].choices = new Choice[]
		{
			AddChoice("Be friendly", ChoiceAction.CONTINUE, 75, 1, "Meet_Alfred_Son"),
			AddChoice("Say nothing", ChoiceAction.CONTINUE, 75, 6)
		};
        gameManager.allObjects[75].dialogues[1].choices = new Choice[]
		{
			AddChoice("Ask about Alfred", ChoiceAction.CONTINUE, 75, 2)
		};
        gameManager.allObjects[75].dialogues[2].choices = new Choice[]
		{
			AddChoice("Ask Alex to meet Alfred", ChoiceAction.CONTINUE, 75, 3)
		};
        gameManager.allObjects[75].dialogues[3].choices = new Choice[]
		{
			AddChoice("Do nothing", ChoiceAction.CONTINUE, 75, 5)
		};
        gameManager.allObjects[75].dialogues[7].choices = new Choice[]
		{
			AddChoice("Do nothing", ChoiceAction.CONTINUE, 75, 6),
		};
        gameManager.allObjects[75].dialogues[8].choices = new Choice[]
		{
			AddChoice("Show him the Jewel", ChoiceAction.CONTINUE, 75, 4, "AlfredSon_Trust"),
		}; gameManager.allObjects[75].dialogues[4].choices = new Choice[]
		{
			AddChoice("Thanks!", ChoiceAction.DESTROY, 75, 4),
		};

        // ================ ALFRED ================ //
        string[] alfred = new string[]
		{
			/*0*/ "\"What do you want?  Go away kid!\"",
			/*1*/ "\"Alex!!!\"",
			/*2*/ "\"Oh Alex, I missed you!  I've been regretting since the day I left you.\"",
			/*3*/ "\"I was wrong.  Will you forgive me Alex?\"",
			/*4*/ "\"I love you, son\"",
			/*5*/ "\"Stop wasting my time.\"",
			/*6*/ "\"*mumble* forgive me... *mumble*\""  ,
			/*7*/ "\"Do you have anything else to say?\""  ,
			
			
		};
        AddNpc(76, "Old Man", "Alfred", alfred);


        // ================ COP ================ //
        string[] cop = new string[]
		{
			/*0*/ "\"Stand back!\"",
			/*1*/ "\"Go home, kid.\""
			
			
		};
        AddNpc(77, "Guard", "Cop", cop);
        gameManager.allObjects[77].dialogues[0].choices = new Choice[]
		{
			AddChoice("What's going on?", ChoiceAction.CONTINUE, 77, 1)
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

    private void LoadQuestTerms()
    {
        gameManager.questTerms = new Dictionary<string, string>()
		{
			{ "PB" , "peanut butter" },
			{ "NM" , "name" },
			{ "" , "" }
		};
    }

    private void LoadQuestData()
    {
        QuestList ql = GameManager.instance.GetComponent<QuestList>();
        #region QUEST TEMPLATE
        //Quest Template
        /*  NPC_ID : ID of the NPC this quest belongs to
         *  dialogue_in_progress : What this NPC says after you talk to him once quest has been activated
         *  dialogue_change : What the NPC says once you FINISH the quest
         *  requirement : Required quest finished in order to activate this quest  Leave it as "none" if nothing is required
         *  changebool : What boolean gets changed once you finish the quest       Leave it as "none" if nothing is changed afterwards
         *  required_item : Item you need in order to finish the quest             Leave it as "none" if nothing is required
         *  questName : name of this quest
         */
        //   ql.AddQuest(NPC_ID, dialogue_in_progress, dialogue_change, requirement, changeBool, required_item, questName);

        #endregion
        // Alfred's son's quest
        // Meet Alex
        ql.AddQuest(75, 1, 7, "none", "none", "none", "MeetAlex");
        // Convince Alex
        ql.AddQuest(75, 7, 8, "MeetAlex", "none", "Alfred's Jewel", "Convince Alfred's Son");

    }

    private void LoadSceneData()
    {
        string sceneName = "";

        #region EMPTY TEMPLATE
        // EMPTY TEMPLATE
        /*
		AddParameters(sceneName, new InteractableObject.Parameters()
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
			});
		*/
        
        #endregion


		sceneName = SceneManager.SCENE_PARK;

		// ======================== DOG ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 12, 14, 20, 22 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 105
			});
		
		// ======================== RAE ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8, 10 },
				
				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 0,  dialogueIDMax = 3,

				// Getter/Setter variables, NpcID is required
				Summary = "Rae looking for dog",
				NpcID = 23
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 16 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 7,

				// Getter/Setter variables, NpcID is required
				Summary = "favorite spot",
				NpcID = 23
			});

		// ======================== HANK ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.DOWN_STRETCH,
				animationSpeed = 0.2f,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 12, 14 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 1, dialogueIDMax = 2,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 16 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 3, dialogueIDMax = 4,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 18 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 5,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 22 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 7,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});


		sceneName = SceneManager.SCENE_MAINSTREET;

		// ======================== DOG ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8, 10, 18 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 105
			});

		// ======================== RAE ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 14 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 6,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 23
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 20 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 11,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 23
			});

		// ======================== HANK ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 20 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 6,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 11
			});

		// ======================== JENEY ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 20 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 4, dialogueIDMax = 5,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 7
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 22 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 6,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 7
			});

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 20 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 7, dialogueIDMax = 9,

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.FALL,
				animationSpeed = 0.0f,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 20 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 2,

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});


		sceneName = SceneManager.SCENE_MALL;

		// ======================== DOG ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 16 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 105
			});

		// ======================== RAE ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 12 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 4,  dialogueIDMax = 5,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = -1
			});

		// ======================== JENEY ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8, 10, 12, 14 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 7
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() {  },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 1,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 7
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 18 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 2,  dialogueIDMax = 3,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 7
			});

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});


		sceneName = SceneManager.SCENE_POLICE;

		// ======================== RAE ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 18 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 8,  dialogueIDMax = 10,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 23
			});

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 10, 12 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 1,  dialogueIDMax = 2,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 14, 16 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 3,  dialogueIDMax = 4,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 10, 12 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 1,  dialogueIDMax = 2,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});


        sceneName = SceneManager.SCENE_HOSPITAL;

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 22 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 10,

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.DOWN_STRETCH,
				animationSpeed = 0f,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});

		// ======================== MEGAN ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 22 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 3,  dialogueIDMax = 5,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 3
			});

		sceneName = SceneManager.SCENE_APARTMENT;

		// ======================== ALFRED ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 18 },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
				dialogueIDMin = 5,  dialogueIDMax = 6,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});
    }

    private void LoadOutcomeData()
    {
        #region EMPTY_TEMPLATE
        /*
        AddOutcome(
            "Name of the boolean to check",
            "Text to display on screen if the boolean is true",
            "Text to display on screen if the boolean is false");
        */
        #endregion
        AddOutcome(
            "TestJewelOutcome",
            "You have found the jewel of ultimate power. Now you set off on your quest to rule the world",
            "The jewel broke and made you sad.");
        AddOutcome(
            "TestPeanutButterOutcome",
            "You ate some delicious peanut butter. Yum",
            "Without peanut butter, you fall into a deep depression and starved to death");
        AddOutcome(
            "NoOutcomeHere",
            "blank",
            "blank");
    }
}
