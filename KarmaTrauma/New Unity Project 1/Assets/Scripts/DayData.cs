using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayData
{
    public Item[] Inventory = new Item[9];
    public int ItemAmount = 0;

	public Dictionary<string, bool> QuestComplete = new Dictionary<string, bool>()
	{
		//{"Quest1", true},
		{"Quest2", false}
		//{"Quest3", false}
	};

	public void SetQuest(string questName, bool value=true)
	{
		QuestComplete[questName] = value;

		// this below section is heavily hard coded and should not be official
		/*if (GetQuest("Quest1"))
		{
			QuestManager.Instance.RemoveQuestFromLog(0);

			// Hard-coded to remove jewel from inventory because I don't know how :(
			for (int i = 0; i < ItemAmount; i++)
			{
				if (Inventory[i].Name == "Jewel")
				{
					Inventory[i] = null;
				}
			}
		}*/
	}

	public bool GetQuest(string questName)
	{
		return QuestComplete[questName];
	}

	public Dictionary<string, bool> DataDictionary = new Dictionary<string, bool>()
	{
		//{"GimmeJewel", false},
		{"JeneyHungry", false},
        {"TestJewelOutcome", false},
        {"TestPeanutButterOutcome", true},
	};
	
	public void SetBool(string boolName, bool value=true)
	{
		DataDictionary[boolName] = value;

		// hardcoded
		/*if (GetBool("GimmeJewel"))
		{
			QuestManager.Instance.AddQuestToLog(0);
		}
		else*/ if (GetBool("JeneyHungry"))
		{
//			QuestManager.Instance.AddQuestToLog(1);
		}
	}
	
	public bool GetBool(string boolName)
	{
		return DataDictionary[boolName];
	}

	public void Wipe()
	{
		Dictionary<string, bool> newQuests = new Dictionary<string, bool>();
		foreach (string s in QuestComplete.Keys)
		{
			newQuests[s] = false;
		}
		QuestComplete = newQuests;

		Dictionary<string, bool> newBools = new Dictionary<string, bool>();
		foreach (string s in DataDictionary.Keys)
		{
			newBools[s] = false;
		}
		DataDictionary = newBools;

		Inventory = new Item[9];

		//temp
//		QuestManager.Instance.quest_log = new ArrayList();
	}
}
