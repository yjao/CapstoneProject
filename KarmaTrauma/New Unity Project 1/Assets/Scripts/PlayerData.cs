using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

[Serializable]
public class PlayerData : MonoBehaviour
{
	// Cutscenes Watched, or we can just use "Esc to skip scene" feature
	public bool AlfredJumpsCW = false;
	bool MomBreakfastCW = false;
	bool ClassIntroductionsCW = false;
}
