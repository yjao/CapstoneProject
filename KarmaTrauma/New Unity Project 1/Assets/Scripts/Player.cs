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

    public static Player Instance;

    // might be useful later on, can change to List<GameObject>?
    public List<int> CollidingWithID;

    public Animator animator;
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

    private bool speeding = false;

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
        if (args.ThisGameObject == null)
        {
            characterAnimations.AnimationState = (args.AnimationState);

        }
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

        // Speeding up!
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!speeding)
            {
                speeding = true;
                speed += 0.1f;
            }
        }
        else
        {
            if (speeding)
            {
                speeding = false;
                speed -= 0.1f;
            }
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
            //animator.SetInteger(animationState, up);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_WALK);
            u = true;
            d = false;
            r = false;
            l = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
            //animator.SetInteger(animationState, down);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_WALK);
            u = false;
            d = true;
            r = false;
            l = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
            //animator.SetInteger(animationState, right);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_WALK);
            u = false;
            d = false;
            r = true;
            l = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
            //animator.SetInteger(animationState, left);
            this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_WALK);
            u = false;
            d = false;
            r = false;
            l = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(Menu, new Vector3(0, 0, 0), Quaternion.identity);
            gameManager.GameMode = GameManager.MODE.MENU;
            gameManager.PrevMode = GameManager.MODE.PLAYING;
        }
        else
        {
            if (u == true)
                this.characterAnimations.AnimationState = (CharacterAnimations.States.UP_IDLE);
            //animator.SetInteger(animationState, upIdle);
            else if (d == true)
                this.characterAnimations.AnimationState = (CharacterAnimations.States.DOWN_IDLE);
            //animator.SetInteger(animationState, downIdle);
            else if (r == true)
                this.characterAnimations.AnimationState = (CharacterAnimations.States.RIGHT_IDLE);
            //animator.SetInteger(animationState, rightIdle);
            else if (l == true)
                this.characterAnimations.AnimationState = (CharacterAnimations.States.LEFT_IDLE);
            //animator.SetInteger(animationState, leftIdle);
        }
        if (Input.GetKey(KeyCode.B))
        {
            Instantiate(Menu, new Vector3(0, 0, 0), Quaternion.identity);
            gameManager.GameMode = GameManager.MODE.MENU;
            gameManager.PrevMode = GameManager.MODE.PLAYING;
        }

        if (Input.GetKey(KeyCode.S))
        {
      //      Save(); ;
        }
        if (Input.GetKey(KeyCode.L))
        {
      //      Load();
        }
    }

    public void InvenButton()
    {
        Instantiate(Menu, new Vector3(0, 0, 0), Quaternion.identity);
        gameManager.GameMode = GameManager.MODE.MENU;
        gameManager.PrevMode = GameManager.MODE.PLAYING;
    }

    public void QuestButton()
    {
        Instantiate(QuestLog, new Vector3(0, 0, 0), Quaternion.identity);
        gameManager.GameMode = GameManager.MODE.LOG;
        gameManager.PrevMode = GameManager.MODE.PLAYING;
    }
    /*

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.DK");   // Saves in SaveData.DK file
        Debug.Log("File Saved");
        Debug.Log(Application.persistentDataPath);
        PlayerData data = new PlayerData();
        data.days = 1;      // data.days = days
        data.progress = 0;  // data.progress = progress

        bf.Serialize(file, data);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.DK"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.DK", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //days = data.days;
            //progress = data.progress;
        }

    }
    */

/*
    [Serializable]  // by putting this bracket, Unity knows this class is serializable, thus savable with our serialized saving function
    class PlayerData
    {
        public int days;
        public int progress;

    }
    */
}