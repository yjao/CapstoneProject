﻿using UnityEngine;
using System.Collections;

public class GameManager
{
    public static GameManager instance;
	public GameObject DialogueContainer;
	public enum MODE
	{
		NONE, PLAYING, DIALOGUE, MENU, CUTSCENE
	};
	public MODE GameMode = MODE.NONE;
	
	public enum AREA
	{
		NONE, HOUSE, APARTMENT, POLICE, MALL, PARK, HOSPITAL, SCHOOL
	};
	public AREA CurrentArea = AREA.HOUSE;

    private GameManager()
    {
    }

    public static GameManager GetInstance()
    {
        if (instance == null)
            instance = new GameManager();
        
        return instance;
    }

	public void DBox(string name, string message)
	{
		GameMode = MODE.DIALOGUE;
		//Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
	}

    public void ExitDialogue()
    {
        GameMode = MODE.PLAYING;
    }
}
