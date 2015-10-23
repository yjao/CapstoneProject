using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
	private GameManager gameManager;
	private const string SCENE_PROLOGUE = "Prologue";
	private const string SCENE_HOUSE = "House in Development";
	public int StoryProgress;

	void Start()
	{
		gameManager = GameManager.Instance;
		DontDestroyOnLoad(this);
	}

	bool Progress(int value)
	{
		return StoryProgress == value;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			switch (Application.loadedLevelName)
			{
			case SCENE_PROLOGUE:
				if (Progress(0))
					gameManager.CreateDialogue("", "YOU TWISTED FATE!");
				else if (Progress(1))
					Application.LoadLevel(SCENE_HOUSE);
				break;
			}
			StoryProgress++;
		}
	}
}
