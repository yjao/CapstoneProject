using UnityEngine;
using System.Collections;

public class Kelly : MonoBehaviour {

    public static Kelly Instance;

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

    // Use this for initialization
    void Start () {
        Instance = this;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
