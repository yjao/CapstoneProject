using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public static MainMenu instance;
    public string startScene;
	public int startTime;
	public enum ScreenState
	{
		MAIN, CREDITS
	};
	ScreenState screenState = ScreenState.MAIN;

    void Start()
    {
		if (instance != null)
		{
			instance.screenState = ScreenState.MAIN;
			Destroy(this.gameObject);
		}

		instance = this;
        DontDestroyOnLoad(this);
		Cursor.visible = false;

		if (startScene == null)
		{
			startScene = SceneManager.SCENE_MAINSTREET;
		}
		if (startTime == 0)
		{
			startTime = 20;
		}
    }

	void Update()
	{
		Debug.Log (screenState);
		switch (screenState)
		{
		case ScreenState.MAIN:
			if (Input.GetKeyDown(KeyCode.Space))
			{
				StartTutorial();
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				StartCredits();
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				StartGame();
			}
			break;
		case ScreenState.CREDITS:

			break;
		default:
			break;
		}
	}

	public void StartTutorial()
	{
		StartCoroutine(EnterTutorialCoroutine());
	}

    public void StartGame()
    {
		StartCoroutine(EnterGameCoroutine());
    }

	public void StartCredits()
	{
		StartCoroutine(EnterCreditsCoroutine());
	}

	IEnumerator EnterTutorialCoroutine()
	{
		SceneManager.instance.fade_black();
		SceneManager.instance.LoadScene(SceneManager.SCENE_TUTORIAL);
		
		Destroy(this);
		yield break;
	}

	IEnumerator EnterGameCoroutine()
	{
		SceneManager.instance.fade_black();
		GameManager.instance.SetTime(GameManager.TimeType.SET, startTime);
		GameManager.instance.MenuLayout.SetActive(true);
		SceneManager.instance.LoadScene(startScene);

		Destroy(this);
		yield break;
	}

	IEnumerator EnterCreditsCoroutine()
	{
		screenState = ScreenState.CREDITS;
		SceneManager.instance.LoadScene(SceneManager.SCENE_CREDITS);
		yield return null;
		GameManager.instance.Play();
		yield break;
	}
}