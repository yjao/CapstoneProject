using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Menu_Layout : MonoBehaviour
{

    GameManager gameManager;
    QuestManager questManager;
    QuestLog quest_log;
    Menu gameMenu;
    Player player;
    private bool timeButton_show = false;
    private bool invenButton_show = false;
    private bool diaryButton_show = false;
    public int startTime;

    private int world_map_level = 17;
    private Texture2D inventory_texture;
    private Texture2D diary_texture;
    // public GUIContent inventory_content;

    // Use this for initialization
    void Start()
    {

        gameManager = GameManager.instance;
        questManager = QuestManager.Instance;
        //gameMenu = FindObjectOfType(typeof(Menu)) as Menu;
        //quest_log = QuestLog.Instance; 
        player = FindObjectOfType(typeof(Player)) as Player;
        ////show_button = gameMenu.In_Mode();
        //inventory_texture = Resources.Load("Bag") as Texture2D;
        //inventory_content.inventory_texture = inventory_texture;
        if (startTime != 0)
        {
            gameManager.SetTime(startTime);
        }

        transform.FindChild("QuestBook").gameObject.SetActive(false);
        transform.FindChild("Inventory").gameObject.SetActive(false);

    }

    //void OnGUI()
    //{
    //    GUI.backgroundColor = Color.clear;

    //    string time = gameManager.GetTime();

    //    bool open_menu = GameObject.FindGameObjectWithTag("Menu");
    //    //GUI.skin.button.normal.background = inventory_texture;
    //    //GUI.skin.toggle.hover.background = inventory_texture;
    //    //GUI.skin.toggle.active.background = inventory_texture;

    //    timeButton_show = GUI.Button(new Rect(0,0,100,40), time);

    //    invenButton_show = GUI.Button(new Rect(0, 40, 40, 40), inventory_texture);

    //    if (invenButton_show && !open_menu)
    //    {
    //        player.InvenButton();
    //        invenButton_show = false;
    //    }

    //    diaryButton_show = GUI.Button(new Rect(0, 80, 40, 40), "Diary");

    //    //Find a place to put this:  Dataloader


    //    if (diaryButton_show && !open_menu)
    //    {
    //        //Insert open diary function here
    //        player.QuestButton();
    //        diaryButton_show = false;

    //    }



    //}
    void M_Clock()
    {
        //Debug.Log(transform.FindChild("Clock_display").GetComponent<Text>().text);
        transform.FindChild("Clock_display").GetComponent<Text>().text = gameManager.GetTime();
    }

    public void M_Fastforward()
    {
        if (gameManager.GetTimeAsInt() < 22)
        {
            gameManager.IncreaseTime();
        }
    }

    public void M_Bag()
    {
        //player.InvenButton();
        gameManager.Wait();
        transform.FindChild("Inventory").gameObject.SetActive(true);
    }

    private void B_Bag()
    {
        transform.FindChild("Inventory").gameObject.SetActive(true);
        invenButton_show = true;
        gameManager.gameMode = GameManager.GameMode.MENU;
        gameManager.prevMode = GameManager.GameMode.PLAYING;

    }

    public void Close_Bag()
    {
        transform.FindChild("Inventory").gameObject.SetActive(false);
        //invenButton_show = false;
        gameManager.Play();
    }

    public void M_Diary()
    {
        //player.QuestButton();
        gameManager.Wait();
        transform.FindChild("QuestBook").gameObject.SetActive(true);
    }

    public void Close_Diary()
    {
        transform.FindChild("QuestBook").gameObject.SetActive(false);
        gameManager.Play();
    }

    public void Fast_Forward_Label(bool setToTrue)
    {
        if (setToTrue)
        {
            transform.FindChild("Fast_forward").gameObject.SetActive(true);
        }
        else
        {
            transform.FindChild("Fast_forward").gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update() {
        M_Clock();
	    if (Input.GetKeyDown(KeyCode.B)){
            B_Bag();
        }
        //if (invenButton_show){
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Close_Bag();
               
        //    }
        //}
	}
}
