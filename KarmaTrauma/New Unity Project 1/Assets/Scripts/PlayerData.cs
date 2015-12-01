using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
	// Cutscenes Watched, or we can just use "Esc to skip scene" feature
	/*public bool AlfredJumpsCW = false;
	bool MomBreakfastCW = false;
	bool ClassIntroductionsCW = false;
    bool JewelTest = false;
	public bool GimmeJewel = false;*/
	public Dictionary<string, bool> DataDictionary = new Dictionary<string, bool>()
    {
		{"AlfredJumps_CutsceneWatched", false},
		{"GimmeJewel_QuestUnlocked", false}
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
