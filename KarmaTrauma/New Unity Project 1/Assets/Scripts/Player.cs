﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    //GameObject saveMachine = GameObject.AddComponent<SaveData>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            SaveLoad.Save(); ;
        }
        if (Input.GetKey(KeyCode.L))
        {
            SaveLoad.Load(); ;
        }
    }
}

