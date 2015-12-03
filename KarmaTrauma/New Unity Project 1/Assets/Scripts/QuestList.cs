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

    public void AddQuest(int NPC, int dialogue, string requirement, string changeBool)
    {
            QClass temporary;
            temporary = new QClass(NPC, dialogue, requirement, changeBool);
            Debug.Log(Qlist);
            
            Qlist.Add(temporary);
        
    }

    public void CheckQuest(InteractableObject obj)
    {

        Debug.Log("Number of quests: " + Qlist.Count);
        // Iterate thorugh all the quests in the quest list
        for (int i = 0; i < Qlist.Count ; i++)
        {
            QClass temp = (QClass)Qlist[i];
            if(obj.ID==temp.NPC && !temp.completed)
            {
                InteractableObject io = obj.GetComponent<InteractableObject>();
               
                //If requirement met

                GameObject PL = GameObject.Find("Player");
                PlayerData pl = PL.GetComponent<PlayerData>();
                if(pl.GetBool(temp.requirement))
                {
                    io.DialogueIDSingle = temp.dialogue;
                    pl.SetBool(temp.changeBool);
                    temp.completed = true;
                }
                
                
               
            }
        }
    }

}
class QClass
{
    public int NPC;
    public int dialogue;
    public string requirement;
    public string changeBool;
    public bool completed;
    public QClass(int npc, int dial, string req, string cb)
    {
        NPC = npc;
        dialogue = dial;
        requirement = req;
        changeBool = cb;
        completed = false;
    }
    
    
}