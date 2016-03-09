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
        LoadOutcomeData();
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
			/*2*/ "\"I've also done a bit of everything else, including story, programming, and art.\"",
			/*2*/ "\"In the game, my role is a side character, a broke college student. Come visit me in-game!\"",
			/*3*/ "\"I'm not entirely sure if Potadonut is even a thing...\""
		};
		AddNpc(id, "Faye Jao", "", faye);
		AddToDialogue(id, 0, ChoiceContinueDialog(id, 1));

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
			/*0*/ "\"Hi, I'm Jeney. I program most of the animations as well as dealing with sprites in general.\"",
            /*1*/ "\"In each scene, I make sure that the sprites are where they should be as well as appearing at the correct time.\"",
            /*2*/ "\"In the game, my character is a donut shop owner. :)\""
		};
		AddNpc(id, "Jeney Lao", "", jeney);

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
            /*6*/ "(I guess it's time that I own up to my actions and put Faraday to jail)",
            /*7*/ "\"Oh Megan...I'm so sorry...\"",
            /*8*/ "(Had I reach the park in time...\"",
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

        };
        AddNpc(id, "Alfred", "Alfred", alfred);

        //Check if Cocodonut is in the bag.  Can give Cocodonut to Alfred any time.
        gameManager.allObjects[2].dialogues[5].choices = new Choice[]
        {
			AddChoice("Give box", ChoiceAction.CONTINUE, 2, 15, checkboolname: "AlfredBox", checkitemname: "Box"),
            AddChoice("Offer Cocodonut", ChoiceAction.CONTINUE, 2, 5, checkboolname: "AlfredCocodonut", checkitemname: "Cocodonut")
        };
        AddToDialogue(2, 5, ChoiceContinueDialog(2, 12));
        AddToDialogue(2, 12, ChoiceContinueDialog(2, 13));
		AddToDialogue(2, 15, ChoiceContinueDialog(2, 16));

        gameManager.allObjects[2].dialogues[4].choices = new Choice[]
        {
            AddChoice("Give box", ChoiceAction.CONTINUE, 2, 15, checkboolname: "AlfredBox", checkitemname: "Box")
        };



        LinkContinueDialogues(id, new int[3] { 0, 5, 2});
        LinkContinueDialogues(id, new int[2] { 4, 3 });
        LinkContinueDialogues(id, new int[2] { 1, 10 });
        LinkContinueDialogues(id, new int[2] { 7, 9 });

        // ================ MEGAN ================ //
        id = 3;
        string[] megan = new string[]
        {
            /*0*/ "\"Alfred...!\"",       
            /*1*/ "(If I could...if only I could go back in time...)",
            /*2*/ "(If only I knew that #someone was out to get him#.)",
            /*3*/ "*Sob* He was going to get his wallet at our apartment then meet me for dinner...\"",
            /*4*/ "\"If I haven't been hospitalize at all, this wouldn't have happened.\"",
            /*5*/ "\"Oh Alfred, please hang in there!\"*Sob*",
            /*6*/"\"Good morning. Just taking my morning stroll in the park.\"",
            /*7*/"\"Sweetie, there's school today. Do you go to Punxsu High? My son also goes there.\"",
            /*8*/"\"Jeney, I'd like a Minty Munchies please!\"",
            /*9*/"\"Oh! And for the coupon, I'd like a #Cocodonut for Alfred#. He goes loco for them Cocodonuts *Teehee*.\"",
            /*10*/ "\"I was hospitalized 2 years ago due to a car accident.\"",  
            /*11*/ "\"Since then, our family has been in a lot of debt because of me...\"",  
            /*12*/ "\"My poor Alfred. I'm so sorry to put him through this.\"",  
            /*13*/ "\"But I've always wondered how he managed to collect so much money that time.\"",  
            /*14*/ "\"A time that I want to go back to?\"", 
            /*15*/ "\"I guess I would want to turn back time so that incident would never happen...\"", 
            /*16*/ "\"Heehee~ It's been such a long time since the two of us are going to go out for dinner together. I can't wait!\"", 
            /*17*/ "\"Oh, I should go do my #checkups# first.\"", 
        };
        AddNpc(id, "Megan", "Megan", megan);
        AddToDialogue(id, 6, ChoiceContinueDialog(id, 7));

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
            AddChoice("Say nothing", ChoiceAction.CONTINUE, id, 10),
            AddChoice("Sure", ChoiceAction.CONTINUE, id, 11)
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
            /*3*/"\"Anything interesting happened today?\"",
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
			/*0*/ "\"Welcome to the Donut Hole! Today's special is the Donot Sprinklez~\"",
			/*1*/ "(Come here doggie! I'll give you a tasty Donut Bone.)",
            /*2*/ "\"Oh no...my friend's #dog was here at 4 PM#.\"",
            /*3*/ "\"I should have offered him #bacon# instead.\"",
            /*4*/ "\"Poor man...he was a really #nice cop# who comes by my store often.\"",
            /*5*/ "\"He always tell me that our #Cocodonut# tastes the best.\"",
            /*6*/ "(Whew...what a long day. So many things happened.)",
            /*7*/ "\"It's a shame. Punxsutown used to be so much better before it all happened.\"",
            /*8*/ "\"2 years ago, Jerry Faraday was elected mayor of town. He seemed great at first...\"",
            /*9*/ "\"But over the years, he had kicked out all these vendors. It's why you barely see anyone anymore.\"",
            /*10*/"\"Some were really nice family businesses that’s been there for a long time...How could he do such a thing?\"",
            /*11*/"\"Well, I hope the Donut Hole will be fine. We have so many loyal customers.\"",
            /*12*/"\"I tried catching him by feeding him a donut, but he ran off.\"",
            /*13*/"\"I hope that he returned home safely.\"",


        };
        AddNpc(7, "Jeney", "Jeney", jeney);

        gameManager.allObjects[id].dialogues[2].choices = new Choice[]
		{
            AddChoice("What happened?", ChoiceAction.CONTINUE, id, 12)
        };
       AddToDialogue(id, 12, ChoiceContinueDialog(id, 3));
       AddToDialogue(id, 3, ChoiceContinueDialog(id, 13));


        gameManager.allObjects[7].dialogues[7].choices = new Choice[]
		{
            AddChoice("What happened?", ChoiceAction.CONTINUE, 7, 8)
        };
       LinkContinueDialogues(id, new int[4]{8,9,10,11});
       LinkContinueDialogues(id, new int[2] { 4, 5 });
       

        // ================ HANK ================ //
        id = 11;
        string[] hank = new string[]
		{
			/*0*/ "(Ugh I'm so sleepy. I hate this job.)",
			/*1*/ "\"That ugly dog belongs to this girl name Rae. She was at the #park until 11:30 PM# looking for the dog while crying.\"",
			/*2*/ "(So annoying ugh...)", 
			/*3*/ "\"Because I took a #nap, that stupid dog started digging# all over the park at 1 PM.\"",
			/*4*/ "\"Ugh! Now I have to go and fill all the holes that he dug up. Now go away and stop bothering me.\"",
			/*5*/ "(Ugh...I'm hungry. I don't get paid enough for this.)",
		    /*6*/ "\"Now go away and stop bothering me. I got to fill up these stupid holes.\"",
            /*7*/ "\"Ugh...Go away, kid. Don't you see that I'm about to go to bed?\"",
            /*8*/ "(Maybe some stupid visitor fed him bacon or something. Dogs always get excited over #stupid bacon#.)",
            /*9*/ "\"#Rae was here at 5 PM# and went to the police station just now. About time she moves on and stop pestering me.\"",
            
        
        };
        AddNpc(id, "Hank", "Hank", hank);
        AddBooleanToDialogue(id, 1, "LostDog");
        AddBooleanToDialogue(id, 3, "DogCanDig");

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
			/*5*/ "(I #want to leave# this place...)"  ,
			/*6*/ "(When a man's got no food or shelter, he might make #extreme decisions#...)"  ,
			/*7*/ "\"Hi there, got some money to spare?\"" , 
			/*8*/ "\"I need money to buy #a train ticket#. I could really use one...\"",
            /*9*/ "(*Mumble in sleep* Should I really #do it for Faraday#...?)"  ,
			/*10*/ "\"I woke up to some girl crying. I think she's #looking for her dog#.\""  ,
			/*11*/ "\"I #tossed the dog some bacon# bits and look at what happened!\""  ,
			/*12*/ "\"At first, he followed me, but I told him to go #dig some holes# to mess with cranky ol' Hank. Ha!\"",
			/*13*/ "\"Oh my goodness, you do have one! Can I really have this?\"",
			/*14*/"\"Thank you so much. I can have a much better life leaving Punxsutown.\"",
		    /*15*/ "\"I don't need to become some murderer, and I can start a new life elsewhere.\"",    
            /*16*/ "\"I'll remember you forever, Chelsey.\"",
            /*17*/ "\"You see, I'm a man without food or shelter.\"",
            /*18*/ "\"Somebody offered me money if I help him with something dirty... I don't know what to think of it.\"",
            /*19*/ "\"If I can get out of here, I can start a new life!\"",
            /*20*/ "\"Thanks, but I can't leave now. It's too late for me to start a new life when I've ruined another's life.\"",
        
        
        };
        AddNpc(id, "Bob", "Hobo master race", bob);

        gameManager.allObjects[id].dialogues[8].choices = new Choice[]
		{
            AddChoice("Why do you need one?", ChoiceAction.CONTINUE, id, 17)
        };
        AddToDialogue(id, 17, ChoiceContinueDialog(id, 18));
        AddToDialogue(id, 18, ChoiceContinueDialog(id, 19));

        //Same problem as Alfred.  Can give ticket to hobo any time.
        gameManager.allObjects[13].dialogues[4].choices = new Choice[]
        {
            AddChoice("Offer train ticket", ChoiceAction.CONTINUE, 13, 13, checkboolname: "BobTrainTicket", checkitemname: "TrainTicket")
        };
        AddToDialogue(13, 13, ChoiceContinueDialog(13, 14));
        AddToDialogue(13, 14, ChoiceContinueDialog(13, 15));
        AddToDialogue(13, 15, ChoiceContinueDialog(13, 16));

        gameManager.allObjects[13].dialogues[0].choices = new Choice[]
        {
            AddChoice("Offer train ticket", ChoiceAction.CONTINUE, 13, 20, checkboolname: "BobTrainTicket", checkitemname: "TrainTicket")
        };


        gameManager.allObjects[13].dialogues[1].choices = new Choice[]
        {
            AddChoice("Offer train ticket", ChoiceAction.CONTINUE, 13, 20, checkboolname: "BobTrainTicket", checkitemname: "TrainTicket")
        };
 
        LinkContinueDialogues(id, new int[3]{7,8,10});
        AddToDialogue(id, 11, ChoiceContinueDialog(id, 12));
        LinkContinueDialogues(id, new int[3] { 4, 5, 6 });
        LinkContinueDialogues(id, new int[3] { 1, 2, 3 });
        LinkContinueDialogues(id, new int[2] { 0, 1 });


        // ================ RAE ================ //
        id = 23;
        string[] rae = new string[]
		{
			/*0*/ "*Sob*\"I #lost my dog# today...\"",
			/*1*/ "\"He went outside to take a leak and hadn't come back since.\"",
			/*2*/ "\"I tried looking for him everywhere, but he's nowhere to be found.\"",
			/*3*/ "\"What should I do...\"*Sob*",
			/*4*/ "\"Jeney is my friend from middle school. She now owns this successful donut shop chain.\"",
			/*5*/ "\"All the cops in this town loves this store and comes by often. You should try some.\"",
			/*6*/ "\"#Have you seen my dog?# I've been looking everywhere for him.\""  ,
			/*7*/ "\"I was hoping that he will at the park since it's his #favorite spot#.\""  ,
			/*8*/ "\"It's been 12 hours since I lost my dog. I don't know what to do.\"" , 
			/*9*/ "\"I hope someone #finds him and returns him to me#.\""  ,
			/*10*/ "(I guess this #train ticket# is useless now since I can't leave town without him.)"  ,
			/*11*/ "(Poor guy...I hope he rests in peace.)"  ,
			/*12*/ "\"I came to ask her to keep an eye out for #my lost dog#. Have you seen him?\"",
			/*13*/ "\"Tomo! Thank you for finding my dog.\"",
			/*14*/"\"I guess this train ticket is useless now since I still haven't packed up because I was out looking for him. Here, you can have it.\"",
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
            /*16*/ "(Is it just me or I keep seeing that dog everywhere?)",
            /*17*/ "(I saw a #dog on Main Street around 11 PM#.)",
            /*18*/ "(Then, I saw another #dog at the Park around 3 PM#.)",
            /*19*/ "(Maybe I'm just seeing things...)",
            /*20*/ "\"Also, I stayed up all night last night programming KT, a game project.\"",
            /*21*/ "\"I'm just not a morning person.\"",
            /*22*/ "\"Maybe I should take a stroll in the park.\"",
            
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
            /*6*/ "\"Did you see anyone supicious around here?\"",
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
            /*17*/"\"!!!!!\"",
            /*18*/"\"Kid, what are you doing at this hour? You shouldn't be here. Go home now!\"",
            /*19*/"\"My daughter also loves the donuts from this store. You should try some.\"",
            /*20*/"\"Shh...go away kid, I'm busy.\"",
            
        };
        AddNpc(id, "Dae", "Dae", dae);


        //Give box to Dae any time.
        //gameManager.allObjects[36].dialogues[0].choices = new Choice[]
        //{
        //    AddChoice("Give box", ChoiceAction.CONTINUE, 27, 9)
        //};
        //AddToDialogue(27, 9, ChoiceContinueDialog(27, 21));
        //AddToDialogue(27, 21, ChoiceContinueDialog(27, 20));
        //AddToDialogue(27, 20, ChoiceContinueDialog(27, 22));

        LinkContinueDialogues(id, new int[2] { 17, 18 });
        LinkContinueDialogues(id, new int[2] { 0, 19 });
        LinkContinueDialogues(id, new int[2] { 13, 14 });
        LinkContinueDialogues(id, new int[2] { 2, 3 });
        LinkContinueDialogues(id, new int[2] { 15, 16 });
        LinkContinueDialogues(id, new int[4] { 4, 5, 6, 7 });

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
            /*7*/"\"When is Alex getting back? I'm bored...\"",
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
            /*1*/ "\"Maybe I should go to the police station and tell his dad that he'd been ditching class.\"",
            /*2*/ "\"Hey, what's up?\"",
            /*3*/ "\"Ugh...we lost the game.\"",
            /*4*/ "\"Oh it's Alex's dad! Maybe I should go talk to him about Alex.\"",
           
            
        };
        AddNpc(id, "Kyle", "Kyle", kyle);


        // ================ ANNA ================ //
        id = 51;
        string[] anna = new string[]
        {
            /*0*/ "\"Did you hear about Yoona? She has a heart disease so that's why she hasn't been coming to school lately. Maybe I should go visit her.\"",       
            /*1*/ "\"Shh Kelly's mom is here. Don't tell Kelly this, but I don't like her mom.\"",
            /*2*/ "\"Her mom works for Faraday. My dad's business is going downhill because of him. They're shady I'm telling you.\"",
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
            /*0*/ "\"On Wednesday, we wear blue\"",       
            /*1*/ "\"Ew look at them running and sweating. Boys.\"",
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
            /*2*/ "\"There's this #hobo in the park# and he smells. I finally reported him. Ugh I hope he goes away and stop ruining my mood in the morning.\"",
            /*3*/ "\"Oh it's Dae. It must be hard for him to raise a daughter all by himself. He's such a good father. It seems like he will do anything for her.\"",
            /*4*/ "\"The Donut Hole Original is the best. Don't you agree?\"",       

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
            /*14*/"\"Me? I'm #Faraday's secretary#. He lets me take breaks whenever I want.\"",
            /*15*/"\"Oops, am I supposed to not tell you this?\"",
            /*16*/ "(The show is about to start...)",
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


        // ================ KELLY ================ //
        id = 67;
        string[] kelly = new string[]
        {
            /*0*/ "\"Phew, classes are finally over.\"",       
            /*1*/ "\"Hey, why don’t we go hang out? I’m craving Jeney’s donuts.\"",
            /*2*/ "\"What is it? Okay, I'll wait a bit, but not for long! Or I can meet you there, too.\"",
            /*3*/ "\"Yesterday I overheard my mom and Faraday talking about #a box#. It looks serious because a cop was there.\"",
            /*4*/ "\"Hey, you don't think that #cop guy works with Faraday#, do you?\"",
            /*5*/ "\"Shh, my mom's sitting over there...\"",
            /*6*/"\"Ugh that slimeball Faraday's pictures are everywhere. I don't get what my mom sees in him.\"",
            /*7*/"\"I want my mom to realizes that what she's doing is wrong, but I don't want her to be taken away from me. After all, she's still my mom and she's the only one I have.\"",
          
            
        };
        AddNpc(id, "Kelly", "Kelly", kelly);
        LinkContinueDialogues(id, new int[2] { 0, 1 });
        gameManager.allObjects[id].dialogues[1].choices = new Choice[]
		{
            AddChoice("Hold on.", ChoiceAction.CONTINUE, id, 2)
        };

        LinkContinueDialogues(id, new int[3] { 3, 4, 5 });

        // ================ PERRY ================ //
        id = 99;
        string[] perry = new string[]
        {
            /*0*/ "\"...\"",       
            /*1*/ "\"....?\"",
            /*2*/ "\"Thanks, I'll take a look at it.\"",
          
        };
        AddNpc(id, "Perry", "Perry", perry);
        //Give box to Perry add choice here.

        gameManager.allObjects[99].dialogues[0].choices = new Choice[]
		{
            AddChoice("Give box.", ChoiceAction.CONTINUE, 99, 1, checkboolname: "PerryBox", checkitemname: "Box")
        };
        AddToDialogue(99, 1, ChoiceContinueDialog(99, 2));

        // ================ DOGE ================ //
        id = 123;
        string[] dog = new string[]
		{
            /*0*/ "\"Woof\"",
            /*1*/ "(You give the dog some bacon)",
            /*2*/ "(The dog appears to be following you)"
        };
        AddNpc(id, "Dog", "Dog", dog);
        gameManager.allObjects[id].dialogues[0].choices = new Choice[]
		{
            AddChoice("Feed the dog bacon", ChoiceAction.CONTINUE, id, 1, checkboolname: "Bacon")
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
            AddChoice("Leave it alone"),
            AddChoice("Take the bacon", ChoiceAction.ITEM, id)
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

        ql.AddQuest(23, 12, 13, "none", "FoundDog", "Lost Dog", "ReturnDogToRae", new List<int> { 8, 10, 12, 14, 16, 18, 20, 22 });

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

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8, 10 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,


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

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 11
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
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

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

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 52
        });

        // ======================== KYLE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 8 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

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
            NpcID = 105
        });

        // ======================== RAE ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

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

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 2
        });

        // ======================== MEGAN ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 16,

            // NPC CharacterAnimations
            startingAnimationState = CharacterAnimations.States.LEFT_IDLE,

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
            dialogueIDSingle = 0,

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
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

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

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 36
        });

        // ======================== KELLY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 16 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 3,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 67
        });

        // ======================== ALEX ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 12,

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
            dialogueIDSingle = 2,
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
            dialogueIDSingle = 0,

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
            dialogueIDSingle = 7,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 4
        });
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 8,

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
            dialogueIDSingle = 14,

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
            timeBlocks = new List<int>() { 12, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 14,

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 3
        });
  
        // ======================== KELLY ======================== //
        AddParameters(sceneName, new InteractableObject.Parameters()
        {
            // Specify the time frames that this set takes effect
            timeBlocks = new List<int>() { 12, 14 },

            // InteractableObject dialogue information
            dialogueIDType = InteractableObject.Dialogue_ID_Type.SINGLE_DIALOGUE_ID,
            dialogueIDSingle = 0,

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

            // Getter/Setter variables, NpcID is required
            Summary = "",
            NpcID = 52
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
