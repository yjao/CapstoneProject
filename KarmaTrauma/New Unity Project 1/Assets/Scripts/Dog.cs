using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour {

    private int up = 1;
    private int down = 2;
    private int right = 3;
    private int left = 4;
    private int up_idle = 5;
    private int down_idle = 6;
    private int right_idle = 7;
    private int left_idle = 8;

    private int interacted = 9;
    private int check = 10;

    private int counter = 0;

    private int animationState;
    private Animator animator;

	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
        animationState = 7;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (animationState == right_idle)
        {
            transform.Translate(-0.01f, 0.0f, 0.0f);
            animator.SetInteger("AnimationState", left);
            counter++;
            if (counter == 100)
            {
                animationState = 4;
                counter = 0;
            }
        }
        else if (animationState == left_idle)
        {
            transform.Translate(0.01f, 0.0f, 0.0f);
            animator.SetInteger("AnimationState", right);
            counter++;
            if (counter == 100)
            {
                animationState = 3;
                counter = 0;
            }
        }
        else if (animationState == right)
        {
            animator.SetInteger("AnimationState", right_idle);
            counter++;
            if (counter == 100)
            {
                animator.SetInteger("AnimationState", left);
                animationState = 7;
                counter = 0;
            }
        }
        else if (animationState == left)
        {
            animator.SetInteger("AnimationState", left_idle);
            counter++;
            if (counter == 100)
            {
                animator.SetInteger("AnimationState", right);
                animationState = 8;
                counter = 0;
            }
        }
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            animationState = interacted;
            Vector3 difference = this.transform.position - Player.Instance.transform.position;
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                if (coll.transform.position.x > transform.position.x)
                {
                    animator.SetInteger("AnimationState", right_idle);
                    check = 11;
                }
                else
                {
                    animator.SetInteger("AnimationState", left_idle);
                    check = 12;
                }
            }
            else if (Mathf.Abs(difference.x) < Mathf.Abs(difference.y))
            {
                if (coll.transform.position.y > transform.position.y)
                {
                    animator.SetInteger("AnimationState", up_idle);
                    check = 11;
                }
                else
                {
                    animator.SetInteger("AnimationState", down_idle);
                    check = 12;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (check == 11)
        {
            animationState = 8;
        }
        else if (check == 12)
        {
            animationState = 7;
        }
        check = 10;
    }
}
