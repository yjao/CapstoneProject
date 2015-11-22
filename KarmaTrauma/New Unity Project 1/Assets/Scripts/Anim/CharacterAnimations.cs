using UnityEngine;
using System.Collections;

public class CharacterAnimations : MonoBehaviour
{
	public enum States
	{
		DOWN_IDLE, DOWN_WALK, DOWN_DANCE, DOWN_STRETCH, DOWN_SWING, DOWN_BOW,
		UP_IDLE, UP_WALK, UP_DANCE, UP_STRETCH, UP_SWING, UP_BOW,
		LEFT_IDLE, LEFT_WALK, LEFT_DANCE, LEFT_STRETCH, LEFT_SWING, LEFT_BOW,
		RIGHT_IDLE, RIGHT_WALK, RIGHT_DANCE, RIGHT_STRETCH, RIGHT_SWING, RIGHT_BOW,
		FALL
	}

	[Header("Stuff")]
	public string atlasName;
	public float speed;
	public States startingAnimationState;
	public CharacterStates states;

	[Header("Sprite Stuff")]
	private SpriteRenderer m_sprite_renderer;
	private Sprite[] sprites;
	private States m_animation_state;
	private States prev_animation_state;

	public SpriteRenderer Sprite_Renderer
	{
		get
		{
			if (m_sprite_renderer == null)
				m_sprite_renderer = GetComponent<SpriteRenderer>();

			return m_sprite_renderer;
		}
	}

	void Start()
	{
		sprites = Resources.LoadAll<Sprite>(atlasName);
		m_animation_state = startingAnimationState;
		StartCoroutine("StateMachine");
	}

	public States AnimationState
	{
		get
		{			
			return m_animation_state;
		}
		set
		{
            if (m_animation_state != value)
            {
                StopCoroutine("StateMachine");
                prev_animation_state = m_animation_state;
                m_animation_state = value;
                StartCoroutine("StateMachine");
            }
		}
	}

	private IEnumerator StateMachine()
	{
		while (true)
		{
			int rangeStart = 0;
			int rangeEnd = 0;
			//Debug.Log (m_animation_state);
			switch (m_animation_state)
			{
			case States.DOWN_IDLE:
				rangeStart = states.downWalkStart;
				rangeEnd = states.downWalkStart;
				break;
			case States.UP_IDLE:
				rangeStart = states.upWalkStart;
				rangeEnd = states.upWalkStart;
				break;
			case States.LEFT_IDLE:
				rangeStart = states.leftWalkStart;
				rangeEnd = states.leftWalkStart;
				break;
			case States.RIGHT_IDLE:
				rangeStart = states.rightWalkStart;
				rangeEnd = states.rightWalkStart;
				break;

			case States.DOWN_WALK:
				rangeStart = states.downWalkStart;
				rangeEnd = states.downWalkEnd;
				break;
			case States.UP_WALK:
				rangeStart = states.upWalkStart;
				rangeEnd = states.upWalkEnd;
				break;
			case States.LEFT_WALK:
				rangeStart = states.leftWalkStart;
				rangeEnd = states.leftWalkEnd;
				break;
			case States.RIGHT_WALK:
				rangeStart = states.rightWalkStart;
				rangeEnd = states.rightWalkEnd;
				break;
			
			case States.DOWN_DANCE:
				rangeStart = states.downDanceStart;
				rangeEnd = states.downDanceEnd;
				break;
			case States.UP_DANCE:
				rangeStart = states.upDanceStart;
				rangeEnd = states.upDanceEnd;
				break;
			case States.LEFT_DANCE:
				rangeStart = states.leftDanceStart;
				rangeEnd = states.leftDanceEnd;
				break;
			case States.RIGHT_DANCE:
				rangeStart = states.rightDanceStart;
				rangeEnd = states.rightDanceEnd;
				break;

			case States.DOWN_STRETCH:
				rangeStart = states.downStretchStart;
				rangeEnd = states.downStretchEnd;
				break;
			case States.UP_STRETCH:
				rangeStart = states.upStretchStart;
				rangeEnd = states.upStretchEnd;
				break;
			case States.LEFT_STRETCH:
				rangeStart = states.leftStretchStart;
				rangeEnd = states.leftStretchEnd;
				break;
			case States.RIGHT_STRETCH:
				rangeStart = states.rightStretchStart;
				rangeEnd = states.rightStretchEnd;
				break;

			case States.DOWN_SWING:
				rangeStart = states.downSwingStart;
				rangeEnd = states.downSwingEnd;
				break;
			case States.UP_SWING:
				rangeStart = states.upSwingStart;
				rangeEnd = states.upSwingEnd;
				break;
			case States.LEFT_SWING:
				rangeStart = states.leftSwingStart;
				rangeEnd = states.leftSwingEnd;
				break;
			case States.RIGHT_SWING:
				rangeStart = states.rightSwingStart;
				rangeEnd = states.rightSwingEnd;
				break;

			case States.DOWN_BOW:
				rangeStart = states.downBowStart;
				rangeEnd = states.downBowEnd;
				break;
			case States.UP_BOW:
				rangeStart = states.upBowStart;
				rangeEnd = states.upBowEnd;
				break;
			case States.LEFT_BOW:
				rangeStart = states.leftBowStart;
				rangeEnd = states.leftBowEnd;
				break;
			case States.RIGHT_BOW:
				rangeStart = states.rightBowStart;
				rangeEnd = states.rightBowEnd;
				break;

			case States.FALL:
				rangeStart = states.fallStart;
				rangeEnd = states.fallEnd;
				break;
			default:
				Debug.Log("YA DONE GOOFED");
				break;
			}

			for (int i = rangeStart; i < rangeEnd; i++)
			{
				Sprite_Renderer.sprite = sprites[i];
				yield return new WaitForSeconds(speed);
			}

			if (rangeStart == rangeEnd)
			{
                //Debug.Log(rangeStart);
				Sprite_Renderer.sprite = sprites[rangeStart];
			}
			yield return null;
		}
		yield break;
	}

