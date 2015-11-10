﻿using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
	private GameManager gameManager;
	private const string SCENE_PROLOGUE = "Prologue";
	private const string SCENE_HOUSE = "PrologueHouse";
    private const string SCHOOL = "RaeClass";
    private const string STORY = "Story";
    private const string MALL = "MallPrologue";
    private const string MAIN_STREET = "MainStreetFalling";
    
	public int StoryProgress;
    public string[] AllScenes;

	void Start()
	{
		gameManager = GameManager.Instance;
		DontDestroyOnLoad(this);

        AllScenes = new string[5] {"Story", "School", "Mall", "Main_street", "Home"};

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
        while (true) { if (Input.GetKey(KeyCode.Space)) break; yield return null; }

		gameManager.CreateMessage("YOU TWISTED FATE!");
        yield return null; while (Pause()) { yield return null; }
        
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(2);
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }

        while (true)
        {
            if (Application.loadedLevelName == SCHOOL)
            {
                MoveToNext();
                yield break;
            }
            yield return null;
        }
  
        //yield break;
    }


    public IEnumerator School()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(SCHOOL);
        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(65, 1);
        yield return null; while (Pause()) { yield return null; }

        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 2);
        yield return null; while (Pause()) { yield return null; }
        Destroy(GameObject.Find("MrLy"));
        yield return new WaitForSeconds(1);
        gameManager.DBox(66, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(66, 1);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 1);
        yield return null; while (Pause()) { yield return null; }
        
        
        MoveToNext();
        yield break;

    }

    public IEnumerator Mall()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(MALL);
        yield return new WaitForSeconds(1);
        gameManager.DBox(66, 2);
        yield return null; while (Pause()) { yield return null; }
        //for (int i = 0; i < 30; i++)
        //{
        //    GameObject.Find("Kelly").transform.Translate(new Vector2(0.05f, 0f));
        //    yield return null;
        //}
        //gameManager.DBox(66, true);
        //yield return null; while (Pause()) { yield return null; }
        //for (int i = 0; i < 150; i++)
        //{
        //    GameObject.Find("Kelly").transform.Translate(new Vector2(0.05f, 0f));
        //    yield return null;
        //}
        MoveToNext();
        yield break;
        
    }

    public IEnumerator Main_street()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(MAIN_STREET);
        yield return new WaitForSeconds(1);
        gameManager.DBox(66, 3);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 2);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(66, 4);
        yield return null; while (Pause()) { yield return null; }

        MoveToNext();
        yield break;
        
    }

    public IEnumerator Home()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(1);
        gameManager.DBox(1, 3);
        yield return null; while (Pause()) { yield return null; }
      
        yield break;

    }

    public void MoveToNext()
    {
        StoryProgress += 1;
        StartCoroutine(AllScenes[StoryProgress]);
    }
}