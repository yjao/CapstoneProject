using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    private int gameClock = 6;
    private string gameClockDisplay = "";
    private const bool PARSING_MODE = true;

    public PlayerData Data = new PlayerData();
    //public PlayerData Data;
    public DayData dayData = new DayData();
    public static GameManager Instance;
    public GameObject DialogueContainer;
    public enum MODE
    {
        NONE, PLAYING, DIALOGUE, MENU, CUTSCENE, WAITING, LOG
    };
    public MODE GameMode = MODE.NONE;
    public MODE PrevMode = MODE.NONE;

    public enum AREA
    {
        NONE, HOUSE, APARTMENT, POLICE, MALL, PARK, HOSPITAL, SCHOOL
    };
    public AREA CurrentArea = AREA.HOUSE;

    public Dictionary<int, Interactable> AllObjects;
	public Dictionary<string, string> questTerms;

	public const char QUEST_KEYWORD = '#';
	public const string QUEST_KEYWORD_COLOR = "yellow";
	public const bool QUEST_KEYWORD_BOLDED = true;

    //for items;
    public Item[] items;
    public int itemAmount;
    public Dictionary<int, Item> AllItems;

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        

        // default to playing mode for now
        GameMode = MODE.PLAYING;

        // Load interaction info here?
		AllObjects = new Dictionary<int, Interactable>();
		questTerms = new Dictionary<string, string>();

        // Bind events
        //EventManager.OnDialogChoiceMade += HandleOnDialogChoiceMade;
        EventManager.OnItemPickup += ItemPickup;

        //item stuff
        AllItems = new Dictionary<int, Item>()
	    {
			{ 150, new Item("Jewel", "Quit stealing jewels already", "jewel")},
            { 151, new Item("TestJewel", "IM NOT EVEN AN ITEM", "jewel")},
            { 133, new Item("Bacon and Eggs", "Feed it to the hungry.", "baconAndEggs")}
        };

        //items = new Item[9];
        //items[0] = new Item("Momo", "The original soul of Chelsey", "sprite1");
        //items[1] = new Item("Jewel", "Contains the soul of a demon. Or just a jewel for debugging", "jewel");
        //items[2] = new Item("Bacon and Eggs", "Yum", "baconAndEggs");
        //items[3] = new Item("Stairs", "How did you steal the stairs?!", "stairs");
        //itemAmount = 4;

        // Parse Game Data
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
        Data = PlayerData.Instance;
    }
    void OnDestroy()
    {
        //EventManager.OnDialogChoiceMade -= HandleOnDialogChoiceMade;
        EventManager.OnItemPickup -= ItemPickup;
    }

    public void Play()
    {
        GameMode = MODE.PLAYING;
    }

    public void Wait()
    {
        GameMode = MODE.WAITING;
    }



    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.DK");   // Saves in SaveData.DK file
        Debug.Log("File Saved");
        Debug.Log(Application.persistentDataPath);
        //PlayerData data = new PlayerData();
        //Data.AlfredJumpsCW = true;

        bf.Serialize(file, Data);
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
            //Debug.Log(Data.AlfredJumpsCW);
            file.Close();

            //days = data.days;
            //progress = data.progress;
        }


    }

    #region DIALOG BOX

    /*void HandleOnDialogChoiceMade(object sender, GameEventArgs args)
	{
		// let's hard code something
        Debug.Log(args.DialogChoice + " and " + args.ChoiceAction);
		//if (args.DialogChoice == "\"I guess I'll eat it.\"")
		//	CreateMessage("That's creepy! You actually ate it!?");
	}*/


    public void DBox(int id, int dialogueId)
    {
        Interactable ch = AllObjects[id];
        ch.LastDialogueDisplayed = dialogueId;
        DBox(id);
    }

    // Re-send current Dialogue
    public void DBox(int id, bool next = false)
    {
        Interactable ch = AllObjects[id];
        if (next)
		{
            ch.LastDialogueDisplayed++;
		}
        else if (!next && ch.LastDialogueDisplayed < 0)
		{
			ch.LastDialogueDisplayed = 0;
		}

		Dialogue dialogue = ch.Dialogue[ch.LastDialogueDisplayed];
		if (dialogue.setbool != null)
        {
			dayData.SetBool(dialogue.setbool);
        }
        if (dialogue.TypeIsChoice())
        {
            CreateChoice(ch.Name, dialogue.text, dialogue.choices);
        }
        else
        {
            CreateDialogue(ch.Name, dialogue);
        }
    }

    public void CreateDialogue(string name, Dialogue message)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().Dialog = message;
        dialog.GetComponent<Textbox>().DrawBox(name, message.text);
    }

    public void CreateChoice(string name, string message, Choice[] options)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);

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
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().DrawMessage(message);
		if (persistUntilClosed)
		{
			DontDestroyOnLoad(dialog);
		}
    }

    public Dialogue GetNextDialogue(int id, int dialogueID)
    {
        Interactable ch = AllObjects[id];
        return ch.Dialogue[dialogueID];
    }

    public void EnterDialogue()
    {
		if (GameMode != MODE.DIALOGUE)
		{
        	PrevMode = GameMode;
		}
        GameMode = MODE.DIALOGUE;
    }

    public void ExitDialogue()
    {
        MODE oldGameMode = GameMode;
        GameMode = PrevMode;
        PrevMode = oldGameMode;
    }

    #endregion

	public Interactable GetInteractableByID(int id)
	{
		return AllObjects[id];
	}

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

    public bool Midnight()
    {
        //Debug.Log("data is null: " + (Data == null));
        Debug.Log("gameClock : " + gameClock);
        if (gameClock == 24) //CHANGE THIS BACK TO 24
        {
            gameClock = 6;
            dayData.Wipe();
            //Data.daysPassed++;
            //CreateMessage("Oops, another day had passed. Try to clear all quests in one go. You're now on Day "+(Data.daysPassed+1), true);
            return true;
        }
   
        return false;
    }

    public void IncreaseTime()
    {
        Debug.Log("gameClock: " + gameClock);
        gameClock += 2;
    }

    public void SetTime(int time)
    {
        gameClock = time;
    }

    void ItemPickup(object sender, GameEventArgs args)
    {
        Debug.Log(args.IDNum);
        dayData.Inventory[dayData.ItemAmount] = AllItems[args.IDNum];
        dayData.ItemAmount += 1;
    }

    public bool GetData(string name)
    {
        //Debug.Log("Here!");
        //Debug.Log("data is null: " + Data == null);
        if (Data.DataDictionary.ContainsKey(name))
        {
            return Data.DataDictionary[name];
        }
        else
        {
            return false;
        }
    }

    public void SetData(string name, bool value)
    {
        if (Data.DataDictionary.ContainsKey(name))
        {
            //Data.DataDictionary[name] = value;
			Data.SetBool(name, value);
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
            GameObject.Destroy(GameManager.Instance.gameObject);
        }
        return result;
    }

    void Update()
    {
        upDateClock();
        // For debug purposes (obviously)
        //Debug.Log (GameMode);
    }

    #region Game Data Loading

    public void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.DK");
        Debug.Log("Game Data saved to file in " + Application.persistentDataPath);

        bf.Serialize(file, AllObjects);
        file.Close();
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.DK"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.DK", FileMode.Open);
            AllObjects = (Dictionary<int, Interactable>)bf.Deserialize(file);
            Debug.Log("Game Data loaded from file");
            file.Close();
        }
    }

    #endregion
}
