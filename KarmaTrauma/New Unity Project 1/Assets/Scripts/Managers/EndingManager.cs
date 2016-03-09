using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingManager : MonoBehaviour {
    public static EndingManager instance;
    private GameObject activeTutorialBox;

	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void CallCoroutineEvent(object sender, GameEventArgs args)
    {
        EndingManager.instance.gameObject.SetActive(true);
        EndingManager.instance.StartCoroutine(args.CoroutineName);
    }

    public IEnumerator AlfredEnding()
    {
        Application.LoadLevel("Ending");
        yield return SceneManager.instance.fade_black();
        yield return StartCoroutine(Slide_Coroutine(new TutorialManager.Slide("Slide5", 5, "OH NO DEBUGGING ENDING")));
        yield return null;
    }

    IEnumerator Slide_Coroutine(TutorialManager.Slide slide)
    {
        GameManager.instance.transform.Find("Menu_layout").gameObject.SetActive(false);
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
}
