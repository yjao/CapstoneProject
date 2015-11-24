using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
	private const bool PARSING_MODE = true;

    private int gameClock = 0;
    private string gameClockDisplay = "";
    PlayerData Data = new PlayerData();
    DayData dayData = new DayData();
    public static GameManager Instance;
    public GameObject DialogueContainer;
    public enum MODE
    {
        NONE, PLAYING, DIALOGUE, MENU, CUTSCENE, WAITING
    };
    public MODE GameMode = MODE.NONE;
    public MODE PrevMode = MODE.NONE;

    public enum AREA
    {
        NONE, HOUSE, APARTMENT, POLICE, MALL, PARK, HOSPITAL, SCHOOL
    };
    public AREA CurrentArea = AREA.HOUSE;

    public Dictionary<int, Interactable> AllObjects;

    public string dialogue_choice;

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
        AllObjects = new Dictionary<int, Interactable>()
		{
			{ 150, new Interactable("Jewel", "", new Dialogue[] {
                    new Dialogue(0,"Choice Testing.", new Choice[] {
                        new Choice("Talk to jewel"),
                        new Choice("Take the jewel", new ChoiceEventArgs(){ChoiceAction = InteractableObject.InteractItem,Testing = "action test", IDNum = 150}),
                        new Choice("Destroy the jewel"),
                        new Choice("Shove the jewel", new ChoiceEventArgs(){ChoiceAction = InteractableObject.InteractMove, ShoveX = 1}),
                        new Choice("Do nothing")
                        })
				    })}
        };
        /*
        AllObjects = new Dictionary<int, Interactable>()
		{
			{ 1, new Interactable("Chelsey","", new string[] {
					"\"Um... my name is Chelsey. I just moved in town recently. I like helping people, and.. yeah.\"",
                    "\"Sure!  Let's go~ furendddd.\"",
                    "\"I live on Maple Street.\"",
					"\"He looks familiar... What a strange night.\"",
                    "\"...First day of school?.\"",
                    "\"??\"",
                    "*Didn't this happen yesterday?...*",
                    "\"%CUm...%CMaybe not today.\"",
                    "*What is happening today??  Is everyone playing a trick on me?*",
                    "!!!The old man!!"
                    
                    
				}) },
			{ 150, new Interactable("Jewel", "", new string[] {
                    //"\"%B~JewelTest~Testing playerdata\"",
					//"\"%CDon't bother me, I'm taking a nap!%COK%CNo\"",
                    "\"%CChoice Testing.%C~D_JewelTest~Talk to jewel%C~Item~Take the jewel%C~Destroy~Destroy the jewel%C~Move~Shove the jewel%CDo nothing\"",
                    "\"%C!!!%CPeanut Butter%CHi%C???%C!!!\""
            }
				) },
            { 151, new Interactable("Jewel2", "", new string[] {
					"\"Listen to the other jewels.\""
            }
				) },
            { 152, new Interactable("Jewel2", "", new string[] {
					"\"That other jewel is weird huh?\""
            }
				) },
            { 153, new Interactable("Jewel2", "", new string[] {
					"\"How did you know I wanted a jewel?\""
            }
                ) },
			{ 21, new Interactable("Mom", "", new string[] {
					"\"Chelsey! Wake up! You don't want to be late on your first day of school!\"",
					"\"The breakfast's on the table. Bacon and eggs, your favorite!\"",
					"\"Come on, you're going to be late!\""
				}) },
			{ 65, new Interactable("Mr.Ly", "Teacher", new string[] {
					"\"Good morning class, we have a new student today. Chelsey, why don't you introduce yourself?\"",
					"\"Thank you Chelsey. Take a seat at the empty desk over there. Now, everybody, get out your textbooks and turn to page 42...\"",
                    "\"That's all for today.  Remember to do the exercises on page 61.\"",
                    "\"Go on, don't be shy and introduce yourself.\""
				}) },
			{ 66, new Interactable("Kelly", "",  new string[] {
					"\"Hey, new kid! Your name is Chelsey right? I'm Kelly.\"",
                    "\"Want me to show you around?\"",
                    "\"This is the mall.  I got my belly piercing here.\"",
					"\"Whoa, it's getting late. Where do you live?\"",
                    "\"Just head down from Main Street and you should be able to reach Maple Street. I'll see you tomorrow!\"",
                    "\"Ok then.  See ya!\""
				}) },
            { 71, new Interactable("Park Dude", "",  new string[] {
					"\"Hey kid, have you seen a brown dog around here somewhere?\"",
                    "\"I've lost him a little while ago, and I am worried sick about him.\"",
                    "\"Thank you!\""
				}) },
			{ 133, new Interactable("Bacon and Eggs", "", new string[] {
					"%C A delicious floating egg on a magical bacon.%C\"I guess I'll eat it.\"%C\"Nah...\" ",
				}) },
            { 31, new Interactable("Alfred", "???", new string[] {
                    "%C A man who jumped off of the building.%C\"Should I talk to him?\"%C\"Nah...\" ",
                }) }
        };
        */

        // Bind events
        //EventManager.OnDialogChoiceMade += HandleOnDialogChoiceMade;
        EventManager.OnItemPickup += ItemPickup;

        //item stuff
        AllItems = new Dictionary<int, Item>()
	    {
			{ 150, new Item("Jewel", "Quit stealing jewels already", "jewel")},
            { 151, new Item("TestJewel", "IM NOT EVEN AN ITEM", "jewel")},
            { 133, new Item("Bacon and Eggs", "Please stop stealing", "baconAndEggs")}
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
        Data.AlfredJumpsCW = true;

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
            Debug.Log(Data.AlfredJumpsCW);
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
            ch.LastDialogueDisplayed++;
        else if (!next && ch.LastDialogueDisplayed < 0)
            ch.LastDialogueDisplayed = 0;
        if (ch.Dialogue[ch.LastDialogueDisplayed].text.Contains("%B"))
        {
            string message = ch.Dialogue[ch.LastDialogueDisplayed].text.Substring(4);
            string dataBool = message.Substring(0, message.IndexOf('~'));
            message = message.Substring(message.IndexOf('~') + 1);
            Data.DataDictionary[dataBool] = true;
            CreateDialogue(ch.Name, "\"" + message);
        }
        else if (ch.Dialogue[ch.LastDialogueDisplayed].choices != null)
        {
            CreateChoice(ch.Name, ch.Dialogue[ch.LastDialogueDisplayed].text, ch.Dialogue[ch.LastDialogueDisplayed].choices);
        }
        else
        {
            CreateDialogue(ch.Name, ch.Dialogue[ch.LastDialogueDisplayed].text);
        }
    }

    public void CreateDialogue(string name, string message)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().DrawBox(name, message);
    }

    public void CreateChoice(string name, string message, Choice[] options)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);

        Debug.Log(options[0].option);
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

    public void CreateMessage(string message)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
        dialog.GetComponent<Textbox>().DrawMessage(message);
    }

    public void EnterDialogue()
    {
        PrevMode = GameMode;
        GameMode = MODE.DIALOGUE;
    }

    public void ExitDialogue()
    {
        GameMode = PrevMode;
    }

    #endregion

    void upDateClock()
    {
        if (gameClock == 24)
        {
            gameClock = 0;
        }
        if (gameClock <= 12)
        {
            gameClockDisplay = gameClock.ToString() + "AM";
        }
        else if (gameClock > 12)
        {
            int time = gameClock - 12;
            gameClockDisplay =  time.ToString() + "PM";
            gameClockDisplay = time.ToString() + "PM";
        }
    }

    public string GetTime()
    {
        return gameClockDisplay;
    }

    public void IncreaseTime()
    {
        gameClock += 2;
    }
    void ItemPickup(object sender, GameEventArgs args)
    {
        dayData.Inventory[dayData.ItemAmount] = AllItems[args.IDNum];
        dayData.ItemAmount += 1;
    }

    public bool GetData(string name)
    {
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
            Data.DataDictionary[name] = value;
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
