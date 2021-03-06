using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;


[Serializable]
public class BoolSceneParameter
{
	public string boolName;
	public Dictionary<string, List<InteractableObject.Parameters>> paramSet;

	public BoolSceneParameter(string name)
	{
		boolName = name;
		paramSet = new Dictionary<string, List<InteractableObject.Parameters>>();
	}

	public void AddToMap(string mapName, InteractableObject.Parameters item)
	{
		if (!paramSet.ContainsKey(mapName))
		{
			paramSet.Add(mapName, new List<InteractableObject.Parameters>());
		}
		paramSet[mapName].Add(item);
	}

	public InteractableObject.Parameters GetParamData(string mapName, int npcID, int time)
	{
		if (!paramSet.ContainsKey(mapName))
		{
			return null;
		}
		List<InteractableObject.Parameters> allNpcParams = paramSet[mapName];
		foreach (InteractableObject.Parameters npcParam in allNpcParams)
		{
			if (npcID == npcParam.NpcID && npcParam.timeBlocks.Contains(time))
			{
				return npcParam;
			}
		}
		return null;
	}
}

[Serializable]
public class GameManager : MonoBehaviour
{
	public GameObject MenuLayout;
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
	public List<BoolSceneParameter> boolSceneParameters;
    public Dictionary<string, string> endDayCoroutine;
    //Questlist in the form of List<[keyword , NPCName, dialogue, time, location]>
    
    //public List<string[]> questList;
    public Dictionary<string,List<string>> questList;
    public List<OutcomeManager.outcome> outcomeList;

	private int keyCooldown;
    public bool has_text_box;
   
	#region CONSTANT VALUES

	public const char QUEST_KEYWORD = '#';
	public const string QUEST_KEYWORD_COLOR = "blue";
	public const bool QUEST_KEYWORD_BOLDED = true;

	public const char TUTORIAL_KEYWORD = '%';
	public const string TUTORIAL_KEYWORD_COLOR = "green";
	public const bool TUTORIAL_KEYWORD_BOLDED = true;

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
    public Dictionary<string, int> itemIDs;

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
		boolSceneParameters = new List<BoolSceneParameter>();
        questList = new Dictionary<string, List<string>>();
        outcomeList = new List<OutcomeManager.outcome>();
        endDayCoroutine = new Dictionary<string, string>();

        // Bind events
        EventManager.OnItemPickup += ItemPickup;

        //item stuff
        allItems = new Dictionary<int, Item>()
	    {
            { 123, new Item("Lost Dog", "A lost dog. It seems to really like bacon.", "RaesDog")},
			{ 150, new Item("Jewel", "Quit stealing jewels already", "jewel")},
            { 110, new Item("Bacon", "Maybe you could feed this to someone", "baconAndEggs")},
            { 111, new Item("Box", "A mysterious box. Maybe it belongs to someone?", "Box")},
            { 23, new Item("Train Ticket", "A train ticket out of town. You don't need to go anywhere but maybe the ticket will have a use.", "Ticket")},
            { 152, new Item("Alfred's Jewel", "This was a gift from his son.", "alfredsjewel")},
            { 170, new Item("Cocodonut", "A donut covered in coconut shavings.", "Cocodonut")},
            { 171, new Item("Donut Sprinklez", "A rainbow donut. Not sure if this is edible.", "DonutSprinklez")},
            { 172, new Item("Chocoloate Crispies", "Donut holes dipped in chocolate. Yum.", "ChocolateCrispies")},
            { 173, new Item("Minty Munchies", "Donut holes with a mint flavor. How did they do that?", "MintyMunchies")},
            { 174, new Item("Strawberry Squishies", "Donut holes with a strawberry flavor. Looks delicious.", "StrawberrySquishies")},
            { 175, new Item("Potadonut Tots", "Are these donuts or potatoes?", "PotadonutTots")},
            { 176, new Item("Donut Holes Original", "Just your regular donut hole.", "DonutHolesOriginal")},
        };
        itemIDs = new Dictionary<string, int>()
        {
            {"Lost Dog", 123},
            {"Jewel", 150},
            {"Bacon", 110},
            {"Box", 111},
            {"Train Ticket", 23},
            {"Alfred's Jewel", 152},
            {"Cocodonut", 170},
            {"DonutSprinklez", 171},
            {"ChocolateCrispies", 172},
            {"MintyMunchies", 173},
            {"StrawberrySquishies", 174},
            {"PotadonutTots", 175},
            {"DonutHolesOriginal", 176}
        };

