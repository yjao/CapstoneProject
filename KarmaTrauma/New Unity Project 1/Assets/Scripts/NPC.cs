using UnityEngine;
using System.Collections;
using System;

public class NPC : MonoBehaviour
{
	public GameObject IntrObject;
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

	private int prevAnimationInt;
	private bool animationLocked = false;

	// Wandering around
	public float WanderDistanceX;
	public int WanderDirectionX = 1;
	private int currentX = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
		EventManager.OnNPC += HandleNPC;
    }

	void OnDestroy()
	{
		EventManager.OnNPC -= HandleNPC;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.rotation = Quaternion.Euler(Vector3.zero);
		if (GameManager.Instance.GameMode != GameManager.MODE.PLAYING)
		{
			return;
		}
		WanderX();
    }

	private void WanderX()
	{
		if (WanderDirectionX == 0)
		{
			return;
		}

		if (WanderDirectionX > 0) // going right
		{
			SetAnimation(right);
			transform.Translate(0.01f, 0, 0);
			currentX += 1;
		}
		else if (WanderDirectionX < 0) // going lft
		{
			SetAnimation(left);
			transform.Translate(-0.01f, 0, 0);
			currentX -= 1;
		}

		if ((currentX < -WanderDistanceX) || (currentX > WanderDistanceX))
		{
			WanderDirectionX *= -1;
		}
	}

	void HandleNPC(object sender, GameEventArgs args)
	{
		if ((Player.Instance == null) || (args.ThisGameObject != IntrObject))
		{
			return;
		}

		int playerAnimationInt = -1;
		Vector3 difference = this.transform.position - Player.Instance.transform.position;
		if (Math.Abs(difference.x) > Math.Abs(difference.y))
		{
			if (this.transform.position.x > Player.Instance.transform.position.x)
			{
				SetAnimation(leftIdle);
				playerAnimationInt = rightIdle;
			}
			else if (this.transform.position.x < Player.Instance.transform.position.x)
			{
				SetAnimation(rightIdle);
				playerAnimationInt = leftIdle;
			}
		}
		else if (Math.Abs(difference.x) < Math.Abs(difference.y))
		{
			if (this.transform.position.y > Player.Instance.transform.position.y)
			{
				SetAnimation(downIdle);
				playerAnimationInt = upIdle;
			}
			else if (this.transform.position.y < Player.Instance.transform.position.y)
			{
				SetAnimation(upIdle);
				playerAnimationInt = downIdle;
			}
		}
		if (playerAnimationInt >= 0)
		{
			EventManager.NotifyNPC(this, new GameEventArgs() { Integer = playerAnimationInt });
		}
	}

	private void SetAnimation(int newInteger)
	{
		prevAnimationInt = animator.GetInteger(animationState);
		animator.SetInteger(animationState, newInteger);
	}

	private void ResumeAnimation()
	{
		SetAnimation(prevAnimationInt);
	}

}
