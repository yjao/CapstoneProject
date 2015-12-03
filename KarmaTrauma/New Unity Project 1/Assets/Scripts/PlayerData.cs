using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
	public int daysPassed;

	public Dictionary<string, bool> DataDictionary = new Dictionary<string, bool>()
    {
		{"AlfredJumps_CutsceneWatched", false},
		{"GimmeJewel_QuestUnlocked", false},
        {"Have_$2", false },
        {"Have_Pizza" ,false},
        {"none", true },
		{"AlfredName_Learned", false},
		{"AlfredIsPolice_Learned", false}
    };

	public void SetBool(string boolName, bool value=true)
	{
		DataDictionary[boolName] = value;

		// hardcoded
		if (GetBool("AlfredName_Learned"))
		{
			QuestManager.Instance.RemoveQuestFromLog(1);

			// Hard-coded to remove bae from inventory because I don't know how :(
			for (int i = 0; i < GameManager.Instance.dayData.ItemAmount; i++)
			{
				if (GameManager.Instance.dayData.Inventory[i].Name == "Bacon and Eggs")
				{
					GameManager.Instance.dayData.Inventory[i] = null;
				}
			}
		}
	}

	public bool GetBool(string boolName)
	{
        Debug.Log("Returning Bool value : " + boolName + DataDictionary[boolName]);
		return DataDictionary[boolName];
        
	}
}
