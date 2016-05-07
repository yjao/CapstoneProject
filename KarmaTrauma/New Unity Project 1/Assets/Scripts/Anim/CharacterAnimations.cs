﻿using System;
using UnityEngine;
using System.Collections;

public class CharacterAnimations : MonoBehaviour
{
	#region ANIMATION STATE CONSTANTS

	[Header("Idle Animations")]
	public const int downIdle = 78;
	public const int upIdle = 60;
	public const int leftIdle = 69;
	public const int rightIdle = 87;

    [Header("Walking Animations")]
    public const int downWalkStart = 79;
    public const int downWalkEnd = 86;
    public const int upWalkStart = 61;
    public const int upWalkEnd = 68;
    public const int leftWalkStart = 70;
    public const int leftWalkEnd = 77;
    public const int rightWalkStart = 88;
    public const int rightWalkEnd = 95;

    [Header("Dancing Animations")]
    public const int downDanceStart = 45;
    public const int downDanceEnd = 51;
    public const int upDanceStart = 29;
    public const int upDanceEnd = 35;
    public const int leftDanceStart = 37;
    public const int leftDanceEnd = 43;
    public const int rightDanceStart = 53;
    public const int rightDanceEnd = 59;

    [Header("Stretching Animations")]
    public const int downStretchStart = 15;
    public const int downStretchEnd = 20;
    public const int upStretchStart = 1;
    public const int upStretchEnd = 6;
    public const int leftStretchStart = 8;
    public const int leftStretchEnd = 13;
    public const int rightStretchStart = 22;
    public const int rightStretchEnd = 27;

    [Header("Swinging Animations")]
    public const int downSwingStart = 109;
    public const int downSwingEnd = 112;
    public const int upSwingStart = 97;
    public const int upSwingEnd = 101;
    public const int leftSwingStart = 103;
    public const int leftSwingEnd = 107;
    public const int rightSwingStart = 115;
    public const int rightSwingEnd = 119;

    [Header("Bowing Animations")]
    public const int downBowStart = 147;
    public const int downBowEnd = 158;
    public const int upBowStart = 121;
    public const int upBowEnd = 132;
    public const int leftBowStart = 134;
    public const int leftBowEnd = 145;
    public const int rightBowStart = 160;
    public const int rightBowEnd = 171;

    [Header("Falling Animations")]
    public const int fallStart = 172;
    public const int fallEnd = 177;

    [Header("Sleeping Animatons")]
    public const int sleeping = 17;
    public const int sleepy = 15;

	public readonly string[] DIRECTIONS = { "DOWN", "UP", "LEFT", "RIGHT" };

	#endregion


    public enum States
    {
        DOWN_IDLE, DOWN_WALK, DOWN_DANCE, DOWN_STRETCH, DOWN_SWING, DOWN_BOW,
        UP_IDLE, UP_WALK, UP_DANCE, UP_STRETCH, UP_SWING, UP_BOW,
        LEFT_IDLE, LEFT_WALK, LEFT_DANCE, LEFT_STRETCH, LEFT_SWING, LEFT_BOW,
		RIGHT_IDLE, RIGHT_WALK, RIGHT_DANCE, RIGHT_STRETCH, RIGHT_SWING, RIGHT_BOW,
		FALL, FALLEN, SLEEPING, SLEEPY, RIGHT_PRAY
    }

	public bool active = true;
	public bool facePlayerOnInteraction = true;
    public string atlasName;
    public float animationSpeed = 0.05f;
    public States startingAnimationState;

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
		if (!active) { return; }

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
			if (!active || !facePlayerOnInteraction) { return; }

