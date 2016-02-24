using UnityEngine;
using System.Collections;

public class TutorialSlide4Triggers : MonoBehaviour {

    public GameObject car;
    
    public AudioClip carCrash;
    public AudioClip traffic;
    private AudioSource source;

    private bool moveTrigger = false;
    private bool KellyTrigger = false;
    private bool trafficTrigger = false;

	// Use this for initialization
	void Start () 
    {
        source = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (moveTrigger && car.transform.position.x < 100)
        {
            car.transform.Translate(0.3f, 0f, 0f);
        }
        if (KellyTrigger) //&& Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide4_Triggers_Coroutine("Kelly"));
        }
        if (trafficTrigger && Input.GetKey(KeyCode.Space))
        {
            source.PlayOneShot(traffic, 1);
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide4_Triggers_Coroutine("traffic"));
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.tag == "InvisCol" && col.gameObject.tag == "Player")
        {
            source.PlayOneShot(carCrash, 1);
            moveTrigger = true;
        }
        else if (this.tag == "TrafficLight" && col.gameObject.tag == "Player")
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide4_Triggers_Coroutine(this.tag));
            trafficTrigger = true;
        }
        else if (this.tag == "Kelly" && col.gameObject.tag == "Player")
        {
            KellyTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (this.tag == "Kelly")
        {
            KellyTrigger = false;
        }
        if (this.tag == "TrafficLight")
        {
            trafficTrigger = false;
        }
    }
}
