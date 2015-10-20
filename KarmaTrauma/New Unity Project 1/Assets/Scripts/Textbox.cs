using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Textbox : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
		gameManager = GameManager.Instance;
		gameManager.EnterDialogue();
    }

	void OnDestroy()
	{
		gameManager.ExitDialogue();
	}
	
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Destroy(gameObject);
        }
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
