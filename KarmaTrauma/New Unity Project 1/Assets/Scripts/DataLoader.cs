using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLoader
{
	protected GameManager gameManager;

    public enum ChoiceAction{ITEM, MOVE, CONTINUE, DESTROY, COROUTINE, NONE}

	// temporary
	public static DataLoader Instance;
	public bool Init()
	{
		if (Instance == null)
		{
			Instance = this;
			return true;
		}
		return false;
	}

	public DataLoader()
	{
		gameManager = GameManager.instance;

		bool initFirstTime = Init();
		if (!initFirstTime)
		{
			return;
		}

		Debug.Log("Loading Game Data");

		// Put in your test Data_ scripts here. Comment out the ones you don't need.
		//var d1 = new Data_FallDemo();
        //var d2 = new Data_WinterPlayTest();
        var d3 = new Data_GameDay();
		Debug.Log("Loading Complete");
		gameManager.SaveGameData();
	}

	protected ChoiceEventArgs ChoiceInteractItem(int id, bool removeInteractable = true)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = id, DestroyItem = removeInteractable };
        return CEA;
    }

	protected ChoiceEventArgs ChoiceContinueDialog(int id, int dialogueID)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.continueDialogue, IDNum = id, DialogueID = dialogueID };
        return CEA;
    }

	// temporarily making it public for hardcoding purposes
    public Choice AddChoice(string text, ChoiceAction CA = ChoiceAction.NONE, int id = -1, int subID = -1, string setboolname = null, string checkboolname = null,
                            string checkitemname = null, string removeitemname = null, string coroutinename = null)
    {
        ChoiceEventArgs CEA;
        if (CA == ChoiceAction.ITEM)
        {
            CEA = ChoiceInteractItem(id);
        }
        else if (CA == ChoiceAction.CONTINUE)
        {
            CEA = ChoiceContinueDialog(id, subID);
        }
        else if (CA == ChoiceAction.DESTROY)
        {
            CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractDestroy };
        }
        else if (CA == ChoiceAction.COROUTINE)
        {
            CEA = new ChoiceEventArgs() { ChoiceAction = TutorialManager.CallCoroutineEvent, CoroutineName = coroutinename };
        }
        else
        {
            CEA = new ChoiceEventArgs();
        }
        return new Choice(text, CEA, setboolname, checkboolname, removeitemname, checki: checkitemname);
    }

	protected void AddChoicesToDialogue(int ID, int dID, Choice[] choices)
	{
		gameManager.allObjects[ID].dialogues[dID].choices = choices;
	}

	protected void AddToDialogue(int ID, int dID, ChoiceEventArgs cea)
    {
        //Debug.Log(ID + "; " + dID);
		gameManager.allObjects[ID].dialogues[dID].CEA = cea;
        gameManager.allObjects[ID].dialogues[dID].Action += cea.ChoiceAction;
	}

    protected void LinkContinueDialogues(int ID, int[] dIDs)
    {
        for (int i = 0; i < dIDs.Length-1; i++)
        {
                AddToDialogue(ID, dIDs[i], ChoiceContinueDialog(ID, dIDs[i + 1]));
        }
    }

	protected void AddNpc(int ID, string name, string strangerName, string[] strings)
	{
		Dialogue[] dialogues = new Dialogue[strings.GetLength(0)];
		for (int i = 0; i < strings.GetLength(0); i++)
		{
			Choice[] choices = null;
			dialogues[i] = new Dialogue(i, strings[i], choices);
		}

		gameManager.allObjects[ID] = new Interactable(name, strangerName, dialogues);
	}

    protected void AddBooleanToDialogue(int id, int dialogueID, string boolName)
    {
        gameManager.allObjects[id].dialogues[dialogueID].setbool = boolName;
    }

	protected void AddParameters(string sceneName, InteractableObject.Parameters parameters)
	{
		List<InteractableObject.Parameters> paramList = null;
		if (gameManager.sceneParameters.ContainsKey(sceneName))
		{
			paramList = gameManager.sceneParameters[sceneName];
		}

		if (paramList == null)
		{
			gameManager.sceneParameters[sceneName] = new List<InteractableObject.Parameters>();
		}

		gameManager.sceneParameters[sceneName].Add(parameters);
	}

    protected void AddOutcome(string booleanName, string trueText, string falseText)
    {
        gameManager.outcomeList.Add(new OutcomeManager.outcome(booleanName, trueText, falseText));
    }
}
