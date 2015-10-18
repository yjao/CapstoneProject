using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public GameObject Dialog;
    GameObject t;
	// Use this for initialization
	void Start () {
	    //t = (Instantiate(Dialog, new Vector3(0,0,0), Quaternion.identity)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) && t != null)
            t.GetComponent<Textbox>().DrawBox("Alfred", "Peanut Butter");
        if (Input.GetKeyDown(KeyCode.W))
            GameObject.Destroy(t);
        if (Input.GetKeyDown(KeyCode.E) && t == null)
            t = (Instantiate(Dialog, new Vector3(0, 0, 0), Quaternion.identity)) as GameObject;
	}
}
