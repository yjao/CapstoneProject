using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class QuestList : MonoBehaviour
{
    List<QClass> Qlist;


	// Use this for initialization
	void Start () {
	}
    void Awake()
    {
        Qlist = new List<QClass>();

    }


	
	// Update is called once per frame
	void Update () {
	}

    public void AddQuest(int NPC, int dialogue_in,int dialogue_change, string requirement, string changeBool, string requiredItem, string questName)
    {
            QClass temporary;
            temporary = new QClass(NPC, dialogue_in, dialogue_change, requirement, changeBool,requiredItem,questName);
            
            Qlist.Add(temporary);    
    }

	public void AddQuest(Quest q)
	{
		AddQuest(q.NPC_ID, q.dialogue_in_progress, q.dialogue_change, q.requirement, q.changeBool, q.required_item, q.questName);
        PlayerData pl = GameManager.instance.playerData;
        pl.RegisterQuest(q.questName);
	}

    public void CheckQuest(InteractableObject obj)
    {
        // Iterate thorugh all the quests in the quest list
        for (int i = 0; i < Qlist.Count; i++)
        {
            QClass temp = (QClass)Qlist[i];
            if (obj.iD == temp.NPC)
            {
                Debug.Log(temp.questName);
                PlayerData pl = GameManager.instance.playerData;
                if(temp.completed)
                {
                    // IF the quest is already finished in a scene
                    // This should be redundant
                }
                    
                else if(pl.CheckQuest(temp.questName))
                {
                  // If the player data says the quest is finished, do nothing
                }
                else
                {
                    InteractableObject io = obj.GetComponent<InteractableObject>();

                    //If requirements are met
                    
                    Debug.Log(pl);
                    DayData dd = GameManager.instance.dayData;
                    Debug.Log(dd);
                    if (pl.GetBool(temp.requirement))
                    {
                        // Trigger the quest 
                        if (temp.triggered == false)
                        {
                            temp.triggered = true;
                            break;
                        }
                        else
                        {
                            Debug.Log("Dialogue in progress");
                            io.dialogueIDSingle = temp.dialogue_in_progress;
                        }    
                        if (temp.itemTurnedIn == false)
                        {
                            for (int j = 0; j < GameManager.instance.dayData.ItemAmount; j++)
                            {
                                if (GameManager.instance.dayData.Inventory[j].Name == temp.requiredItem)
                                {
                                    Debug.Log("Have Item");
                                    GameManager.instance.dayData.Inventory[j] = null;
                                    GameManager.instance.dayData.ItemAmount--;
                                    temp.itemTurnedIn = true;
                                    break;
                                }
                                else
                                {
                                }
                            }
                        }
                    }

                    if (temp.itemTurnedIn)
                    {
                        io.dialogueIDSingle = temp.dialogue_change;
                        pl.SetBool(temp.changeBool);
                        pl.FinishQuest(temp.questName);
                        temp.completed = true;
                    }
                    

                }
            }
        }
    }

}
class QClass
{
    public int NPC;
    public int dialogue_in_progress;
    public int dialogue_change;
    public string requirement;
    public string changeBool;
    public bool completed;
    public string requiredItem;
    public bool itemTurnedIn;
    public bool triggered;
    public string questName;
    public QClass(int npc, int dial_in, int dial_ch, string req, string cb,string item, string name)
    {
        NPC = npc;
        dialogue_in_progress = dial_in;
        dialogue_change = dial_ch;
        requirement = req;
        changeBool = cb;
        completed = false;
        requiredItem = item;
        triggered = false;
        questName = name;
        
        
        ////
    
        if (requiredItem == "none")
        {
            itemTurnedIn = true;
        }
        else
            itemTurnedIn = false;


    }
    
    
}