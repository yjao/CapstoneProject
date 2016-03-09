using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;

public class WorldMapManager : MonoBehaviour
{
	public GameObject school;
	public GameObject hospital;
	public GameObject police;
	public GameObject donutshop;
	public GameObject park;
	public GameObject mainstreet;
	public GameObject house;

	public GameObject kelly;

	public enum LocationString
	{
		NONE, DONUTSHOP, HOUSE, PARK, POLICE, HOSPITAL, SCHOOL, MAINSTREET, T_WRONG, T_DONUTSHOP
	};

	public Dictionary<LocationString, string> locationsMap = new Dictionary<LocationString, string>()
	{
		{ LocationString.NONE, null },
		{ LocationString.DONUTSHOP, SceneManager.SCENE_MALL },
		{ LocationString.HOUSE, SceneManager.SCENE_HOUSE },
		{ LocationString.PARK, SceneManager.SCENE_PARK },
		{ LocationString.POLICE, SceneManager.SCENE_POLICE },
		{ LocationString.HOSPITAL, SceneManager.SCENE_HOSPITAL },
		{ LocationString.SCHOOL, SceneManager.SCENE_CLASS },
		{ LocationString.MAINSTREET, SceneManager.SCENE_MAINSTREET },
		{ LocationString.T_WRONG, "T_Wrong" },
		{ LocationString.T_DONUTSHOP, "T_Mall" },
	};

	private string placeToEnter;

	private TutorialManager tutorialManager;
	private GameManager gameManager;

	void Start()
	{
		tutorialManager = TutorialManager.instance;
		gameManager = GameManager.instance;
	}

	void Update()
	{

	}

	public void LoadTutorialWorldMap()
	{
		school.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		hospital.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		police.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		park.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		mainstreet.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		house.GetComponent<MapLocation>().locationString = LocationString.T_WRONG;
		donutshop.GetComponent<MapLocation>().locationString = LocationString.T_DONUTSHOP;
		kelly.SetActive(true);
	}

	public void ReadyLocation(LocationString locationString)
	{
		placeToEnter = locationsMap[locationString];
	}
	public void ReadyLocation(string locationString)
	{
		placeToEnter = locationString;
	}

	public void EnterLocation()
	{
		if (placeToEnter == null)
		{
			return;
		}

		switch (placeToEnter)
		{
		case "T_Wrong":
			tutorialManager.Slide10ReminderMessage();
			break;
		case "T_Mall":
			tutorialManager.endCondition = true;
			break;
		case SceneManager.SCENE_CLASS:
			if (gameManager.GetTimeAsInt() != 8)
			{
				gameManager.CreateMessage("It's too late to go to school.", false);
			}
			else
			{
				StartCoroutine(gameManager.ClassFade());
			}
			break;
		default:
			SceneManager.instance.LoadScene(placeToEnter);
			break;
		}
	}

	public void LoadMapInfo(string prev_map)
	{
		Vector3 temp = new Vector3(-1.35f, -1.56f, 4);
		GameObject playerChar = GameObject.Find("Player");

		if (prev_map == SceneManager.SCENE_HOUSE)
		{
			temp = new Vector3(-4, -2, 4);
		}
		if (prev_map == SceneManager.SCENE_CLASS)
		{
			temp = new Vector3(-.5f, 2.7f, 4);
		} 
		if (prev_map == SceneManager.SCENE_PARK)
		{
			temp = new Vector3(4.56f, 2.56f, 4);
		}
		if (prev_map == SceneManager.SCENE_HOSPITAL)
		{
			temp = new Vector3(4.75f, -2.29f, 4);
		}
		if (prev_map == SceneManager.SCENE_MALL)
		{
			temp = new Vector3(2.05f, -0.33f, 4);
		}
		if (prev_map == SceneManager.SCENE_MAINSTREET)
		{
			temp = new Vector3(-1.35f, -1.56f, 4);
		}
		if (prev_map == SceneManager.SCENE_POLICE)
		{
			temp = new Vector3(1.74f, -3.15f, 4);
		}
		playerChar.transform.position = temp;
		ReadyLocation(prev_map);

		gameManager.MenuLayout.GetComponent<Menu_Layout>().Fast_Forward_Label(true);
	}

}
