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
		    /*2*/ "\"Oh no...my friend's #dog was here at 4 PM#. I tried catching him by feeding him food, but he ran off.\"",
            /*3*/ "\"I hope that he returned home safely.\"",
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
        

        // ================ DOGE ================ //
        string[] dog = new string[]
		{
            /*0*/ "\"Woof\"",
        };
        AddNpc(24, "Dog", "Dog", dog);


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
            wanderDistanceX = 30f,
            wanderDirectionX = 1,

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
            dialogueIDMin = 3,
            dialogueIDMax = 4,

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
            dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
            dialogueIDSingle = 0,
            dialogueIDMin = 0,
            dialogueIDMax = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "jeney tells you about alfred and his son",
            NpcID = 73
        };
        AddParameters(sceneName, jeney2);


        // ================ ALEX ================ //
        InteractableObject.Parameters alex2 = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
            dialogueIDSingle = 9,
            dialogueIDMin = 9,
            dialogueIDMax = 10,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "alex is devastated",
            NpcID = 75
        };
        AddParameters(sceneName, alex2);

        // ================ MANNY ================ //
        InteractableObject.Parameters manny2 = new InteractableObject.Parameters()
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
            Summary = "cop tells people to back off",
            NpcID = 77
        };
        AddParameters(sceneName, manny2);

        // ================ ALFRED ================ //
        InteractableObject.Parameters deadalfred = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.DIALOGUE_MIN_MAX,
            dialogueIDSingle = 0,
            dialogueIDMin = 0,
            dialogueIDMax = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "a dead body",
            NpcID = -999
        };
        AddParameters(sceneName, deadalfred);

        sceneName = SceneManager.SCENE_MALL;

        // ================ JENEY ================ //
        InteractableObject.Parameters jeney = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 6, 8, 10, 12, 14, 16, 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.DOWN_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "jeney sells stuff",
            NpcID = 73
        };
        AddParameters(sceneName, jeney);

        // ================ ALEX ================ //
        InteractableObject.Parameters alex = new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,
            animationSpeed = 0.0f,

            // Getter/Setter variables, NpcID is required
            Summary = "jeney tells you about alfred and his son",
            NpcID = 75
        };
        AddParameters(sceneName, alex);
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
