using UnityEngine;
using System.Collections;

public class DataLoader
{
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
			"\"He looks familiar... What a strange night.\"",
			"\"...First day of school?.\"",
			"\"??\"",
			"*Didn't this happen yesterday?...*",
			"\"Um...\"",
			"*What is happening today??  Is everyone playing a trick on me?*",
			"!!!The old man!!"
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
			"\"Go on, don't be shy and introduce yourself.\""
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
			"\"Welcome to Frost's Pizza.\"",
			"\"Today's special is Hwaiian Combo. It's only $2 a piece!\"",
            
        };
        AddNpc(67, "Frost", "", frost);

		string[] parkdude = new string[]
		{
			"\"Hey kid, have you seen a brown dog around here somewhere?\"",
			"\"I've lost him a little while ago, and I am worried sick about him.\"",
			"\"Thank you!\""
        };
        AddNpc(71, "Park Dude", "", parkdude);
        
		string[] baconandeggs = new string[]
		{
			"A delicious floating egg on a magical bacon."
		};
		AddNpc(71, "Bacon and Eggs", "Food???", baconandeggs);
		GameManager.Instance.AllObjects[71].Dialogue[0].choices = new Choice[2]
		{
			new Choice("I guess I'll eat it" /*Insert Message Box "You actually ate it?"*/),
			new Choice("Nah...")
		};
        
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
	}

	private void LoadTestData()
	{
		string[] test = new string[] 
		{
			"Oh um, hey, I didn't see you there. ...Go back to class!",
			"And how dare you ditch your first day?",
			"I can't believe this! Will you stop stealing? High schooler these days think they're all grown up..."
		};
		AddNpc(999, "Mr. Ly", "", test);
	}

	public DataLoader()
	{
		Debug.Log("Loading Game Data");
		
		LoadOldData();
		LoadTestData();
		
		Debug.Log("Loading Complete");
		GameManager.Instance.SaveGameData();
	}
}
