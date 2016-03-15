using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingManager : MonoBehaviour {
    public static EndingManager instance;
    private GameObject activeTutorialBox;

	private GameObject gmObj;
	private GameManager gm;
	private SceneManager sm;
	private SoundManager sdm;

	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
		gmObj = GameObject.Find("GameManager");
		gm = gmObj.GetComponent<GameManager>();
		sm = gmObj.GetComponent<SceneManager>();
		sdm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static IEnumerator CallCoroutineEvent(string coroutineName)
	{
		yield return EndingManager.instance.StartCoroutine(coroutineName);
		yield break;
	}

    public static void CallCoroutineEvent(object sender, GameEventArgs args)
    {
        //EndingManager.instance.gameObject.SetActive(true);
        EndingManager.instance.StartCoroutine(args.CoroutineName);
    }

	public IEnumerator DaeEnding()
	{
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		yield return new WaitForSeconds(1f);
		CreateDialogue("Dae", "\"Here. This is what you wanted. Now where is the heart you promised?\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Patricia", "\"Very well. First thing after the polls come out.\"");
		yield return null; while (Pause()) { yield return null; }
		yield return StartCoroutine(sdm.FadeOutAudioSource(sdm.currentSong, rate: 0.1f));
		sdm.PlayOtherSong("EndingMusic");
		CreateDialogue("Patricia", "\"Don't worry, it's a good deed you've done for your daughter.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Patricia", "\"I would have done the exact same thing for mine.\"");
		yield return null; while (Pause()) { yield return null; }
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide33(2)", 8, "Jerry Faraday: Thank you, thank you everybody! I'll make our county a much, much better place, turning this land into a wealthy land of gold!"), false));
		yield return StartCoroutine(sm.fade_black());
        yield return new WaitForSeconds(1f);
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: And so, it turns out that Jerry Faraday had won the election."), false, Textbox.TutorialBoxPosition.MIDDLE));
		StartCoroutine(LoadSceneCoroutine("E_Mall"));
		yield return StartCoroutine(sm.fade_out());
		CreateDialogue("Jeney", "\"So, it still happened.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Faye", "(Was it because I didn't vote?)");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Jeney", "\"Well, it's been nice having you around. You've been a great help.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Faye", "(Help with eating all your Potadonut Tots...)");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Faye", "\"I mean, my pleasure working here. I'm still sorry about the shop.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Jeney", "\"Don't worry about it. I'm sure I'll find somewhere great.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Jeney", "\"I'll definitely miss Punxsutown, though.\"");
		yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(LoadSceneCoroutine("E_Park"));
		yield return StartCoroutine(sm.fade_out());
		CreateDialogue("Yoona", "\"Alex, I just want to say...\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Alex", "\"You don't have to say anything. Just enjoy the new world!\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Hank", "\"Hey, kids! I said, be quiet!\"");
		yield return null; while (Pause()) { yield return null; }
		yield return new WaitForSeconds(1f);
		CreateDialogue("Yoona", "\"Thank you, Alex... *blush*\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Yoona", "\"And thank you, Daddy.\"");
		yield return null; while (Pause()) { yield return null; }
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Chelsey: All this time loop, was to save Alfred."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4.5f, "Chelsey: Even though Faraday was elected as senator, nobody was hurt."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: Of course, some people still had to sacrifice, just for a bit of peace."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(sm.fade_black());
		yield return new WaitForSeconds(1f);
		CreateTutorialBox(" ~ The End ~ ", standalone: true);
		yield return new WaitForSeconds(5f);
        CreateDialogue("Mr. Ly", "\"YES! OH YES, FARADAY! Mah boy! Woohoo!!\"");
        yield return null; while (Pause()) { yield return null; }
        while (true)
        {
            yield return null;
        }
        yield return null;
    }
    
    public IEnumerator AlfredEnding()
    {
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(sdm.FadeOutAudioSource(sdm.currentSong, rate: 0.1f));
		sdm.PlayOtherSong("BadThingMusic");
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide31", 4, "Alfred: Hey, Dae! What's up, buddy?"), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide32", 4, "Dae: Sorry buddy... I didn't have a choice..."), false));
		yield return StartCoroutine(sm.fade_black());
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Maybe Alfred just needs to live, but how?"), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(true);
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
		GameManager.instance.Midnight(true, 3f, false);
		yield return null;
    }

    public IEnumerator PerryEnding()
    {
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		yield return new WaitForSeconds(1f);
		CreateDialogue("Perry", "\"Faraday... I knew you were up to no good.\"");
		yield return null; while (Pause()) { yield return null; }
		yield return StartCoroutine(sdm.FadeOutAudioSource(sdm.currentSong, rate: 0.1f));
		sdm.PlayOtherSong("EndingMusic");
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide37", 4, "Perry: Chief of Police. Patricia Ferguson, you are under arrest."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide37", 4, "Patricia: No! Get your hands off me! Jerry won't forgive you for this!"), false));
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Perry: Chief of Police. Jerry Faraday, you are under arrest."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Jerry Faraday: What is this? Ouch! That hurts!"), false));
		yield return new WaitForSeconds(1f);
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: And so, it turns out that everybody was arrested."), false, Textbox.TutorialBoxPosition.MIDDLE));
		StartCoroutine(LoadSceneCoroutine("E_Police"));
		yield return StartCoroutine(sm.fade_out());
		CreateDialogue("Alfred", "\"Dae... I can't believe you did this to me.\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Alfred", "\"But I guess we're both really willing to do whatever it takes to save our loved ones...\"");
		yield return null; while (Pause()) { yield return null; }
		CreateDialogue("Dae", "\"Alfred... I'm sorry.\"");
		yield return null; while (Pause()) { yield return null; }
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(LoadSceneCoroutine("E_Hospital"));
		yield return StartCoroutine(sm.fade_out());
		CreateDialogue("Dae", "\"I just wish I could visit Yoona one more time. I don't think she has much time left.\"");
		yield return null; while (Pause()) { yield return null; }
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Chelsey: All this time loop, was to save Alfred."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Chelsey: But I guess everybody got the justice they deserved."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: Not without the sacrifices of their loved ones, of course."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(sm.fade_black());
		yield return new WaitForSeconds(1f);
		CreateTutorialBox(" ~ The End ~ ", standalone: true);
		yield return new WaitForSeconds(5f);
		CreateDialogue("Mr. Ly", "\"NO! MAH BOY, FARADAY! Why aren't you elected...!??!??\"");
		yield return null; while (Pause()) { yield return null; }
		while (true)
		{
			yield return null;
		}
		yield return null;
    }

    public IEnumerator AlfredSavedEnding()
    {
        yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(sdm.FadeOutAudioSource(sdm.currentSong, rate: 0.1f));
		sdm.PlayOtherSong("BadThingMusic");
        gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
        gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
        yield return StartCoroutine(LoadSceneCoroutine("Ending"));
        StartCoroutine(sm.fade_out());
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide34", 5, "???: ..."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: Alfred still died... he was murdered..."), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Was that a police officer? Which one...?"), false, Textbox.TutorialBoxPosition.MIDDLE));
        yield return StartCoroutine(sm.fade_black());
        gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(true);
        gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
        yield return null;
    }

	public IEnumerator AlfredFellEnding()
	{
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(sdm.FadeOutAudioSource(sdm.currentSong, rate: 0.1f));
		sdm.PlayOtherSong("BadThingMusic");
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide13", 4, "Megan: \"Alfred...!\nIf I could… if only I could go back in time…\""), false));
		yield return StartCoroutine(sm.fade_black());
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Maybe Alfred just needs to live, but how?"), false, Textbox.TutorialBoxPosition.MIDDLE));
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(true);
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
		GameManager.instance.Midnight(true, 3f, false);
		yield return null;
	}

	IEnumerator Slide_Coroutine(TutorialManager.Slide slide, bool hideMenu=true, Textbox.TutorialBoxPosition boxPos=Textbox.TutorialBoxPosition.BOTTOM)
    {
		if (hideMenu)
		{
			GameManager.instance.transform.Find("Menu_layout").gameObject.SetActive(false);
		}
        GameObject.Find("Canvas/Slide").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slide.imageName);
        if (slide.text != null && slide.text != "")
        {
            yield return new WaitForSeconds(1f);
			CreateTutorialBox(slide.text, boxPos, slide.timer - 1f);
            yield return null;
        }
        yield return new WaitForSeconds(slide.timer);
    }

	private bool Pause()
	{
		return gm.gameMode == GameManager.GameMode.DIALOGUE;
	}

	private void CreateDialogue(string name, string message)
	{
		//Choice[] choices = null;
		Dialogue d = new Dialogue(-1, Textbox.ColorTutorialKeyword(message));
		gm.CreateDialogue(name, d, -1);
	}

    public GameObject CreateTutorialBox(string message, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE, float destroyTimer = -1, bool transparent = false, bool standalone = false)
    {
        if (activeTutorialBox != null && !standalone)
        {
            Destroy(activeTutorialBox);
        }
        GameObject dialog = (GameObject)Instantiate(GameManager.instance.dialogueContainer, GameManager.instance.dialogueContainer.transform.position, Quaternion.identity);
        StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(Textbox.ColorTutorialKeyword(message), destroyTimer, position, transparent));
        if (!standalone)
        {
            activeTutorialBox = dialog;
        }
        return dialog;
    }

	IEnumerator LoadSceneCoroutine(string mapname)
	{
		Application.LoadLevel(mapname);
		//SceneManager.instance.tint_screen(mapname, gameManager.GetTimeAsInt());
		yield return null;
	}
}
