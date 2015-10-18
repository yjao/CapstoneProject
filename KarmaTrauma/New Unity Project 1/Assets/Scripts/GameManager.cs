using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject DialogueContainer;
	public enum MODE
	{
		NONE, PLAYING, DIALOGUE, MENU, CUTSCENE
	};
	public MODE GameMode = MODE.NONE;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void DBox(string name, string message)
	{
		GameMode = MODE.DIALOGUE;
		Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
	}
}
