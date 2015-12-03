using UnityEngine;
using System.Collections;

public class DataLoader
{
    enum ChoiceAction{ITEM, MOVE, CONTINUE, DESTROY, NONE}

	private void AddNpc(int ID, string name, string strangerName, string[] strings)
	{
		Dialogue[] dialogues = new Dialogue[strings.GetLength(0)];
		for (int i = 0; i < strings.GetLength(0); i++)
		{
			Choice[] choices = null;
			dialogues[i] = new Dialogue(i, strings[i], choices);
		}
		
		GameManager.Instance.AllObjects[ID] = new Interactable(name, strangerName, dialogues);
	}
	
	private void LoadOldData()
	{
		string[] chelsey = new string[]
		{
			"\"Um... my name is Chelsey. I just moved in town recently. I like helping people, and.. yeah.\"",
			"\"Sure!  Let's go~ furendddd.\"",
			"\"I live on Maple Street.\"",
			"\"What just happened? That man... He looks familiar. What a strange night.\"",
			"\"...First day of school?.\"",
			"\"??\"",
			"*Didn't this happen yesterday?...*",
			"\"Um...\"",
			"*What is happening today??  Is everyone playing a trick on me?*",
			"!!!The old man!!",
            "\"...\""
		};
		AddNpc(1, "Chelsey", "", chelsey);
		GameManager.Instance.AllObjects[1].Dialogue[7].choices = new Choice[1] { new Choice("Maybe not today.") };

		string[] mom = new string[]
		{
			"\"Chelsey! Wake up! You don't want to be late on your first day of school!\"",
			"\"The breakfast's on the table. Bacon and eggs, your favorite!\"",
			"\"Come on, you're going to be late!\""
        };
		AddNpc(21, "Mom", "Mom", mom);

		string[] mrly = new string[]
		{
			"\"Good morning class, we have a new student today. Chelsey, why don't you introduce yourself?\"",
			"\"Thank you Chelsey. Take a seat at the empty desk over there. Now, everybody, get out your textbooks and turn to page 42...\"",
			"\"That's all for today.  Remember to do the exercises on page 61.\"",
			"\"Go on, don't be shy and introduce yourself.\"",
            "\"Why did he do it?\""
		};
		AddNpc(65, "Mr.Ly", "Teacher", mrly);

		string[] kelly = new string[]
		{
			"\"Hey, new kid! Your name is Chelsey right? I'm Kelly.\"",
			"\"Want me to show you around?\"",
			"\"This is the mall.  I got my belly piercing here.\"",
			"\"Whoa, it's getting late. Where do you live?\"",
			"\"Just head down from Main Street and you should be able to reach Maple Street. I'll see you tomorrow!\"",
            "\"Ok then.  See ya!\""
        };
        AddNpc(66, "Kelly", "", kelly);

        string[] frost = new string[]
		{
            "\"Welcome to Frost's Pizza.  Today's special is Hwaiian Combo. It's only $2 a piece!\"",
            "\"Here's your pizza.\""
        };
        AddNpc(67, "Frost", "", frost);

		string[] parkdude = new string[]
		{
			"\"Hey kid, have you seen a brown dog around here somewhere?\"",
			"\"I've lost him a little while ago, and I am worried sick about him.\"",
			"\"Thank you!\""
        };
        AddNpc(71, "Park Dude", "", parkdude);

        string[] stylish_guy = new string[]
		{
			"\"He just suddenly jumped off the building!\""
        };
        AddNpc(72, "Stylish Guy", "", stylish_guy);

        string[] jeney = new string[]
		{
			"\"This is what he deserves...\""
        };
        AddNpc(73, "Random Woman", "", jeney);
        
		string[] baconandeggs = new string[]
		{
			"A delicious floating egg on a magical bacon."
		};

        string[] monologue = new string[]
        {
            "\"Ooh, money on the floor!\"",
            "\"This is where I found $2\""
        };
        AddNpc(111, "Chelsey", "Chelsey", monologue);

        AddNpc(171, "Bacon and Eggs", "Food???", baconandeggs);
		GameManager.Instance.AllObjects[71].Dialogue[0].choices = new Choice[2]
		{
			new Choice("I guess I'll eat it" /*Insert Message Box "You actually ate it?"*/),
			new Choice("Nah...")
		};

        string[] jewel = new string[]
        {
            "hi",
            "continue",
            "more"
        };
        AddNpc(150, "Jewel", "Jewel", jewel);
		GameManager.Instance.AllObjects[150].Dialogue[0].choices = new Choice[]
		{
            addChoice("Talk to the jewel", ChoiceAction.CONTINUE, 150, 1),
            addChoice("Take the jewel", ChoiceAction.ITEM, 150),
            addChoice("Destroy the jewel", ChoiceAction.DESTROY),
            addChoice("Do nothing")
		};

        string[] jewel2 = new string[]
        {
            "Bool is false"
        };
        AddNpc(151, "Jewel", "Jewel", jewel2);

        string[] jewel3 = new string[]
        {
            "Bool is true"
        };
        AddNpc(152, "Jewel", "Jewel", jewel3);

		string[] alfred = new string[]
		{
			"A man who jumped off of the building."
		};
		AddNpc(31, "Alfred", "", alfred);
		GameManager.Instance.AllObjects[31].Dialogue[0].choices = new Choice[2]
		{
			new Choice("Should I talk to him?"),
			new Choice("Nah...")
        };

		string[] jewel4 =
		{
			"Are you worthy enough to take the Debugger's Jewel?"
		};
		AddNpc(153, "Jewel", "", jewel4);
		GameManager.Instance.AllObjects[153].Dialogue[0].choices = new Choice[2]
		{
			new Choice("You bet!", new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = 150 }),
			new Choice("Nah, Idk Ruby.")
		};
	}

	private void LoadTestData()
	{
		string[] test = new string[] 
		{
			"Oh um, hey, I didn't see you there. ...Go back to class!",
			"Shh kid, can you find me one of those debugger jewels?",
			"Did you bring the Jewel?",
			"O.M.G.!!! You actually brought it. Gimme!",
			"Well whatever, you don't get to clear the game."
		};
		AddNpc(999, "Mr. Test", "", test);
		GameManager.Instance.AllObjects[999].Dialogue[1].setbool = "GimmeJewel";
		GameManager.Instance.AllObjects[999].Dialogue[3].choices = new Choice[]
		{
			new Choice("Never!", new ChoiceEventArgs() { ChoiceAction =  Textbox.continueDialogue, IDNum = 999, DialogueID = 4}),
			//new Choice("I guess...")
            addChoice("I guess...", boolname:"Quest1")
		};
	}

	public DataLoader()
	{
		Debug.Log("Loading Game Data");
		
		LoadOldData();
		LoadTestData();
		
		Debug.Log("Loading Complete");
		GameManager.Instance.SaveGameData();
	}

    private ChoiceEventArgs item(int id)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = id };
        return CEA;
    }

    private ChoiceEventArgs continueDialog(int id, int dialogueID)
    {
        ChoiceEventArgs CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.continueDialogue, IDNum = id, DialogueID = dialogueID };
        return CEA;
    }

    private Choice addChoice(string text, ChoiceAction CA = ChoiceAction.NONE, int id = -1, int subID = -1, string boolname = null)
    {
        ChoiceEventArgs CEA;
        if (CA == ChoiceAction.ITEM)
        {
            CEA = item(id);
        }
        else if (CA == ChoiceAction.CONTINUE)
        {
            CEA = continueDialog(id, subID);
        }
        else if (CA == ChoiceAction.DESTROY)
        {
            CEA = new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractDestroy };
        }
        else
        {
            CEA = new ChoiceEventArgs();
        }
        return new Choice(text, CEA, boolname);
    }
}
