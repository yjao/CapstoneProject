using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioClip select;
    private AudioSource source;

	public static MainMenu instance;
	public string startScene;
	public int startTime;
	public enum ScreenState
	{
		MAIN, CREDITS, GAME
	};
	ScreenState screenState = ScreenState.MAIN;

	private AudioSource soundtrack()
	{
		return GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}

    void Start()
    {
        source = GetComponent<AudioSource>();

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
		switch (screenState)
		{
		case ScreenState.MAIN:
			if (Input.GetKeyDown(KeyCode.Space))
			{
                source.PlayOneShot(select, 1);
				StartGame();
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
                source.PlayOneShot(select, 1);
				StartCredits();
			}
			else if (Input.GetKeyDown(KeyCode.N))
			{
                source.PlayOneShot(select, 1);
				StartTutorial();
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				source.PlayOneShot(select, 1);
				StartAlfred();
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

	public void StartAlfred()
	{
		StartCoroutine(EnterAlfredCoroutine());
	}

	IEnumerator EnterTutorialCoroutine()
	{
		screenState = ScreenState.GAME;
		StartCoroutine(SoundManager.instance.FadeOutAudioSource(soundtrack(), rate: 0.1f));
		yield return StartCoroutine(SceneManager.instance.LoadSceneCoroutine(SceneManager.SCENE_TUTORIAL));
		
		Destroy(this.gameObject);
		yield break;
	}

	IEnumerator EnterGameCoroutine()
	{
		screenState = ScreenState.GAME;
		GameManager.instance.SetTime(GameManager.TimeType.SET, startTime);
		StartCoroutine(SoundManager.instance.FadeOutAudioSource(soundtrack(), rate: 0.1f));
        GameManager.instance.LoadPlayerData();
		yield return StartCoroutine(SceneManager.instance.LoadSceneCoroutine(startScene));
		GameManager.instance.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);

		Destroy(this.gameObject);
		yield break;
	}

	IEnumerator EnterAlfredCoroutine()
	{
		screenState = ScreenState.GAME;
		GameManager.instance.SetTime(GameManager.TimeType.SET, 20);
		StartCoroutine(SoundManager.instance.FadeOutAudioSource(soundtrack(), rate: 0.1f));
		yield return StartCoroutine(SceneManager.instance.LoadSceneCoroutine("G_MainStreet"));
		GameManager.instance.MenuLayout.GetComponent<Menu_Layout>().GameMenus(true);
		
		Destroy(this.gameObject);
		yield break;
	}

	IEnumerator EnterCreditsCoroutine()
	{
		screenState = ScreenState.CREDITS;
		yield return StartCoroutine(SceneManager.instance.fade_black());
		yield return StartCoroutine(SoundManager.instance.FadeOutAudioSource(soundtrack(), rate: 0.1f));
		Application.LoadLevel(SceneManager.SCENE_CREDITS);
		yield return StartCoroutine(SceneManager.instance.fade_out());
		yield return null;
		GameManager.instance.Play();
		yield break;
	}
}