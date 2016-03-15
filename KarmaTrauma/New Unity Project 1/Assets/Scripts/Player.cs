using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
	public GameObject worldMapManager;
    public bool t_mall = false;
    public static InvisibleWallTop walltop;
    private GameManager gameManager;
    public GameObject PlayerCamera;
    public float speed;
    public GameObject Menu;
    public GameObject QuestLog;
    public CharacterAnimations characterAnimations;
    public string locationString;
    private AudioSource source;
    private TutorialManager tutorialManager;
    public float WalkVolume;
    public static Player Instance;

    // might be useful later on, can change to List<GameObject>?
    //public List<int> CollidingWithID;

	public const bool SPEED_BOOST_ALLOWED = true;
	private const float SPEED_BOOST = 4f;

    private bool speeding = false;

    //GameObject saveMachine = GameObject.AddComponent<SaveData>();

    void Awake()
    {
        /*if ((Instance != null) && (Instance != this))
            Destroy(gameObject);
        else
            Instance = this;
		
        DontDestroyOnLoad(this);*/

        //CollidingWithID = new List<int>();
    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        walltop = FindObjectOfType(typeof(InvisibleWallTop)) as InvisibleWallTop;
        gameManager = GameManager.instance;
        tutorialManager = TutorialManager.instance;
		EventManager.OnNPC += HandleNPC;
        source = GetComponent<AudioSource>();
        source.volume = WalkVolume;
        if (source.volume == 0)
            source.volume = 0.5f;
    }

    void OnDestroy()
    {
        EventManager.OnNPC -= HandleNPC;
    }

    void HandleNPC(object sender, GameEventArgs args)
    {
        characterAnimations.AnimationState = (args.AnimationState);
    }

	#region OnHit

    public void onHitTop(float wall_y)
    {
        //Debug.Log("hittingTop");
        float charposy = this.transform.position.y;
        float charposx = this.transform.position.x;
        //Debug.Log("char " + charposy + "wally: " + wall_y);
        if (charposy >= wall_y)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(charposx, wall_y);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, up);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);

            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.Translate(0, -speed, 0);
                //animator.SetInteger(animationState, down);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            }
        }
    }

    public void onHitBot(float wall_y)
    {
        //Debug.Log("hittingTop");
        float charposy = this.transform.position.y;
        float charposx = this.transform.position.x;
        //Debug.Log("char " + charposy + "wally: " + wall_y);
        if (charposy <= wall_y)
        {

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(charposx, wall_y);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, down);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Translate(0, speed, 0);
                //animator.SetInteger(animationState, up);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);
            }
        }
    }

    public void onHitRight(float wall_x)
    {
        //Debug.Log("hittingTop");
        float charposy = this.transform.position.y;
        float charposx = this.transform.position.x;
        Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x);
        if (charposx >= wall_x)
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x);
                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(wall_x, charposy);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, right);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_WALK);
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("here");
                transform.Translate(-speed, 0, 0);
                //                animator.SetInteger(animationState, left);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            }
        }
    }

    public void onHitLeft(float wall_x)
    {
        //Debug.Log("hittingTop");
        float charposy = this.transform.position.y;
        float charposx = this.transform.position.x;
        //Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x); 
        if (charposx <= wall_x)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x);
                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(wall_x, charposy);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, left);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("here");
                transform.Translate(speed, 0, 0);
                //animator.SetInteger(animationState, left);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            }
        }
    }

	#endregion
    void OnTriggerEnter2D(Collider2D coll)
    {
		if (gameManager.gameMode != GameManager.GameMode.PLAYING)
		{
			return;
		}

		MapLocation location = coll.GetComponent<MapLocation>();
		if (worldMapManager != null && location != null)
		{
			worldMapManager.GetComponent<WorldMapManager>().ReadyLocation(location.locationString);
		}
    }
    void OnTriggerExit2D(Collider2D coll)
    {
		if (gameManager.gameMode != GameManager.GameMode.PLAYING)
		{
			return;
		}

		if (worldMapManager != null)
		{
			worldMapManager.GetComponent<WorldMapManager>().ReadyLocation(WorldMapManager.LocationString.NONE);
		}
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (gameManager.gameMode != GameManager.GameMode.PLAYING)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			characterAnimations.SetIdle();
            return;
        }
		
        // Speeding up!
        if (SPEED_BOOST_ALLOWED)
		{
			SpeedUp();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //transform.Translate(0, speed, 0);
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            //animator.SetInteger(animationState, up);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //transform.Translate(0, -speed, 0);
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            //animator.SetInteger(animationState, down);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            //transform.Translate(speed, 0, 0);
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            //animator.SetInteger(animationState, right);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //transform.Translate(-speed, 0, 0);
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            //animator.SetInteger(animationState, left);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
           
        // Entering World Map Location 
        else if (Input.GetKeyDown(KeyCode.Space))
        {
			// Cooldown procedure
			//if (!gameManager.CheckKeyCooldown()) { return; } else { gameManager.SetKeyCooldown(); }
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (worldMapManager != null)
			{
				worldMapManager.GetComponent<WorldMapManager>().EnterLocation();
			}
        }


        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            characterAnimations.SetIdle();
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

    }

	private void SpeedUp()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			if (!speeding)
			{
				speeding = true;
				speed += SPEED_BOOST;
				characterAnimations.animationSpeed -= .1f;
                source.pitch += .3f;
			}
		}
		else
		{
			if (speeding)
			{
				speeding = false;
				speed -= SPEED_BOOST;
				characterAnimations.animationSpeed += .1f;
                source.pitch -= .3f;
			}
		}
	}

}