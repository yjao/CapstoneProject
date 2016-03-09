using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    public static QuestLog instance;
    private GameManager gameManager;
   
    //index of current quest;
    private int q_i;

    //index of
    private int q_l;
    int timeLocationPanelCounter = 1;
    int pointer;
    private int max_page = 1;
    private int page_index = 0;
    private int quest_per_page = 4;

    private List<string> q_log;

    AudioSource source;
    public AudioClip choose;
    public AudioClip page;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();

        pointer = 0;
        q_i = 0;
        q_l = 0;
        q_log = new List<string>();

        gameManager = GameManager.instance;

        this.tag = "Menu";

        
        instance = this;

        
        LocationTime();
        HideDescription();
        Log();
        
        DrawSelect();
    }

    void LocationTime()
    {
        //transform.Find("Panel").gameObject.SetActive(false);
        //transform.Find("Text").gameObject.SetActive(false);
        //transform.Find("Pointer").gameObject.SetActive(false);
        transform.Find("LocationTimePanel0").gameObject.SetActive(false);


        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Transform z = (Instantiate(transform.Find("LocationTimePanel0"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
                    z.transform.Translate(new Vector2(0.1f, 0.2f));
                    z.transform.SetParent(transform, false);
                    z.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.355f + .25f * x), (.167f - .25f * y));
                    z.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.149f + .25f * x), (.0f - .25f * y));
                    z.name = "LocationTimePanel" + x;
                    transform.Find("LocationTimePanel" + x).gameObject.SetActive(false);

                }
            }
        }
    }

    void Log()
    {
        //transform.Find("Panel").gameObject.SetActive(false);
        //transform.Find("Text").gameObject.SetActive(false);
        //transform.Find("Pointer").gameObject.SetActive(false);
        transform.Find("QuestPanel0").gameObject.SetActive(true);


        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < quest_per_page; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Transform z = (Instantiate(transform.Find("QuestPanel0"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
                    z.transform.Translate(new Vector2(0.1f, 0.2f));
                    z.transform.SetParent(transform, false);
                    z.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.8f + .14f * x), (.95f - .15f * y));
                    z.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f + .14f * x), (.8216f - .15f * y));
                    z.name = "QuestPanel" + y;

                    //foreach (string key in gameManager.questList.Keys)
                    //{
                    //    if (!(q_log.Contains(key)))
                    //    {
                    //        q_log.Add(key);
                    //    }
                    //}
                    //if (gameManager.questList.Count != 0 && q_l < gameManager.questList.Count)
                    //{
                    //    Debug.Log("ql " + q_l);
                    //    for (int quest = 0; quest < gameManager.questList.Count && quest < 5; ++quest)
                    //    {
                    //        transform.Find("QuestPanel" + q_l).transform.Find("QuestText").GetComponent<Text>().text = q_log[q_l];
                    //        ++q_l;
                    //    }
                    //}

                }
            }
        }
        DrawSelect();
    }

    public void display()
    {
        //Debug.Log("q_l" + q_l);
        q_l = page_index * quest_per_page;
        //q_i = page_index * quest_per_page;
        //Debug.Log("questlist count: " + gameManager.questList.Count);
        max_page = (gameManager.questList.Count / quest_per_page) + 1;

        if (gameManager.questList.Count % quest_per_page == 0)
        {
            max_page = gameManager.questList.Count / quest_per_page;
        }
        transform.Find("PageIndex").gameObject.transform.Find("PageIndexText").GetComponent<Text>().text = (page_index + 1).ToString() + "/" + max_page.ToString();

        foreach (string key in gameManager.questList.Keys)
        {
            if (!(q_log.Contains(key)))
            {
                q_log.Add(key);
            }
        }
        Debug.Log("q_log has: " + q_log.Count);
        if (gameManager.questList.Count != 0 && q_l < gameManager.questList.Count)
        {
            //transform.Find("QuestPanel" + q_l).transform.Find("QuestText").GetComponent<Text>().text = q_log[q_l];
            //++q_l;
            Debug.Log("page index " + page_index);
            for (int quest = page_index*quest_per_page; quest < gameManager.questList.Count && (quest - page_index*quest_per_page) < quest_per_page; ++quest)
            {
                Debug.Log("Quest name is: " + q_log[q_l]);
                Debug.Log("Math: " + q_l % quest_per_page);
                transform.Find("QuestPanel" + q_l%quest_per_page).transform.Find("QuestText").GetComponent<Text>().text = q_log[q_l];
                ++q_l;
            }
            
        }
    }

    public void DrawSelect()
    {
       
        transform.Find("QuestSelect").gameObject.SetActive(true);
   
        transform.Find("QuestSelect").transform.GetComponent<RectTransform>().anchorMax = new Vector2((.8f), (.95f - .15f * pointer));
        transform.Find("QuestSelect").transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f), (.8216f - .15f * pointer));
        transform.Find("QuestSelect").SetAsLastSibling();


        if (q_log.Count > q_i && gameManager.questList.Count != 0 && gameManager.questList.Count > q_i)
        {
            string NPCName = (gameManager.questList[q_log[q_i]])[0];
            string dialog = (gameManager.questList[q_log[q_i]])[1];


            DrawDescription(NPCName, dialog, q_i);
        }
        else
        {
            HideDescription();
          
            
        }
    }

    void ClearLocationTimePanels()
    {
        transform.Find("LocationTimePanel0").gameObject.SetActive(false);
        transform.Find("LocationTimePanel1").gameObject.SetActive(false);
        transform.Find("LocationTimePanel2").gameObject.SetActive(false);
    }

    void ClearQuestPanelTexts()
    {
        for (int i = 0; i < quest_per_page; i++)
        {
            transform.Find("QuestPanel" + i).transform.Find("QuestText").GetComponent<Text>().text = "";
        }
    }


    void DrawDescription(string name, string description, int q_i)
    {
        transform.Find("DialogText").gameObject.SetActive(true);
        transform.Find("NameText").gameObject.SetActive(true);
        transform.Find("DialogTextPanel").gameObject.SetActive(true);
        transform.Find("NamePanel").gameObject.SetActive(true);
        transform.Find("DialogText").transform.GetComponent<Text>().text = description;
        transform.Find("NameText").transform.GetComponent<Text>().text = name;
        List<string> temp_list = gameManager.questList[q_log[q_i]];
        int j = 0;
        ClearLocationTimePanels();
        for(int i = 2; i < temp_list.Count -1; i+=2){
            transform.Find("LocationTimePanel" + j).gameObject.SetActive(true);
            //transform.Find("LocationTimePanel" + j).transform.Find("LocationTimeText").GetComponent<Text>().text = "hi";
            transform.Find("LocationTimePanel" + j).transform.Find("LocationTimeText").GetComponent<Text>().text = temp_list[i] + "\n" + temp_list[i+1];
            j++;
        }
      
        
    }

    void HideDescription()
    {
        transform.Find("DialogText").gameObject.SetActive(false);
        transform.Find("NameText").gameObject.SetActive(false);
        transform.Find("DialogTextPanel").gameObject.SetActive(false);
        transform.Find("NamePanel").gameObject.SetActive(false);
        ClearLocationTimePanels();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (pointer < quest_per_page-1)
            {                        
                source.PlayOneShot(choose, 1);

                pointer += 1;
                q_i += 1;
                DrawSelect();
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (pointer > 0)
            {
                source.PlayOneShot(choose, 1);

                q_i -= 1;
                pointer -= 1;
                DrawSelect();
            }
            Debug.Log(max_page);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && page_index < max_page - 1)
        {
            source.PlayOneShot(page, 1);

            pointer = 0;
            page_index++;
            q_i = page_index * quest_per_page;

            ClearQuestPanelTexts();
            display();
            DrawSelect();
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && page_index > 0)
        {
            source.PlayOneShot(page, 1);

            pointer = 0;
            page_index--;
            q_i = page_index * quest_per_page;
            //q_l = page_index * quest_per_page;
            //q_i = page_index*quest_per_page;

            ClearQuestPanelTexts();
            display();
            DrawSelect();
        }
    }
}
