using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Textbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void DrawBox(string name, string dialog)
    {
        transform.Find("Name").GetComponent<Text>().text = name;
        transform.Find("Text").GetComponent<Text>().text = dialog;
    }

    /*
    void Dialogue(string d)
    {
        float maxlen = transform.Find("Text").GetComponent<Text>().preferredWidth;
        while (d.Length > (int)maxlen)
        {
            string current = d.Substring(0, (int)maxlen);
            d = d.Substring((int)maxlen);
        }
    }*/
}