            if (m_animation_state != value)
            {
                StopCoroutine("StateMachine");
                prev_animation_state = m_animation_state;
                m_animation_state = value;
                StartCoroutine("StateMachine");
            }
        }
    }

    private int[] AnimationStateRange(CharacterAnimations.States state)
    {
        int rangeStart = 0;
        int rangeEnd = 0;

        int[] range;
        range = new int[2];

        switch (state)
        {
        case States.DOWN_IDLE:
            rangeStart = downIdle;
			rangeEnd = downIdle;
            break;
        case States.UP_IDLE:
			rangeStart = upIdle;
			rangeEnd = upIdle;
            break;
        case States.LEFT_IDLE:
			rangeStart = leftIdle;
			rangeEnd = leftIdle;
            break;
        case States.RIGHT_IDLE:
			rangeStart = rightIdle;
			rangeEnd = rightIdle;
            break;

        case States.DOWN_WALK:
            rangeStart = downWalkStart;
            rangeEnd = downWalkEnd;
            break;
        case States.UP_WALK:
            rangeStart = upWalkStart;
            rangeEnd = upWalkEnd;
            break;
        case States.LEFT_WALK:
            rangeStart = leftWalkStart;
            rangeEnd = leftWalkEnd;
            break;
        case States.RIGHT_WALK:
            rangeStart = rightWalkStart;
            rangeEnd = rightWalkEnd;
            break;

        case States.DOWN_DANCE:
            rangeStart = downDanceStart;
            rangeEnd = downDanceEnd;
            break;
        case States.UP_DANCE:
            rangeStart = upDanceStart;
            rangeEnd = upDanceEnd;
            break;
        case States.LEFT_DANCE:
            rangeStart = leftDanceStart;
            rangeEnd = leftDanceEnd;
            break;
        case States.RIGHT_DANCE:
            rangeStart = rightDanceStart;
            rangeEnd = rightDanceEnd;
            break;

        case States.DOWN_STRETCH:
            rangeStart = downStretchStart;
            rangeEnd = downStretchEnd;
            break;
        case States.UP_STRETCH:
            rangeStart = upStretchStart;
            rangeEnd = upStretchEnd;
            break;
        case States.LEFT_STRETCH:
            rangeStart = leftStretchStart;
            rangeEnd = leftStretchEnd;
            break;
        case States.RIGHT_STRETCH:
            rangeStart = rightStretchStart;
            rangeEnd = rightStretchEnd;
            break;

        case States.DOWN_SWING:
            rangeStart = downSwingStart;
            rangeEnd = downSwingEnd;
            break;
        case States.UP_SWING:
            rangeStart = upSwingStart;
            rangeEnd = upSwingEnd;
            break;
        case States.LEFT_SWING:
            rangeStart = leftSwingStart;
            rangeEnd = leftSwingEnd;
            break;
        case States.RIGHT_SWING:
            rangeStart = rightSwingStart;
            rangeEnd = rightSwingEnd;
            break;

        case States.DOWN_BOW:
            rangeStart = downBowStart;
            rangeEnd = downBowEnd;
            break;
        case States.UP_BOW:
            rangeStart = upBowStart;
            rangeEnd = upBowEnd;
            break;
        case States.LEFT_BOW:
            rangeStart = leftBowStart;
            rangeEnd = leftBowEnd;
            break;
        case States.RIGHT_BOW:
            rangeStart = rightBowStart;
            rangeEnd = rightBowEnd;
            break;

		case States.RIGHT_PRAY:
			rangeStart = rightStretchStart + 1;
			rangeEnd = rightStretchStart + 3;
			break;

        case States.FALL:
            rangeStart = fallStart;
            rangeEnd = fallEnd;
            break;
        case States.FALLEN:
            rangeStart = fallEnd;
            rangeEnd = fallEnd;
            break;
        case States.SLEEPING:
            rangeStart = sleeping;
            rangeEnd = sleeping;
            break;
        case States.SLEEPY:
            rangeStart = sleepy;
            rangeEnd = sleepy;
            break;
        default:
            Debug.Log("YA DONE GOOFED");
            break;
        }

        range[0] = rangeStart;
        range[1] = rangeEnd;

        return range;
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            int rangeStart = AnimationStateRange(m_animation_state)[0];
            int rangeEnd = AnimationStateRange(m_animation_state)[1];
            //Debug.Log (m_animation_state);

            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                Sprite_Renderer.sprite = sprites[i];
                yield return new WaitForSeconds(animationSpeed);
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

	public void ResumePreviousAnimation()
	{
		AnimationState = prev_animation_state;
	}

	public States Idle(States state)
	{
		foreach (string direction in DIRECTIONS)
		{
			if (state.ToString().Contains(direction))
			{
				return (States) Enum.Parse(typeof(States), direction + "_IDLE");
			}
		}
		Debug.Log("You must've fallen somehow.");
		return state;
	}

	public void SetIdle()
	{
		if (!active) { return; }

		States newState = Idle(m_animation_state);
		if (newState == m_animation_state)
		{
			return;
		}

		States prev = prev_animation_state;
		AnimationState = newState;
		prev_animation_state = prev;
	}

	public IEnumerator PlayAnimation(CharacterAnimations.States state, bool resumeAfter=false)
    {
        StopCoroutine("StateMachine");

        int rangeStart = AnimationStateRange(state)[0];
        int rangeEnd = AnimationStateRange(state)[1];

        for (int i = rangeStart; i < rangeEnd; i++)
        {
            Sprite_Renderer.sprite = sprites[i];
            yield return new WaitForSeconds(animationSpeed);
        }

		if (resumeAfter)
		{
			StartCoroutine("StateMachine");
		}
        yield break;
    }

    public IEnumerator Move(int animatorDirection, float newPositionValue, CharacterAnimations.States state, float speed = 0.02f)
    {
        AnimationState = state;
        switch (animatorDirection)
        {
            case 4:
                while (gameObject.transform.position.x >= newPositionValue)
                {
                    gameObject.transform.Translate(-speed, 0.0f, 0.0f);
                    yield return null;
                }
                break;
            case 3:
                while (gameObject.transform.position.x <= newPositionValue)
                {
                    gameObject.transform.Translate(speed, 0.0f, 0.0f);
                    yield return null;
                }
                break;
            case 2:
                while (gameObject.transform.position.y >= newPositionValue)
                {
                    gameObject.transform.Translate(0.0f, -speed, 0.0f);
                    yield return null;
                }
                break;
            case 1:
                while (gameObject.transform.position.y <= newPositionValue)
                {
                    gameObject.transform.Translate(0.0f, speed, 0.0f);
                    yield return null;
                }
                break;
        }

        yield break;
    }
}

/*
public class Move : MonoBehaviour
{
    public int direction;
    public float position;
    public CharacterAnimations.States state;
    public GameObject gameObj;

    public IEnumerator MoveGroup()
    {
        yield break;
    }
}
*/