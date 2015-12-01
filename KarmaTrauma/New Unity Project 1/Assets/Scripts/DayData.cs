using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayData
{
    public Item[] Inventory = new Item[9];
    public int ItemAmount = 0;

	public Dictionary<string, bool> DataDictionary = new Dictionary<string, bool>()
	{
		{"GimmeJewel", false},
		{"HolyEgg", false}
	};
	
	public void SetBool(string boolName, bool value=true)
	{
		DataDictionary[boolName] = value;
	}
	
	public bool GetBool(string boolName)
	{
		return DataDictionary[boolName];
	}
}
