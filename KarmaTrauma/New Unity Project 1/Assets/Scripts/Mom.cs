using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Mom : MonoBehaviour
{
    private GameManager gameManager;

    Animator animator;
    const int idle = 0;
    const int up = 1;
    const int down = 2;
    const int right = 3;
    const int left = 4;
    const string animationState = "MomAnimationState";

    int k = 0;
    bool l = false;
    bool r = true;

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
        if (gameManager.GameMode != GameManager.MODE.PLAYING)
            return;

        if (r && k != 60)
        {
            transform.Translate(0.01f, 0, 0);
            animator.SetInteger(animationState, right);
            k += 1;
        }
        else if (l && k != -60)
        {
            transform.Translate(-0.01f, 0, 0);
            animator.SetInteger(animationState, left);
            k -= 1;
        }
        else
        {
            if (r)
            {
                r = false;
                l = true;
            }
            else if (l)
            {
                l = false;
                r = true;
            }
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