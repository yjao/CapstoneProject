using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_GameDay : DataLoader
{
    public Data_GameDay()
    {
		LoadCreditsData();

        LoadNpcData();
        LoadQuestTerms();
        LoadQuestData();
        LoadSceneData();
		LoadBoolSceneData();
        LoadOutcomeData();
        LoadEndDayCoroutineData();
    }

    // At the end of each day, if this bool is true, play this coroutine.
    private void LoadEndDayCoroutineData()
    {
        AddBoolCoroutinePair("AlfredSaved", "AlfredSavedEnding");
    }

	private void LoadCreditsData()
	{
		int id = -1;
		
		// ================ DAE ================ //
		id = 1001;
		string[] dae = new string[]
		{
			/*0*/ "\"Hi I'm Dae. I've done mostly game design for the team.\"",
			/*1*/ "\"I love my daughter.\""
		};
		AddNpc(id, "Dae Kim", "", dae);

		// ================ FAYE ================ //
		id = 1002;
		string[] faye = new string[]
		{
			/*0*/ "\"Hey guys, I'm Faye. I'm the producer and project manager of this game.\"",
			/*1*/ "\"I've also done a bit of everything else, including story, programming, and art.\"",
			/*2*/ "\"In the game, my role is a side character, a broke college student. Come visit me in-game!\"",
			/*3*/ "\"I'm not entirely sure if Potadonut is even a thing...\""
		};
		AddNpc(id, "Faye Jao", "", faye);
		//AddToDialogue(id, 0, ChoiceContinueDialog(id, 1));
		//AddToDialogue(id, 2, ChoiceContinueDialog(id, 3));

		// ================ PERRY ================ //
		id = 1003;
		string[] perry = new string[]
		{
			/*0*/ "\"Hi, I'm Perry. I programmed various systems for the game such as the dialog.\""
		};
		AddNpc(id, "Perry To", "", perry);

		// ================ RAE ================ //
		id = 1004;
		string[] rae = new string[]
		{
			/*0*/ "\"Hi, I'm Rae. I do the programming for all the UI in the game.\""
		};
		AddNpc(id, "Rae Tran", "", rae);

		// ================ JENEY ================ //
		id = 1005;
		string[] jeney = new string[]
		{
			/*0*/ "\"Hi, I'm Jeney. I'm a general programmer and one of the story designers.\"", //I also program most of the animations and deal with the sprites.\"",
            /*1*/ "\"In each scene, I make sure that the sprites are where they should be as well as appearing at the correct time.\"",
            /*2*/ "\"In the game, my character is a donut shop owner. :)\""
		};
		AddNpc(id, "Jeney Lao", "", jeney);
		//AddToDialogue(id, 0, ChoiceContinueDialog(id, 1));

		// ================ DOGE ================ //
		id = 1006;
		string[] doge = new string[]
		{
			/*0*/ "\"All cats have 4 legs. I have 4 legs. Therefore, I am a cat! Meow!!\"",
			/*1*/ "\"By the way, I don't talk in game.\""
		};
		AddNpc(id, "Doge", "", doge);
		AddToDialogue(id, 0, ChoiceContinueDialog(id, 1));
	}

    private void LoadNpcData()
    {
        int id = -1;
        
        // ================ ALFRED ================ //
        id = 2;
        string[] alfred = new string[]
        {
            /*0*/ "\"Good morning! The donuts here are tasty. You should try some. I recommend the Cocodonut.\"",
            /*1*/ "(Today is a big day. I need that #box# soon. That filthy man will get what he deserves...)",
            /*2*/ "\"I hope Jeney will still let me have one. I need my daily fix.\"",
            /*3*/ "\"Is there something wrong? No class today?\"",
            /*4*/ "(If only I didn't accept his dirty money...)",
            /*5*/ "\"Oh, I forgot my wallet today! I should go back to get it after I get out of work at 7 PM.\"",
            /*6*/ "(I guess it's time that I own up to my actions and put Faraday to jail.)",
            /*7*/ "\"Oh Megan...I'm so sorry...\"",
            /*8*/ "(If only I had reached the park in time...\"",
            /*9*/ "(#Jerry Faraday#...you will regret this.)",
            /*10*/"\"Oh, hello there. Got out early from classes today, huh?\"",
            /*11*/"\"Better get to class soon!\"",
            /*12*/"\"Oh! How did you know this was my favorite? Thank you, you're so sweet, just like my son.\"",
            /*13*/"\"I remember when he was little, I would #hide things underground# and we'd play scavenger hunt together. Good old times.\"",
            /*14*/"\"But we always had to watch out for #Hank, who gets mad if he sees you# do it!\"",
            /*15*/ "\"This box...Thanks, kid.\"",  
            /*16*/ "\"With this, I can finally make Faraday pay for all his wrongdoings.\"",  
            /*17*/ "...",
            /*18*/ "\"Sorry I'm not up for a chat. I'm in a hurry to meet up with my wife. Today is our anniversary~\"",
            /*19*/ "\"A few weeks ago, we got a new chief. He doesn't really talk much so we don't really know about him too well.\"",
            /*20*/ "\"Speaking of which, he left the office just now. He should be #back around 7 PM#.\"",
            /*21*/ "\"Ok, let me know if you need help with anything.\"",  
            /*22*/ "(Before this wonderful day ends, there's one more thing I must do...)",
            /*23*/ "\"...Oh! I didn't see you there. What are you doing here? It's late, go home!\"",  
            /*24*/ "(Before this wonderful day ends, there's one more thing I must do...)",
            /*25*/"\"...Oh! I didn't see you there. What are you doing here at this hour? You should head home, kid.\"",
        };
        AddNpc(id, "Officer Alfred", "Officer Alfred", alfred);

        //Check if Cocodonut is in the bag.  Can give Cocodonut to Alfred any time.
        gameManager.allObjects[2].dialogues[5].choices = new Choice[]
        {
            AddChoice("Offer Cocodonut.", ChoiceAction.CONTINUE, 2, 5, checkboolname: "AlfredCocodonut", checkitemname: "Cocodonut")
        };

		

        gameManager.allObjects[2].dialogues[10].choices = new Choice[]
        {
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 0, checkitemname: "Box"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, 2, 21, checkitemname: "Box"),
        };

        gameManager.allObjects[2].dialogues[20].choices = new Choice[]
        {
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 0, checkitemname: "Box"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, 2, 21, checkitemname: "Box"),
        };

        gameManager.allObjects[2].dialogues[18].choices = new Choice[]
        {
            AddChoice("Give donut.", ChoiceAction.CONTINUE, 2, 5, checkitemname: "Cocodonut"),
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 0, checkitemname: "Box"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, 2, 21, checkitemname: "Box"),
        };

        gameManager.allObjects[id].dialogues[4].choices = new Choice[]
        {
            AddChoice("Give donut.", ChoiceAction.CONTINUE, id, 5, checkitemname: "Cocodonut"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 10, checkitemname: "Cocodonut"),

        };

        AddToDialogue(2, 5, ChoiceContinueDialog(2, 12));
        AddToDialogue(2, 12, ChoiceContinueDialog(2, 13));
        LinkContinueDialogues(id, new int[2] { 4, 10 });
        gameManager.allObjects[id].dialogues[10].choices = new Choice[]
        {
            AddChoice("Ask about the station.", ChoiceAction.CONTINUE, id, 19),
        };
        AddToDialogue(id, 19, ChoiceContinueDialog(id, 20));
        AddToDialogue(2, 15, ChoiceContinueDialog(2, 16));

        AddToDialogue(2, 16, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "AlfredEnding" });


        LinkContinueDialogues(id, new int[3] { 0, 5, 2});
       
        LinkContinueDialogues(id, new int[2] { 1, 3 });
        LinkContinueDialogues(id, new int[2] { 7, 9 });

        // ================ MEGAN ================ //
        id = 3;
        string[] megan = new string[]
        {
            /*0*/ "\"Alfred...!\"",       
            /*1*/ "(If I could...if only I could go back in time...)",
            /*2*/ "(If only I knew that #someone was out to get him#.)",
            /*3*/ "*Sob* The doctors said that he won't be able to hold on past midnight...\"",
            /*4*/ "\"If I wasn't hospitalized, this wouldn't have happened.\"",
            /*5*/ "\"Oh Alfred, please hang in there!\" *Sob*",
            /*6*/"\"Good morning. Just taking my morning stroll in the park.\"",
            /*7*/"\"Sweetie, there's school today. Do you go to Punxsu High? My son also goes there.\"",
            /*8*/"\"Jeney, I'd like some Minty Munchies please!\"",
            /*9*/"\"Oh! And for the coupon, I'd like a #Cocodonut for Alfred#. He goes loco for them Cocodonuts *Teehee*.\"",
            /*10*/ "\"I was hospitalized 2 years ago due to a car accident.\"",  
            /*11*/ "\"Since then, our family has been in a lot of debt because of me...\"",  
            /*12*/ "\"My poor Alfred. I'm so sorry to put him through this.\"",  
            /*13*/ "\"But I've always wondered how he managed to collect so much money that time.\"",  
            /*14*/ "\"A time that I want to go back to?\"", 
            /*15*/ "\"I guess I would want to turn back time so that incident would never happen...\"", 
            /*16*/ "\"Heehee~ It's been such a long time since the two of us have gone out for dinner together. I can't wait!\"", 
            /*17*/ "\"Oh, I should go do my #checkups# first.\"", 
            /*18*/ "\"Hi there, I hope you enjoyed the discussion about time loops.\"", 
            /*19*/ "\"I'm glad to hear that. I was working as the mayor's secretary for many years, and this was a good change of atmosphere!\"", 
            /*20*/ "\"Two years ago, after Jerry Faraday won the election, I was asked to pack up and leave.\"",
            /*21*/ "\"I'm not sure what I did wrong back then. I was accused of spreading rumors about the new mayor when I didn't.\"", 
            /*22*/ "\"I hope the new assistant at the town hall knows what she's doing.\"", 
            /*23*/ "\"Yes, her name is Patricia. She used to be my roommate back in college.\"", 
            /*24*/ "\"I think she hates me now after what I've said to her back then.\"", 
            /*25*/ "\"Well...*sigh* Patricia never studied and went out partying all the time.\"", 
            /*26*/ "\"I once yelled at her to get it all together and she got really upset.\"",
            /*27*/ "\"Eventually, she dropped out. I met her again after she started working as the town hall's janitor.\"", 
            /*28*/ "\"She's...I don't think she's qualified for the job.\"", 
            /*29*/ "\"Honestly, I think it has something to do with why I was fired.\"", 
            /*30*/ "(That was such a wonderful night.)", 
            /*31*/ "\"Sweetie, why are you still out here at a late hour? Would you like my husband to escort you home? He's a police officer.\"", 

        };
        AddNpc(id, "Megan", "Megan", megan);
        AddToDialogue(id, 6, ChoiceContinueDialog(id, 7));
        gameManager.allObjects[id].dialogues[18].choices = new Choice[]
        {
            AddChoice("Yes I do!", ChoiceAction.CONTINUE, id, 19),
        };
        gameManager.allObjects[id].dialogues[19].choices = new Choice[]
        {
            AddChoice("Used to?", ChoiceAction.CONTINUE, id, 20),
        };
        LinkContinueDialogues(id, new int[2] { 20, 21 });

        gameManager.allObjects[id].dialogues[22].choices = new Choice[]
        {
            AddChoice("Do you know her?", ChoiceAction.CONTINUE, id, 23),
            AddChoice("What do you think of her?", ChoiceAction.CONTINUE, id, 28),

        };
        LinkContinueDialogues(id, new int[2] { 23, 24 });
        LinkContinueDialogues(id, new int[2] { 28, 29 });

        gameManager.allObjects[id].dialogues[24].choices = new Choice[]
        {
            AddChoice("What did you say?", ChoiceAction.CONTINUE, id, 25),
        };
        AddToDialogue(id, 25, ChoiceContinueDialog(id, 26));

        LinkContinueDialogues(id, new int[4] { 10, 11, 12, 13 });
        LinkContinueDialogues(id, new int[3] { 3, 4, 5 });
        LinkContinueDialogues(id, new int[2] { 14, 15 });
        LinkContinueDialogues(id, new int[2] { 16, 17 });
        LinkContinueDialogues(id, new int[3] { 0, 1, 2 });

        // ================ Alex ================ //
        id = 4;
        string[] alex = new string[]
		{
			/*0*/ "\"I told dad I'll be in class soon, but he doesn't know that I'm skipping class again. Shh, you won't tell anyone, right? I'm doing this for Yoona.\"",
			/*1*/ "\"...You're kidding me, right???\"",
			/*2*/ "\"You don't know Yoona? She's in our class!...Well, SUPPOSED to be in our class...\"", 
			/*3*/ "\"She has been hospitalized since the beginning of the year, so she was never in class.\"",
			/*4*/ "\"Yoona got a heart disease some time last year...\"",
			/*5*/ "\"I promised to take her to the zoo once she gets better.\"",
		    /*6*/ "\"...Whenever that may be, anyways. But I believe in her.\"",
            /*7*/ "\"Yoona's not looking so great today. I really hope she gets better.\"",
            /*8*/ "\"Yoona didn't want to eat lunch. Maybe she'll enjoy a donut?\"",
            /*9*/ "\"Hey, can you watch over her for me while I run to Jeney's?\"",
            /*10*/ "\"...Whatever. I'll be right back anyways.\"", 
            /*11*/ "\"Thanks, Chels!\"", 
            /*12*/ "(I should hurry. She must be waiting for me.)", 
            /*13*/ "\"Yoona. She's waiting for me at the hospital.\"", 
            /*14*/ "\"Hey Chelsey, got out of class? How was it?\"", 
            /*15*/ "\"Don't feel like talking? Hey I mean, that's fine.\"", 
            /*16*/ "\"Aw crap, she's my mom...!\"", 
            /*17*/ "\"Man... I hope she doesn't remember I was supposed to be in that class.\"", 
            /*18*/ "\"I better head home soon or my parents will yell at me.\"", 
            /*19*/ "*Sob* Dad, don't leave me.", 
        };
        AddNpc(id, "Alex", "Kid", alex);

        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
            AddChoice("I'm telling your dad", ChoiceAction.CONTINUE, id, 1),
            AddChoice("Who's Yoona?", ChoiceAction.CONTINUE, id, 2)

        };
        AddToDialogue(id, 2, ChoiceContinueDialog(id, 3));

        gameManager.allObjects[id].dialogues[3].choices = new Choice[]
		{
            AddChoice("What happened to her?", ChoiceAction.CONTINUE, id, 4),

        };
        AddToDialogue(id, 4, ChoiceContinueDialog(id, 5));
        AddToDialogue(id, 5, ChoiceContinueDialog(id, 6));

        LinkContinueDialogues(id, new int[2] { 8, 9 });
        gameManager.allObjects[id].dialogues[9].choices = new Choice[]
		{
            AddChoice("Sure.", ChoiceAction.CONTINUE, id, 11),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 10)
        };

        gameManager.allObjects[id].dialogues[12].choices = new Choice[]
		{
            AddChoice("Who?", ChoiceAction.CONTINUE, id, 13),

        };

        gameManager.allObjects[id].dialogues[14].choices = new Choice[]
		{
            AddChoice("Mrs. Freewoman was our sub today.", ChoiceAction.CONTINUE, id, 16),

        };
        AddToDialogue(id, 16, ChoiceContinueDialog(id, 17));
        AddToDialogue(id, 5, ChoiceContinueDialog(id, 6));
        

        // ================ MOM ================ //
        id = 5;
        string[] mom = new string[]
        {
            /*0*/ "\"Don't be late for class! I made breakfast for you. Today's menu is #bacon and eggs#.\"",       
            /*1*/"\"How was school today?\"",
            /*2*/ "(What should I make for dinner?)",
            /*3*/"\"Anything interesting happen today?\"",
            /*4*/"\"I made dinner for you.\"",
            /*5*/"\"Did you do your homework yet?\"",
            /*6*/"\"You should do your homework and go to bed.\"",
            /*7*/"\"It's late! Where have you been?\"",
            /*8*/"\"Go to bed now!\"",
            
        };
        AddNpc(id, "Mom", "Mom", mom);
        LinkContinueDialogues(id, new int[2] { 3, 4 });
        LinkContinueDialogues(id, new int[2] { 5, 6 });
        LinkContinueDialogues(id, new int[2] { 7, 8 });



        // ================ JENEY ================ //
        id = 7;
        string[] jeney = new string[]
		{
			/*0*/ "\"Welcome to the Donut Hole! What can I get you today? Today's special is the Donut Sprinklez~\"",
			/*1*/ "(Come here doggie! I'll give you a tasty Donut Bone.)",
            /*2*/ "\"Oh no...my friend's #dog was here at 5 PM#.\"",
            /*3*/ "\"I should have offered him #bacon# instead.\"",
            /*4*/ "\"Poor man...he was a really nice cop who came by my store often.\"",
            /*5*/ "\"He always told me that our #Cocodonut# tastes the best.\"",
            /*6*/ "(Whew...what a long day. So many things happened.) I should go home right now. The #murderer# is probably #still in town#.",
            /*7*/ "\"It's a shame. Punxsutown used to be so much better before it all happened.\"",
            /*8*/ "\"2 years ago, Jerry Faraday was elected mayor of town. He seemed great at first...\"",
            /*9*/ "\"But over the years, he had kicked out all these vendors. It's why you barely see anyone anymore.\"",
            /*10*/"\"Some were really nice family businesses that were there for a long time...How could he do such a thing?\"",
            /*11*/"\"Well, I hope the Donut Hole will be fine. We have so many loyal customers.\"",
            /*12*/"\"I tried catching him by feeding him a donut, but he ran off.\"",
            /*13*/"\"I hope that he returned home safely.\"",
            /*14*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*15*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*16*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*17*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*18*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*19*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
            /*20*/"\"Here you go, I've put it inside your bag. Have a nice day!\"",
			/*21*/"\"I'm so glad Rae found her dog! If he came over, I wouldn't have known how to catch him.\"",
			/*22*/"\"Maybe I'll offer him a Donut Bone the next time I see him.\"",
            /*23*/ "\"Welcome to the Donut Hole! What can I get you today? Today's special is the Donut Sprinklez~\"",
            /*24*/ "\"Our store had a lot of customers today. I'm exhausted.\"",
        };
        AddNpc(7, "Jeney", "Jeney", jeney);

        gameManager.allObjects[id].dialogues[2].choices = new Choice[]
		{
            AddChoice("What happened?", ChoiceAction.CONTINUE, id, 12)
        };
       AddToDialogue(id, 12, ChoiceContinueDialog(id, 3));
       AddToDialogue(id, 3, ChoiceContinueDialog(id, 13));

       gameManager.allObjects[id].dialogues[0].choices = new Choice[]
        {
            AddChoice("Cocodonut", ChoiceAction.CONTINUE, id, 14, checkboolname: "Moonlight"),
            AddChoice("Donut Sprinklez", ChoiceAction.CONTINUE, id, 15, checkboolname: "Moonlight"),
            AddChoice("Chocolate Crispies", ChoiceAction.CONTINUE, id, 16, checkboolname: "Moonlight"),
            AddChoice("Minty Munchies", ChoiceAction.CONTINUE, id, 17, checkboolname: "Moonlight"),
            AddChoice("Strawberry Squishies", ChoiceAction.CONTINUE, id, 18, checkboolname: "Moonlight"),
            AddChoice("Potadonut Tots", ChoiceAction.CONTINUE, id, 19, checkboolname: "Moonlight"),
            AddChoice("Donut Holes Original", ChoiceAction.CONTINUE, id, 20, checkboolname: "Moonlight")
        };
       AddDayDataToDialogue(id, 14, "DonutPicked");
       AddDayDataToDialogue(id, 15, "DonutPicked");
       AddDayDataToDialogue(id, 16, "DonutPicked");
       AddDayDataToDialogue(id, 17, "DonutPicked");
       AddDayDataToDialogue(id, 18, "DonutPicked");
       AddDayDataToDialogue(id, 19, "DonutPicked");
       AddDayDataToDialogue(id, 20, "DonutPicked");

       AddToDialogue(id, 14, ChoiceInteractItem(170, false));
       AddToDialogue(id, 15, ChoiceInteractItem(171, false));
       AddToDialogue(id, 16, ChoiceInteractItem(172, false));
       AddToDialogue(id, 17, ChoiceInteractItem(173, false));
       AddToDialogue(id, 18, ChoiceInteractItem(174, false));
       AddToDialogue(id, 19, ChoiceInteractItem(175, false));
       AddToDialogue(id, 20, ChoiceInteractItem(176, false));


        gameManager.allObjects[7].dialogues[7].choices = new Choice[]
		{
            AddChoice("What happened?", ChoiceAction.CONTINUE, 7, 8)
        };
       LinkContinueDialogues(id, new int[4]{8,9,10,11});
       LinkContinueDialogues(id, new int[2] { 4, 5 });

        AddBooleanToDialogue(id, 3, "LikesBacon");
        AddBooleanToDialogue(id, 14, "Cocodonut");
        

        // ================ HANK ================ //
        id = 11;
        string[] hank = new string[]
		{
			/*0*/ "(Ugh, I'm so sleepy. I hate this job. If only I can take a vacation... Why does Faraday get a vacation? He's our mayor! He should be working.)",
			/*1*/ "\"That ugly dog belongs to this girl Rae. She was crying at the #park until 11:30 PM# while looking for the dog.\"",
			/*2*/ "(So annoying ugh...)", 
			/*3*/ "\"Yesterday, that stupid #dog started digging# all over the park.\"",
			/*4*/ "\"I'm glad my nap today went ok. Unlike yesterday...\"",
			/*5*/ "(Ugh...these birds are so noisy. I don't get paid enough for this.)",
		    /*6*/ "\"Now go away and stop bothering me. Time to go fill up the rest of those stupid holes.\"",
            /*7*/ "\"Ugh...Go away, kid. Don't you see that I'm about to go to bed?\"",
            /*8*/ "(Maybe some stupid visitor fed him bacon or something. Dogs always get excited over #stupid bacon#.)",
            /*9*/ "\"#Rae was here at 5 PM# and went to the police station just now. About time she moved on and stopped pestering me.\"",
			/*10*/ "\"I'm so glad she found her dog. That Rae was starting to get really annoying with her wailings.\"",
            
        
        };
        AddNpc(id, "Hank", "Hank", hank);
        AddBooleanToDialogue(id, 1, "LostDog");
        AddBooleanToDialogue(id, 3, "DogCanDig");
        AddBooleanToDialogue(id, 8, "LikesBacon");

        gameManager.allObjects[id].dialogues[4].choices = new Choice[]
		{
            AddChoice("What's going on?", ChoiceAction.CONTINUE, id, 3)
        };
        LinkContinueDialogues(id, new int[3] { 3, 8, 6 });
        LinkContinueDialogues(id, new int[3] { 1, 9, 2 });

        // ================ BOB ================ //
        id = 13;
        string[] bob = new string[]
		{
			/*0*/ "\"...\"",
			/*1*/ "(#If I had left Punxsutown#, this wouldn't have happened...)",
			/*2*/ "(What am I supposed to do after #what I've done#?)",
			/*3*/ "(*Sigh* It's sure going to be a sleepless night...)",
			/*4*/ "(It's almost time. I feel anxious...)",
			/*5*/ "(I #want to leave this place#...)"  ,
			/*6*/ "(When a man's got no food or shelter, he might make #extreme decisions#...)"  ,
			/*7*/ "\"Hi there, got some money to spare?\"" , 
			/*8*/ "\"I need money to buy #a train ticket#. I could really use one...\"",
            /*9*/ "(*Mumble in sleep* Should I really #do it for Faraday#...?)"  ,
			/*10*/ "\"I woke up to some girl crying. I think she's #looking for her dog#.\""  ,
			/*11*/ "\"Man, I wish I had some bacon today. Yesterday, I #tossed the dog some bacon# bits.\""  ,
			/*12*/ "\"At first, he followed me, but I told him to go #dig some holes# to mess with cranky ol' Hank. Ha!\"",
			/*13*/ "\"Oh my goodness, you do have one! Can I really have this?\"",
			/*14*/"\"Thank you so much. I can have a much better life leaving Punxsutown.\"",
		    /*15*/ "\"I don't need to become some murderer, and I can start a new life elsewhere.\"",    
            /*16*/ "\"I'll remember you forever, Chelsey.\"",
            /*17*/ "\"You see, I'm a man without food or shelter.\"",
            /*18*/ "\"Somebody offered me money if I help him with something dirty... I don't know what to think of it.\"",
            /*19*/ "\"If I can get out of here, I can start a new life!\"",
            /*20*/ "\"Thanks, but I can't leave now. It's too late for me to start a new life when I've ruined another's life.\"",
            /*21*/ "(Should I do this?)",
            /*22*/ "\"I don't want to talk right now. Please leave me alone.\"",
            /*23*/ "\"Man, I wish I had some bacon today. Yesterday, I #tossed the dog some bacon# bits.\""  ,
			/*24*/ "\"At first, he followed me, but I told him to go #dig some holes# to mess with cranky ol' Hank. Ha!\"",
            /*25*/ "\"But it seems like he isn't here right now. I'm glad she found him!\"",
            /*26*/ "\"Thanks, but I don't need it anymore.\"",
            
        };
        AddNpc(id, "Bob", "Hobo master race", bob);

		AddBooleanToDialogue(id, 1, "BobWantsToLeave");
		AddBooleanToDialogue(id, 5, "BobWantsToLeave");
		AddBooleanToDialogue(id, 8, "BobWantsToLeave");

        AddBooleanToDialogue(id, 11, "LikesBacon");
        AddBooleanToDialogue(id, 12, "DogCanDig");

        gameManager.allObjects[id].dialogues[8].choices = new Choice[]
		{
            AddChoice("Why do you need one?", ChoiceAction.CONTINUE, id, 17)
        };
        AddToDialogue(id, 17, ChoiceContinueDialog(id, 18));
        AddToDialogue(id, 18, ChoiceContinueDialog(id, 19));

        //Same problem as Alfred.  Can give ticket to hobo any time.
        gameManager.allObjects[13].dialogues[6].choices = new Choice[]
        {
			AddChoice("Offer train ticket.", ChoiceAction.CONTINUE, 13, 13, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket", removeitemname: "Train Ticket"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 21, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket")
        };
       

        gameManager.allObjects[13].dialogues[3].choices = new Choice[]
        {
			AddChoice("Offer train ticket.", ChoiceAction.CONTINUE, 13, 20, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 22, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket")
        };


        gameManager.allObjects[13].dialogues[19].choices = new Choice[]
        {
			AddChoice("Offer train ticket.", ChoiceAction.CONTINUE, 13, 13, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket", removeitemname: "Train Ticket"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 21, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket")
        };

        gameManager.allObjects[13].dialogues[12].choices = new Choice[]
        {
			AddChoice("Offer train ticket.", ChoiceAction.CONTINUE, 13, 13, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket", removeitemname: "Train Ticket"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 21, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket")
        };

        AddToDialogue(13, 13, ChoiceContinueDialog(13, 14));
        AddToDialogue(13, 14, ChoiceContinueDialog(13, 15));
        AddToDialogue(13, 15, ChoiceContinueDialog(13, 16));
		AddDayDataToDialogue(id, 15, "AlfredSaved");
        
        AddToDialogue(13, 16, new ChoiceEventArgs(){ChoiceAction = InteractableObject.RemoveNpcThatDay, IDNum = 13});

        LinkContinueDialogues(id, new int[3]{7,8,10});
        AddToDialogue(id, 11, ChoiceContinueDialog(id, 12));
        LinkContinueDialogues(id, new int[3] { 4, 5, 6 });
        LinkContinueDialogues(id, new int[4] { 0, 1, 2, 3 });


        // ================ RAE ================ //
        id = 23;
        string[] rae = new string[]
		{
			/*0*/ "*Sob*\"I #lost my dog# today...\"",
			/*1*/ "\"He went outside to take a leak and hasn't come back since.\"",
			/*2*/ "\"I tried looking for him everywhere, but he's nowhere to be found.\"",
			/*3*/ "\"What should I do...\"*Sob*",
			/*4*/ "\"Jeney is my friend from middle school. She now owns this successful donut shop chain.\"",
			/*5*/ "\"All the cops in this town love this store and come by often. You should try some.\"",
			/*6*/ "\"#Have you seen my dog?# I've been looking everywhere for him.\""  ,
			/*7*/ "\"I was hoping that he would be at the park since it's his #favorite spot#.\""  ,
			/*8*/ "\"It's been 12 hours since I lost my dog. I don't know what to do.\"" , 
			/*9*/ "\"I hope someone #finds him and returns him to me#.\""  ,
			/*10*/ "(I guess this #train ticket# is useless now since I can't leave town without him.)"  ,
			/*11*/ "(Poor guy...I hope he rests in peace.)"  ,
			/*12*/ "\"I came to ask her to keep an eye out for #my lost dog#. Have you seen him?\"",
			/*13*/ "\"Tomo! Thank you for finding my dog.\"",
			/*14*/"\"I guess this train ticket is useless now since I still haven't packed up because I was out looking for him. Here, you can have it.\"",
            /*15*/ "\"Please let me know if you find him.\""  ,
		};
        AddNpc(id, "Rae", "Rae", rae);
        AddBooleanToDialogue(id, 0, "LostDog");
        AddBooleanToDialogue(id, 6, "LostDog");
        AddBooleanToDialogue(id, 8, "LostDog");

        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
            AddChoice("What happened?", ChoiceAction.CONTINUE, id, 1)
        };
        AddToDialogue(id, 1, ChoiceContinueDialog(id, 2));
        AddToDialogue(id, 2, ChoiceContinueDialog(id, 3));
        AddToDialogue(id, 6, ChoiceContinueDialog(id, 7));

        LinkContinueDialogues(id, new int[2] { 4, 12 });
        LinkContinueDialogues(id, new int[3] { 8, 9, 10 });

        Choice[] turn_in_dog = new Choice[]
		{
            AddChoice("Is this your dog?", ChoiceAction.CONTINUE, id, 13, checkitemname:"Lost Dog", removeitemname:"Lost Dog"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 15, checkitemname: "Lost Dog"), 
        };
        LinkContinueDialogues(id, new int[2] { 13, 14 });
		AddDayDataToDialogue(id, 13, "DogReturned");
        AddToDialogue(id, 14, ChoiceInteractItem(id));
        gameManager.allObjects[id].dialogues[3].choices = turn_in_dog;
        gameManager.allObjects[id].dialogues[12].choices = turn_in_dog;
        gameManager.allObjects[id].dialogues[7].choices = turn_in_dog;
        gameManager.allObjects[id].dialogues[10].choices = turn_in_dog;
        gameManager.allObjects[id].dialogues[11].choices = turn_in_dog;

        // ================ FAYE ================ //
        id = 27;
        string[] faye = new string[]
        {
            /*0*/ "\"I hear sirens out there. I wonder #what's up on Main Street#.\"",       
            /*1*/ "(Should I go check? Nah, curiosity kills the cat. But I'm not a cat...)",
            /*2*/ "(Wait, if all the cops are over there, does that mean I get to take a break?)",
            /*3*/ "\"I heard that someone fell off a building at 9 PM on Main Street.\"",
            /*4*/ "\"You should #check out the hospital# for me to see what's going on.\"",
            /*5*/ "\"Did you hear that ticking? Maybe it's time for you to go home.\"",
            /*6*/"\"Oh, cute dog. I wonder if #somebody lost it#?\"",
            /*7*/"(I remember when I lost my dog... it really sucked. But I still had 5 left...)",
            /*8*/"(Should I help? Maybe the owner is nearby?)",
            /*9*/"\"Hi, I just woke up. Wow, I'm sleepy...\"",
            /*10*/ "\"I run night shifts at Jeney's Donut Hole, starting around 9 PM or whenever Jeney leaves I guess.\"",
            /*11*/ "\"Jeney's cool. But you know what's cooler? Their Potadonut Tots!\"",
            /*12*/ "\"It's the only reason I work there. Just kidding. I'm a broke college student so I'm just doing what I can.\"",
            /*13*/ "\"Hey, it's Wednesday, are you ditching class?\"",
            /*14*/ "\"I'm not the one to talk as I overslept my morning class...\"",
            /*15*/ "\"But wait, you're not in college...?\"",
            /*16*/ "(Is it just me or do I keep seeing that dog everywhere?)",
            /*17*/ "(I saw a #dog on Main Street around 11 AM#.)",
            /*18*/ "(Then, I saw another #dog at the Park around 3 PM#.)",
            /*19*/ "(Maybe I'm just seeing things...)",
            /*20*/ "\"Also, I stayed up all night last night programming KT, a game project.\"",
            /*21*/ "\"I'm just not a morning person.\"",
            /*22*/ "\"Maybe I should take a stroll in the park.\"",
            /*23*/"\"I'm not an outdoor person. I go to the park so I can see dogs! Unfortunately, there isn't one right now.\"",
            /*24*/"\"Just getting ready for work. Man, it's going to be another long night. But it's okay, I have my Potadonut Tots...\"",
            /*25*/"\"Jeney will never know if a few are missing, right?\"",
            /*26*/"\"Do you believe in Karma?\"",
            /*27*/"\"Heck yeah, I do too. Well, here’s a story...\"",
            /*28*/"\"Once upon a time, young Faye ate too many donut samples without supervision; I think it’s why she’s now running endless night shifts at the Donut Hole.\"",
            /*29*/"\"You should be careful with your actions, it might impact you in unexpected ways.\"",
            /*30*/"\"But the donut samples were so good...heh heh...\"",
            /*31*/"\"Well, I do. It’s not just religion. It’s a belief.\"",
            /*32*/"\"Be careful though. You’ll never know if it strikes back one day. Experience speaking here.\"",
            /*33*/"\"Hey there! Are you here to visit me, or are you just a night owl like I am?\"",
            /*34*/"\"But sorry, I'm not feeling very talkative right now. Maybe if you came by 2 hours ago...I was just thinking about karma!\"",
            /*35*/"\"The most important question will always be, is Potadonut Tots a bunch of donut holes, or tater tots?\"",
            /*36*/"\"That's the kind of question that keeps me up at night...or at work, since I work at night. Ha ha...\"",
            /*37*/"\"Did you hear that ticking? Maybe it's time for you to go home.\"",

        };
        AddNpc(id, "Faye", "Faye", faye);

        LinkContinueDialogues(id, new int[2] { 9, 22 });
        gameManager.allObjects[id].dialogues[9].choices = new Choice[]
		{
            AddChoice("What's wrong?", ChoiceAction.CONTINUE, id, 21)
        };
        AddToDialogue(id, 21, ChoiceContinueDialog(id, 20));
        AddToDialogue(id, 20, ChoiceContinueDialog(id, 22));
        LinkContinueDialogues(id, new int[3]{6,7,8});
        LinkContinueDialogues(id, new int[3] { 10, 11, 12 });
        LinkContinueDialogues(id, new int[3] { 0, 1, 2 });
        LinkContinueDialogues(id, new int[3] { 3, 4, 5 });
        LinkContinueDialogues(id, new int[3] { 13, 14, 15 });
        LinkContinueDialogues(id, new int[4] { 16, 17, 18, 19 });

        // ================ DAE ================ //
        id = 36;
        string[] dae = new string[]
        {
            /*0*/ "\"Good morning. The Chocolate Crispies here are so good.\"",       
            /*1*/ "\"Alfred's been mumbling all day about #a box and the mayor#.\"",
            /*2*/ "\"I thought the #charges against Faraday# were dropped?\"",
            /*3*/ "(Oh well, young people won't understand anyways.)",
            /*4*/ "\"Poor Alfred... It must be hard for Alex and Megan.\"",
            /*5*/ "\"Alfred would never leave behind his family like this. Someone must have murdered him.\"",
            /*6*/ "\"Did you see anyone suspicious around here?\"",
            /*7*/"\"Stay back. The #murderer might still be around# here.\"",
            /*8*/"\"I was going to go home, but after seeing what happened to Alfred, I must stay.\"",
            /*9*/"\"Punxsu needs me!\"",
            /*10*/"\"It's getting really late. You need to go home right now.\"",
            /*11*/"\"Thanks, kid. I have been looking for this.\"",
            /*12*/"(With this, Faraday will give me enough money for Yoona's treatment.)",
            /*13*/"(I wonder how she's been doing. I hope she's not mad at me for not visiting her lately.)",
            /*14*/"(I'll give her a surprise visit tomorrow!)",
            /*15*/"\"That guy Perry is suspicious. He's been going around town asking people about Faraday.\"",
            /*16*/"\"Even though he's my superior, I still don't trust that guy.\"",
            /*17*/"!!!!!",
            /*18*/"\"Kid, what are you doing at this hour? You shouldn't be here. Go home now!\"",
            /*19*/"\"My daughter also loves the donuts from this store. You should try some.\"",
            /*20*/"\"Shh...go away kid, I'm busy.\"",
            /*21*/"\"(I should follow him and see what that's all about...\"",
            /*22*/"\"(Hm...Perry is gone again. Whew, it's tiring with him around.)\"",
            /*23*/"\"Ok, come to me if you need help.\"",
            /*24*/"\"Are you here to see Yoona? She's asleep right now, isn't she the sweetest thing?\"",
            /*25*/"\"I'll do anything...ANYTHING for my girl to get better...even if it means losing my job, or my life.\"",
            /*26*/"\"Thanks, kid. I have been looking for this.\"",         //ending sequence
            /*27*/"(With this, Faraday will give me enough money for Yoona's treatment.)", //Ending
            /*28*/"\"(Hm...Perry is gone again. Whew, it's tiring with him around.)\"",
            /*29*/"\"Even though he's my superior, I still don't trust that guy.\"",
            /*30*/"\"Shh...go away kid, I'm busy.\"",

            
        };
        AddNpc(id, "Officer Dae", "Officer Dae", dae);


        //Give box to Dae any time.
        gameManager.allObjects[id].dialogues[22].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 1, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 23)
        };


        AddToDialogue(id, 9, ChoiceContinueDialog(id, 21));
        AddToDialogue(id, 21, ChoiceContinueDialog(id, 20));
        AddToDialogue(id, 20, ChoiceContinueDialog(id, 22));
   
        LinkContinueDialogues(id, new int[2] { 17, 18 });
        LinkContinueDialogues(id, new int[2] { 0, 19 });
        LinkContinueDialogues(id, new int[2] { 13, 14 });
        LinkContinueDialogues(id, new int[4] { 1, 2, 3, 21 });
        LinkContinueDialogues(id, new int[2] { 15, 16 });
        gameManager.allObjects[id].dialogues[16].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 1, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 23, checkitemname: "Box")
        };
        gameManager.allObjects[id].dialogues[20].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 1, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, id, 23, checkitemname: "Box")
        };

        LinkContinueDialogues(id, new int[4] { 4, 5, 6, 7 });


        LinkContinueDialogues(id, new int[2] { 11, 12 });
        AddToDialogue(id, 12, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "AlfredFellEnding" });

        // ================ YOONA ================ //
        id = 37;
        string[] yoona = new string[]
        {
            /*0*/ "(Daddy... I miss you.)",       
            /*1*/ "\"Good morning. Are you here to visit me too? How sweet!\"",
            /*2*/ "\"Oh... I got a heart disease about a year ago.\"",
            /*3*/ "\"Alex's also here to visit. He's my childhood friend. *Heehee*\"",
            /*4*/ "\"My dad used to visit me often, but he stopped coming lately. I guess he'd been busy with work.\"",
            /*5*/ "\"Oh! My dad is a cop.  He's like a superhero for the town!\"",
            /*6*/ "\"Aren't you supposed to be in class?...I miss going to school and hanging out with my friends.\"",
            /*7*/"\"Alex told me that his dad has been working late and #mumbling about a box# lately. I hope he's doing ok at work.\"",
            /*8*/"\"There's nothing to do at the hospital. I'm so bored, but at least Alex comes here often to hang out with me.\"",
            /*9*/"*zzz*",
            /*10*/"\"Isn't that Alex's dad? I hope he's okay...\"",
            
        };
        AddNpc(id, "Yoona", "Yoona", yoona);

        gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
            AddChoice("What happened to you?", ChoiceAction.CONTINUE, id, 2),
        };
        AddToDialogue(id, 2, ChoiceContinueDialog(id, 3));
        AddToDialogue(id, 3, ChoiceContinueDialog(id, 4));

        gameManager.allObjects[id].dialogues[4].choices = new Choice[]
		{
            AddChoice("Your dad?", ChoiceAction.CONTINUE, id, 5),
        };
   


        // ================ KYLE ================ //
        id = 50;
        string[] kyle = new string[]
        {
            /*0*/ "\"Have you seen #Alex#? I haven't seen him these past few days.\"",       
            /*1*/ "\"Maybe I should go to the police station and tell his dad that he's been ditching class.\"",
            /*2*/ "\"Hey, what's up?\"",
            /*3*/ "\"Ugh...we lost the game.\"",
            /*4*/ "\"Oh it's Alex's dad! Maybe I should go talk to him about Alex.\"",
            /*5*/ "\"Oh my god! Someone get help!\"",
            /*6*/ "\"Ah, what a great day!\""
            
        };
        AddNpc(id, "Kyle", "Kyle", kyle);


        // ================ ANNA ================ //
        id = 51;
        string[] anna = new string[]
        {
            /*0*/ "\"Did you hear about Yoona? She has a heart disease so that's why she hasn't been coming to school lately. Maybe I should go visit her.\"",       
            /*1*/ "\"Shh Kelly's mom is here. Don't tell Kelly this, but I don't like her mom.\"",
            /*2*/ "\"Her mom works for Faraday. My dad's business is going downhill because of him. They're shady I'm telling you. I hope he doesn't get elected as Senator. \"",
            /*3*/ "\"Oh no, I missed the basketball game...\"",
          
        };
        AddNpc(id, "Anna", "Anna", anna);

        gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
            AddChoice("Why?", ChoiceAction.CONTINUE, id, 2),
        };

        // ================ SUSAN ================ //
        id = 52;
        string[] susan = new string[]
        {
            /*0*/ "\"On Wednesday, we wear blue.\"",       
            /*1*/ "\"My parents love Faraday and they hope he gets elected as Senator. I can't wait for new malls!\"",
            /*2*/ "\"I don't know where she is. Her mom is rarely home so she gets to go out and do whatever she wants.\"",
            /*3*/ "\"Oh no, I'm late for my massage appointment. Oh well, I'll visit #Yoona# another time.\"",
          
        };
        AddNpc(id, "Susan", "Susan", susan);

        gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
            AddChoice("Where's Kelly?", ChoiceAction.CONTINUE, id, 2),
        };

        // ================ APRIL ================ //
        id = 53;
        string[] april = new string[]
        {
            /*0*/ "\"Good morning! Are you here to exercise too? It's good for your health.\"",       
            /*1*/ "\"She used to be Megan's roommate in college. I don't really know what happened, but I heard they fought.\"",
            /*2*/ "\"There's this #hobo in the park# and he smells. I finally reported him. Ugh, I hope he goes away and stops ruining my mood in the morning.\"",
            /*3*/ "\"Oh it's Dae. It must be hard for him to raise a daughter all by himself. He's such a good father. It seems like he will do anything for her.\"",
            /*4*/ "\"I'm voting for Faraday in the upcoming election. He told us that he'll ban all hobos from the park.\"",       

        };
        AddNpc(id, "April", "April", april);

        gameManager.allObjects[id].dialogues[4].choices = new Choice[]
		{
            AddChoice("Who's Patricia?", ChoiceAction.CONTINUE, id, 1),
        };
        AddToDialogue(id, 1, ChoiceContinueDialog(id, 2));

        // ================ PATRICIA ================ //
        id = 66;
        string[] patricia = new string[]
        {
            /*0*/ "\"Did you see the look on her face? Priceless!\"",       
            /*1*/ "(So this #hobo# didn't do so terribly after all.)",
            /*2*/ "(Well, time to report back to Jerry. Heh heh...)",
            /*3*/ "\"What's up with kids and ditching school nowadays?\"",
            /*4*/ "\"Wait, that was me. You remind me of my younger self. Ha!\"",
            /*5*/ "\"Only some overachiever like Megan would get perfect attendance since preschool.\"",
            /*6*/ "\"She makes me sick. Living with her in college was the worst year of my life.\"",
            /*7*/ "(The show should start soon...)",
            /*8*/ "(I should head over to #Main Street at 7 PM# and wait there.)",
            /*9*/ "(I hope this #Bob# knows what he's doing.)",
            /*10*/"(Yuck, why is there a dog in here?)",
            /*11*/"\"The donut here is good, no doubt, but their coffee sucks. I'm picky with my coffee.\"",
            /*12*/"(I'll just tell Jerry to get rid of this place, like the rest of them...)",
            /*13*/"\"Oh, you're still here? Go away.\"",
            /*14*/"\"Me? I'm #Faraday's secretary#. He's on a vacation right now so he lets me take breaks whenever I want.\"",
            /*15*/"\"Actually, I still get to take breaks whenever I want, but it'd be great if he took me with him.\"",
            /*16*/ "(The show is about to start...)",
            /*17*/ "(Is that Kelly?)",
            /*18*/ "Oh hey, you look like my daughter's age.",
            /*19*/ "I love my girl, but I never have time to take care of her.",
            /*20*/ "I try everything I can to give her a good life, but I don't think she understands. But as long as she's happy, I'm happy.",
            /*21*/ "(The show should start soon...)",
            /*22*/ "(I should head over to #Main Street at 7 PM# and wait there.)",
            /*23*/ "(Where is this Bob? Shouldn't he be here right now?)",
            /*24*/ "(Wait, what? Where is Bob? Why isn't Alfred dead yet? Ugh, I knew I couldn't trust some random hobo!)",
            /*25*/ "(Well, there's still time before he gets the box...I've got to tell Jerry this!)",
        };
        AddNpc(id, "Patricia", "Patricia", patricia);

        LinkContinueDialogues(id, new int[3] { 3, 4, 5 });

        gameManager.allObjects[id].dialogues[4].choices = new Choice[]
		{
            AddChoice("Who are you?", ChoiceAction.CONTINUE, id, 14)
        };
        AddToDialogue(id, 14, ChoiceContinueDialog(id, 15));


        gameManager.allObjects[id].dialogues[5].choices = new Choice[]
		{
            AddChoice("You know Megan?", ChoiceAction.CONTINUE, id, 13)
        };
        AddToDialogue(id, 13, ChoiceContinueDialog(id, 6));
        LinkContinueDialogues(id, new int[3] { 7, 8, 10 });
        LinkContinueDialogues(id, new int[2] { 16, 9 });
        LinkContinueDialogues(id, new int[3] { 0, 1, 2 });
        LinkContinueDialogues(id, new int[4] { 17, 18, 19, 20 });
        

        // ================ KELLY ================ //
        id = 67;
        string[] kelly = new string[]
        {
            /*0*/ "\"Phew, classes are finally over.\"",       
            /*1*/ "\"Hey, why don’t we go hang out? I’m craving Jeney’s donuts.\"",
            /*2*/ "\"What is it? Okay, I'll wait a bit, but not for long! Or I can meet you there, too.\"",
            /*3*/ "\"Yesterday I overheard my mom and Faraday talking about #a box#. It looked serious because a cop was there.\"",
            /*4*/ "\"Hey, you don't think that #cop guy works with Faraday#, do you?\"",
            /*5*/ "\"Shh, my mom's sitting over there...\"",
            /*6*/ "\"Ugh, that slimeball Faraday's pictures are everywhere. I don't get what my mom sees in him. I hope he doesn't get elected.\"",
            /*7*/ "\"I want my mom to realize that what she's doing is wrong, but I don't want her to be taken away from me. After all, she's still my mom and she's the only one I have.\"",
            /*8*/ "\"Excuse me, I'd like some Strawberry Squishies.\"",
            /*9*/ "\"Wait, wasn't there a coupon code Freewoman gave us in class?\"",
            /*10*/ "\"What was it...do you remember Chels?\"",
            /*11*/ "\"I knew you would remember!\"",
            /*12*/ "\"Which one do you want? I'll buy as promised~\"",
            /*13*/ "\"Hm...I can't seem to remember either.\"",
            /*14*/ "\"These Strawberry Squishies are sooooo good.\"",
            
        };
        AddNpc(id, "Kelly", "Kelly", kelly);
        LinkContinueDialogues(id, new int[2] { 0, 1 });
        gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
            AddChoice("Hold on.", ChoiceAction.CONTINUE, id, 2)
        };

        LinkContinueDialogues(id, new int[3] { 3, 4, 5 });

        LinkContinueDialogues(id, new int[3] { 8, 9, 10 });
        gameManager.allObjects[id].dialogues[10].choices = new Choice[]
		{
            AddChoice("Moonlight.", ChoiceAction.CONTINUE, id, 11),
            AddChoice("I don't remember.", ChoiceAction.CONTINUE, id, 13),
        };


        LinkContinueDialogues(id, new int[2] { 11, 12 });
        AddBooleanToDialogue(id, 11, "Moonlight");
        AddToDialogue(id, 11, ChoiceContinueDialog(7, 0));
        AddDayDataToDialogue(id, 11, "DonutPicked");

        // ================ PERRY ================ //
        id = 99;
        string[] perry = new string[]
        {
            /*0*/ "\"...\"",       
            /*1*/ "\"....?\"",
            /*2*/ "\"Thanks, I'll take a look at it.\"",
            /*3*/ "\"...Ok\"", 
            /*4*/ "\"...\"", /*ENDINGVERSION*/
            /*5*/ "\"....?\"", /*ENDINGVERSION*/
            /*6*/ "\"Thanks, I'll take a look at it.\"", /*ENDINGVERSION*/
            /*7*/ "(What is Faraday's secretary doing here...?)",
            /*8*/ "\"Go home, kid.\"",
            /*9*/ "\"...\"", 
            /*10*/ "\"....?\"", /*ENDINGVERSION*/
            /*11*/ "\"Thanks, I'll take a look at it.\"", /*ENDINGVERSION*/

        };
        AddNpc(id, "Chief Perry", "Chief Perry", perry);
        //Give box to Perry add choice here.
        //Can't give box to Perry after 8 (when Alfred falls)//Day will restart.
        gameManager.allObjects[99].dialogues[0].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 2, checkitemname: "Box"),
            AddChoice("Do nothing.", ChoiceAction.CONTINUE, id, 3, checkitemname: "Box") 
        };
        gameManager.allObjects[99].dialogues[4].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 3, checkitemname: "Box"),
            AddChoice("Do nothing.", ChoiceAction.CONTINUE, id, 3, checkitemname: "Box") 
        };
        AddToDialogue(99, 5, ChoiceContinueDialog(99, 6));
        AddToDialogue(99, 1, ChoiceContinueDialog(99, 2));
        AddToDialogue(99, 6, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "AlfredFellEnding" });
        AddToDialogue(99, 2, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "AlfredFellEnding" });

        // ================ DOGE ================ //
        id = 123;
        string[] dog = new string[]
		{
            /*0*/ "\"Woof\"",
            /*1*/ "(You give the dog some bacon.)",
            /*2*/ "(The dog appears to be following you.)"
        };
        AddNpc(id, "Dog", "Dog", dog);
        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
            AddChoice("Feed the dog bacon.", ChoiceAction.CONTINUE, id, 1, checkboolname: "LikesBacon", checkitemname: "Bacon", removeitemname: "Bacon")
        };
        AddToDialogue(id, 1, ChoiceContinueDialog(id, 2));
        AddToDialogue(id, 2, ChoiceInteractItem(id));


        // ================ ITEMS & OBJECTS ================ //
        id = 110;
        string[] bacon = new string[]
        {
            /*0*/ "Mom left some bacon here. It doesn't look very appetizing."
        };
        AddNpc(id, "Bacon", "Bacon", bacon);
        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
        {
            AddChoice("Leave it alone."),
            AddChoice("Take the bacon.", ChoiceAction.ITEM, id)
        };

        id = 100;
        string[] bed = new string[]
		{
			/*0*/ "\"Sleep and end the day?\""
		};
        AddNpc(id, "Bed", "Bed", bed);
        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
			new Choice("Good Night!", new ChoiceEventArgs() { ChoiceAction = GameManager.UseBed }),
			AddChoice("I ain't weak!")
		};

        id = 111;
        string[] box = new string[]
        {
            /*0*/ "(There is a suspicious mound of dirt.)",
            /*1*/ "(The dog digs up a mysterious box. You decide to take it with you.)",
            /*2*/ "(There is a suspicious mound of dirt.)",
            /*3*/ "(You notice the park ranger glaring at you so you decide to stop for now.)"
        };
        AddNpc(id, "Dirt", "Dirt", box);
        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
        {
            AddChoice("Direct the dog to the dirt.", ChoiceAction.CONTINUE, id, subID:1, checkboolname:"DogCanDig", checkitemname:"Lost Dog")
        };
        AddToDialogue(id, 1, ChoiceInteractItem(id));
        gameManager.allObjects[id].dialogues[2].choices = new Choice[]
        {
            AddChoice("Direct the dog to the dirt.", ChoiceAction.CONTINUE, id, subID:3, checkboolname:"DogCanDig", checkitemname:"Lost Dog")
        };

		id = 1;
		string[] Chelsey = new string[]
		{
			/*0*/ "This box seems really important. Can I really trust him with it?",
			/*1*/ "This box seems really important. Can I really trust him with it?",
			/*2*/ "This box seems really important. Can I really trust him with it?",
			/*3*/ "This box seems really important. Can I really trust him with it?",
			/*4*/ "This box seems really important. Can I really trust him with it?",
			/*5*/ "This box seems really important. Can I really trust him with it?"
		};
		AddNpc(id, "Chelsey", "Chelsey", Chelsey);
		gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
			AddChoice("Keep box", ChoiceAction.CONTINUE, 2, 21),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 2, 15, checkitemname: "Box", removeitemname: "Box"),
		};
		gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
			AddChoice("Keep box"),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 36, 11, checkitemname: "Box", removeitemname: "Box"),
		};
		gameManager.allObjects[id].dialogues[2].choices = new Choice[]
		{
			AddChoice("Keep box"),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 99, 1, checkitemname: "Box", removeitemname: "Box"),
		};
		gameManager.allObjects[id].dialogues[3].choices = new Choice[]
		{
			AddChoice("Keep box"),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 99, 5, checkitemname: "Box", removeitemname: "Box"),
		};
		gameManager.allObjects[id].dialogues[4].choices = new Choice[]
		{
			AddChoice("Keep box"),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 99, 10, checkitemname: "Box", removeitemname: "Box"),
		};
		gameManager.allObjects[id].dialogues[5].choices = new Choice[]
		{
			AddChoice("Keep box"),
			AddChoice("Give box.", ChoiceAction.CONTINUE, 36, 26, checkitemname: "Box", removeitemname: "Box"),
		};
    }

    private void LoadQuestTerms()
    {
        gameManager.questTerms = new Dictionary<string, string>()
		{
			{ "PB" , "peanut butter" },
			{ "NM" , "name" },
			{ "" , "" }
		};
    }

    private void LoadQuestData()
    {
        QuestList ql = GameManager.instance.GetComponent<QuestList>();
        #region QUEST TEMPLATE
        //Quest Template
        /*  NPC_ID : ID of the NPC this quest belongs to
         *  dialogue_in_progress : What this NPC says after you talk to him once quest has been activated
         *  dialogue_change : What the NPC says once you FINISH the quest
         *  requirement : Required quest finished in order to activate this quest  Leave it as "none" if nothing is required
         *  changebool : What boolean gets changed once you finish the quest       Leave it as "none" if nothing is changed afterwards
         *  required_item : Item you need in order to finish the quest             Leave it as "none" if nothing is required
         *  questName : name of this quest
         *  Time : When the quest is accessible : 0 for anytime, otherwise, list timeblocks i.e. {6,8,10}
         */
        //   ql.AddQuest(NPC_ID, dialogue_in_progress, dialogue_change, requirement, changeBool, required_item, questName);

        #endregion
        // Alfred's son's quest
        // Meet Alex
        // ql.AddQuest(75, 1, 7, "none", "none", "none", "MeetAlex");
        // Convince Alex
        //  ql.AddQuest(75, 7, 8, "MeetAlex", "none", "Alfred's Jewel", "Convince Alfred's Son");

        //ql.AddQuest(11, 1, 1, "none", "LostDog", "none", "HankMorning", new List<int> { 12, 14 });
        //ql.AddQuest(11, 3, 3, "none", "DogCanDig", "none", "HankAfternoon", new List<int> { 16 });

        //ql.AddQuest(23, 12, 13, "none", "FoundDog", "Lost Dog", "ReturnDogToRae", new List<int> { 8, 10, 12, 14, 16, 18, 20, 22 });

    }

    private void LoadSceneData()
    {
        string sceneName = "";

        #region EMPTY TEMPLATE
        // EMPTY TEMPLATE
        /*
		AddParameters(sceneName, new InteractableObject.Parameters()
			{
				// Specify the time frames that this set takes effect
				timeBlocks = new List<int>() {  },

				// InteractableObject dialogue information
				dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
				dialogueIDSingle = 0,
				dialogueIDMin = 0,  dialogueIDMax = 0,
				dialogueIDMulti = new List<int>() {  },

				// NPC CharacterAnimations
				turnOnInteract = true,
				startingAnimationState = CharacterAnimations.States.DOWN_IDLE,
				animationSpeed = 0f,
				wanderDistanceX = 0f,  wanderDirectionX = 0,
				wanderDistanceY = 0f,  wanderDirectionY = 0,

				// Getter/Setter variables, NpcID is required
				Summary = "",
				NpcID = -1
			});
		*/
        #endregion

#region SCENE_HOUSE
        sceneName = SceneManager.SCENE_HOUSE;

        // ======================== BACON ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
            {
                // Specify the time frames that this set takes effect
                timeBlocks = new List<int>() { 6, 8, 10, 12, 14, 16, 18, 20, 22 },
				
                // Getter/Setter variables, NpcID is required
                Summary = "",
                NpcID = 110
            });
        // ======================== MOM ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 6 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
        
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 5,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 5
        });
