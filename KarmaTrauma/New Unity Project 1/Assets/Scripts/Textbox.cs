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
    private Choice[] choices;
    public Dialogue Dialog;
    void Awake()
    {
		gameManager = GameManager.instance;
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
        //Debug.Log("updating");
        if (choice_mode == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursor < choices.Length - 1)
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
                GameEventArgs g = new GameEventArgs();
                g.DialogueBox = this;
                if (choices[cursor].setbool != null)
                {
                    gameManager.SetData(choices[cursor].setbool, true);
                }
                if (choices[cursor].CEA != null)
                {
                    g.ConvertChoiceEventArgs(choices[cursor].CEA);
                    choice_mode = false;
                    Interactable.Action oldaction = g.ChoiceAction;
                    EventManager.NotifyDialogChoiceMade(this, g);
                    if (oldaction != continueDialogue)
                    {
                        EventManager.NotifySpaceBar(this, new GameEventArgs());
                    }
                }
                else
                {
                    EventManager.NotifySpaceBar(this, new GameEventArgs());
                }
            }
        }
        else if (choice_mode == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (Dialog.CEA != null)
            {
                GameEventArgs g = new GameEventArgs();
                g.ConvertChoiceEventArgs(Dialog.CEA);
                g.DialogueBox = this;
                Interactable.Action oldaction = Dialog.CEA.ChoiceAction;
                EventManager.NotifyDialogChoiceMade(this, g);
                if (oldaction != continueDialogue)
                {
                    EventManager.NotifySpaceBar(this, new GameEventArgs());
                }
            }
            else
            {
                EventManager.NotifySpaceBar(this, new GameEventArgs());
            }
        }
    }

	#region DIALOG BOXES

	private static string FormatMessage(string message)
	{
		GameManager gameManager = GameManager.instance;

		string newMessage = message;
		while (true)
		{
			int keywordIndex = newMessage.IndexOf(GameManager.QUEST_KEYWORD);
			if (keywordIndex < 0)
			{
				return newMessage;
			}
			
			string keyword = "";
			for (int i = keywordIndex+1; i <= newMessage.Length; i++)
			{
				if (newMessage[i] == GameManager.QUEST_KEYWORD)
				{
					break;
				}
				keyword += newMessage[i];
			}

			string newKeyword = keyword;
			if (gameManager.questTerms.ContainsKey(keyword))
			{
				newKeyword = gameManager.questTerms[keyword];
			}

			if (GameManager.QUEST_KEYWORD_BOLDED)
			{
				newKeyword = "<b><color="+GameManager.QUEST_KEYWORD_COLOR+">" + newKeyword + "</color></b>";
			}
			else
			{
				newKeyword = "<color="+GameManager.QUEST_KEYWORD_COLOR+">" + newKeyword + "</color>";
			}
			newMessage = newMessage.Replace(GameManager.QUEST_KEYWORD+keyword+GameManager.QUEST_KEYWORD, newKeyword);
		}
	}
	
	public void DrawBox(string name, string message)
    {
		message = FormatMessage(message);
        transform.Find("Name").GetComponent<Text>().text = name;
		transform.Find("Text").GetComponent<Text>().text = message;
    }

    public void DrawMessage(string message)
    {
		message = FormatMessage(message);
        transform.Find("Name_Panel").gameObject.SetActive(false);
        transform.Find("Text_Panel").gameObject.SetActive(false);
        transform.Find("Name").gameObject.SetActive(false);
        transform.Find("Text").gameObject.SetActive(false);
        transform.Find("Message").gameObject.SetActive(true);
        transform.Find("Message_Panel").gameObject.SetActive(true);
        transform.Find("Message").GetComponent<Text>().text = message;
    }

    public void Choice(string name, string dialog, Choice[] options)
    {
        cursor = options.Length-1;
        DrawBox(name, dialog);
        transform.Find("Choice_Panel").gameObject.SetActive(true);
        transform.Find("Select").gameObject.SetActive(true);
        Transform[,] c = MultipleChoice(options);
        transform.Find("Choice_Panel").gameObject.SetActive(false);
        transform.Find("Select").gameObject.SetActive(false);
        transform.Find("Pointer").gameObject.SetActive(true);
        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.62f, .325f+.1f*cursor);
        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.665f, .4f+.1f*cursor);
        //Debug.Log("start");
        choice_mode = true;
        choices = options;
    }
    
    Transform[,] MultipleChoice(Choice[] options)
    {
        Transform[,] g = new Transform[options.Length,2];
        transform.Find("Select").gameObject.SetActive(true);
        for (int i = 0; i < options.Length; i++)
        {
            Transform box = (Instantiate(transform.Find("Choice_Panel"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            Transform c = (Instantiate(transform.Find("Select"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            box.transform.SetParent(transform, false);
            c.transform.SetParent(transform, false);
            box.transform.GetComponent<RectTransform>().anchorMax = new Vector2(.9f, (.4f+.1f*i));
            box.transform.GetComponent<RectTransform>().anchorMin = new Vector2(.66f, (.325f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMax = new Vector2(.895f, (.385f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMin = new Vector2(.665f, (.34f+.1f*i));
            c.transform.GetComponent<Text>().text = options[i].option;
            g[i, 0] = box;
            g[i, 1] = c;
        }
        return g;
    }

	#endregion

    public static void continueDialogue(object sender, GameEventArgs args)
    {
        args.DialogueBox.Dialog = args.DialogueBox.gameManager.GetNextDialogue(args.IDNum, args.DialogueID);
        if (args.DialogueBox.transform.Find("Pointer").gameObject.active == true)
        {
            args.DialogueBox.gameManager.DBox(args.IDNum, args.DialogueID);
            if (args.DialogueBox.gameManager.allObjects[args.IDNum].dialogues[args.DialogueID].TypeIsChoice() || GameManager.instance.allObjects[args.IDNum].dialogues[args.DialogueID].Action != null)
            {
                EventManager.OnDialogChoiceMade += args.ThisGameObject.GetComponent<InteractableObject>().HandleOnDialogChoiceMade;
            }
            args.DialogueBox.gameManager.ExitDialogue();
			args.DialogueBox.SelfDestruct(args.DialogueBox, new GameEventArgs());;
        }
		args.DialogueBox.transform.Find("Text").GetComponent<Text>().text = FormatMessage(args.DialogueBox.Dialog.text);
    }
}
