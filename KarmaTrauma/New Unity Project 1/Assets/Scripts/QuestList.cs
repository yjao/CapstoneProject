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

    public void AddQuest(int NPC, int dialogue_in,int dialogue_change, string requirement, string changeBool, string requiredItem)
    {
            QClass temporary;
            temporary = new QClass(NPC, dialogue_in, dialogue_change, requirement, changeBool,requiredItem);
            
            Qlist.Add(temporary);
        
    }

    public void CheckQuest(InteractableObject obj)
    {

        // Iterate thorugh all the quests in the quest list
        for (int i = 0; i < Qlist.Count; i++)
        {
            QClass temp = (QClass)Qlist[i];
            if (obj.ID == temp.NPC)
            {
                if(temp.completed)
                {
                }
                else
                {
                    InteractableObject io = obj.GetComponent<InteractableObject>();

                    //If requirement met
                    PlayerData pl = GameManager.Instance.playerData;
                    Debug.Log(pl);
                    DayData dd = GameManager.Instance.dayData;
                    Debug.Log(dd);
                    if (pl.GetBool(temp.requirement))   
                    {
                        if (temp.triggered == false)
                        {
                            temp.triggered = true;
                            io.DialogueIDSingle = temp.dialogue_in_progress;
                            break;
                        }
                        if (temp.itemTurnedIn == false)
                        {
                            for (int j = 0; j < GameManager.Instance.dayData.ItemAmount; j++)
                            {
                                if (GameManager.Instance.dayData.Inventory[j].Name == temp.requiredItem)
                                {
                                    GameManager.Instance.dayData.Inventory[j] = null;
                                    GameManager.Instance.dayData.ItemAmount--;
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
                        io.DialogueIDSingle = temp.dialogue_change;
                        pl.SetBool(temp.changeBool);
                        temp.completed = true;

                    }

                    else
                    {
                        io.DialogueIDSingle = temp.dialogue_in_progress;
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
    public QClass(int npc, int dial_in, int dial_ch, string req, string cb,string item)
    {
        NPC = npc;
        dialogue_in_progress = dial_in;
        dialogue_change = dial_ch;
        requirement = req;
        changeBool = cb;
        completed = false;
        requiredItem = item;
        triggered = false;
        
        
        ////
    
        if (requiredItem == "none")
        {
            itemTurnedIn = true;
        }
        else
            itemTurnedIn = false;


    }
    
    
}