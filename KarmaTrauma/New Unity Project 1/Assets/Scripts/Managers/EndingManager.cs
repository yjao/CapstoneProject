﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingManager : MonoBehaviour {
    public static EndingManager instance;
    private GameObject activeTutorialBox;

	private GameObject gmObj;
	private GameManager gm;
	private SceneManager sm;

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
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		yield return sm.fade_black();
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide33(2)", 20, "Jerry Faraday: Thank you, thank you everybody! I'll make our county a much, much better place, turning this land into a wealthy land of gold!")));
		yield return null;
    }

    public IEnumerator AlfredEnding()
    {
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
		yield return StartCoroutine(LoadSceneCoroutine("Ending"));
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide31", 4, "Alfred: Hey, Dae! What's up, buddy?"), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide32", 4, "Dae: Sorry buddy... I didn't have a choice..."), false));
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Maybe Alfred just needs to live, but how?"), false));
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
		StartCoroutine(sm.fade_out());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide37", 4, "Perry: Chief of Police. Patricia Ferguson, you are under arrest."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide37", 4, "Patricia: No! Get your hands off me! Jerry won't forgive you for this!"), false));
		yield return StartCoroutine(sm.fade_black());
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Perry: Chief of Police. Jerry Faraday, you are under arrest."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Jerry Faraday: What is this? Ouch! That hurts!"), false));
		yield return new WaitForSeconds(1f);
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: And so, it turns out that everybody was arrested."), false));
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
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Chelsey: All this time loop, was to save Alfred."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 4, "Chelsey: But I guess everybody got the justice they deserved."), false));
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide7", 5, "Chelsey: Not without the sacrifices of their loved ones, of course."), false));
		yield return StartCoroutine(sm.fade_black());
		yield return new WaitForSeconds(1f);
		CreateTutorialBox(" ~ The End ~ ", standalone: true);
		yield return new WaitForSeconds(5f);
		CreateDialogue("Mr. Ly", "\"NO! MAH BOI, FARADAY! Why aren't you elected...!??!??\"");
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
        gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
        gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
        yield return StartCoroutine(LoadSceneCoroutine("Ending"));
        StartCoroutine(sm.fade_out());
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide34", 5, "???: ..."), false));
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Was that a police officer? Which one...?"), false));
        yield return StartCoroutine(sm.fade_black());
        gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(true);
        gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
        yield return null;
    }

    IEnumerator Slide_Coroutine(TutorialManager.Slide slide, bool hideMenu=true)
    {
		if (hideMenu)
		{
			GameManager.instance.transform.Find("Menu_layout").gameObject.SetActive(false);
		}
        GameObject.Find("Canvas/Slide").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slide.imageName);
        if (slide.text != null && slide.text != "")
        {
            yield return new WaitForSeconds(1f);
            CreateTutorialBox(slide.text, Textbox.TutorialBoxPosition.BOTTOM, slide.timer - 1f);
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
