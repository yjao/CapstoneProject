using UnityEngine;
using System.Collections;

public class TutorialSlideTriggers : MonoBehaviour {

    public GameObject car;
    
    public AudioClip carCrash;
    public AudioClip traffic;
    public AudioClip thump;
    private AudioSource source;

    private bool moveTrigger = false;
    private bool KellyTrigger = false;
    private bool trafficTrigger = false;
    private bool fallTrigger = false;
    private bool playThump = false;
    public static bool defaultTrigger = false;


	// Use this for initialization
	void Start () 
    {
        source = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (moveTrigger && car.transform.position.x < 80)
        {
            car.transform.Translate(0.3f, 0f, 0f);
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("Pause"));
        }
        else if (KellyTrigger)
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("Kelly"));
        }
        else if (trafficTrigger && Input.GetKey(KeyCode.Space))
        {
            source.PlayOneShot(traffic, 1);
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("Traffic"));
            Destroy(GameObject.Find("TempWall"));
            Destroy(GameObject.Find("TriggerPanelMini"));
            defaultTrigger = true;
        }
        else if (car.transform.position.x >= 80)
        {
            //StartCoroutine(TutorialManager.instance.EndCutscene(false));
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("Done"));
        }
        else if (fallTrigger)
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("Fall"));
            fallTrigger = false;
            playThump = true;
        }
        
        if (playThump && GameObject.Find("Alfred").transform.position.y < -11.00f)
        {
            Debug.Log("thump");
            source.PlayOneShot(thump, 1);
            Debug.Log("yay");
            StartCoroutine(TutorialManager.instance.EndCutscene(false));
            playThump = false;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.tag == "InvisCol" && col.gameObject.tag == "Player")
        {
            StartCoroutine(TutorialManager.instance.StartCutscene(false));
            source.PlayOneShot(carCrash, 1);
            moveTrigger = true;
        }
        else if (this.tag == "TriggerPanelMini" && col.gameObject.tag == "Player")
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine(this.tag));
        }
        else if (this.tag == "TrafficLight" && col.gameObject.tag == "Player")
        {
            trafficTrigger = true;
        }
        else if (this.tag == "Kelly" && col.gameObject.tag == "Player")
        {
            KellyTrigger = true;
        }
        else if (this.tag == "TriggerPanel" && col.gameObject.tag == "Player")
        {
            StartCoroutine(TutorialManager.instance.StartCutscene(false));
            fallTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (defaultTrigger)
        {
            KellyTrigger = false;
            trafficTrigger = false;
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("SecondDefaultText"));
        }
        else if (this.tag == "Kelly")
        {
            KellyTrigger = false;
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("FirstDefaultText"));
        }
        else if (this.tag == "TriggerPanelMini")
        {
            StartCoroutine(GameObject.Find("TutorialManager").GetComponent<TutorialManager>().Slide_Triggers_Coroutine("FirstDefaultText"));
        }
        else if (this.tag == "TrafficLight")
        {
            trafficTrigger = false;
        }
    }
}
