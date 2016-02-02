using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public static InvisibleWallTop walltop;
    private GameManager gameManager;
    public GameObject PlayerCamera;
    public float speed;
    public GameObject Menu;
    public GameObject QuestLog;
    public CharacterAnimations characterAnimations;
    public string locationString;
    private AudioSource source;

    public static Player Instance;

    // might be useful later on, can change to List<GameObject>?
    //public List<int> CollidingWithID;

	public const bool SPEED_BOOST_ALLOWED = true;
	private const float SPEED_BOOST = 0.1f;

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
		EventManager.OnNPC += HandleNPC;
        source = GetComponent<AudioSource>();
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

            if (Input.GetKey(KeyCode.UpArrow))
            {

                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(charposx, wall_y);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, up);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);

            }

            else if (Input.GetKey(KeyCode.DownArrow))
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

            if (Input.GetKey(KeyCode.DownArrow))
            {

                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(charposx, wall_y);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, down);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            }

            else if (Input.GetKey(KeyCode.UpArrow))
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

            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x);
                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(wall_x, charposy);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, right);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_WALK);
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
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

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("charx " + charposx + " chary " + charposy + "wallx: " + wall_x);
                //transform.position = new Vector3(Mathf.Clamp;
                this.transform.position = new Vector3(wall_x, charposy);
                //transform.Translate(0, 0, 0);
                //animator.SetInteger(animationState, left);
                this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
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
        //Debug.Log(coll);
        if (coll.gameObject.name == "Mall")
        {
            locationString = "Mall";
        }
        if (coll.gameObject.name == "Hospital")
        {
            locationString = "Hospital";
        }
        if (coll.gameObject.tag == "MainStreet")
        {
            locationString = "MainStreet";
        }
        if (coll.gameObject.tag == "House")
        {
            locationString = "House";
        }
        if (coll.gameObject.tag == "Park")
        {
            locationString = "Park";
        }
        if (coll.gameObject.tag == "PoliceStation")
        {
            locationString = "PoliceStation";
        }
        if (coll.gameObject.tag == "Apartment")
        {
            locationString = "Apartment";
        }

    }
    void OnTriggerExit2D(Collider2D coll)
    {
        locationString = "";
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (gameManager.gameMode != GameManager.GameMode.PLAYING)
        {
			characterAnimations.SetIdle();
            return;
        }
		
        // Speeding up!
        if (SPEED_BOOST_ALLOWED)
		{
			SpeedUp();
		}

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
            //animator.SetInteger(animationState, up);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
            //animator.SetInteger(animationState, down);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
            //animator.SetInteger(animationState, right);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
            //animator.SetInteger(animationState, left);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
           
        // Entering World Map Location 
        else if (Input.GetKey(KeyCode.Space))
        {
            if(locationString =="Mall")
            {
                SceneManager.instance.LoadScene("G_Mall");
            }
            if (locationString == "MainStreet")
            {
                SceneManager.instance.LoadScene("G_MainStreetSmall");
            }
            if (locationString == "House")
            {
                SceneManager.instance.LoadScene("G_House");
            }
            if (locationString == "Park")
            {
                SceneManager.instance.LoadScene("G_Park");
            }
            if (locationString == "PoliceStation")
            {
                SceneManager.instance.LoadScene("G_PoliceStation");
            }
            if (locationString == "Apartment")
            {
                SceneManager.instance.LoadScene("G_Apartment");
            }
            if (locationString == "Hospital")
            {
                SceneManager.instance.LoadScene("G_Hospital");
            }
            // Not on any location
            if (locationString =="")
            {

            }
        }



        ///Hot Keys
        //else if (Input.GetKeyDown(KeyCode.Q)){
        //    Instantiate(QuestLog, new Vector3(0, 0, 0), Quaternion.identity);
        //    gameManager.gameMode = GameManager.GameMode.LOG;
        //    gameManager.prevMode = GameManager.GameMode.PLAYING;

        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Instantiate(Menu, new Vector3(0, 0, 0), Quaternion.identity);
        //    gameManager.gameMode = GameManager.GameMode.MENU;
        //    gameManager.prevMode = GameManager.GameMode.PLAYING;
        //}

        else
        {
            characterAnimations.SetIdle();
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

    }

	private void SpeedUp()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (!speeding)
			{
				speeding = true;
				speed += SPEED_BOOST;
				characterAnimations.animationSpeed -= SPEED_BOOST;
                source.pitch += .3f;
			}
		}
		else
		{
			if (speeding)
			{
				speeding = false;
				speed -= SPEED_BOOST;
				characterAnimations.animationSpeed += SPEED_BOOST;
                source.pitch -= .3f;
			}
		}
	}

    


    //public void InvenButton()
    //{
    //    Instantiate(Menu, new Vector3(0, 0, 0), Quaternion.identity);
    //    gameManager.gameMode = GameManager.GameMode.MENU;
    //    gameManager.prevMode = GameManager.GameMode.PLAYING;
    //}

    //public void QuestButton()
    //{
    //    Instantiate(QuestLog, new Vector3(0, 0, 0), Quaternion.identity);
    //    gameManager.gameMode = GameManager.GameMode.LOG;
    //    gameManager.prevMode = GameManager.GameMode.PLAYING;
    //}
    
}