#endregion
#region SCENE_PARK

        sceneName = SceneManager.SCENE_PARK;

        // ======================== DOG ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12, 14, 20, 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 123
        });

        // ======================== BOX ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 111
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10, 14, 16, 18, 20, 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 111
        });

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "Rae looking for dog",
            NpcID = 23
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "favorite spot",
            NpcID = 23
        });

        // ======================== HANK ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.DOWN_STRETCH,
            animationSpeed = 0.2f,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 5,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
        });

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,
            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });

        // ======================== BOB ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPING,
            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 11,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,


            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });

        // ======================== FAYE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 10,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });

        // ======================== DAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 17,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        // ======================== KYLE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_STRETCH,
            animationSpeed = 0.2f,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });

        // ======================== ANNA ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 51
        });

        // ======================== SUSAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 52
        });

        // ======================== APRIL ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_STRETCH,
            animationSpeed = 0.2f,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 53
        });

#endregion

#region SCENE_MAINSTREET
        sceneName = SceneManager.SCENE_MAINSTREET;

        // ======================== DOG ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10, 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 123
        });

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 23
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 11,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 23
        });

        // ======================== JENEY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });

        // ======================== ALFRED ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 18,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.FALLEN,
            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 22,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
        
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 16,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });

        // ======================== BOB ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 13
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            // Getter/Setter variables, NpcID is required

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,
            turnOnInteract = false,

            Summary = "",
            NpcID = 13
        });

        // ======================== FAYE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 13,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 16,
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });

        // ======================== PATRICIA ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 16,
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });

        // ======================== DAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 20,
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        // ======================== PERRY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 99
        });

        // ======================== KELLY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 67
        });

        // ======================== ALEX ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

        // ======================== KYLE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 5,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,
            turnOnInteract = true,
            wanderDirectionX = 1,
            wanderDistanceX = 10,
            speed = 0.09f,
            animationSpeed = 0.07f,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });

        // ======================== SUSAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 52
        });

        // ======================== APRIL ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 53
        });


        // ======================== CONES ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 220
        });
