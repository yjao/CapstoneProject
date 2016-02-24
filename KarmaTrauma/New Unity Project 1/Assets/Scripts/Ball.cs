using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public AudioClip carCrash;
    private AudioSource source;
	// Use this for initialization
	void Start () 
    {
        source = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            source.PlayOneShot(carCrash, 1);
        }
    }
}
