using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    public static QuestLog Instance;
    private GameManager gameManager;
   
    //index of current quest;
    private int q_i;

    //index of
    private int q_l;

    int pointer;

    // Use this for initialization
    void Start()
    {

        pointer = 0;
        q_i = 0;
        q_l = 0;

        gameManager = GameManager.instance;

        this.tag = "Menu";

        HideDescription();
        Instance = this;

        Log();
        DrawSelect();

     
    }

   
    void Log()
    {
        //transform.Find("Panel").gameObject.SetActive(false);
        //transform.Find("Text").gameObject.SetActive(false);
        //transform.Find("Pointer").gameObject.SetActive(false);
        transform.Find("QuestPanel0").gameObject.SetActive(true);


        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Transform z = (Instantiate(transform.Find("QuestPanel0"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
                    z.transform.Translate(new Vector2(0.1f, 0.2f));
                    z.transform.SetParent(transform, false);
                    z.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.8f + .14f * x), (.95f - .13f * y));
                    z.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f + .14f * x), (.84f - .13f * y));
                    z.name = "QuestPanel" + y;

                    if (gameManager.questList.Count != 0 && q_l < gameManager.questList.Count)
                    {

                        transform.Find("QuestPanel0").transform.Find("QuestText").GetComponent<Text>().text = gameManager.questList[q_l][0];
                        ++q_l;
                    }

                }
            }
        }
        DrawSelect();
    }

    public void display()
    {

        if (gameManager.questList.Count != 0 && q_l < gameManager.questList.Count)
        {
            for (int quest = 0; quest < gameManager.questList.Count && quest < 5; ++quest)
                transform.Find("QuestPanel" + quest).transform.Find("QuestText").GetComponent<Text>().text = gameManager.questList[q_l][0];
            ++q_l;
        }
    }

    void DrawSelect()
    {
        transform.Find("QuestSelect").gameObject.SetActive(true);
   
        transform.Find("QuestSelect").transform.GetComponent<RectTransform>().anchorMax = new Vector2((.8f), (.95f - .13f * pointer));
        transform.Find("QuestSelect").transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f), (.84f - .13f * pointer));
        transform.Find("QuestSelect").SetAsLastSibling();
 
        if (gameManager.questList.Count != 0 && gameManager.questList.Count > q_i)
        {
            Debug.Log("index " + q_i);
            string NPCName = (gameManager.questList[q_i])[1];
            string dialog = (gameManager.questList[q_i])[2];
    
            DrawDescription(NPCName, dialog);
        }
        else
            HideDescription();
    }


    void DrawDescription(string name, string description)
    {
        transform.Find("DialogText").gameObject.SetActive(true);
        transform.Find("NameText").gameObject.SetActive(true);
        transform.Find("DialogTextPanel").gameObject.SetActive(true);
        transform.Find("NamePanel").gameObject.SetActive(true);
        transform.Find("DialogText").transform.GetComponent<Text>().text = description;
        transform.Find("NameText").transform.GetComponent<Text>().text = name;
    }

    void HideDescription()
    {
        transform.Find("DialogText").gameObject.SetActive(false);
        transform.Find("NameText").gameObject.SetActive(false);
        transform.Find("DialogTextPanel").gameObject.SetActive(false);
        transform.Find("NamePanel").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pointer < 4)
            {
                pointer += 1;
                q_i += 1;
                DrawSelect();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pointer > 0)
            {
                q_i -= 1;
                pointer -= 1;
                DrawSelect();
            }
        }
    }
}
