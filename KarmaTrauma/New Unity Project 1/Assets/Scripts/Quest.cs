using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {
    public int NPC_ID;
    //    public string prereq;
    public string questName;
    public string requirement = "none";
    public string required_item = "none";
    public int dialogue_in_progress;
    public int dialogue_change;
    public string changeBool = "none";
	// Use this for initialization
    void Start()
    {
            QuestList ql = GameManager.instance.GetComponent<QuestList>();
            ql.AddQuest(NPC_ID, dialogue_in_progress,dialogue_change, requirement, changeBool,required_item, questName);
	}
	void OnEnable()
    {

    }


	// Update is called once per frame
	void Update () {
	
	}
}
