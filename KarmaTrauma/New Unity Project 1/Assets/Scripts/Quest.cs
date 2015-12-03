using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {
    public int NPC_ID;
//    public string prereq;
    public int dialogue;
    public string requirement;
    public string changeBool;

   
	// Use this for initialization
    void Start()
    {
            GameObject QL = GameObject.Find("QuestList");
            QuestList ql = QL.GetComponent<QuestList>();
            ql.AddQuest(NPC_ID, dialogue, requirement, changeBool);
	}
	void OnEnable()
    {

    }


	// Update is called once per frame
	void Update () {
	
	}
}
