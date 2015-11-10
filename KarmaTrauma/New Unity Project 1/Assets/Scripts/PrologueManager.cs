using UnityEngine;
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


    // PLAYER STUFF 
    Animator animator;
    const int idle = 0;
    const int up = 1;
    const int down = 2;
    const int right = 3;
    const int left = 4;

    const int upIdle = 5;
    const int downIdle = 6;
    const int rightIdle = 7;
    const int leftIdle = 8;

    const string animationState = "AnimationState";

    bool u = false;
    bool d = false;
    bool r = false;
    bool l = false;
    // END OF PLAYER STUFF


    public int StoryProgress;
    public string[] AllScenes;

	void Start()
	{
		gameManager = GameManager.Instance;
		DontDestroyOnLoad(this);

        AllScenes = new string[8] {"Story_0", "School_0", "Mall_0", "Main_street_0", "Home_0", "Home_1", "School_1", "Main_street_1"};

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


        //Debug.Log(gameManager.GameMode);
	}

    private bool Pause()
    {
        return gameManager.GameMode == GameManager.MODE.DIALOGUE;
    }

	public IEnumerator Story()
	{
        gameManager.Play();

        // Get Space
        while (true) { if (Input.GetKey(KeyCode.Space)) break; yield return null; }

		gameManager.CreateMessage("YOU TWISTED FATE!");
        yield return null; while (Pause()) { yield return null; }

        // Loading the Scene: At House (Mom wakes you up)
        Application.LoadLevel(SCENE_HOUSE);
        gameManager.Wait();
        yield return new WaitForSeconds(2);

        // Mom's dialogue appears
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }

        while (true)
        {
            gameManager.Play();
            if (Application.loadedLevelName == SCHOOL)
            {
                MoveToNext();
                yield break;
            }
            yield return null;
        }
  
        //yield break;
    }


    public IEnumerator School_0()
    {
<<<<<<< HEAD
        gameManager.Wait();
        yield return new WaitForSeconds(1);
=======
       // yield return new WaitForSeconds(1);
>>>>>>> ab4197583e24e45edcee93230ea739946d653ea9
        Application.LoadLevel(SCHOOL);
        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(65, 1);
        yield return null; while (Pause()) { yield return null; }


        while(Player.Instance.transform.position.y >= -2.18)
        {
            Player.Instance.transform.Translate(0.0f, -0.02f, 0.0f);
            Player.Instance.animator.SetInteger(animationState, down);
            yield return null;
        }
        while (Player.Instance.transform.position.y <= -2.18 && Player.Instance.transform.position.x > -0.6)
        {
            Player.Instance.transform.Translate(-0.02f, 0.0f, 0.0f);
            Player.Instance.animator.SetInteger(animationState, left);
            yield return null;

        }

        Player.Instance.animator.SetInteger(animationState, upIdle);

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

    public IEnumerator Mall_0()
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

    public IEnumerator Main_street_0()
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

    public IEnumerator Home_0()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(1);
        gameManager.DBox(1, 3);
        yield return null; while (Pause()) { yield return null; }

        MoveToNext();
        yield break;

    }

    //Need to fade to black:  Start of DAY 1
    public IEnumerator Home_1(){
        yield return new WaitForSeconds(1);
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 4);
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
    }

    public IEnumerator School_1()
    {
        // yield return new WaitForSeconds(1);
        Application.LoadLevel(SCHOOL);
        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 5);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(65, 3);
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
        gameManager.DBox(1, 6);
        yield return null; while (Pause()) { yield return null; }

        gameManager.DBox(66, 1);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 7);
        yield return null; while (Pause()) { yield return null; }


        MoveToNext();
        yield break;

    }

    /// <summary>
    ///Allow player to move around at this point and then continue with prologue.
    /// </summary>

    public IEnumerator Main_street_1()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(MAIN_STREET);

        yield return new WaitForSeconds(1);
        gameManager.DBox(1, 9);
        yield return null; while (Pause()) { yield return null; }

        MoveToNext();
        yield break;

    }
    public void MoveToNext()
    {
        StoryProgress += 1;
        StartCoroutine(AllScenes[StoryProgress]);
    }
}