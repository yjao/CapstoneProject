using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{

    public PlayerData playerData = new PlayerData();
    public DayData dayData = new DayData();
    public static GameManager instance;
    public GameObject dialogueContainer;
    public enum GameMode
    {
        NONE, PLAYING, DIALOGUE, MENU, CUTSCENE, WAITING, LOG
    };
    public GameMode gameMode = GameMode.NONE;
    public GameMode prevMode = GameMode.NONE;

    public enum Area
    {
        NONE, HOUSE, APARTMENT, POLICE, MALL, PARK, HOSPITAL, SCHOOL
    };
    public Area currentArea = Area.HOUSE;

	public enum TimeType
	{
		INCREASE, END_DAY, SET
	};

    public Dictionary<int, Interactable> allObjects;
	public Dictionary<string, string> questTerms;
	public Dictionary<string, List<InteractableObject.Parameters>> sceneParameters;
    //Questlist in the form of List<[keyword , NPCName, dialogue]>
    
    //public List<string[]> questList;
    public Dictionary<string,string[]> questList;
    public List<OutcomeManager.outcome> outcomeList;

    private bool reset;
    public bool has_text_box;
   
	#region CONSTANT VALUES

	public const char QUEST_KEYWORD = '#';
	public const string QUEST_KEYWORD_COLOR = "yellow";
	public const bool QUEST_KEYWORD_BOLDED = true;

	private const bool PARSING_MODE = true;
	private const int END_DAY_HOUR = 24;
	private const int START_DAY_HOUR = 6;

	#endregion

	// Clock
	private int gameClock = 8;
	private string gameClockDisplay = "";

    //for items;
    public Item[] items;
    public int itemAmount;
    public Dictionary<int, Item> allItems;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // default to playing mode for now
        gameMode = GameMode.PLAYING;

        // Load interaction info here?
		allObjects = new Dictionary<int, Interactable>();
		questTerms = new Dictionary<string, string>();
		sceneParameters = new Dictionary<string, List<InteractableObject.Parameters>>();
        questList = new Dictionary<string, string[]>();
        outcomeList = new List<OutcomeManager.outcome>();

        // Bind events
        EventManager.OnItemPickup += ItemPickup;

        //item stuff
        allItems = new Dictionary<int, Item>()
	    {
            { 24, new Item("Lost Dog", "A lost dog. It seems to really like bacon.", "RaesDog")},
			{ 150, new Item("Jewel", "Quit stealing jewels already", "jewel")},
            { 151, new Item("TestJewel", "IM NOT EVEN AN ITEM", "jewel")},
            { 110, new Item("Bacon", "Maybe you could feed this to someone", "baconAndEggs")},
            { 152, new Item("Alfred's Jewel", "This was a gift from his son.", "alfredsjewel")}
        };

        //items = new Item[9];
        //items[0] = new Item("Momo", "The original soul of Chelsey", "sprite1");
        //items[1] = new Item("Jewel", "Contains the soul of a demon. Or just a jewel for debugging", "jewel");
        //items[2] = new Item("Bacon and Eggs", "Yum", "baconAndEggs");
        //items[3] = new Item("Stairs", "How did you steal the stairs?!", "stairs");
        //itemAmount = 4;

        // Parse Game playerData
        if (PARSING_MODE)
        {
            DataLoader dataLoader = new DataLoader();
        }
        else
        {
            LoadGameData();
        }
    }

    void Start()
    {
        gameClockDisplay = gameClock.ToString() + " - " +  (gameClock+2).ToString() + "PM";

        reset = true;
    }
    void OnDestroy()
    {
        EventManager.OnItemPickup -= ItemPickup;
    }

    public void Play()
    {
        gameMode = GameMode.PLAYING;
    }

    public void Wait()
    {
        gameMode = GameMode.WAITING;
    }



    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.DK");   // Saves in SaveData.DK file
        Debug.Log("File Saved");
        Debug.Log(Application.persistentDataPath);
        //PlayerData data = new PlayerData();
        //playerData.AlfredJumpsCW = true;

        bf.Serialize(file, playerData);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.DK"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.DK", FileMode.Open);
            PlayerData Data = (PlayerData)bf.Deserialize(file);
            Debug.Log("File Load");
            //Debug.Log(playerData.AlfredJumpsCW);
            file.Close();

            //days = data.days;
            //progress = data.progress;
        }


    }

    #region DIALOG BOX

    public void DBox(int id, int dialogueId)
    {
        Interactable ch = allObjects[id];
        ch.lastDialogueDisplayed = dialogueId;
        DBox(id);
    }

    // Re-send current Dialogue
    public void DBox(int id, bool next = false)
    {
        Interactable ch = allObjects[id];
        if (next)
		{
            ch.lastDialogueDisplayed++;
		}
        else if (!next && ch.lastDialogueDisplayed < 0)
		{
			ch.lastDialogueDisplayed = 0;
		}

		Dialogue dialogue = ch.dialogues[ch.lastDialogueDisplayed];
		if (dialogue.setbool != null)
        {
			dayData.SetBool(dialogue.setbool);
        }
        if (dialogue.TypeIsChoice())
        {
            CreateChoice(ch.name, dialogue, id);
        }
        else
        {
            CreateDialogue(ch.name, dialogue, id);
        }
    }

    public void CreateDialogue(string name, Dialogue message, int id)
    {
     
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().Dialog = message;
        dialog.GetComponent<Textbox>().DrawBox(name, message.text, id);
    }

    public void CreateChoice(string name, Dialogue dialogue, int id)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);

        //Debug.Log(options[0].option);
        dialog.GetComponent<Textbox>().Dialog = dialogue;
        dialog.GetComponent<Textbox>().Choice(name, dialogue.text, dialogue.choices, id);
        /*
        message = message.Substring(3);
        string text = message.Substring(0, message.IndexOf("%C"));
        message = message.Substring(message.IndexOf("%C") + 2);
        string[] choices = message.Split(new string[] { "%C" }, System.StringSplitOptions.None);
        choices[choices.Length - 1] = choices[choices.Length - 1].Substring(0, choices[choices.Length - 1].Length - 1);
        yield return null;
        yield return StartCoroutine(dialog.GetComponent<Textbox>().Choice(name, "\"" + text + "\"", ID, choices));
        */
        //Debug.Log(dialogue_choice);
    }

	public void CreateMessage(string message, bool persistUntilClosed=false)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().DrawMessage(message);
		if (persistUntilClosed)
		{
			DontDestroyOnLoad(dialog);
		}
    }

    public GameObject CreateTutorialBox(string message, int destroyTimer = -1, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        StartCoroutine(dialog.GetComponent<Textbox>().DrawTutorialBox(message, destroyTimer, position));
        return dialog;
    }

    public Dialogue GetNextDialogue(int id, int dialogueID)
    {
        Interactable ch = allObjects[id];
        return ch.dialogues[dialogueID];
    }

    public void EnterDialogue()
    {
        has_text_box = true;
		if (gameMode != GameMode.DIALOGUE)
		{
        	prevMode = gameMode;
		}
        gameMode = GameMode.DIALOGUE;
    }

    public void ExitDialogue()
    {
        
        GameMode oldGameMode = gameMode;
        gameMode = prevMode;
        prevMode = oldGameMode;
        has_text_box = false;
    }

    #endregion

	public Interactable GetInteractableByID(int id)
	{
		return allObjects[id];
	}

	#region CLOCK & TIME

    string upDateClock()
    {
        int temp = gameClock + 2;

        if (gameClock < 12 && temp != 12)
        {
            return gameClock.ToString() + " - " + temp + "AM";
        }
        else if (temp == 12)
        {
            return "10 - 12PM";
        }
        else if (gameClock == 12)
        {
            return "12 - 2PM";
        }
        else if (gameClock > 12 && gameClock < 22)
        {
            int time = gameClock - 12;
            int temp1 = time + 2;
            return time.ToString() + " - " + temp1 + "PM";
        }
        else if (gameClock >= 22)
        {
            return "10 - 12AM";
        }
        Debug.Log("lllllllllllllllll");
        return gameClockDisplay;
    }

    public string GetTime()
    {
        return gameClockDisplay;
    }

    public int GetTimeAsInt()
    {
        return gameClock;
    }

    public bool MN_call()
    {
        return reset;
    }

    private bool Midnight(bool createMessage=true)
    {
        //Debug.Log("data is null: " + (playerData == null));
        //Debug.Log("gameClock : " + gameClock);
		if (GetTimeAsInt() == END_DAY_HOUR)
        {
            gameClock = START_DAY_HOUR;
            dayData.Wipe();
            reset = true;
            playerData.WipeQuest();
            playerData.daysPassed++;
			if (createMessage)
			{
	            CreateMessage("Day "+(playerData.daysPassed+1)+".", true);
			}
            transform.Find("Menu_layout/Inventory").GetComponent<Menu>().close();
            transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
			SceneManager.instance.LoadScene(SceneManager.SCENE_HOUSE);
            return true;
        }
        reset = false;
        return false;
    }

    public bool SetTime(TimeType type, int time=0, bool delay=false)
	{
		switch (type)
		{
		case TimeType.INCREASE:
			gameClock += 2;
			break;
		case TimeType.END_DAY:
			gameClock = END_DAY_HOUR;
			break;
		default:
			if (time >= 6 && time <= 24)
			{
				gameClock = time;
			}
			break;
		}

		bool midnight = Midnight();
		SetTime(gameClock, delay);
		return midnight;
	}

    private void SetTime(int time, bool delay=false)
    {
        gameClock = time;
        string newDisplay = upDateClock();
        if (delay)
        {
            StartCoroutine(DelaySetClock(newDisplay));
        }
        else
        {
			ApplyTint();
            gameClockDisplay = newDisplay;
        }
    }

    IEnumerator DelaySetClock(string display)
    {
        yield return new WaitForSeconds(1.5f);
        gameClockDisplay = display;
		ApplyTint();
        yield break;       
    }

	private void ApplyTint()
	{
		SceneManager.instance.tint_screen(Application.loadedLevelName, GetTimeAsInt());
	}

	#endregion


    void ItemPickup(object sender, GameEventArgs args)
    {
        dayData.Inventory[dayData.ItemAmount] = allItems[args.IDNum];
        dayData.DataDictionary[allItems[args.IDNum].Name] = true;
        dayData.ItemAmount += 1;
    }

    public bool HasData(string name)
    {
        if (playerData.DataDictionary.ContainsKey(name) || dayData.DataDictionary.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    public bool GetData(string name)
    {
        if (playerData.DataDictionary.ContainsKey(name))
        {
            return playerData.DataDictionary[name];
        }
        else if (dayData.DataDictionary.ContainsKey(name))
        {
            return dayData.DataDictionary[name];
        }
        else
        {
            return false;
        }
    }

    public void SetData(string name, bool value)
    {
        if (playerData.DataDictionary.ContainsKey(name))
        {
            //playerData.DataDictionary[name] = value;
			playerData.SetBool(name, value);
        }
        else if (dayData.DataDictionary.ContainsKey(name))
        {
            dayData.SetBool(name, value);
        }
        else if (dayData.QuestComplete.ContainsKey(name))
        {
            //dayData.QuestComplete[name] = value;
			dayData.SetQuest(name, value);
			AllQuestsDone();
        }
    }

    public void SetDayData(string name, bool value)
    {
        if (dayData.DataDictionary.ContainsKey(name))
        {
            dayData.SetBool(name, value);
        }
        else
        {
            dayData.DataDictionary.Add(name,value);
        }
    }

    public Item[] GetItemData()
    {
        return dayData.Inventory;
    }

    public int GetItemAmount()
    {
        return dayData.ItemAmount;
    }

    public bool HasItem(string name)
    {
        for (int item = 0; item < dayData.Inventory.Length; item++)
        {
            if (dayData.Inventory[item] != null)
            {
                if (dayData.Inventory[item].Name == name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool AllQuestsDone()
    {
        bool result = true;
        foreach (bool questcomplete in dayData.QuestComplete.Values)
        {
            result = (result && questcomplete);
            if (result == false)
            {
                break;
            }
        }
        if (result == true)
        {
            Application.LoadLevel("TempEnding");
            GameObject.Destroy(GameManager.instance.gameObject);
        }
        return result;
    }

    void Update()
    {
        // For debug purposes (obviously)
        //Debug.Log (GameMode);
    }

    #region Game data Loading

    public void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.DK");
        Debug.Log("Game data saved to file in " + Application.persistentDataPath);

        bf.Serialize(file, allObjects);
        file.Close();
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.DK"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.DK", FileMode.Open);
            allObjects = (Dictionary<int, Interactable>)bf.Deserialize(file);
            Debug.Log("Game data loaded from file");
            file.Close();
        }
    }

    #endregion

	public static void UseBed(object sender, GameEventArgs args)
	{
		instance.SetTime(TimeType.END_DAY, delay:true);
	}

	public static void UnlockDoor(object sender, GameEventArgs args)
	{
		GameObject obj = GameObject.Find(args.String);
		if (obj == null)
		{
			Debug.Log("The door (teleporter) '" + args.String + "' is not found.");
			return;
		}
		obj.GetComponent<Stairs>().active = true;
        GameManager.instance.SetDayData(args.String, true);
	}

}
