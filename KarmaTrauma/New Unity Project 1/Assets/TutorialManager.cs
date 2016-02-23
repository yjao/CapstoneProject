using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
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
        slides = new List<Slide>()
        {
            new Slide("Slide1", 5f, "Testing text")
        };
        
        StartCoroutine(Start_Slides());
	}

    IEnumerator Start_Slides()
    {
        for (int i = 0; i < slides.Count; i++)
        {
            Slide current = slides[i];
            yield return StartCoroutine(Slide_Coroutine(current));
        }
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

    public GameObject CreateTutorialBox(string message, float destroyTimer = -1, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(message, destroyTimer, position));
        return dialog;
    }

	// Update is called once per frame
	void Update()
	{
	
	}
}
