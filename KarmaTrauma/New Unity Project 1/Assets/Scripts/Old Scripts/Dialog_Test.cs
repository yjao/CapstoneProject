using UnityEngine;
using System.Collections;

public class Dialog_Test : MonoBehaviour {
    string[] s;
    public GameObject Textbox;
    GameObject t;
	// Use this for initialization
	void Start () {
        s = new string[] {"1","2","3","4","5"};
	    //t = (Instantiate(Dialog, new Vector3(0,0,0), Quaternion.identity)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) && t != null)
            t.GetComponent<Textbox>().DrawBox("Alfred", "Peanut Butter");
        if (Input.GetKeyDown(KeyCode.W))
            GameObject.Destroy(t);
        if (Input.GetKeyDown(KeyCode.E) && t == null)
            t = (Instantiate(Textbox, new Vector3(0, 0, 0), Quaternion.identity)) as GameObject;
            //t.GetComponent<Textbox>().DrawMessage("YOU DIED");
        if (Input.GetKeyDown(KeyCode.A) && t != null)
        {
            //t.GetComponent<Textbox>().test();
        }
        //if (Input.GetKeyDown(KeyCode.R) && t != null)
        //{
        //    StartCoroutine(t.GetComponent<Textbox>().MultiDialog("test", s));
        //}
	}
}
