using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManager;
    //private const string SCENE_HOUSE = "T_House";
    //private const string SCENE_SCHOOL = "T_Class";
    //private const string SCENE_MALL = "T_Mall";
    //private const string SCENE_MAIN_STREET = "T_MainStreet";

    private const string SCENE_MINI_MAIN_STREET = "T_MiniMainStreet";

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
        DontDestroyOnLoad(this.gameObject);
        //gameManager = GameManager.instance;
        slides = new List<Slide>()
        {
			new Slide("Slide1", 1f, "\"Hey Kelly! Help us out here!\""),
            new Slide("Slide1", 1f, "\"Yeah Chelsey, go help 'em. I'm too lazy to go.\""),
            new Slide("Slide1", 1f, "\"Alright...\"")
        };

		StartCoroutine(Start_Tutorial());
	}

    IEnumerator Start_Tutorial()
    {
		yield return StartCoroutine(Slide_Coroutine(slides[0]));
		//yield return StartCoroutine(Slide_Coroutine(slides[1]));
        //yield return StartCoroutine(Slide_Coroutine(slides[2]));
        yield return StartCoroutine(LoadMiniMainStreet());
        yield return StartCoroutine(Slide4_Coroutine());
        yield break;
    }

	IEnumerator Slide_Coroutine(Slide slide)
	{
        GameObject.Find("Canvas/Slide").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slide.imageName);
        if (slide.text != null)
        {
            yield return new WaitForSeconds(1f);
            CreateTutorialBox(slide.text, Textbox.TutorialBoxPosition.BOTTOM, slide.timer - 1f);
            yield return null;
        }
        yield return new WaitForSeconds(slide.timer);
	}

    IEnumerator Slide4_Coroutine()
    {
        gameManager = GameManager.instance;
        gameManager.Wait();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(GameObject.Find("Invis").GetComponent<CharacterAnimations>().Move(1, -12.5f, CharacterAnimations.States.DOWN_WALK));

        GameObject.Find("Invis").transform.parent = GameObject.Find("Player").transform;
        CreateTutorialBox("What’s wrong? Have you forgotten how to walk? Haha, you’re so awkward Chels! It’s why I love ya. #Use the arrow keys or WASD keys to move# your butt. Now go #get the ball#!", Textbox.TutorialBoxPosition.BOTTOM);
        gameManager.Play();
        yield break;
    }

    public IEnumerator Slide4_Triggers_Coroutine(string tag)
    {
        if (tag == "TrafficLight")
        {
            CreateTutorialBox("Um, yeah, you might want to hit the pedestrian light before crossing. What? Hey, are you spacing out again? #Space bar#, not space out! Press that and like, #interact with objects#.", Textbox.TutorialBoxPosition.BOTTOM);
        }
        else if (tag == "Kelly")
        {
            CreateTutorialBox("What is it? Come on, get your nose out of your book and go exercise a bit! Go, #get the ball#! For me!", Textbox.TutorialBoxPosition.BOTTOM);
        }
        else if (tag == "traffic")
        {
            CreateTutorialBox("Hrm… I guess it doesn’t work. Well it’s whatever, I don’t see any cops; just be careful! If I were you, I’d #move while holding shift key to run#. The cars are less likely to hit you if you run faster, right?", Textbox.TutorialBoxPosition.BOTTOM);
        }
        yield break;
    }

    IEnumerator LoadMiniMainStreet()
    {
        Application.LoadLevel(SCENE_MINI_MAIN_STREET);
        yield return null;
    }


    public void CreateTutorialBox(string message, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE, float destroyTimer = -1)
    {
        if (activeTutorialBox != null)
        {
            Destroy(activeTutorialBox);
        }
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(message, destroyTimer, position));
        activeTutorialBox = dialog;
    }

	// Update is called once per frame
	void Update()
	{
	
	}
}