        dayData.Inventory = new List<Item>(new Item[9]);

        // Parse Game playerData
        if (PARSING_MODE)
        {
            DataLoader dataLoader = new DataLoader();
        }
        else
        {
            LoadGameData();
        }
		//LoadPlayerData();
    }

    void Start()
    {
		//Debug.Log ("START");
        gameClockDisplay = gameClock.ToString() + " - " +  (gameClock+2).ToString() + "PM";
    }
    void OnDestroy()
    {
		if (this == instance)
		{
	        EventManager.OnItemPickup -= ItemPickup;
			playerData.daysPassed++;
			SavePlayerData();
		}
    }

    public void Play()
    {
        gameMode = GameMode.PLAYING;
    }

    public void Wait()
    {
        gameMode = GameMode.WAITING;
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
			playerData.SetBool(dialogue.setbool);
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

    public void CreateDialogue(string name, Dialogue message, int id = -1)
    {
        //Debug.Log(message.text);
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

    public GameObject CreateTutorialBox(string message, float destroyTimer = -1, Textbox.TutorialBoxPosition position = Textbox.TutorialBoxPosition.MIDDLE)
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
        //SavePlayerData();
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
            SoundManager.instance.LoadSceneSound("WorldMapMidnight", .5f, true);
            return "10 - <b><color=red>12AM</color></b>";
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

	public bool Midnight(bool forceSetMidnight=false, float delay = -1, bool runEndDayCoroutine=true)
    {
        //Debug.Log("data is null: " + (playerData == null));
        //Debug.Log("gameClock : " + gameClock);
		if (GetTimeAsInt() == END_DAY_HOUR || forceSetMidnight)
        {
			StartCoroutine(MidnightFade(delay, runEndDayCoroutine));
            return true;
        }
        return false;
    }

    private IEnumerator MidnightFade(float delay = -1, bool runEndDayCoroutine=true)
    {
		if (runEndDayCoroutine)
		{
			yield return StartCoroutine(RunEndDayCoroutine());
		}

        yield return StartCoroutine(SceneManager.instance.fade_black());
        StartCoroutine(SceneManager.instance.map_name("Day " + (playerData.daysPassed + 1) + ".", 2.25f));
        StartCoroutine(SoundManager.instance.FadeOutAudioSource(SoundManager.instance.currentSong, true));
        dayData.Wipe();
        playerData.WipeQuest();
        playerData.daysPassed++;
		//SavePlayerData();
        transform.Find("Menu_layout/Inventory").GetComponent<Menu>().close();
        transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
        yield return StartCoroutine(GradualClock(START_DAY_HOUR, .25f, true));
        if (delay != -1)
        {
            yield return new WaitForSeconds(delay);
        }
        SoundManager.instance.StopAllBackgroundSounds();
        SceneManager.instance.LoadScene(SceneManager.SCENE_HOUSE);
    }
    public IEnumerator ClassFade()
    {
        yield return StartCoroutine(SceneManager.instance.fade_black());
        yield return StartCoroutine(GradualClock(12, 1f, false));
        SoundManager.instance.StopAllBackgroundSounds();
        SoundManager.instance.LoadSceneSound("SchoolBellRing", 0.5f);
        SceneManager.instance.LoadScene(SceneManager.SCENE_CLASS);
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
			//ApplyTint();
            gameClockDisplay = newDisplay;
        }
    }

    IEnumerator DelaySetClock(string display)
    {
        yield return new WaitForSeconds(1.5f);
        gameClockDisplay = display;
		//ApplyTint();
        yield break;       
    }

	private void ApplyTint()
	{
		SceneManager.instance.tint_screen(Application.loadedLevelName, GetTimeAsInt());
	}

    public IEnumerator GradualClock(int endTime, float timer, bool reverse = false)
    {
        while (gameClock != endTime)
        {
            yield return new WaitForSeconds(timer);
            if (reverse)
            {
                SetTime(gameClock -= 2);
            }
            else
            {
                SetTime(gameClock += 2);
            }
        }
    }

	#endregion


    IEnumerator RunEndDayCoroutine()
    {
		foreach (string key in endDayCoroutine.Keys)
        {
            if (dayData.GetBool(key))
            {
				yield return StartCoroutine(EndingManager.CallCoroutineEvent(endDayCoroutine[key]));
            }
        }
		yield break;
    }

    void ItemPickup(object sender, GameEventArgs args)
    {
        //dayData.Inventory[dayData.ItemAmount] = allItems[args.IDNum];
        dayData.Inventory[dayData.ItemAmount] = allItems[args.IDNum];
		if (allObjects.ContainsKey(args.IDNum))
		{
			dayData.DataDictionary[allObjects[args.IDNum].name] = true;
		}
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

    public List<Item> GetItemData()
    {
        return dayData.Inventory;
    }

    public int GetItemAmount()
    {
        return dayData.ItemAmount;
    }

    public bool HasItem(string name)
    {
        /*
        for (int item = 0; item < dayData.Inventory.Length; item++)
        {
            if (dayData.Inventory[item] != null)
            {
                if (dayData.Inventory[item].Name == name)
                {
                    return true;
                }
            }
        }*/
        return dayData.Inventory.Contains(allItems[itemIDs[name]]);
        //return false;
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
		//Debug.Log (gameMode);

		if (keyCooldown > 0)
		{
			keyCooldown -= 1;
		}
    }


	#region Key Cooldown
	public void SetKeyCooldown(int cooldown = 5)
	{
		keyCooldown = cooldown;
		Debug.Log("Cooldown SET");
	}

	public bool CheckKeyCooldown()
	{
		if (keyCooldown != 0) { Debug.Log("False"); }
		return (keyCooldown == 0);
	}
	#endregion

    #region Game data Loading

    public void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.DK");
        Debug.Log("Game data saved to file in " + Application.persistentDataPath);

        bf.Serialize(file, allObjects);
		bf.Serialize(file, sceneParameters);
		bf.Serialize(file, boolSceneParameters);
		bf.Serialize(file, questTerms);
		bf.Serialize(file, endDayCoroutine);
		bf.Serialize(file, outcomeList);
        file.Close();
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.DK"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.DK", FileMode.Open);
            allObjects = (Dictionary<int, Interactable>)bf.Deserialize(file);
			sceneParameters = (Dictionary<string, List<InteractableObject.Parameters>>)bf.Deserialize(file);
			boolSceneParameters = (List<BoolSceneParameter>)bf.Deserialize(file);
			questTerms = (Dictionary<string, string>)bf.Deserialize(file);
			endDayCoroutine = (Dictionary<string, string>)bf.Deserialize(file);
			outcomeList = (List<OutcomeManager.outcome>)bf.Deserialize(file);
            Debug.Log("Game data loaded from file");
            file.Close();
        }
    }

	public void SavePlayerData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveData.DK");
		Debug.Log("Gameplay data saved to file in " + Application.persistentDataPath);

		bf.Serialize(file, playerData);
		bf.Serialize(file, questList);
		file.Close();
	}
	
	public void LoadPlayerData()
	{
		if (File.Exists(Application.persistentDataPath + "/SaveData.DK"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveData.DK", FileMode.Open);
			playerData = (PlayerData)bf.Deserialize(file);
			questList = (Dictionary<string, List<string>>)bf.Deserialize(file);
			Debug.Log("Gameplay data loaded from file");
			file.Close();
		}
	}
	
	#endregion
	
	public static void UseBed(object sender, GameEventArgs args)
	{
		instance.Midnight(true, 3f);
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
