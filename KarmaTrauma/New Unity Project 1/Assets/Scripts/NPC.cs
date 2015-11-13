using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
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
    void Update()
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
			animator.SetInteger(animationState, right);
			transform.Translate(0.01f, 0, 0);
			currentX += 1;
		}
		else if (WanderDirectionX < 0) // going lft
		{
			animator.SetInteger(animationState, left);
			transform.Translate(-0.01f, 0, 0);
			currentX -= 1;
		}

		if ((currentX < -WanderDistanceX) || (currentX > WanderDistanceX))
		{
			WanderDirectionX *= -1;
		}
	}

	// Working on getting NPC to face player.
	void HandleNPC(object sender, GameEventArgs args)
	{
		if (Player.Instance == null)
		{
			return;
		}

		if (this.transform.position.x > Player.Instance.transform.position.x)
		{
			animator.SetInteger(animationState, leftIdle);
		}
		else if (this.transform.position.x < Player.Instance.transform.position.x)
		{
			animator.SetInteger(animationState, rightIdle);
		}
	}

}
