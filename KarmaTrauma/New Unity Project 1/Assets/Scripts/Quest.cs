using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {
    public int NPC_ID;
//    public string prereq;
    public int dialogue_in_progress;
    public int dialogue_change;
    public string requirement = "none";
    public string changeBool = "none";
    public string required_item = "none";
   
	// Use this for initialization
    void Start()
    {
            GameObject QL = GameObject.Find("QuestList");
            QuestList ql = QL.GetComponent<QuestList>();
            ql.AddQuest(NPC_ID, dialogue_in_progress,dialogue_change, requirement, changeBool,required_item);
	}
	void OnEnable()
    {

    }


	// Update is called once per frame
	void Update () {
	
	}
}
