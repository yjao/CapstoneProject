﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManager;
    //private const string SCENE_HOUSE = "T_House";
    private const string SCENE_SCHOOL = "T_Class";
    //private const string SCENE_MALL = "T_Mall";
    //private const string SCENE_MAIN_STREET = "T_MainStreet";

    private const string SCENE_MINI_MAIN_STREET = "T_MiniMainStreet";
	private const string SCENE_DONUT_SHOP = "T_Mall";

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
        //yield return StartCoroutine(Slide4_Coroutine());
        //yield return StartCoroutine(Slide8_Coroutine());
        yield return StartCoroutine(Slide4_Coroutine());

		yield return StartCoroutine(Slide11_Coroutine());
        yield break;
    }

	IEnumerator Slide_Coroutine(Slide slide)
	{
        GameObject.Find("Canvas/Slide").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slide.imageName);
        if (slide.text != null)
        {
            yield return new WaitForSeconds(1f);
            CreateTutorialBox(slide.text, slide.timer - 1f, Textbox.TutorialBoxPosition.BOTTOM);
            yield return null;
        }
        yield return new WaitForSeconds(slide.timer);
	}

    IEnumerator Slide4_Coroutine()
    {
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_MINI_MAIN_STREET));
        gameManager = GameManager.instance;
        gameManager.Wait();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(GameObject.Find("Invis").GetComponent<CharacterAnimations>().Move(1, -12.5f, CharacterAnimations.States.DOWN_WALK));

        GameObject.Find("Invis").transform.parent = GameObject.Find("Player").transform;
        gameManager.Play();
        yield break;
    }

    IEnumerator Slide8_Coroutine()
    {
        //School Scene
        yield return StartCoroutine(LoadSceneCoroutine(SCENE_SCHOOL));
        gameManager = GameManager.instance;
        gameManager.Wait();
        yield return new WaitForSeconds(1f);
        CreateDialogue("Mrs. Freewoman", "Happy Monday, class, my name is Megan Freewoman. As a reminder, %use the space bar to progress speech%.");

        yield return null;
    }

	IEnumerator Slide11_Coroutine()
	{
		/*yield return StartCoroutine(LoadSceneCoroutine(SCENE_DONUT_SHOP));
		yield return new WaitForSeconds(1);
		gameManager.CreateDialogue("Kelly", "");
		yield return null; while (Pause()) { yield return null; }*/
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

    public GameObject CreateTutorialBox(string message, float destroyTimer = -1, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(message, destroyTimer, position));
        return dialog;
    }

    private void CreateDialogue(string name, string message)
    {
        Choice[] choices = null;
        Dialogue d = new Dialogue(-1, message);
        gameManager.CreateDialogue(name, d, -1);
    }

	// Update is called once per frame
	void Update()
	{
	
	}
}
