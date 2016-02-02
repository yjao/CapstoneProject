using UnityEngine;
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
        // ================ ALFRED ================ //
        string[] alfred = new string[]
        {
            /*0*/ "\"Good morning! The donuts here are tasty. You should try some. I recommend the Cocodonut.\"",
            /*1*/ "\"Today is a big day. I need that #box# soon. That filthy man will get what he deserves...\"",
            /*2*/ "\"Oh, hello there. Are you lost? Better hurry before you're late to class!\"",
            /*3*/ "\"Is there something wrong? No class today?\"",
            /*4*/ "\"Work just doesn't feel right when he's still running about, but it'll be over soon. He'll get what he deserves.\"",
            /*5*/ "\"Hey there! I need to go grab some clothes for my wife at the hospital.\"",
            /*6*/ "\"Sorry I'm not up for a chat.  Stay safe out there!\"",
            /*7*/ "\"Oh Megan...I'm so sorry...\"",
            /*8*/ "\"Had I reach the park in time...\"",
            /*9*/ "\"Faraday...you will regret this.\"",
            /*10*/ "\"...\"",

        };
        AddNpc(2, "Police Man", "Alfred", alfred);


        // ================ MEGAN ================ //
        string[] megan = new string[]
        {
            /*0*/ "\"Hello there. I'm waiting for my husband to get here with my clothes.\"",       
            /*1*/ "\"He should be here any minute now. What's taking him so long?\"",
            /*2*/ "\"If only I knew that #someone was out to get him#.\"",
            /*3*/ "*Sob* \"He was going to get something from home to deliver to me here.\"",
            /*4*/ "\"If I haven't been hospitalize at all, this wouldn't have happened.\"",
            /*5*/ "\"Oh Alfred, please hang in there!\"*Sob*",
        };
        AddNpc(3, "Nice Woman", "Megan", megan);


        // ================ JENEY ================ //
        string[] jeney = new string[]
		{
			/*0*/ "\"Welcome to the Donut Hole! Today's special is the Donot Sprinklez~\"",
			/*1*/ "\"Come by the Donut Hole when you have time. We sell really tasty donuts.\"",
            /*2*/ "\"Oh no...my friend's #dog was here at 4 PM#.  I tried catching him by feeding him a donut, but he ran off.\"",
            /*3*/ "\"Dogs like meat; maybe he'll stay if there's #bacon#? I hope that he returned home safely.\"",
            /*4*/ "\"Poor man...he was a really #nice cop# who comes by my store often.\"",
            /*5*/ "\"He always tell me that our Cocodonut tastes the best.\"",
            /*6*/ "\"Whew...what a long day.  So many things happened.\"",
        };
        AddNpc(7, "Donut Shop Owner", "Jeney", jeney);


        // ================ HANK ================ //
        string[] hank = new string[]
		{
			/*0*/ "\"Ugh I'm so sleepy. I hate this job.\"",
			/*1*/ "\"That ugly dog belongs to this girl name Rae. She was at the #park until noon# looking for the dog while crying.\"",
			/*2*/ "\"So annoying ugh...\"", 
			/*3*/ "\"I was enjoying my nap when that stupid #dog started digging# all over the park at 2 PM.\"",
			/*4*/ "\"I had to go and fill all the holes that he dug up. Now go away and stop bothering me.\"",
			/*5*/ "\"Ugh...I'm hungry. I don't get paid enough for this.\"",
		    /*6*/ "\"I see him sometimes at the park. I guess it must be his time to go.\"",
            /*7*/ "\"Ugh...Go away, kid. Don't you see that I'm about to go to bed?\"",
        };
        AddNpc(11, "Park Ranger", "Hank", hank);
        AddBooleanToDialogue(11, 1, "LostDog");
        AddBooleanToDialogue(11, 3, "DogCanDig");

        // ================ RAE ================ //
        string[] rae = new string[]
		{
			/*0*/ "*Sob*\"I #lost my dog# today...\"",
			/*1*/ "\"He went outside to take a leak and hadn't come back since.\"",
			/*2*/ "\"I tried looking for him everywhere, but he's nowhere to be found.\"",
			/*3*/ "\"What should I do...\"*Sob*",
			/*4*/ "\"Jeney is my friend from middle school. She now owns this successful donut shop chain.\"",
			/*5*/ "\"All the cops in this town loves this store and comes by often. You should try some.\"",
			/*6*/ "\"Have you seen my dog? I've been looking everywhere for him.\""  ,
			/*7*/ "\"I was hoping that he will at the park since it's his #favorite spot#.\""  ,
			/*8*/ "\"It's been 12 hours since I lost my dog. I don't know what to do.\"" , 
			/*9*/ "\"I hope someone finds him and returns him to me.\""  ,
			/*10*/ "\"I guess this #train ticket# is useless now since I can't leave town without him.\""  ,
			/*11*/ "\"Poor guy...I hope he rests in peace.\""  
			
		};
        AddNpc(23, "Crying Girl", "Rae", rae);
        AddBooleanToDialogue(23, 0, "LostDog");

        // ================ DOGE ================ //
        string[] dog = new string[]
		{
            /*0*/ "\"Woof\"",
            /*1*/ "\"(You give the dog some bacon)\"",
            /*2*/ "\"(The dog appears to be following you)\""
        };
        AddNpc(24, "Dog", "Dog", dog);
        gameManager.allObjects[24].dialogues[0].choices = new Choice[]
		{
            AddChoice("Feed the dog bacon", ChoiceAction.CONTINUE, 24, 1, checkboolname: "Bacon")
        };
        AddToDialogue(24, 1, ChoiceContinueDialog(24, 2));
        AddToDialogue(24, 2, ChoiceInteractItem(24));


        // ================ ITEMS & OBJECTS ================ //

        string[] bacon = new string[]
        {
            /*0*/ "\"There's some bacon here\""
        };
        AddNpc(110, "Bacon", "Bacon", bacon);
        gameManager.allObjects[110].dialogues[0].choices = new Choice[]
        {
            AddChoice("Leave it alone"),
            AddChoice("Take the bacon", ChoiceAction.ITEM, 110)
        };

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
         *  Time : When the quest is accessible : 0 for anytime, otherwise, list timeblocks i.e. {6,8,10}
         */
        //   ql.AddQuest(NPC_ID, dialogue_in_progress, dialogue_change, requirement, changeBool, required_item, questName);

        #endregion
        // Alfred's son's quest
        // Meet Alex
       // ql.AddQuest(75, 1, 7, "none", "none", "none", "MeetAlex");
        // Convince Alex
      //  ql.AddQuest(75, 7, 8, "MeetAlex", "none", "Alfred's Jewel", "Convince Alfred's Son");

        ql.AddQuest(11, 1, 1, "none", "LostDog", "none", "HankMorning",new List<int>{12,14});
        ql.AddQuest(11, 3, 3, "none", "DogCanDig", "none", "HankAfternoon", new List<int> {16});

        

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

		sceneName = SceneManager.SCENE_HOUSE;

		// ======================== BACON ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
	    	{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() { 8, 10, 12, 14, 16, 18, 20, 22 },
				
				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 110
			});


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
				NpcID = 24
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
				NpcID = 24
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

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

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

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.UP_IDLE,

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

				// NPC CharacterAnimations
				startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

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
				startingAnimationState = CharacterAnimations.States.FALLEN,
				animationSpeed = 0.0f,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = 2
			});

		// ======================== MEGAN ======================== //
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
				NpcID = 3
			});

		// ======================== CONES ======================== //
		AddParameters(sceneName, new InteractableObject.Parameters()
		              {
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 20 },

			// Getter/Setter variables, NpcID is required
			Summary = "",
			NpcID = 120
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
				NpcID = 24
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
				startingAnimationState = CharacterAnimations.States.SLEEPING,
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
                wanderDistanceX = 20f,
                wanderDirectionX = -1,
                animationSpeed = 0.05f,
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
