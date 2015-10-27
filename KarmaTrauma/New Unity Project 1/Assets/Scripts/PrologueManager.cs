using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
	private GameManager gameManager;
	private const string SCENE_PROLOGUE = "Prologue";
	private const string SCENE_HOUSE = "House in development";
    private const string STORY = "Story";
	public int StoryProgress;

	void Start()
	{
		gameManager = GameManager.Instance;
		DontDestroyOnLoad(this);

        StartCoroutine(STORY);
	}

	bool Progress(int value)
	{
		return StoryProgress == value;
	}

	void Update()
	{
        /*if (Input.GetKey(KeyCode.Space))
		{
			switch (Application.loadedLevelName)
			{
			case SCENE_PROLOGUE:
				if (Progress(0))
					gameManager.CreateDialogue("", "YOU TWISTED FATE!");
				else if (Progress(1))
				{
					Application.LoadLevel(SCENE_HOUSE);
					gameManager.DBox(21, 0);
				}
				break;
			case SCENE_HOUSE:
				if (Progress(3))
				{

				}
				break;
			}
			StoryProgress++;
		}*/
	}

    private bool Pause()
    {
        return gameManager.GameMode != GameManager.MODE.PLAYING;
    }

	public IEnumerator Story()
	{
        gameManager.Play();

        // Get Space
        while (true) { if (Input.GetKey(KeyCode.S)) break; yield return null; }

        gameManager.CreateDialogue("", "YOU TWISTED FATE!");
        yield return null; while (Pause()) { yield return null; }
        
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(2);
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }

        yield break;
    }

}