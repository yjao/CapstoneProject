using UnityEngine;
using System.Collections;

public class ShortPrologue : MonoBehaviour
{
	public GameObject[] slideshow;
	private int currentSlide;
	private int slideshowSize;
	private GameObject lastSlide;

	void Start()
	{
		foreach (GameObject slide in slideshow)
		{
			slide.SetActive(false);
		}

		currentSlide = 0;
		slideshowSize = slideshow.GetLength(0);
		lastSlide = slideshow[slideshowSize-1];
		DontDestroyOnLoad(lastSlide);
		DontDestroyOnLoad(this);
	}
	
	void Update()
	{
		if (currentSlide >= slideshowSize)
		{
			StartCoroutine("ExitPrologue");
			return;
		}

		slideshow[currentSlide].SetActive(true);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentSlide++;
		}
	}

	IEnumerator ExitPrologue()
	{
		Application.LoadLevel(SceneManager.SCENE_MAINSTREET);
		yield return null;

		GameManager.instance.SetTime(20);
		yield return null;

		SceneManager.instance.LoadScene(SceneManager.SCENE_MAINSTREET);
		yield return null;

		Destroy(lastSlide);
		Destroy(this);
		yield break;
	}
}
