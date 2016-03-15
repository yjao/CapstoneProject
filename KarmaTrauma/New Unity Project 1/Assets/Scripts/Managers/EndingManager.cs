using UnityEngine;
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
		yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide14", 5, "Chelsey: ...Maybe Alfred just needs to live, but how?"), false));
		yield return StartCoroutine(sm.fade_black());
		gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(true);
		gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
		GameManager.instance.Midnight(true, 3f, false);
		yield return null;
    }

    public IEnumerator PerryEnding()
    {
        Application.LoadLevel("Ending");
        yield return SceneManager.instance.fade_black();
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide37", 20, "Patricia: No! Get your hands off me! Jerry won't forgive you for this!")));
        yield return null;
    }

    public IEnumerator AlfredSavedEnding()
    {
        yield return StartCoroutine(sm.fade_black());
        gm.MenuLayout.GetComponent<Menu_Layout>().GameMenus(false);
        gm.MenuLayout.GetComponent<Menu_Layout>().timeTint.SetActive(false);
        yield return StartCoroutine(LoadSceneCoroutine("Ending"));
        StartCoroutine(sm.fade_out());
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide34", 5, "..."), false));
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
