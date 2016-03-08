using UnityEngine;
using System.Collections;

public class CreditsDoor : MonoBehaviour
{
	public AudioSource soundtrack;
	private GameManager gameManager;
	private bool colliding = false;

	void Start()
	{
		DontDestroyOnLoad(this);
		gameManager = GameManager.instance;
	}
	
	void Update()
	{
		if (colliding == true && Input.GetKeyDown(KeyCode.Space) && gameManager.gameMode != GameManager.GameMode.NONE)
		{
			colliding = false;
			StartCoroutine(GoBackToMainMenu());
		}
	}

	IEnumerator GoBackToMainMenu()
	{
		yield return StartCoroutine(SceneManager.instance.fade_black());
		yield return StartCoroutine(SoundManager.instance.FadeOutAudioSource(soundtrack, rate: 0.1f));
		Application.LoadLevel(SceneManager.SCENE_TITLESCREEN);
		yield return StartCoroutine(SceneManager.instance.fade_out());

		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			colliding = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
			colliding = false;
	}

}
