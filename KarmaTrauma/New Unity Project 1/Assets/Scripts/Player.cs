using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    Animator animator;
    const int idle = 0;
    const int up = 1;
    const int down = 2;
    const int right = 3;
    const int left = 4;
    const string animationState = "AnimationState"; 

    //GameObject saveMachine = GameObject.AddComponent<SaveData>();

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
            animator.SetInteger(animationState, up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
            animator.SetInteger(animationState, down);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
            animator.SetInteger(animationState, right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
            animator.SetInteger(animationState, left);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
            animator.SetInteger(animationState, up);
        }
        else
        {
            animator.SetInteger(animationState, idle);
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

