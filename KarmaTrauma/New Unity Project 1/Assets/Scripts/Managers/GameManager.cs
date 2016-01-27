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

    public Dictionary<int, Interactable> allObjects;
	public Dictionary<string, string> questTerms;
	public Dictionary<string, List<InteractableObject.Parameters>> sceneParameters;
    //Questlist in the form of List<[keyword , NPCName, dialogue]>
    
    public List<string[]> questList;
	#region CONSTANT VALUES

	public const char QUEST_KEYWORD = '#';
	public const string QUEST_KEYWORD_COLOR = "yellow";
	public const bool QUEST_KEYWORD_BOLDED = true;

	private const bool PARSING_MODE = true;
	private const int END_DAY_HOUR = 24;
	private const int START_DAY_HOUR = 6;

	#endregion

	// Clock
	private int gameClock = 6;
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
        questList = new List<string[]>();

        // Bind events
        EventManager.OnItemPickup += ItemPickup;

        //item stuff
        allItems = new Dictionary<int, Item>()
	    {
			{ 150, new Item("Jewel", "Quit stealing jewels already", "jewel")},
            { 151, new Item("TestJewel", "IM NOT EVEN AN ITEM", "jewel")},
            { 133, new Item("Bacon and Eggs", "Feed it to the hungry.", "baconAndEggs")},
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
        gameClockDisplay = gameClock.ToString() + "AM";
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
            CreateChoice(ch.name, dialogue.text, dialogue.choices);
        }
        else
        {
            CreateDialogue(ch.name, dialogue);
        }
    }

    public void CreateDialogue(string name, Dialogue message)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().Dialog = message;
        dialog.GetComponent<Textbox>().DrawBox(name, message.text);
    }

    public void CreateChoice(string name, string message, Choice[] options)
    {
        GameObject dialog = (GameObject)Instantiate(dialogueContainer, dialogueContainer.transform.position, Quaternion.identity);

        //Debug.Log(options[0].option);
        dialog.GetComponent<Textbox>().Choice(name, message, options);
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

    public Dialogue GetNextDialogue(int id, int dialogueID)
    {
        Interactable ch = allObjects[id];
        return ch.dialogues[dialogueID];
    }

    public void EnterDialogue()
    {
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
    }

    #endregion

	public Interactable GetInteractableByID(int id)
	{
		return allObjects[id];
	}

	#region CLOCK & TIME

    void upDateClock()
    {
        int temp = gameClock + 2;
        
        if (gameClock < 12 && temp != 12)
        {            
            gameClockDisplay = gameClock.ToString() + " - " + temp + "AM";
        }
        else if (temp == 12)
        {
            gameClockDisplay = "10 - 12PM";
        }
        else if (gameClock == 12)
        {
            gameClockDisplay = "12 - 2PM";
        }
        else if (gameClock > 12 && gameClock < 22)
        {
            int time = gameClock - 12;
            int temp1 = time + 2;
            gameClockDisplay = time.ToString() + " - " + temp1 + "PM";
        }
        else if (gameClock >= 22)
        {
            gameClockDisplay = "10 - 12AM";
        }
    }

    public string GetTime()
    {
        return gameClockDisplay;
    }

    public int GetTimeAsInt()
    {
        return gameClock;
    }

    public bool Midnight(bool createMessage=true)
    {
        //Debug.Log("data is null: " + (playerData == null));
        //Debug.Log("gameClock : " + gameClock);
		if (gameClock == END_DAY_HOUR)
        {
            gameClock = START_DAY_HOUR;
            dayData.Wipe();
            playerData.WipeQuest();
            playerData.daysPassed++;
			if (createMessage)
			{
	            CreateMessage("Day "+(playerData.daysPassed+1)+".", true);
			}
			SceneManager.instance.LoadScene();
            return true;
        }
   
        return false;
    }

    public void IncreaseTime()
    {
        //Debug.Log("prevgameClock: " + gameClock);
        gameClock += 2;
        Debug.Log("gameClock: " + gameClock);
    }

    public void SetTime(int time)
    {
        gameClock = time;
    }

	#endregion


    void ItemPickup(object sender, GameEventArgs args)
    {
        Debug.Log(args.IDNum);
        dayData.Inventory[dayData.ItemAmount] = allItems[args.IDNum];
        dayData.ItemAmount += 1;
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

    public Item[] GetItemData()
    {
        return dayData.Inventory;
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
        upDateClock();
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
		instance.SetTime(END_DAY_HOUR);
		instance.Midnight(false);
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
	}
}
