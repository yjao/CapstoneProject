﻿using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
    private GameManager gameManager;
    private const string SCENE_PROLOGUE = "Prologue";
    private const string STORY = "Story";
    private const string SCENE_HOUSE = "P_House";
    private const string SCENE_SCHOOL = "P_Class";
    private const string SCENE_MALL = "P_Mall";
    private const string SCENE_MAIN_STREET = "P_MainStreet";


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

        AllScenes = new string[8] { "Story_0", "School_0", "Mall_0", "Main_street_0", "Home_0", "Home_1", "School_1", "Main_street_1" };

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

    /*
	private IEnumerator MovePlayer(GameObject character, int animatorDirection, float newPositionValue, float speed=0.02f)
	{
        Animator animator = character.GetComponent<Animator>();
		switch (animatorDirection)
		{
		case left:
			while (character.transform.position.x >= newPositionValue)
			{
                character.transform.Translate(-speed, 0.0f, 0.0f);
                animator.SetInteger(animationState, left);
				yield return null;
			}
			break;
		case right:
            while (character.transform.position.x <= newPositionValue)
			{
                character.transform.Translate(speed, 0.0f, 0.0f);
                animator.SetInteger(animationState, right);
				yield return null;
			}
			break;
		case down:
            while (character.transform.position.y >= newPositionValue)
			{
                character.transform.Translate(0.0f, -speed, 0.0f);
                animator.SetInteger(animationState, down);
				yield return null;
			}
			break;
		case up:
            while (character.transform.position.y <= newPositionValue)
			{
                character.transform.Translate(0.0f, speed, 0.0f);
                animator.SetInteger(animationState, up);
				yield return null;
			}
			break;
		}
		yield break;
	}*/

    public IEnumerator Story()
    {
        //gameManager.Play();
        gameManager.Wait();
        gameManager.GetComponent<Menu_Layout>().enabled = false;

        // Get Space
        while (true) { if (Input.GetKey(KeyCode.Space)) break; yield return null; }
        gameManager.CreateMessage("YOU TWISTED FATE!");
        yield return null; while (Pause()) { yield return null; }

        // Loading the Scene: At House (Mom wakes you up)
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(2);

        // Mom's dialogue appears
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }

        gameManager.GetComponent<Menu_Layout>().enabled = true;

        gameManager.Play();
        while (true)
        {
            if (Application.loadedLevelName == SCENE_SCHOOL)
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
        gameManager.Wait();
        
        yield return new WaitForSeconds(1);
        // yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_SCHOOL);
        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(65, 1);
        yield return null; while (Pause()) { yield return null; }

        // Chelsey walks to her desk
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(down, 2.45f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(right, 0.24f, CharacterAnimations.States.RIGHT_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(down, 1.31f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(left, -0.49f, CharacterAnimations.States.LEFT_WALK));
        GameObject.Find("Player").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.UP_IDLE);


        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 2);
        yield return null; while (Pause()) { yield return null; }
        Destroy(GameObject.Find("MrLy"));
        yield return new WaitForSeconds(1);

        // STUDENTS LEAVE

        /*
        Move Stylish_Guy1;
        Stylish_Guy1.direction = left;
        Stylish_Guy1.position = -1.25f;
        Stylish_Guy1.state = CharacterAnimations.States.LEFT_WALK;

        yield return StartCoroutine(GameObject.Find("Stylish_guy").GetComponent<CharacterAnimations>().Move(left, -1.25f, CharacterAnimations.States.LEFT_WALK));
        yield return StartCoroutine(GameObject.Find("Stylish_guy").GetComponent<CharacterAnimations>().Move(down, -1.10f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(GameObject.Find("Stylish_guy").GetComponent<CharacterAnimations>().Move(left, -3.25f, CharacterAnimations.States.LEFT_WALK));
        yield return StartCoroutine(GameObject.Find("Stylish_guy").GetComponent<CharacterAnimations>().Move(up, 2.5f, CharacterAnimations.States.UP_WALK));
        yield return StartCoroutine(GameObject.Find("Stylish_guy").GetComponent<CharacterAnimations>().Move(left, -6.0f, CharacterAnimations.States.LEFT_WALK));


        yield return StartCoroutine(GameObject.Find("Girl_in_drama").GetComponent<CharacterAnimations>().Move(right, 3.4f, CharacterAnimations.States.RIGHT_WALK));
        yield return StartCoroutine(GameObject.Find("Girl_in_drama").GetComponent<CharacterAnimations>().Move(up, 2.5f, CharacterAnimations.States.UP_WALK));
        yield return StartCoroutine(GameObject.Find("Girl_in_drama").GetComponent<CharacterAnimations>().Move(up, 2.5f, CharacterAnimations.States.UP_WALK));
        yield return StartCoroutine(GameObject.Find("Girl_in_drama").GetComponent<CharacterAnimations>().Move(left, -6.0f, CharacterAnimations.States.LEFT_WALK));
        */


        // Kelly talks
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_WALK);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(right, -1.00f, CharacterAnimations.States.RIGHT_WALK));
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_IDLE);

        GameObject.Find("Player").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.LEFT_IDLE);
        gameManager.DBox(66, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(66, 1);
        yield return null; while (Pause()) { yield return null; }

        gameManager.DBox(1, 1);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.LEFT_DANCE));
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().PlayAnimation(CharacterAnimations.States.RIGHT_DANCE));

        yield return null; while (Pause()) { yield return null; }

        MoveToNext();
        yield break;

    }

    public IEnumerator Mall_0()
    {
        gameManager.IncreaseTime();
        yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_MALL);
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
        Application.LoadLevel(SCENE_MAIN_STREET);
        gameManager.IncreaseTime();
        yield return new WaitForSeconds(1);
        Player.Instance.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_IDLE);

        gameManager.DBox(66, 3);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 2);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(66, 4);
        //yield return null; while (Pause()) { yield return null; }
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(right, 30.00f, CharacterAnimations.States.RIGHT_WALK));
        //GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_WALK);
        Destroy(GameObject.Find("Kelly"));

        gameManager.Play();

        while (Player.Instance.transform.position.x > 10)
        {
            yield return null;
        }

        gameManager.Wait();

        while (!GameObject.Find("Alfred").GetComponent<Falling>().HasFallen())
        {
            Player.Instance.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_IDLE);
            yield return null;
        }

        gameManager.Play();

        while (true)
        {
            if (Application.loadedLevelName == SCENE_HOUSE)
            {
                MoveToNext();
                yield break;
            }
            yield return null;
        }

    }

    public IEnumerator Home_0()
    {
        gameManager.Wait();
        yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_HOUSE);
        yield return new WaitForSeconds(1);
        gameManager.DBox(1, 3);
        yield return null; while (Pause()) { yield return null; }

        MoveToNext();
        yield break;
    }

    //Need to fade to black:  Start of DAY 1
    public IEnumerator Home_1()
    {
        gameManager.CreateMessage("The next day...");
        gameManager.ResetTime();
        yield return null; while (Pause()) { yield return null; }

        yield return new WaitForSeconds(1);
        gameManager.DBox(21, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(21, true);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 4);
        yield return null; while (Pause()) { yield return null; }

        gameManager.Play();
        while (true)
        {
            if (Application.loadedLevelName == SCENE_SCHOOL)
            {
                MoveToNext();
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator School_1()
    {
        gameManager.Wait();
        // yield return new WaitForSeconds(1);
        Application.LoadLevel(SCENE_SCHOOL);
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

        // Chelsey walks to her desk
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(down, 2.45f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(right, 0.24f, CharacterAnimations.States.RIGHT_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(down, 1.31f, CharacterAnimations.States.DOWN_WALK));
        yield return StartCoroutine(GameObject.Find("Player").GetComponent<CharacterAnimations>().Move(left, -0.49f, CharacterAnimations.States.LEFT_WALK));
        GameObject.Find("Player").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.UP_IDLE);

        yield return new WaitForSeconds(1);
        gameManager.DBox(65, 2);
        yield return null; while (Pause()) { yield return null; }
        Destroy(GameObject.Find("MrLy"));
        yield return new WaitForSeconds(1);

        // Kelly talks
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_WALK);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(right, -1.00f, CharacterAnimations.States.RIGHT_WALK));
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.RIGHT_IDLE);

        GameObject.Find("Player").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.LEFT_IDLE);

        gameManager.DBox(66, 0);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 6);
        yield return null; while (Pause()) { yield return null; }

        gameManager.DBox(66, 1);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(1, 7);
        yield return null; while (Pause()) { yield return null; }
        gameManager.DBox(66, 5);
        yield return null; while (Pause()) { yield return null; }

        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.LEFT_WALK);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(left, -3.00f, CharacterAnimations.States.LEFT_WALK));
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.UP_WALK);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(up, 2.50f, CharacterAnimations.States.UP_WALK));
        GameObject.Find("Kelly").GetComponent<NPC>().SetAnimation(CharacterAnimations.States.LEFT_WALK);
        yield return StartCoroutine(GameObject.Find("Kelly").GetComponent<CharacterAnimations>().Move(left, -6.00f, CharacterAnimations.States.LEFT_WALK));

        gameManager.DBox(1, 8);
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
        Application.LoadLevel(SCENE_MAIN_STREET);
        gameManager.IncreaseTime();
        yield return new WaitForSeconds(0);
        Destroy(GameObject.Find("Kelly"));
        gameManager.DBox(1, 9);
        yield return null; while (Pause()) { yield return null; }

        GameObject.Find("Door").GetComponent<Door>().AltDestination = "WorldMap";

        gameManager.Play();
        while (true)
        {
            /*if (Application.loadedLevelName == SCENE_HOUSE)
            {
                MoveToNext();
                yield break;
            }*/
            yield return null;
        }

        MoveToNext();
        yield break;
    }

    public IEnumerator EndPrologue()
    {
        Destroy(this);
        yield break;
    }

    public void MoveToNext()
    {
        StoryProgress += 1;
        StartCoroutine(AllScenes[StoryProgress]);
    }
}