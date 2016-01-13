using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData 
{
	public int daysPassed = 0;
    public static PlayerData Instance;

    void Awake()
    {
        Instance = this;
    }

	public Dictionary<string, bool> DataDictionary = new Dictionary<string, bool>()
    {
		{"AlfredJumps_CutsceneWatched", false},
		{"GimmeJewel_QuestUnlocked", false},
        {"Have_$2", false },
        {"Have_Pizza" ,false},
        {"none", true },
		{"AlfredName_Learned", false},
		{"AlfredIsPolice_Learned", false},
        {"Meet_Alfred_Son", false},
        {"AlfredSon_Trust", false}
    };

    public Dictionary<string, bool> QuestDictionary = new Dictionary<string, bool>()
    {
        {"Alex_1",false}
    };

    public void SetBool(string boolName, bool value = true)
    {
        DataDictionary[boolName] = value;

        // hardcoded
        if (GetBool("AlfredName_Learned"))
        {
  //          QuestManager.Instance.RemoveQuestFromLog(1);

            // Hard-coded to remove bae from inventory because I don't know how :(
            for (int i = 0; i < GameManager.instance.dayData.ItemAmount; i++)
            {
                if (GameManager.instance.dayData.Inventory[i].Name == "Bacon and Eggs")
                {
                    GameManager.instance.dayData.Inventory[i] = null;
                }
            }
        }
    }
    public void FinishQuest(string boolName)
    {
        QuestDictionary[boolName] = true;
    }
    public bool CheckQuest(string boolName)
    {
        if (QuestDictionary.ContainsKey(boolName))
        {
            return QuestDictionary[boolName];
        }
        else
        {
            return false;
        }
    }
    public void RegisterQuest(string questName)
    {
        if(QuestDictionary.ContainsKey(questName))
        {
            // If the questName is already in the dictionary, do nothing
        }
        else
        {
            QuestDictionary.Add(questName, false);
        }
    }

    public void RegisterData(string dataName)
    {
        if (DataDictionary.ContainsKey(dataName))
        {
            // If the questName is already in the dictionary, do nothing
        }
        else
        {
            DataDictionary.Add(dataName, false);
        }
    }

    public void WipeQuest()
    {
        QuestDictionary.Clear();
    }

	public bool GetBool(string boolName)
	{
        Debug.Log("Returning Bool value : " + boolName + DataDictionary[boolName]);
		return DataDictionary[boolName];
        
	}
}
