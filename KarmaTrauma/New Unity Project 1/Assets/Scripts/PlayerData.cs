using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
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
    };

    public Dictionary<string, bool> QuestDictionary = new Dictionary<string, bool>()
    {
    };

    public Dictionary<string, bool> DialogueHistory = new Dictionary<string, bool>()
    {
    };

    public void SetBool(string boolName, bool value = true)
    {
        DataDictionary[boolName] = value;
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
        //Debug.Log("Returning Bool value : " + boolName + DataDictionary[boolName]);
		return DataDictionary[boolName];
        
	}
}
