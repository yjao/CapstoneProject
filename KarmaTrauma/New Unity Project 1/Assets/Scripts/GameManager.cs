using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject DialogueContainer;
	public enum MODE
	{
		NONE, PLAYING, DIALOGUE, MENU, CUTSCENE
	};
	public MODE GameMode = MODE.NONE;
	public MODE PrevMode = MODE.NONE;
	
	public enum AREA
	{
		NONE, HOUSE, APARTMENT, POLICE, MALL, PARK, HOSPITAL, SCHOOL
	};
	public AREA CurrentArea = AREA.HOUSE;

	public Dictionary<int, Character> AllObjects;

    void Awake()
    {
		// default to playing mode for now
		GameMode = MODE.PLAYING;

		if ((Instance != null) && (Instance != this))
			Destroy(gameObject);
		else
			Instance = this;
		
		DontDestroyOnLoad(this);

		// Load interaction info here?
		AllObjects = new Dictionary<int, Character>()
		{
			{ 150, new Character("Jewel", new string[] {"\"Don't bother me, I'm taking a nap!\""}) }/*,
			{ 0, new Character() }*/
		};
    }

	// Send the ONLY dialog available for character
	public void DBox(int id)
	{
		Character ch = AllObjects[id];
		CreateDialogue(ch.Name, ch.Dialogue[0]);
	}

	public void CreateDialogue(string name, string message)
	{
		GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
		dialog.GetComponent<Textbox>().DrawBox(name, message);
	}

	public void EnterDialogue()
	{
		PrevMode = GameMode;
		GameMode = MODE.DIALOGUE;
	}

    public void ExitDialogue()
    {
		GameMode = PrevMode;
    }
}