#endregion

#region SCENE_MALL

        sceneName = SceneManager.SCENE_MALL;

        // ======================== DOG ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 123
        });

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 23
        });

        // ======================== JENEY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 23,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });

        // ======================== ALFRED ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,
            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });

        // ======================== FAYE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });

        // ======================== PATRICIA ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });
        
        
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 17,

            turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });

        // ======================== DAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        // ======================== KELLY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 67
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 67
        });

        // ======================== ALEX ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 12,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

        // ======================== ANNA ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 51
        });

        // ======================== APRIL ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 53
        });
#endregion

#region SCENE_POLICE
        sceneName = SceneManager.SCENE_POLICE;

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 23
        });

        // ======================== ALFRED ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10, 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14, 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        // ======================== DAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 13,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 22,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 15,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        // ======================== PERRY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10, 12, 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

			turnOnInteract = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 99
        });


        // ======================== APRIL ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 2,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 53
        });
#endregion

#region SCENE_HOSPITAL
        sceneName = SceneManager.SCENE_HOSPITAL;

        // ======================== ALFRED ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 17,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPING,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 10,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_PRAY,
            animationSpeed = 1f,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });

        // ======================== YOONA ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPING,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 1,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        }); AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 4,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPING,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        }); AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 10,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.SLEEPY,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });

        // ======================== ALEX ======================== //
     
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 14,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 18,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 19,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });

