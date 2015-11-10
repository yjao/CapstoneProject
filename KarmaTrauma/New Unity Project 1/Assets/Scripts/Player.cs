using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


public class Player : MonoBehaviour
{
    public static InvisibleWallTop walltop;
	private GameManager gameManager;
    public GameObject PlayerCamera;
    public float speed;

	public static Player Instance;

	// might be useful later on, can change to List<GameObject>?
	public List<int> CollidingWithID;

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


    //GameObject saveMachine = GameObject.AddComponent<SaveData>();

	void Awake()
	{
		/*if ((Instance != null) && (Instance != this))
			Destroy(gameObject);
		else
			Instance = this;
		
		DontDestroyOnLoad(this);*/

		CollidingWithID = new List<int>();
	}

    // Use this for initialization
    void Start()
    {
		Instance = this;
        walltop = FindObjectOfType(typeof(InvisibleWallTop)) as InvisibleWallTop;
		gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        EventManager.OnNPC += HandleNPC;
    }

    void onDestroy()
    {
        EventManager.OnNPC -= HandleNPC;
    }

    void HandleNPC(object sender, GameEventArgs args)
    {
       
        //Fix this senderObj -> sender returns null
        //GameObject senderObj = sender as GameObject;
        if (sender == null)
        {
            return;
        }
        //this.transform.rotation = Quaternion.Euler(Vector3.zero);
        //Debug.Log(this.transform.rotation);
        //set player to face sender's (NPC) direction
        if (args.Position[0] < this.transform.position.x)
        {
            animator.SetInteger(animationState, leftIdle);
        }
        else if (args.Position[0] > this.transform.position.x)
        {
            animator.SetInteger(animationState, rightIdle);
        }
        //Need to fix up and down facing;
        //else if (args.Position[1] < this.transform.position.y)
        //{
        //    animator.SetInteger(animationState, downIdle);
        //}
        //else if (args.Position[1] > this.transform.position.y)
        //{
        //    animator.SetInteger(animationState, upIdle);
        //}
    }

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
                animator.SetInteger(animationState, up);
                
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, -speed, 0);
                animator.SetInteger(animationState, down);
                
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
                animator.SetInteger(animationState, down);

            }

            else if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, speed, 0);
                animator.SetInteger(animationState, up);

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
                animator.SetInteger(animationState, right);

            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("here");
                transform.Translate(-speed ,0, 0);
                animator.SetInteger(animationState, left);

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
                animator.SetInteger(animationState, left);

            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("here");
                transform.Translate(speed, 0, 0);
                animator.SetInteger(animationState, left);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (gameManager.GameMode != GameManager.MODE.PLAYING)
        {

            return;
        }
        
        //if (gameManager.GameMode != GameManager.MODE.PLAYING) {

        //    if (this.transform.position.x > GameObject.FindGameObjectWithTag("Mom").transform.position.x)
        //    {
        //        animator.SetInteger(animationState, leftIdle);
        //    }
        //    else if (this.transform.position.x < GameObject.FindGameObjectWithTag("Mom").transform.position.x)
        //    {
        //        animator.SetInteger(animationState, rightIdle);
        //    }

        //    return;
        //}
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
            animator.SetInteger(animationState, up);
            u = true;
            d = false;
            r = false;
            l = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
            animator.SetInteger(animationState, down);
            u = false;
            d = true;
            r = false;
            l = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
            animator.SetInteger(animationState, right);
            u = false;
            d = false;
            r = true;
            l = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
            animator.SetInteger(animationState, left);
            u = false;
            d = false;
            r = false;
            l = true;
        }
        else
        {
            if (u == true)
                animator.SetInteger(animationState, upIdle);
            else if (d == true)
                animator.SetInteger(animationState, downIdle);
            else if (r == true)
                animator.SetInteger(animationState, rightIdle);
            else if (l == true)
                animator.SetInteger(animationState, leftIdle);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gameManager.Save(); ;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameManager.Load();
          
        }
    }



   



}