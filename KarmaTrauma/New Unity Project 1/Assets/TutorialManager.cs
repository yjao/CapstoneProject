﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private GameObject gameManagerObject;
    private SceneManager sm;
    private GameManager gameManager;
	private const string SCENE_TUTORIAL = "WP2_Tutorial";

    //private const string SCENE_HOUSE = "T_House";
    private const string SCENE_SCHOOL = "T_Class";

    private const string SCENE_MALL = "T_Mall";
    //private const string SCENE_MAIN_STREET = "T_MainStreet";

    //private const string SCENE_MALL = "T_Mall";
    private const string SCENE_MAIN_STREET = "T_MainStreet";

    private const string SCENE_MINI_MAIN_STREET = "T_MiniMainStreet";
	private const string SCENE_DONUT_SHOP = "T_Mall";
    private const string SCENE_WORLD_MAP = "T_WorldMap";

    private const string SCENE_G_MAIN_STREET = "G_MainStreet";
    
    bool endCondition = false;


    private GameObject activeTutorialBox;

    public GameObject dialogueContainer;
    private List<Slide> slides;

    private class Slide
    {
        public string imageName;
        public float timer;
        public string text;

        public Slide(string _imageName, float _timer, string _text)
        {
            imageName = _imageName;
            timer = _timer;
            text = _text;
        }
    }
	// Use this for initialization
	void Start()
	{
      
		if (instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //gameManager = GameManager.instance;
        slides = new List<Slide>()
        {
			/*0*/ new Slide("Slide1", 3, "\"Hey Kelly! Help us out here!\""),
			/*1*/ new Slide("Slide2", 3, "\"Yeah Chelsey, go help 'em. I'm too lazy to go.\""),
			/*2*/ new Slide("Slide3", 3, "\"Alright...\""),
			/*3*/ new Slide("Slide5", 3, ""),
			/*4*/ new Slide("Slide6", 3, ""),
			/*5*/ new Slide("Slide7(3)", 3, ""),
			/*6*/ new Slide("Slide13", 3, "\"Alfred...!\nIf I could… if only I could go back in time…\""),
			/*7*/ new Slide("Slide14", 3, "\"What... is going on...\nThis all seems too familiar...\"")
        };
        gameManager = GameManager.instance;
        HideGameManager();
        gameManagerObject = GameObject.Find("GameManager");
        sm = gameManagerObject.GetComponent<SceneManager>();
		StartCoroutine(Start_Tutorial());
	}

    IEnumerator Start_Tutorial()
    {
		yield return StartCoroutine(Slide_Coroutine(slides[0]));
		//yield return StartCoroutine(Slide_Coroutine(slides[1]));
        //yield return StartCoroutine(Slide_Coroutine(slides[2]));
		//Destroy(GameManager.instance.gameObject);
		//yield return null;

        yield return StartCoroutine(Slide4_Coroutine());
		//Destroy(GameManager.instance.gameObject);

		// PICTURE SLIDE: Fallen Alfred and Book
		//yield return StartCoroutine(LoadSceneCoroutine(SCENE_TUTORIAL));
		//yield return StartCoroutine(Slide_Coroutine(slides[3]));
		//yield return StartCoroutine(Slide_Coroutine(slides[4]));
		yield return StartCoroutine(Slide7_Coroutine());
		//Destroy(GameManager.instance.gameObject);

		yield return StartCoroutine(Slide8_Coroutine());
        yield return StartCoroutine(Slide9_Coroutine());
		yield return StartCoroutine(Slide10_Coroutine());
        //yield break;

		yield return StartCoroutine(Slide11_Coroutine());

        yield return StartCoroutine(Slide12_Coroutine());
		//yield return StartCoroutine(Slide_Coroutine(slides[6]));

        //yield return StartCoroutine(sm.fade_black());
      
        yield return StartCoroutine(Slide14_Coroutine());

        yield return StartCoroutine(Slide19_Coroutine());

        Application.LoadLevel(SCENE_G_MAIN_STREET);
        Debug.Log("loaded g main street" + Application.loadedLevel);
        yield return null;
        GameManager.instance.SetTime(GameManager.TimeType.SET, 20);
        yield return null;

        SceneManager.instance.LoadScene();
        yield return null;
        yield return StartCoroutine(SceneManager.instance.fade_out());

        gameManager.Play();
        Destroy(this);
        yield break;

    }

	IEnumerator Slide_Coroutine(Slide slide)
	{
        GameObject.Find("Canvas/Slide").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slide.imageName);
        if (slide.text != null && slide.text != "")
        {
            yield return new WaitForSeconds(1f);
            CreateTutorialBox(slide.text, Textbox.TutorialBoxPosition.BOTTOM, slide.timer - 1f);
            yield return null;
        }
        yield return new WaitForSeconds(slide.timer);
	}

    IEnumerator Slide4_Coroutine()
    {
        StartCoroutine(StartCutscene(true));
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_MINI_MAIN_STREET));
        gameManager.Wait();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(GameObject.Find("Invis").GetComponent<CharacterAnimations>().Move(1, -12.5f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(EndCutscene(false));
        GameObject.Find("Invis").transform.parent = GameObject.Find("Player").transform;
        CreateTutorialBox("What’s wrong? Have you forgotten how to walk? Haha, you’re so awkward Chels! It’s why I love ya. %Use the arrow keys or WASD keys to move and hold Shift to run%. Now go %get the ball%!", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        gameManager.Play();

		// Exit Condition
        while (!endCondition) { yield return null; } endCondition = false;

        //StartCoroutine(EndCutscene(true));
		//yield return StartCoroutine(SceneManager.instance.fade_black());
    }

	IEnumerator Slide7_Coroutine()
	{
		yield return StartCoroutine(Slide_Coroutine(slides[5]));

		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(sm.display_text("2 years later..."));
		gameManager.transform.Find("Menu_layout/Clock_background").gameObject.SetActive(true);
		gameManager.transform.Find("Menu_layout/Clock_display").gameObject.SetActive(true);
		gameManager.SetTime(GameManager.TimeType.SET, 6);
		yield return StartCoroutine(gameManager.GradualClock(12, .2f));
	}

    IEnumerator Slide8_Coroutine()
    {
        //School Scene
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_SCHOOL));
        //gameManager = GameManager.instance;

        gameManager.SetTime(GameManager.TimeType.SET, 8);
        gameManager.Wait();
        //gameManager.transform.Find("Menu_layout").gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        //
        //EXAMPLE CODE TO BE REMOVED
        //CreateChoice("testperson", "teststring", new Choice[]
        //{
        //    new Choice("testchoice1", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = new string[2]{"testd1", "testd2"}, TutorialDialogueCounter = 3 }),
        //    new Choice("testchoice2", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = new string[2]{"testd3", "testd4"}, TutorialDialogueCounter = 3 })
        //});
        //yield return null; while (Pause()) { yield return null; }
        //
        //
		yield return StartCoroutine(SceneManager.instance.fade_out());

        MultiDialogue("Mrs. Freewoman", new string[2]
        {
            "Happy Monday, class, my name is Megan Freewoman. As a reminder, %use the Spacebar to progress speech%.",
            "Unfortunately, Mr. Ly is out today, so I’ll be your literature sub for today"
        });
        NPC kelly = GameObject.Find("Kelly").GetComponent<NPC>();
        NPC stylishguy = GameObject.Find("Stylish_guy").GetComponent<NPC>();
        NPC redhairguy = GameObject.Find("Red_guy").GetComponent<NPC>();
        NPC girlindrama = GameObject.Find("Girl_in_drama").GetComponent<NPC>();
        NPC pinkguy = GameObject.Find("Pink_hair_dude").GetComponent<NPC>();

        yield return null; while (Pause()) { yield return null; }

        kelly.SetAnimation(CharacterAnimations.States.RIGHT_IDLE);
        girlindrama.SetAnimation(CharacterAnimations.States.UP_DANCE);
        /*
        CreateDialogue("Kelly", "*Whispers* Psst, I hope she won’t go on about how great Jerry Faraday is, like how Ly does all the time. Ugh.");
        yield return null; while (Pause()) { yield return null; }
        girlindrama.SetAnimation(CharacterAnimations.States.UP_IDLE);
        kelly.SetAnimation(CharacterAnimations.States.UP_IDLE);
        */
        kelly.SetAnimation(CharacterAnimations.States.RIGHT_IDLE);
        yield return new WaitForSeconds(.25f); 
        CreateDialogue("Kelly", "*Whispers* Psst, I hope she won’t go on about how great Jerry Faraday is, like how Ly does all the time. Ugh.");
        yield return null; while (Pause()) { yield return null; }
        yield return new WaitForSeconds(.25f); 
        kelly.SetAnimation(CharacterAnimations.States.UP_IDLE);
     
        CreateDialogue("Mrs. Freewoman", "Oh right, before I forget!");
        yield return null; while (Pause()) { yield return null; }

        Dialogue d = new Dialogue(2, "I was at Jeney’s this morning and told her #Moonlight#. It’s the coupon code that expires today, and you get an extra donut if you use it! Isn’t it wonderful?");
        gameManager.CreateDialogue("Mrs. Freewoman", d, -1);
        redhairguy.SetAnimation(CharacterAnimations.States.RIGHT_SWING);
        stylishguy.SetAnimation(CharacterAnimations.States.LEFT_SWING);
        //CreateDialogue("Mrs. Freewoman", "I was at Jeney’s this morning and told her #Moonlight#. It’s the coupon code that expires today, and you get an extra donut if you use it! Isn’t it wonderful?");
        yield return null; while (Pause()) { yield return null; }
        CreateDialogue("Kelly", "Oooooh… Yes…");
        //yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.LEFT_DANCE));

        yield return StartCoroutine(kelly.GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.LEFT_DANCE));
        yield return StartCoroutine(kelly.GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.RIGHT_DANCE));
        redhairguy.SetAnimation(CharacterAnimations.States.UP_IDLE);
        stylishguy.SetAnimation(CharacterAnimations.States.UP_IDLE);
        //kelly.SetAnimation(CharacterAnimations.States.LEFT_DANCE);
        //kelly.SetAnimation(CharacterAnimations.States.UP_IDLE);
        yield return null; while (Pause()) { yield return null; }
        //yield return StartCoroutine(kelly.GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.UP_IDLE));
        kelly.SetAnimation(CharacterAnimations.States.UP_IDLE);     ///Not getting called for some reason...
        //red guy and stylish guy talking

        MultiDialogue("Mrs. Freewoman", new string[4]
        {
            "Anyways, Mr. Ly called this morning and said the discussion topic is up to me.",
            "So let me ask this, how many of you are into time travels?",
            "...I myself am a huge fan of it, and if I could, I’d go back to 2 years from today, right before I was hospitalized.",
            "What’s something you’d go back in time and change?"
        });
        yield return null; while (Pause()) { yield return null; }

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SceneManager.instance.fade_black());
        yield return StartCoroutine(gameManager.GradualClock(14, .1f));
    }
    IEnumerator Slide9_Coroutine()
    {
        GameObject.Find("NPCS").gameObject.SetActive(false);
        yield return StartCoroutine(sm.fade_out());
        gameManager.Wait();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(3, -1.25f, CharacterAnimations.States.RIGHT_WALK));
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_IDLE);
        GameObject.Find("Player").GetComponent<CharacterAnimations>().AnimationState = CharacterAnimations.States.LEFT_IDLE;
        MultiDialogue("Kelly", new string[2]
        {
            "Phew, classes are finally over.",
            "Hey, why don’t we go to Punxsu--er I mean JF Mall! I’m craving Jeney’s donuts…"
        });
        yield return null; while (Pause()) { yield return null; }
        yield return new WaitForSeconds(1f);
        CreateDialogue("Kelly", "Alright, it’s decided! I really want those Strawberry Squishies.");
        yield return null; while (Pause()) { yield return null; }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SceneManager.instance.fade_black());
        yield return StartCoroutine(SceneManager.instance.map_name("World Map"));
        yield return new WaitForSeconds(1f);
    }


    IEnumerator Slide10_Coroutine()
    {
        
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_WORLD_MAP));

        gameManager.Wait();
        yield return StartCoroutine(SceneManager.instance.fade_out());
        yield return new WaitForSeconds(1f);
        CreateDialogue("Kelly", "To the Punxsu-- I mean %J.F. Mall%!");
        yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(EndCutscene(false));
        CreateTutorialBox("%Press Spacebar to enter a location%", Textbox.TutorialBoxPosition.TOP, -1, true);
        gameManager.Play();

        while (true)
        {
            if (Application.loadedLevelName == SCENE_MALL)
            {
                yield return StartCoroutine(SceneManager.instance.fade_black());
                yield return StartCoroutine(gameManager.GradualClock(16, .25f));
                yield break;
            }
            yield return null;
        }

    }

   

	IEnumerator Slide11_Coroutine()
	{
     
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_DONUT_SHOP));
        gameManager.Wait();
        yield return StartCoroutine(SceneManager.instance.fade_out());
        //yield return new WaitForSeconds(1);
        MultiDialogue("Kelly", new string[3]
        {
            "Excuse me, I'd like some Strawberry Squishies.",
            "Wait, wasn't there a coupon code Freewoman gave us in class?",
            "What was it...do you remember, Chels?"
        });
        yield return null; while (Pause()) { yield return null; }
        CreateTutorialBox("I know you have great memory! If you remember the %coupon word%, I'll but you a donut", Textbox.TutorialBoxPosition.BOTTOM, 2f, true);
        //yield return new WaitForSeconds(1);
        CreateTutorialBox("Come on, Chels, think harder! %Press 'M'%, maybe you'll think of something.", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        gameManager.Wait();
        gameManager.transform.Find("Menu_layout/Quest_background").gameObject.SetActive(true);
        gameManager.transform.Find("Menu_layout/Quest_label").gameObject.SetActive(true);
        //transform.Find("PageIndex").gameObject.transform.Find("PageIndexText").GetComponent<Text>().text
        Menu_Layout menu_layout = GameObject.Find("GameManager").transform.Find("Menu_layout").GetComponent<Menu_Layout>();
        //Menu_Layout menu_layout = gameManager.transform.Find("Menu_layout").GetComponent<Menu_Layout>();
        while (!menu_layout.GetMemoryLogOpen()) { yield return null; }

        CreateTutorialBox("I knew you would remember!", Textbox.TutorialBoxPosition.MIDDLE, 2f, true);
        while (menu_layout.GetMemoryLogOpen()) { yield return null; }
        gameManager.Wait();
        CreateDialogue("Kelly", "Which one do you want? I'll buy as promised~");

        yield return null; while (Pause()) { yield return null; }
        //Maybe add walking animation here

        MultiDialogue("Jeney", new string[2]
        {
            "Welcome to the Donut Hole! What can I get you today?",
            "You can %use WS or Up and Down arrow keys to navigate choices, and Spacebar to select%",
          
        });
        string[] action = {"Here you go, I’ve put it inside your bag. %Press B to take a look%. Have a nice day!"};
        action[0] = Textbox.ColorTutorialKeyword(action[0]);
        yield return null; while (Pause()) { yield return null; }
        CreateChoice("Jeney", "Our special today is the Donut Sprinklez!", new Choice[]
        {
            new Choice("Chocolate Crispies", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Cocodonut", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Donut Hole Originals", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Donut Sprinklez", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Minty Muncies", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Potadonut Tots", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
            new Choice("Strawberry Squishies", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = action, TutorialDialogueCounter = 3 }),
        
        });
        yield return null; while (Pause()) { yield return null; }
        gameManager.transform.Find("Menu_layout/Bag_background").gameObject.SetActive(true);
        gameManager.transform.Find("Menu_layout/Bag_label").gameObject.SetActive(true);

        yield return StartCoroutine(SceneManager.instance.fade_black());
        yield return StartCoroutine(gameManager.GradualClock(20, .25f));
        
	}

    IEnumerator Slide12_Coroutine()
    {
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_MAIN_STREET));
        StartCoroutine(StartCutscene(true));
        yield return StartCoroutine(SceneManager.instance.fade_out());

        gameManager.Wait();

        yield return new WaitForSeconds(2f);
        MultiDialogue("Kelly", new string[2]
        {
            "She’s still better than Mr. Ly fanboying over Faraday though…",
            "But yeah, that Freewoman was so boring! I’d hate to have her again. She’s just so... scholarly."
        });
        yield return null; while (Pause()) { yield return null; }

        CreateChoice("Kelly", "Oh please, don’t tell me you liked her, with all that geeky sci-fi talk...", new Choice[]
        {
            new Choice("Say nothing.", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = new string[1]{"No? Well, I expected that you would have #talked to her after class# or something."}, TutorialDialogueCounter = 3 }),
            new Choice("Yeah, I liked her.", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = new string[2]{"Of course you would. I expected noooo less from you, Chels. I can totally see you two being friends.", "Hey, why didn’t you #talk to her after class#?"}, TutorialDialogueCounter = 3 }),
            new Choice("Doesn't she look familiar to you?", new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = new string[2]{"No? You’re the one with photographic memory, not me! I couldn’t even remember the quadratic formula.", "But I dunno, if she looks familiar, maybe you #should have talked to her after class# about it? I don’t remember a thing."}, TutorialDialogueCounter = 3 })
        });
        yield return null; while (Pause()) { yield return null; }

        CreateDialogue("Kelly", "Oh wow, I didn’t realize it was already #9 PM#! Daddy’s gonna be mad at me if I don’t go soon. Thanks for the hang! Gotta run, see ya tomorrow!");
        yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(3, 19.00f, CharacterAnimations.States.RIGHT_WALK, 0.04f));
        Destroy(GameObject.Find("Kelly"));

        CreateDialogue("Chelsey", "I should go home now.");
        yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(EndCutscene(false));
        gameManager.Play();

        while (!endCondition)
        {
            yield return null;
        }
        endCondition = false;
        // SCREEN TURNS RED
        yield return StartCoroutine(SceneManager.instance.fade_black());
        GameObject gameManagerObject = GameObject.Find("GameManager");
        SceneManager sm = gameManagerObject.GetComponent<SceneManager>();
        yield return StartCoroutine(sm.fade_black());
        yield return StartCoroutine(sm.display_text("You went to bed and woke up to go to class.  Same things keep on happening...The day is repeating itself.  You decided to go to Main Street to see if THAT incident is going to happen again.", 8f));
        //yield return new WaitForSeconds(10);
    }



    IEnumerator Slide14_Coroutine()
    {

        yield return StartCoroutine(sm.fade_out());
        yield return StartCoroutine(Slide_Coroutine(slides[7]));

      
        yield return StartCoroutine(sm.fade_black());
        yield return StartCoroutine(sm.display_text("The next day...\n\n...?"));
        gameManager.transform.Find("Menu_layout/Clock_background").gameObject.SetActive(true);
        gameManager.transform.Find("Menu_layout/Clock_display").gameObject.SetActive(true);
        gameManager.SetTime(GameManager.TimeType.SET, 6);
        yield return StartCoroutine(gameManager.GradualClock(12, .1f));
    }

    IEnumerator Slide19_Coroutine()
    {
        
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_MAIN_STREET));
        Destroy(GameObject.Find("Kelly"));
        yield return StartCoroutine(SceneManager.instance.fade_out());
        while (!endCondition)
        {
            yield return null;
        }

        // SCREEN TURNS RED
        CreateDialogue("Chelsey", "Alfred...!");
        yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(SceneManager.instance.fade_black());
        yield return new WaitForSeconds(1f);
        CreateDialogue("Megan Freewoman", "If I could... if only I could go back in time...");
        yield return null; while (Pause()) { yield return null; }

        endCondition = false;
        yield break;
    }

    public IEnumerator Slide_Triggers_Coroutine(string tag)
    {
        if (tag == "TrafficLight")
        {
            CreateTutorialBox("Um, yeah, you might want to hit the pedestrian light before crossing. What? Hey, are you spacing out again? %Space bar%, not space out! Press that and like, %interact with objects%.", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        }
        else if (tag == "Kelly")
        {
            CreateTutorialBox("What is it? Come on, get your nose out of your book and go exercise a bit! Go, %get the ball%! For me!", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        }
        else if (tag == "DefaultText")
        {
            CreateTutorialBox("What’s wrong? Have you forgotten how to walk? Haha, you’re so awkward Chels! It’s why I love ya. %Use the arrow keys or WASD keys to move and hold Shift to run%. Now go %get the ball%!", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        }
        else if (tag == "Traffic")
        {
            CreateTutorialBox("Hrm… I guess it doesn’t work. Well it’s whatever, I don’t see any cops; just be careful! If I were you, I’d %move while holding shift key to run%. The cars are less likely to hit you if you run faster, right?", Textbox.TutorialBoxPosition.BOTTOM, -1, true);
        }
        else if (tag == "Fall")
        {

            gameManager.Wait();
            GameObject.Find("Main Camera").transform.parent = GameObject.Find("Alfred").transform;
            GameObject.Find("Main Camera").transform.position = new Vector3(GameObject.Find("Alfred").transform.position.x, GameObject.Find("Alfred").transform.position.y, GameObject.Find("Main Camera").transform.position.z);
            yield return StartCoroutine(GameObject.Find("Alfred").GetComponent<CharacterAnimations>().Move(2, -11.00f, CharacterAnimations.States.FALLEN, 0.15f));
            yield return new WaitForSeconds(1f);
            //GameObject.Find("Main Camera").transform.parent = GameObject.Find("Player").transform;
            //GameObject.Find("Main Camera").transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Main Camera").transform.position.z);
            //gameManager.Play();
            //yield return new WaitForSeconds(3f);
            endCondition = true;
        }
        else if (tag == "Pause")
        {

            gameManager.Wait();
            yield return new WaitForSeconds(3f);
            gameManager.Play();
        }
        else if (tag == "Done")
        {
            endCondition = true;
        }
        yield break;
    }

    IEnumerator LoadSceneCoroutine(string mapname)
    {
        Application.LoadLevel(mapname);
        yield return null;
    }

	private bool Pause()
	{
		return gameManager.gameMode == GameManager.GameMode.DIALOGUE;
	}

    public void CreateTutorialBox(string message, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE, float destroyTimer = -1, bool transparent=false)
    {
        if (activeTutorialBox != null)
        {
            Destroy(activeTutorialBox);
        }
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
		StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(Textbox.ColorTutorialKeyword(message), destroyTimer, position, transparent));
        activeTutorialBox = dialog;
    }

    private void CreateDialogue(string name, string message)
    {
        //Choice[] choices = null;
		Dialogue d = new Dialogue(-1, Textbox.ColorTutorialKeyword(message));
        gameManager.CreateDialogue(name, d, -1);
    }

    private void MultiDialogue(string name, string[] messages)
    {
        EventManager.OnDialogChoiceMade += InteractableObject.HandleTutorial;
    
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i] = Textbox.ColorTutorialKeyword(messages[i]);
        }
        //for (int j = 0; j < messages.Length; j++)
        //{
        //    Debug.Log(messages[j]);
        //}
        Dialogue d = new Dialogue(-1, messages[0]);
        d.CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.ContinueTutorialDialogue, TutorialDialogues = messages, TutorialDialogueCounter = messages.Length };
        d.Action += d.CEA.ChoiceAction;
        gameManager.CreateDialogue(name, d, -1);
    }

    private void CreateChoice(string name, string message, Choice[] choices)
    {
        EventManager.OnDialogChoiceMade += InteractableObject.HandleTutorial;
        Dialogue d = new Dialogue(-1, message);
        d.choices = choices;
        gameManager.CreateChoice(name, d, -1);
    }    

    private void HideGameManager()
    {
        gameManager.transform.Find("Menu_layout/Clock_display").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Clock_background").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Bag_background").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Bag_label").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Quest_background").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Quest_label").gameObject.SetActive(false);
        //gameManager.transform.Find("Menu_layout/EventSystem").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/Fast_forward").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/MapPanel").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/MapName").gameObject.SetActive(false);
    }

    public IEnumerator StartCutscene(bool instant)
    {
        gameManager.transform.Find("Menu_layout").gameObject.SetActive(true);
        gameManager.transform.Find("Menu_layout/TopCinemaBar").gameObject.SetActive(true);
        gameManager.transform.Find("Menu_layout/BottomCinemaBar").gameObject.SetActive(true);
        Image top_bar = gameManager.transform.Find("Menu_layout/TopCinemaBar").GetComponent<Image>();
        Image bottom_bar = gameManager.transform.Find("Menu_layout/BottomCinemaBar").GetComponent<Image>();
        if (!instant)
        {
            for (int i = 0; i < 15; i++)
            {
                Debug.Log(bottom_bar.rectTransform.anchorMin.y);
                top_bar.rectTransform.anchorMax = new Vector2(top_bar.rectTransform.anchorMax.x, top_bar.rectTransform.anchorMax.y - .01f);
                top_bar.rectTransform.anchorMin = new Vector2(top_bar.rectTransform.anchorMin.x, top_bar.rectTransform.anchorMin.y - .01f);
                bottom_bar.rectTransform.anchorMax = new Vector2(bottom_bar.rectTransform.anchorMax.x, bottom_bar.rectTransform.anchorMax.y + .01f);
                bottom_bar.rectTransform.anchorMin = new Vector2(bottom_bar.rectTransform.anchorMin.x, bottom_bar.rectTransform.anchorMin.y + .01f);
                yield return null;
            }
        }
        else
        {
            top_bar.rectTransform.anchorMax = new Vector2(top_bar.rectTransform.anchorMax.x, 1f);
            top_bar.rectTransform.anchorMin = new Vector2(top_bar.rectTransform.anchorMin.x, .85f);
            bottom_bar.rectTransform.anchorMax = new Vector2(bottom_bar.rectTransform.anchorMax.x, .15f);
            bottom_bar.rectTransform.anchorMin = new Vector2(bottom_bar.rectTransform.anchorMin.x, 0f);
        }
        yield return null;
    }

    public IEnumerator EndCutscene(bool instant)
    {
        Image top_bar = gameManager.transform.Find("Menu_layout/TopCinemaBar").GetComponent<Image>();
        Image bottom_bar = gameManager.transform.Find("Menu_layout/BottomCinemaBar").GetComponent<Image>();
        if (!instant)
        {
            for (int i = 0; i < 15; i++)
            {
                Debug.Log(bottom_bar.rectTransform.anchorMin.y);
                top_bar.rectTransform.anchorMax = new Vector2(top_bar.rectTransform.anchorMax.x, top_bar.rectTransform.anchorMax.y + .01f);
                top_bar.rectTransform.anchorMin = new Vector2(top_bar.rectTransform.anchorMin.x, top_bar.rectTransform.anchorMin.y + .01f);
                bottom_bar.rectTransform.anchorMax = new Vector2(bottom_bar.rectTransform.anchorMax.x, bottom_bar.rectTransform.anchorMax.y - .01f);
                bottom_bar.rectTransform.anchorMin = new Vector2(bottom_bar.rectTransform.anchorMin.x, bottom_bar.rectTransform.anchorMin.y - .01f);
                yield return null;
            }
        }
        else
        {
            top_bar.rectTransform.anchorMax = new Vector2(top_bar.rectTransform.anchorMax.x, 1.15f);
            top_bar.rectTransform.anchorMin = new Vector2(top_bar.rectTransform.anchorMin.x, 1f);
            bottom_bar.rectTransform.anchorMax = new Vector2(bottom_bar.rectTransform.anchorMax.x, 0f);
            bottom_bar.rectTransform.anchorMin = new Vector2(bottom_bar.rectTransform.anchorMin.x, -.15f);
        }
        gameManager.transform.Find("Menu_layout/TopCinemaBar").gameObject.SetActive(false);
        gameManager.transform.Find("Menu_layout/BottomCinemaBar").gameObject.SetActive(false);
        yield return null;
    }

	// Update is called once per frame
	void Update()
	{
	    
	}



}
