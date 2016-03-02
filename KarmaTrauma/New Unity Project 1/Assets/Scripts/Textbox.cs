using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Textbox : MonoBehaviour
{
    private GameManager gameManager;

   
    bool choice_mode;
    bool tutorial_mode;
    bool tutorial_choice;
    int cursor;
    public bool done;
    public string res;
    private Choice[] choices;
    public Dialogue Dialog;

    public enum TutorialBoxPosition
    {
        TOP, BOTTOM, MIDDLE
    }
    void Awake()
    {
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;

            gameManager.EnterDialogue();
        }
        choice_mode = false;
        done = false;
        

        EventManager.OnSpaceBar += SelfDestruct;
    }

    static string BuildIntoQuestList(string name, string message)
    {
        bool addToLog =false;
        List<string> qvalue = new List<string>();
        string keyword = FindKeyword(message);
        string newMessage = AddKeywordToMessage(message, keyword);
        if (keyword != "" && !(GameManager.instance.questList.ContainsKey(keyword)))
        {
            qvalue.Add(name);
            qvalue.Add(newMessage);
            addToLog = true;

            GameManager.instance.questList[keyword] = qvalue;
        }
        else if (keyword != "" && GameManager.instance.questList.ContainsKey(keyword))
        {
            List<string> temp = new List<string>();
            for(int i = 2; i < GameManager.instance.questList[keyword].Count; i+= 2){

                temp.Add(GameManager.instance.questList[keyword][i]);
           
           }
            
            for (int k = 0; k < temp.Count; k++)
            {
                if (temp[k] == GameManager.instance.GetTime())
                {
                    addToLog = false;
                }
                else if (k == temp.Count - 1)
                {
                    if (temp[k] != GameManager.instance.GetTime())
                    {
                        addToLog = true;
                    }
                    else
                    {
                        addToLog = false;
                    }
                }
            }

    
        }
        if (addToLog)
        {
            GameManager.instance.questList[keyword].Add(GameManager.instance.GetTime());
            GameManager.instance.questList[keyword].Add(SceneManager.instance.SceneName(Application.loadedLevelName));
        }
        return newMessage;
    }

    void SelfDestruct(object sender, GameEventArgs args)
    {
        EventManager.OnSpaceBar -= SelfDestruct;
        StartCoroutine(play_close_sound());
    }


	void OnDestroy()
	{
        //EventManager.OnSpaceBar -= SelfDestruct;
        if (!(tutorial_mode || tutorial_choice))
        {
            gameManager.ExitDialogue();
        }
	}
	
	void Update()
    {
        if (!tutorial_mode)
        {
            if (choice_mode == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    if (cursor < choices.Length - 1)
                    {
                        cursor += 1;
                        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.50f, .235f + .1f * cursor);
                        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.56f, .31f + .1f * cursor);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    if (cursor > 0)
                    {
                        cursor -= 1;
                        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.50f, .235f + .1f * cursor);
                        transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.56f, .31f + .1f * cursor);
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
                    if (!gameManager.playerData.DialogueHistory.ContainsKey(choices[cursor].CEA.IDNum + "," + choices[cursor].CEA.DialogueID + "," + cursor))
                    {
                        gameManager.playerData.DialogueHistory.Add(choices[cursor].CEA.IDNum + "," + choices[cursor].CEA.DialogueID + "," + cursor, true);
                    }
                    if (choices[cursor].removeitem != null)
                    {
                        gameManager.dayData.removeItem(choices[cursor].removeitem);
                    }
                    if (choices[cursor].CEA != null)
                    {
                        g.ConvertChoiceEventArgs(choices[cursor].CEA);
                        g.DialogueBox = this;
                        choice_mode = false;
                        Interactable.Action oldaction = g.ChoiceAction;
                        EventManager.NotifyDialogChoiceMade(this, g);
                        if (!(oldaction == continueDialogue || oldaction == ContinueTutorialDialogue))
                        {
                            gameObject.GetComponent<AudioSource>().enabled = true;
                            EventManager.NotifySpaceBar(this, new GameEventArgs());
                        }
                    }
                    else
                    {
                        EventManager.NotifyDialogChoiceMade(this, new GameEventArgs());
                        EventManager.NotifySpaceBar(this, new GameEventArgs());
                    }
                }
            }
            else if (choice_mode == false && Input.GetKeyDown(KeyCode.Space))
            {
                if (Dialog != null)
                {
                    if (Dialog.CEA != null)
                    {
                        GameEventArgs g = new GameEventArgs();
                        g.ConvertChoiceEventArgs(Dialog.CEA);
                        g.DialogueBox = this;
                        Interactable.Action oldaction = Dialog.CEA.ChoiceAction;
                        EventManager.NotifyDialogChoiceMade(this, g);
                        if (!(oldaction == continueDialogue || oldaction == ContinueTutorialDialogue))
                        {
                            gameObject.GetComponent<AudioSource>().enabled = true;
                            EventManager.NotifySpaceBar(this, new GameEventArgs());
                        }
                    }
                    else
                    {
                        if (Dialog.choices != null)
                        {
                            EventManager.NotifyDialogChoiceMade(this, new GameEventArgs());
                        }
                        EventManager.NotifySpaceBar(this, new GameEventArgs());
                    }
                }
                else
                {
                    EventManager.NotifySpaceBar(this, new GameEventArgs());
                }
            }
        }
    }

	#region DIALOG BOXES

	public static string ColorTutorialKeyword(string message)
	{
		GameManager gameManager = GameManager.instance;
		
		string newMessage = "";
		int tagOpened = 0;
		for (int i = 0; i < message.Length; i++)
		{
			if (message[i] == GameManager.TUTORIAL_KEYWORD)
			{
				string tag = "";
				if (GameManager.TUTORIAL_KEYWORD_BOLDED && tagOpened <= 1)
				{
					tag = "<b><color=" + GameManager.TUTORIAL_KEYWORD_COLOR + ">";
					tagOpened = 2;
				}
				else if (GameManager.TUTORIAL_KEYWORD_BOLDED && tagOpened == 2)
				{
					tag = "</color></b>";
					tagOpened = 1;
				}
				else if (tagOpened <= 1)
				{
					tag = "<color=" + GameManager.TUTORIAL_KEYWORD_COLOR + ">";
					tagOpened = 2;
				}
				else if (tagOpened == 2)
				{
					tag = "</color>";
					tagOpened = 1;
				}

				newMessage += tag;
			}
			else
			{
				newMessage += message[i];
			}
		}
		return newMessage;
	}
	
	private static string FindKeyword(string message)
	{
		GameManager gameManager = GameManager.instance;
		
		string newMessage = message;
		while (true)
		{
			int keywordIndex = newMessage.IndexOf(GameManager.QUEST_KEYWORD);
			if (keywordIndex < 0)
			{
				return "";
			}
			
			string keyword = "";
			for (int i = keywordIndex + 1; i <= newMessage.Length; i++)
            {
                if (newMessage[i] == GameManager.QUEST_KEYWORD)
                {
                    return keyword;
                    //break;
                }
                keyword += newMessage[i];
            }
        }
    }

    private static string FormatKeyWord(string keyword)
    {
        GameManager gameManager = GameManager.instance;
        string newKeyword = keyword;
        if (gameManager.questTerms.ContainsKey(keyword))
        {
            newKeyword = gameManager.questTerms[keyword];
        }

        if (GameManager.QUEST_KEYWORD_BOLDED)
        {
            newKeyword = "<b><color=" + GameManager.QUEST_KEYWORD_COLOR + ">" + newKeyword + "</color></b>";
            return newKeyword;

        }
        else
        {
            newKeyword = "<color=" + GameManager.QUEST_KEYWORD_COLOR + ">" + newKeyword + "</color>";
            return newKeyword;
        }
        //return newKeyword;
    }

    

    private static string AddKeywordToMessage(string message, string keyword)
    {
        string newkeyword = FormatKeyWord(keyword);
         
        return message.Replace(GameManager.QUEST_KEYWORD + keyword + GameManager.QUEST_KEYWORD, newkeyword);
    }

	private static string FormatMessage(string message)
	{
        return AddKeywordToMessage(message, FindKeyword(message));
	}


    public void DrawBox(string name, string message)
    {
        
        message = BuildIntoQuestList(name, message);
    }
	
	public void DrawBox(string name, string message, int id)
    {
        play_open_sound();
        message = BuildIntoQuestList(name, message);
        if (gameManager.playerData.DialogueHistory.ContainsKey(id + "," + Dialog.iD))
        {
            if (gameManager.playerData.DialogueHistory[id + "," + Dialog.iD])
            {
                transform.Find("Text_Panel").GetComponent<Image>().color = new Color((22f/255f), (22f/255f), (92f/255f), (150f/255f));
            }
        }
        else
        {
            if (id != -1)
            {
                gameManager.playerData.DialogueHistory.Add(id + "," + Dialog.iD, true);
            }
        }
		message = FormatMessage(message);

        transform.Find("Name").GetComponent<Text>().text = name;
		transform.Find("Text").GetComponent<Text>().text = message;
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

    public IEnumerator DrawTutorialBox(string message, float destroytimer = -1, TutorialBoxPosition position = TutorialBoxPosition.MIDDLE, bool transparent=false)
    {
        EventManager.OnSpaceBar -= SelfDestruct;
        tutorial_mode = true;
        if (gameManager != null)
        {
            gameManager.ExitDialogue();
        }
        if (transparent)
        {
            Color c = transform.Find("Message_Panel").GetComponent<Image>().color;
            c.a = 160f/255;
            transform.Find("Message_Panel").GetComponent<Image>().color = c;
        }
        else if (!transparent)
        {
            Color c = transform.Find("Message_Panel").GetComponent<Image>().color;
            c.a = 255f/255;
            transform.Find("Message_Panel").GetComponent<Image>().color = c;
        }
        DrawMessage(message);
        if (position == TutorialBoxPosition.TOP)
        {
            transform.Find("Message_Panel").GetComponent<RectTransform>().anchorMin = new Vector2(.2f, .65f);
            transform.Find("Message_Panel").GetComponent<RectTransform>().anchorMax = new Vector2(.8f, .95f);
            transform.Find("Message").GetComponent<RectTransform>().anchorMin = new Vector2(.2f, .65f);
            transform.Find("Message").GetComponent<RectTransform>().anchorMax = new Vector2(.8f, .95f);
        }
        if (position == TutorialBoxPosition.BOTTOM)
        {
            transform.Find("Message_Panel").GetComponent<RectTransform>().anchorMin = new Vector2(.2f, .05f);
            transform.Find("Message_Panel").GetComponent<RectTransform>().anchorMax = new Vector2(.8f, .35f);
            transform.Find("Message").GetComponent<RectTransform>().anchorMin = new Vector2(.2f, .05f);
            transform.Find("Message").GetComponent<RectTransform>().anchorMax = new Vector2(.8f, .35f);
        }
        yield return null;
        if (destroytimer != -1)
        {
            yield return new WaitForSeconds(destroytimer);
            GameObject.Destroy(gameObject);
        }
        //GameObject.Destroy(gameObject);
    }

    public void Choice(string name, string dialog, Choice[] options, int id)
    {
        options = checkChoices(options);
        if (options == null)
        {
            DrawBox(name, dialog, id);
        }
        else
        {
            cursor = options.Length - 1;
            DrawBox(name, dialog, id);
            transform.Find("Choice_Panel").gameObject.SetActive(true);
            transform.Find("Select").gameObject.SetActive(true);
            Transform[,] c = MultipleChoice(options);
            transform.Find("Choice_Panel").gameObject.SetActive(false);
            transform.Find("Select").gameObject.SetActive(false);
            transform.Find("Pointer").gameObject.SetActive(true);
            transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.50f, .235f + .1f * cursor);
            transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.56f, .31f + .1f * cursor);
            choice_mode = true;
            choices = options;
        }
    }
    
    private Choice[] checkChoices(Choice[] options)
    {
        int counter = 0;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].checkbool != null)
            {
                if (gameManager.GetData(options[i].checkbool) == false && gameManager.HasItem(options[i].checkbool) == false)
                {
                    counter += 1;
                }
            }
        }
        if (counter == options.Length)
        {
            return null;
        }
        Choice[] result = new Choice[options.Length - counter];
        counter = 0;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].checkbool == null || gameManager.GetData(options[i].checkbool) == true)
            {
                result[counter] = options[i];
                counter += 1;
            }
        }
        choices = result;
        return result;
    }

    Transform[,] MultipleChoice(Choice[] options)
    {
        Vector2 choice_anchor_min = transform.Find("Choice_Panel").GetComponent<RectTransform>().anchorMin;
        Vector2 choice_anchor_max = transform.Find("Choice_Panel").GetComponent<RectTransform>().anchorMax;
        Vector2 text_anchor_min = transform.Find("Select").GetComponent<RectTransform>().anchorMin;
        Vector2 text_anchor_max = transform.Find("Select").GetComponent<RectTransform>().anchorMax;
        Transform[,] g = new Transform[options.Length,2];
        transform.Find("Select").gameObject.SetActive(true);
        for (int i = 0; i < options.Length; i++)
        {
            Transform box = (Instantiate(transform.Find("Choice_Panel"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            Transform c = (Instantiate(transform.Find("Select"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
            box.transform.SetParent(transform, false);
            c.transform.SetParent(transform, false);
            box.transform.GetComponent<RectTransform>().anchorMax = new Vector2(choice_anchor_max.x, (.3f+.1f*i));
            box.transform.GetComponent<RectTransform>().anchorMin = new Vector2(choice_anchor_min.x, (.225f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMax = new Vector2(text_anchor_max.x, (.285f+.1f*i));
            c.transform.GetComponent<RectTransform>().anchorMin = new Vector2(text_anchor_min.x, (.24f+.1f*i));
            c.transform.GetComponent<Text>().text = options[i].option;
            g[i, 0] = box;
            g[i, 1] = c;
            if (gameManager.playerData.DialogueHistory.ContainsKey(options[i].CEA.IDNum + "," + options[i].CEA.DialogueID + "," + i))
            {
                box.transform.GetComponent<Image>().color = new Color(22 / 255f, 22 / 255f, 92 / 255f, 150 / 255f);
            }
        }
        return g;
    }

	#endregion

    public static void continueDialogue(object sender, GameEventArgs args)
    {
        args.DialogueBox.GetComponent<AudioSource>().enabled = false;
        args.DialogueBox.Dialog = args.DialogueBox.gameManager.GetNextDialogue(args.IDNum, args.DialogueID);
        if (args.DialogueBox.transform.Find("Pointer").gameObject.active == true)
        {
            args.DialogueBox.gameManager.DBox(args.IDNum, args.DialogueID);
            if (args.DialogueBox.gameManager.allObjects[args.IDNum].dialogues[args.DialogueID].TypeIsChoice() || GameManager.instance.allObjects[args.IDNum].dialogues[args.DialogueID].Action != null)
            {
                //EventManager.OnDialogChoiceMade += args.ThisGameObject.GetComponent<InteractableObject>().HandleOnDialogChoiceMade;
            }
            args.DialogueBox.gameManager.ExitDialogue();
			args.DialogueBox.SelfDestruct(args.DialogueBox, new GameEventArgs());;
        }
        string name = args.DialogueBox.transform.Find("Name").GetComponent<Text>().text;
        string message = BuildIntoQuestList(name, args.DialogueBox.Dialog.text);
		args.DialogueBox.transform.Find("Text").GetComponent<Text>().text = message;
        if (args.DialogueBox.gameManager.allObjects[args.IDNum].dialogues[args.DialogueID].TypeIsChoice() || GameManager.instance.allObjects[args.IDNum].dialogues[args.DialogueID].Action != null)
        {
            EventManager.OnDialogChoiceMade += args.ThisGameObject.GetComponent<InteractableObject>().HandleOnDialogChoiceMade;
        }
    }

    public static void ContinueTutorialDialogue(object sender, GameEventArgs args)
    {
        args.DialogueBox.GetComponent<AudioSource>().enabled = false;
        args.TutorialDialogueCounter -= 1;
        if (args.DialogueBox.transform.Find("Pointer").gameObject.active == true)
        {
            EventManager.OnDialogChoiceMade += InteractableObject.HandleTutorial;
            string text = Textbox.FormatMessage(args.TutorialDialogues[0]);
            Dialogue d = new Dialogue(-1, Textbox.ColorTutorialKeyword(text));
            d.CEA = new ChoiceEventArgs() { ChoiceAction = args.ChoiceAction, TutorialDialogues = args.TutorialDialogues, TutorialDialogueCounter = args.TutorialDialogueCounter };
            d.Action += d.CEA.ChoiceAction;
            GameManager.instance.CreateDialogue(args.DialogueBox.transform.Find("Name").GetComponent<Text>().text, d, -1);
            args.DialogueBox.tutorial_choice = true;
            args.DialogueBox.transform.Find("Pointer").gameObject.SetActive(false);
            args.DialogueBox.SelfDestruct(args.DialogueBox, new GameEventArgs()); ;
        }
        else
        {
            args.DialogueBox.Dialog.CEA.TutorialDialogueCounter -= 1;
            string name = args.DialogueBox.transform.Find("Name").GetComponent<Text>().text;
            string text = Textbox.FormatMessage(args.TutorialDialogues[args.TutorialDialogues.Length - args.TutorialDialogueCounter]);
            args.DialogueBox.transform.Find("Text").GetComponent<Text>().text = Textbox.ColorTutorialKeyword(text);
            if (args.TutorialDialogueCounter != 1)
            {
                EventManager.OnDialogChoiceMade += InteractableObject.HandleTutorial;
            }
            else
            {
                args.DialogueBox.Dialog.CEA = null;
            }
        }
    }

    private void play_open_sound()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (!source.enabled)
        {
            source.enabled = true;
        }
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.clip = Resources.Load<AudioClip>("Open");
        source.time = .4f;
        source.Play();
    }
    private IEnumerator play_close_sound()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (source.enabled)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            source.clip = Resources.Load<AudioClip>("Close");
            source.time = 0;
            source.Play();
            source.SetScheduledEndTime(AudioSettings.dspTime + (.224f));
            gameObject.GetComponent<Canvas>().enabled = false;
            while (source.isPlaying)
            {
                yield return null;
            }
        }

        GameObject.Destroy(gameObject);
        yield return null;
    }
}