    public IEnumerator Move(int animatorDirection, float newPositionValue, float speed = 0.02f)
    {
        switch (animatorDirection)
        {
            case 4:
                while (gameObject.transform.position.x >= newPositionValue)
                {
                    gameObject.transform.Translate(-speed, 0.0f, 0.0f);
                    AnimationState = CharacterAnimations.States.LEFT_WALK;
                    yield return null;
                }
                break;
            case 3:
                while (gameObject.transform.position.x <= newPositionValue)
                {
                    gameObject.transform.Translate(speed, 0.0f, 0.0f);
                    AnimationState = CharacterAnimations.States.RIGHT_WALK;
                    yield return null;
                }
                break;
            case 2:
                while (gameObject.transform.position.y >= newPositionValue)
                {
                    gameObject.transform.Translate(0.0f, -speed, 0.0f);
                    AnimationState = CharacterAnimations.States.DOWN_WALK;
                    yield return null;
                }
                break;
            case 1:
                while (gameObject.transform.position.y <= newPositionValue)
                {
                    gameObject.transform.Translate(0.0f, speed, 0.0f);
                    AnimationState = CharacterAnimations.States.UP_WALK;
                    yield return null;
                }
                break;
        }

        yield break;
    }
}

[System.Serializable]
public class CharacterStates
{
	[Header("Walking Animations")]
	public int downWalkStart;
	public int downWalkEnd;
	public int upWalkStart;
	public int upWalkEnd;
	public int leftWalkStart;
	public int leftWalkEnd;
	public int rightWalkStart;
	public int rightWalkEnd;

	[Header("Dancing Animations")]
	public int downDanceStart;
	public int downDanceEnd;
	public int upDanceStart;
	public int upDanceEnd;
	public int leftDanceStart;
	public int leftDanceEnd;
	public int rightDanceStart;
	public int rightDanceEnd;

	[Header("Stretching Animations")]
	public int downStretchStart;
	public int downStretchEnd;
	public int upStretchStart;
	public int upStretchEnd;
	public int leftStretchStart;
	public int leftStretchEnd;
	public int rightStretchStart;
	public int rightStretchEnd;

	[Header("Swinging Animations")]
	public int downSwingStart;
	public int downSwingEnd;
	public int upSwingStart;
	public int upSwingEnd;
	public int leftSwingStart;
	public int leftSwingEnd;
	public int rightSwingStart;
	public int rightSwingEnd;

	[Header("Bowing Animations")]
	public int downBowStart;
	public int downBowEnd;
	public int upBowStart;
	public int upBowEnd;
	public int leftBowStart;
	public int leftBowEnd;
	public int rightBowStart;
	public int rightBowEnd;

	[Header("Falling Animations")]
	public int fallStart;
	public int fallEnd;
}
