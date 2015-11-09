using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Textbox : MonoBehaviour
{
    private GameManager gameManager;
    bool choice_mode;
    int cursor;
    public bool done;
    public string res;
    void Start()
    {
        Debug.Log("hi");
		gameManager = GameManager.Instance;
		gameManager.EnterDialogue();
        choice_mode = false;
        done = false;

        EventManager.OnSpaceBar += SelfDestruct;
    }

    void SelfDestruct(object sender, GameEventArgs args)
    {
        GameObject.Destroy(gameObject);
    }


	void OnDestroy()
	{
        EventManager.OnSpaceBar -= SelfDestruct;
        gameManager.ExitDialogue();
	}
	
	void Update()
    {
        
        if (/*choice_mode == false &&*/ Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject.Destroy(gameObject);
            Debug.Log("DID IT WORK");
            EventManager.NotifySpaceBar(this, new GameEventArgs());
        }
    }

    public void DrawBox(string name, string dialog)
    {
        transform.Find("Name").GetComponent<Text>().text = name;
        transform.Find("Text").GetComponent<Text>().text = dialog;
    }

    public void DrawMessage(string message)
    {
        transform.Find("Name_Panel").gameObject.SetActive(false);
        transform.Find("Text_Panel").gameObject.SetActive(false);
        transform.Find("Name").gameObject.SetActive(false);
        transform.Find("Text").gameObject.SetActive(false);
        transform.Find("Message").gameObject.SetActive(true);
        transform.Find("Message_Panel").gameObject.SetActive(true);
        transform.Find("Message").GetComponent<Text>().text = message;
    }

    public IEnumerator Choice(string name, string dialog, params string[] choices)
    {
        cursor = choices.Length-1;
        choice_mode = true;
        DrawBox(name, dialog);
        transform.Find("Choice_Panel").gameObject.SetActive(true);
        transform.Find("Select").gameObject.SetActive(true);
        Transform[,] c = MultipleChoice(choices);
        transform.Find("Choice_Panel").gameObject.SetActive(false);
        transform.Find("Select").gameObject.SetActive(false);
        transform.Find("Pointer").gameObject.SetActive(true);
        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.62f, .325f+.1f*cursor);
        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.665f, .4f+.1f*cursor);
        yield return null;
        yield return StartCoroutine(WaitForKeyDown(choices.Length-1));

        gameManager.dialogue_choice = choices[cursor];
        choice_mode = false;
        GameObject.Destroy(gameObject);

		// bad code, just testing events
		yield return null;
		EventManager.NotifyDialogChoiceMade(this, new GameEventArgs() {DialogChoice = choices[cursor]});
    }
    
    Transform[,] MultipleChoice(string[] s)
    {
        Transform[,] g = new Transform[s.Length,2];
        transform.Find("Select").gameObject.SetActive(true);
        for (int i = 0; i < s.Length; i++)
        {
            Transform box = (Instantiate(transform.Find("Choice_Panel"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            Transform c = (Instantiate(transform.Find("Select"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            box.transform.SetParent(transform, false);
            c.transform.SetParent(transform, false);
            box.transform.GetComponent<RectTransform>().anchorMax = new Vector2(.9f, (.4f+.1f*i));
            box.transform.GetComponent<RectTransform>().anchorMin = new Vector2(.66f, (.325f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMax = new Vector2(.895f, (.385f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMin = new Vector2(.665f, (.34f+.1f*i));
            c.transform.GetComponent<Text>().text = s[i];
            g[i, 0] = box;
            g[i, 1] = c;
        }
        return g;
    }

    IEnumerator WaitForKeyDown(int x)
    {
        while (true)
        {   
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursor < x)
                {
                    cursor += 1;
                    transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.62f, .325f + .1f * cursor);
                    transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.665f, .4f + .1f * cursor);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (cursor > 0)
                {
                    cursor -= 1;
                    transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.62f, .325f + .1f * cursor);
                    transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.665f, .4f + .1f * cursor);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                done = true;
                yield break;
            }
            yield return null;
        }
    }
}
