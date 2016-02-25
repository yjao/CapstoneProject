using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NPC : MonoBehaviour
{
	#region Public Variables
	public InteractableObject interactableObject;
	public CharacterAnimations characterAnimations;

	// Wandering
	public float wanderDistanceX;
	public int wanderDirectionX;
	public float wanderDistanceY;
	public int wanderDirectionY;
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
		//EventManager.OnNPC += HandleNPC;
    }

	void OnDestroy()
	{
		//EventManager.OnNPC -= HandleNPC;
	}

    void FixedUpdate()
    {
		transform.rotation = Quaternion.Euler(Vector3.zero);
		if (GameManager.instance.gameMode != GameManager.GameMode.PLAYING)
		{
			return;
		}
		WanderX();
		WanderY();
    }

	#region Wandering

	private void WanderX()
	{
		if (wanderDirectionX == 0)
		{
			return;
		}

		if (wanderDirectionX > 0) // going right
		{
			SetAnimation(CharacterAnimations.States.RIGHT_WALK);
			transform.Translate(0.01f, 0, 0);
			currentX += 1;
		}
		else if (wanderDirectionX < 0) // going left
		{
			SetAnimation(CharacterAnimations.States.LEFT_WALK);
			transform.Translate(-0.01f, 0, 0);
			currentX -= 1;
		}

		if ((currentX < -wanderDistanceX) || (currentX > wanderDistanceX))
		{
			wanderDirectionX *= -1;
		}
	}

	private void WanderY()
	{
		if (wanderDirectionY == 0)
		{
			return;
		}
		
		if (wanderDirectionY > 0) // going up
		{
			SetAnimation(CharacterAnimations.States.UP_WALK);
			transform.Translate(0, 0.01f, 0);
			currentY += 1;
		}
		else if (wanderDirectionY < 0) // going down
		{
			SetAnimation(CharacterAnimations.States.DOWN_WALK);
			transform.Translate(0, -0.01f, 0);
			currentY -= 1;
		}
		
		if ((currentY < -wanderDistanceY) || (currentY > wanderDistanceY))
		{
			wanderDirectionY *= -1;
		}
	}

	#endregion

	public void TurnNpcAndPlayer()
	{
		CharacterAnimations.States playerNewState = characterAnimations.AnimationState;

		Vector3 difference = this.transform.position - Player.Instance.transform.position;
		if (Math.Abs(difference.x) > Math.Abs(difference.y))
		{
			if (this.transform.position.x > Player.Instance.transform.position.x)
			{
				SetAnimation(CharacterAnimations.States.LEFT_IDLE);
                playerNewState = CharacterAnimations.States.RIGHT_IDLE;
			}
			else if (this.transform.position.x < Player.Instance.transform.position.x)
			{
				SetAnimation(CharacterAnimations.States.RIGHT_IDLE);
                playerNewState = CharacterAnimations.States.LEFT_IDLE;
            }
		}
		else if (Math.Abs(difference.x) < Math.Abs(difference.y))
		{
			if (this.transform.position.y > Player.Instance.transform.position.y)
			{
				SetAnimation(CharacterAnimations.States.DOWN_IDLE);
                playerNewState = CharacterAnimations.States.UP_IDLE;
            }
			else if (this.transform.position.y < Player.Instance.transform.position.y)
			{
				SetAnimation(CharacterAnimations.States.UP_IDLE);
                playerNewState = CharacterAnimations.States.DOWN_IDLE;
            }
		}

		EventManager.NotifyNPC(this, new GameEventArgs() { AnimationState = playerNewState });
	}

	#region Animation Control

	public void SetAnimation(CharacterAnimations.States newState)
	{
		if (characterAnimations.AnimationState != newState)
		{
			characterAnimations.AnimationState = newState;
		}
	}

	#endregion
}
