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

    public string dialogue_choice;

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
			{ 1, new Character("Chelsey", new string[] {
					"\"Um... my name is Chelsey. I just moved in town recently. I like helping people, and.. yeah.\"",
					"\"He looks familiar... What a strange night.\""
				}) },
			{ 150, new Character("Jewel", new string[] {
					"\"%CDon't bother me, I'm taking a nap!%COK%CNo\"",
                    "\"%C!!!%CPeanut Butter%CHi%C???%C!!!\""
            }
				) },
			{ 21, new Character("Mom", new string[] {
					"\"Chelsey! Wake up! You don't want to be late on your first day of school!\"",
					"\"The breakfast's on the table. Bacon and eggs, your favorite!\"",
					"\"Come on, you're going to be late!\""
				}) },
			{ 65, new Character("Teacher", new string[] {
					"\"Good morning class, we have a new student today. Chelsey, why don't you introduce yourself?\"",
					"\"Thank you, Chelsey. Welcome, and we hope you enjoy our time together. Now, everybody, get out your textbooks and turn to page 42...\""
				}) },
			{ 66, new Character("Girl", new string[] {
					"\"Hey, your name is Chelsey right? You must be new to town, want me to show you around?\"",
					"\"Whoa, it's getting late. Just go west from the Main Street and you should find home. I'll see you tomorrow!\""
				}) }
		};
    }

	public void DBox(int id, int dialogueId)
	{
		Character ch = AllObjects[id];
		ch.LastDialogueDisplayed = dialogueId;
		DBox(id);
	}

	// Re-send current Dialogue
	public void DBox(int id, bool next=false)
	{
		Character ch = AllObjects[id];
		if (next)
			ch.LastDialogueDisplayed++;
		else if (!next && ch.LastDialogueDisplayed < 0)
			ch.LastDialogueDisplayed = 0;
        if (ch.Dialogue[ch.LastDialogueDisplayed].Contains("%C"))
        {
            StartCoroutine(CreateChoice(ch.Name, ch.Dialogue[ch.LastDialogueDisplayed]));
        }
        else
        {
            CreateDialogue(ch.Name, ch.Dialogue[ch.LastDialogueDisplayed]);
        }
	}

	public void CreateDialogue(string name, string message)
	{
		GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
		dialog.GetComponent<Textbox>().DrawBox(name, message);
	}

    public IEnumerator CreateChoice(string name, string message)
    {
        GameObject dialog = (GameObject)Instantiate(DialogueContainer, DialogueContainer.transform.position, Quaternion.identity);
        message = message.Substring(3);
        string text = message.Substring(0,message.IndexOf("%C"));
        message = message.Substring(message.IndexOf("%C")+2);
        string[] choices = message.Split(new string[] { "%C" }, System.StringSplitOptions.None);
        choices[choices.Length-1] = choices[choices.Length-1].Substring(0, choices[choices.Length-1].Length-1);
        yield return null;
        yield return StartCoroutine(dialog.GetComponent<Textbox>().Choice(name, "\""+text+"\"" , choices));
        Debug.Log(dialogue_choice);
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
