using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_OldStuff : DataLoader
{
	public Data_OldStuff()
	{
		LoadNpcData();
		LoadQuestTerms();
		LoadQuestData();
		LoadSceneData();
	}
	
	private void LoadNpcData()
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
		
		gameManager.allObjects[1].dialogues[7].choices = new Choice[1] { new Choice("Maybe not today.") };
		
		string[] alfred = new string[]
		{
			"\"Help me...\"",
			"[You told Alfred his son loves him now]"
			
		};
		AddNpc(2, "???", "Alfred", alfred);
		
		string[] mom = new string[]
		{
			/*0*/ "\"Chelsey! Wake up! You don't want to be late on your first day of school!\"",
			/*1*/ "\"The breakfast's on the table. Bacon and eggs, your favorite!\"",
			/*2*/ "\"Come on, you're going to be late!\"",
			/*3*/ "\"Hi Chelsey, hope you had a great time at school today!\"",
			/*4*/ "\"It's late, go to bed!\""
		};
		
		AddNpc(21, "Mom", "Mom", mom);
		
		string[] mrly = new string[]
		{
			/*0*/ "\"Good morning class, we have a new student today. Chelsey, why don't you introduce yourself?\"",
			/*1*/ "\"Thank you Chelsey. Take a seat at the empty desk over there. Now, everybody, get out your textbooks and turn to page 42...\"",
			/*2*/ "\"That's all for today.  Remember to do the exercises on page 61.\"",
			/*3*/ "\"Go on, don't be shy and introduce yourself.\"",
			/*4*/  "\"Why did he do it?\"",
			/*5*/ "\"You don't even know him, anyway. It's too sad.\"",
			/*6*/ "\"Yup. Great cop, wasn't he? But that was all in the past. Ask his ex-coworkers, they all know! That poor, poor soul.\""
		};
		AddNpc(65, "Mr.Ly", "Teacher", mrly);
		gameManager.allObjects[65].dialogues[4].choices = new Choice[]
		{
			AddChoice("Say nothing"),
			AddChoice("Who is he?", ChoiceAction.CONTINUE, 65, 5)
		};
		//GameManager.Instance.AllObjects[65].Dialogue[5].CEA = new ChoiceEventArgs() { ChoiceAction = Textbox.continueDialogue, IDNum = 65, DialogueID = 4};
		/*GameManager.Instance.AllObjects[65].Dialogue[6].choices = new Choice[]
		{
			addChoice("Say nothing"),
			addChoice("What happened?", ChoiceAction.CONTINUE, 65, 7)
		};*/
		gameManager.allObjects[65].dialogues[6].choices = new Choice[]
		{
			AddChoice("Okay.", setboolname:"Quest2")
		};
		//GameManager.Instance.AllObjects[65].Dialogue[6].setbool = "Quest2";
		
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
			"\"He just suddenly jumped off the building!\"",
			"\"How am I supposed to know?\""
		};
		AddNpc(72, "Stylish Guy", "", stylish_guy);
		
		string[] jeney = new string[]
		{
			/*0*/ "[Jeney tells you she saw the crying person at the mall around 4PM]",
			/*1*/ "\"Welcome to Jeney's Donut Shop!\"",
			/*2*/ "\"   \""
		};
		AddNpc(73, "Random Woman", "Jeney", jeney);
		
		string[] alex = new string[]
		{
			/*0*/ "[A crying person tells you that's his father]",
			/*1*/ "[Dialogue. This person does not trust you]",
			/*2*/ "[He refuses to believe that Alfred is going to jump off a building today]",
			/*3*/ "[You showed him the jewel and now he trusts you]"
		};
		AddNpc(74, "???", "", alex);
		
		/*
		GameManager.Instance.AllObjects[73].Dialogue[0].choices = new Choice[]
		{
			addChoice("Say nothing"),
			addChoice("Who is he?", ChoiceAction.CONTINUE, 73, 1)
		};
		GameManager.Instance.AllObjects[73].Dialogue[1].setbool = "JeneyHungry";
        */
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
		gameManager.allObjects[71].dialogues[0].choices = new Choice[2]
		{
			new Choice("I guess I'll eat it"),
			new Choice("Nah...")
		};
		
		string[] jewel = new string[]
		{
			"hi",
			"continue",
			"more"
		};
		AddNpc(150, "Jewel", "Jewel", jewel);
		gameManager.allObjects[150].dialogues[0].choices = new Choice[]
		{
			AddChoice("Talk to the jewel", ChoiceAction.CONTINUE, 150, 1),
			AddChoice("Take the jewel", ChoiceAction.ITEM, 150, checkboolname: "Jewel"),
			AddChoice("Destroy the jewel", ChoiceAction.DESTROY),
			AddChoice("Do nothing")
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
		
		//string[] alfred = new string[]
		//{
		//    "A man who jumped off of the building."
		//};
		//AddNpc(31, "Alfred", "", alfred);
		//GameManager.Instance.AllObjects[31].Dialogue[0].choices = new Choice[2]
		//{
		//    new Choice("Should I talk to him?"),
		//    new Choice("Nah...")
		//};
		
		string[] jewel4 =
		{
			"Are you worthy enough to take the Debugger's Jewel?"
		};
		AddNpc(153, "Jewel", "", jewel4);
		gameManager.allObjects[153].dialogues[0].choices = new Choice[2]
		{
			new Choice("You bet!", new ChoiceEventArgs() { ChoiceAction = InteractableObject.InteractItem, IDNum = 150 }),
			new Choice("Nah, Idk Ruby.")
		};
	}
	
	private void LoadQuestTerms()
	{

	}
	
	private void LoadQuestData()
	{

	}
	
	private void LoadSceneData()
	{

	}
	
}
