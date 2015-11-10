using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;


[System.Serializable]
public class PlayerData 
{
	// Cutscenes Watched, or we can just use "Esc to skip scene" feature
	public bool AlfredJumpsCW = false;
	bool MomBreakfastCW = false;
	bool ClassIntroductionsCW = false;
    
}
