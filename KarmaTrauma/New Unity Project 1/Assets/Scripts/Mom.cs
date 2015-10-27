using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Mom : InteractableObject
{
    Animator animator;
    const int idle = 0;
    const int up = 1;
    const int down = 2;
    const int right = 3;
    const int left = 4;

    const int rightIdle = 5;
    const int leftIdle = 6;
    const string animationState = "MomAnimationState";

    int k = 0;
    bool l = false;
    bool r = true;

    void Start()
    {
		gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
    }

	void Update()
    {
		CheckAndInteract();

        transform.rotation = Quaternion.identity;
        if (gameManager.GameMode != GameManager.MODE.PLAYING)
        {

            if (this.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
            {
                animator.SetInteger(animationState, leftIdle);
            }
            else if (this.transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
            {
                animator.SetInteger(animationState, rightIdle);
            }
            return;
        }

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

            if (r && k == 60)
            {
                r = false;
                l = true;
            }
            else if (l && k == -60)
            {
                l = false;
                r = true;
            }
        }
    }
}