#endregion

#region SCENE_CLASS


        sceneName = SceneManager.SCENE_CLASS;

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 18,

            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,


            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
  
        // ======================== KELLY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12},

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 67
        });
   

        // ======================== KYLE ======================== //
   
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });

        // ======================== ANNA ======================== //
   
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 51
        });

        // ======================== SUSAN ======================== //
       
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,
            startingAnimationState = CharacterAnimations.States.UP_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 52
        });
#endregion
	}

	private void LoadBoolSceneData()
	{
		// They will be loaded in order of priority. Scene data with AlfredSaved will always be used first.
		LoadAlfredSavedSceneData();
		LoadDogReturnedSceneData();
        LoadDonutPickingSceneData();
	}
    #region AlfredSavedScene
    private void LoadAlfredSavedSceneData()
	{
        const string boolParamSet = "AlfredSaved";
        
        // ======================== ALFRED ======================== //

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 24,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });
        LinkContinueDialogues(2, new int[2] { 24, 25 });

        gameManager.allObjects[2].dialogues[25].choices = new Choice[]
        {
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 0, checkitemname: "Box"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, 2, 21, checkitemname: "Box"),
        };
        AddToDialogue(2, 15, ChoiceContinueDialog(2, 16));

        AddParameters(boolParamSet, SceneManager.SCENE_HOSPITAL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });


        // ======================== MEGAN ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 30,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
        LinkContinueDialogues(3, new int[2] { 30, 31 });

        AddParameters(boolParamSet, SceneManager.SCENE_HOSPITAL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });


        // ======================== ALEX ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_HOSPITAL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });


          // ======================== JENEY ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 24,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 7
        });


        // ======================== FAYE ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_MALL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 26,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });

        AddParameters(boolParamSet, SceneManager.SCENE_MALL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 33,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 27
        });

        gameManager.allObjects[27].dialogues[26].choices = new Choice[]
		{
            AddChoice("Yes.", ChoiceAction.CONTINUE, 27, 28),
            AddChoice("No.", ChoiceAction.CONTINUE, 27, 31)
        };
        LinkContinueDialogues(27, new int[3] { 28, 29, 30});
        LinkContinueDialogues(27, new int[2] { 31, 32});
        LinkContinueDialogues(27, new int[5] { 33, 34, 35, 36, 37 });



        // ======================== DAE ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_POLICE, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 28,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(boolParamSet, SceneManager.SCENE_POLICE, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 29,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 30,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        gameManager.allObjects[36].dialogues[28].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 5, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, 36, 23)
        };

        gameManager.allObjects[36].dialogues[29].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 5, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, 36, 23, checkitemname: "Box")
        };
        gameManager.allObjects[36].dialogues[30].choices = new Choice[]
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 5, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, 36, 23, checkitemname: "Box")
        };


        LinkContinueDialogues(36, new int[2] { 26, 27 });
        AddToDialogue(36, 27, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "DaeEnding" });

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },
            showNpc = false,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        AddParameters(boolParamSet, SceneManager.SCENE_HOSPITAL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 24,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });
        LinkContinueDialogues(36, new int[2] { 24, 25 });

        gameManager.allObjects[36].dialogues[25].choices = new Choice[]  //Day just restart
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 1, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, 36, 23, checkitemname: "Box")
        };

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 17,
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        AddParameters(boolParamSet, SceneManager.SCENE_PARK, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },
            showNpc = false,
            // InteractableObject dialogue information
          
            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        LinkContinueDialogues(36, new int[2] { 17, 18 });
        gameManager.allObjects[36].dialogues[17].choices = new Choice[]  //Day just restart
        {
              AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 1, checkitemname: "Box"),
              AddChoice("Say nothing.", ChoiceAction.CONTINUE, 36, 23, checkitemname: "Box")
        };

        LinkContinueDialogues(36, new int[2] { 11, 12 });

        // ======================== YOONA ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_HOSPITAL, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 37
        });


        // ======================== PATRICIA ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 23,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 24,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });
        LinkContinueDialogues(66, new int[2] { 24, 25 });

        // ======================== PERRY ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_POLICE, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 9,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 99
        });

        gameManager.allObjects[99].dialogues[9].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 4, checkitemname: "Box"),
            AddChoice("Do nothing.", ChoiceAction.CONTINUE, 99, 3, checkitemname: "Box") 
        };

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 7,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 99
        });

        gameManager.allObjects[99].dialogues[7].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 4, checkitemname: "Box"),
            AddChoice("Do nothing.", ChoiceAction.CONTINUE, 99, 3, checkitemname: "Box") 
        };
  

        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 22 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 99
        });
        gameManager.allObjects[99].dialogues[8].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 1, 4, checkitemname: "Box"),
            AddChoice("Do nothing.", ChoiceAction.CONTINUE, 99, 3, checkitemname: "Box") 
        };
        AddToDialogue(99, 10, ChoiceContinueDialog(99, 11));
        AddToDialogue(99, 1, ChoiceContinueDialog(99, 2));

        AddToDialogue(99, 11, new ChoiceEventArgs() { ChoiceAction = EndingManager.CallCoroutineEvent, CoroutineName = "PerryEnding" });

        // ======================== KYLE ======================== //
        AddParameters(boolParamSet, SceneManager.SCENE_MAINSTREET, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 20 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 6,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.RIGHT_IDLE,
            turnOnInteract = true,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 50
        });

	}

    #endregion
    #region DogReturnedScene
    private void LoadDogReturnedSceneData()
	{
		const string boolParamSet = "DogReturned";
		const int hank = 11;
		const int jeney = 7;
        const int bob = 13;
        const int faye = 27;
		const string park = SceneManager.SCENE_PARK;
		const string donutshop = SceneManager.SCENE_MALL;
		const string mainstreet = SceneManager.SCENE_MAINSTREET;


		// ======================== HANK ======================== //
		AddParameters(boolParamSet, park, new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 18 },
			
			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
			dialogueIDSingle = 10,
			
			// NPC CharacterAnimations
			startingAnimationState = CharacterAnimations.States.LEFT_IDLE,
			
			// Getter/Setter variables, NpcID is required
			Summary = "she was gettin' annoying",
			NpcID = hank
		});


        // ======================== FAYE ======================== //
        AddParameters(boolParamSet, park, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 24,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = faye
        });

        AddParameters(boolParamSet, mainstreet, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 18 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 25,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = faye
        });

        // ======================== BOB ======================== //
        AddParameters(boolParamSet, park, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 23,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

            // Getter/Setter variables, NpcID is required
            Summary = "dog gone",
            NpcID = bob
        });
        LinkContinueDialogues(bob, new int[3] { 23, 24, 25 });
        gameManager.allObjects[13].dialogues[25].choices = new Choice[]
        {
			AddChoice("Offer train ticket.", ChoiceAction.CONTINUE, bob, 13, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket", removeitemname: "Train Ticket"),
            AddChoice("Say nothing.", ChoiceAction.CONTINUE, bob, 21, checkboolname: "BobWantsToLeave", checkitemname: "Train Ticket")
        };
		// ======================== JENEY ======================== //
		AddParameters(boolParamSet, donutshop, new InteractableObject.Parameters()
		{
			// Specify the time frames that this set takes effect
			timeBlocks = new List<int>() { 16, 18 },
			
			// InteractableObject dialogue information
			dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
			dialogueIDSingle = 21,
			
			// Getter/Setter variables, NpcID is required
			Summary = "glad she found it lulz donut bone",
			NpcID = jeney
		});
		AddToDialogue(jeney, 21, ChoiceContinueDialog(jeney, 22));

        // ======================== PATRICIA ======================== //
        AddParameters(boolParamSet, donutshop, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16},

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 21,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 66
        });
        AddToDialogue(66, 21, ChoiceContinueDialog(66, 22));
	}
    #endregion

    #region DonutPickingScene
    private void LoadDonutPickingSceneData()
    {
        // ======================== KELLY ======================== //
        const string boolParamSet = "DonutPicked";
        const int kelly = 67;
        const string donutshop = SceneManager.SCENE_MALL;
 
        AddParameters(boolParamSet, donutshop, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 14,

            Summary = "",
            NpcID = kelly
        });

    }
    #endregion
    private void LoadOutcomeData()
    {
        #region EMPTY_TEMPLATE
        /*
        AddOutcome(
            "Name of the boolean to check",
            "Text to display on screen if the boolean is true",
            "Text to display on screen if the boolean is false");
        */
        #endregion

        AddOutcome(
            "FoundDog",
            "You have found and returned the Dog to Rae! She's happy 5ever.",
            "Rae never found her dog. She left the city, alone, with that useless train ticket... :'(");
        /*AddOutcome(
            "TestJewelOutcome",
            "You have found the jewel of ultimate power. Now you set off on your quest to rule the world",
            "The jewel broke and made you sad.");
        AddOutcome(
            "TestPeanutButterOutcome",
            "You ate some delicious peanut butter. Yum",
            "Without peanut butter, you fall into a deep depression and starved to death");
        AddOutcome(
            "NoOutcomeHere",
            "blank",
            "blank");*/
    }

}
