using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayData
{
    public Item[] Inventory = new Item[9];
    public int ItemAmount = 0;
    bool ChelseyHouseDoorUnlocked = false;
    public Dictionary<string, bool> QuestComplete = new Dictionary<string, bool>()
    {
		{"Quest1", false},
		{"Quest2", true},
        //{"Quest3", false}
    };
	//bool FrostWantsJewel = false;
}
