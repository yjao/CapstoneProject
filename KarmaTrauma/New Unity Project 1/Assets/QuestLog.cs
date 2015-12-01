using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour {
    public static QuestLog Instance;
    private QuestManager questManager;
    private GameManager gameManager;
    private Dictionary<int, string> quests_table; 
    private string tasks;
    

	// Use this for initialization
	void Start () {
        Instance = this;
        questManager = QuestManager.Instance;
        gameManager = GameManager.Instance;
        this.tag = "Menu";
        transform.Find("Dim").gameObject.SetActive(false);
        transform.Find("Header").gameObject.SetActive(false);
        transform.Find("QuestList").gameObject.SetActive(false);
        transform.Find("ItemTextPanel").gameObject.SetActive(false);



        //Quests
        quests_table = new Dictionary<int, string> {
            {0 , "Get debugger jewel for sensei lel"},
            {1 , "Some other quest"}
        };
	}

    void OpenQuestLog()
    {
        tasks = ""; //tentative

        transform.Find("Dim").gameObject.SetActive(true);
        transform.Find("Header").gameObject.SetActive(true);
        transform.Find("QuestList").gameObject.SetActive(true);
        transform.Find("ItemTextPanel").gameObject.SetActive(true);

        Debug.Log(questManager.Quest_log().Count);
        foreach (int quest in questManager.Quest_log())
        {
            tasks += quests_table[quest] + "\n";
        };
        transform.Find("QuestList").transform.GetComponent<Text>().text = tasks;
    }
    

	// Update is called once per frame
	void Update () {
        OpenQuestLog();
	    if (Input.GetKeyDown(KeyCode.Space)){

            gameManager.Play();
            GameObject.Destroy(gameObject);
            
        }
	}
}
