using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour
{
	private GameManager gameManager;
    public float speed;
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

    // Use this for initialization
    void Start()
    {
		gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (gameManager.GameMode != GameManager.MODE.PLAYING) { 

            if (u)
            {
                animator.SetInteger(animationState, upIdle);
            }
            else if (d)
            {
                animator.SetInteger(animationState, downIdle);
            }
            else if (r)
            {
                animator.SetInteger(animationState, rightIdle);
            }
            else if (l)
            {
                animator.SetInteger(animationState, leftIdle);
            }
            return;
        }
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

        if (Input.GetKey(KeyCode.S))
        {
            Save(); ;
        }
        if (Input.GetKey(KeyCode.L))
        {
            Load();
        }
    }



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



    [Serializable]  // by putting this bracket, Unity knows this class is serializable, thus savable with our serialized saving function
    class PlayerData
    {
        public int days;
        public int progress;

    }

}