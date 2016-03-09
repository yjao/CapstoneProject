using UnityEngine;
using System.Collections;

public class LocationArrow : MonoBehaviour {
    public string locationName;

    SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(Player.Instance.locationString==locationName)
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
	}
}
