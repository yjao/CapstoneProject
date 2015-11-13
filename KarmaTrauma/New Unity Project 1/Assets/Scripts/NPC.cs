using UnityEngine;
using System.Collections;
using System;

public class NPC : MonoBehaviour
{
	#region Public Variables
	public GameObject IntrObject;
    public Animator animator;

	// Wandering
	public float WanderDistanceX;
	public int WanderDirectionX;
	public float WanderDistanceY;
	public int WanderDirectionY;
	#endregion

	#region Constants
	const string animationState = "AnimationState";
	const int idle = 0;
    const int up = 1;
    const int down = 2;
    const int right = 3;
    const int left = 4;
    const int upIdle = 5;
    const int downIdle = 6;
    const int rightIdle = 7;
    const int leftIdle = 8;
	#endregion
    
	#region Private Variables
	private int prevAnimationInt;

	// Wandering around
	private int currentX = 0;
	private int currentY = 0;
	#endregion


    void Start()
    {
        animator = GetComponent<Animator>();
		EventManager.OnNPC += HandleNPC;
    }

	void OnDestroy()
	{
		EventManager.OnNPC -= HandleNPC;
	}

    void FixedUpdate()
    {
		transform.rotation = Quaternion.Euler(Vector3.zero);
		if (GameManager.Instance.GameMode != GameManager.MODE.PLAYING)
		{
			return;
		}
		WanderX();
		WanderY();
    }

	#region Wandering

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
		else if (WanderDirectionX < 0) // going left
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

	private void WanderY()
	{
		if (WanderDirectionY == 0)
		{
			return;
		}
		
		if (WanderDirectionY > 0) // going up
		{
			SetAnimation(up);
			transform.Translate(0, 0.01f, 0);
			currentY += 1;
		}
		else if (WanderDirectionY < 0) // going down
		{
			SetAnimation(down);
			transform.Translate(0, -0.01f, 0);
			currentY -= 1;
		}
		
		if ((currentY < -WanderDistanceY) || (currentY > WanderDistanceY))
		{
			WanderDirectionY *= -1;
		}
	}

	#endregion

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

	#region Animation Control

	private void SetAnimation(int newInteger)
	{
		prevAnimationInt = animator.GetInteger(animationState);
		animator.SetInteger(animationState, newInteger);
	}

	private void ResumeAnimation()
	{
		SetAnimation(prevAnimationInt);
	}

	#endregion
}
