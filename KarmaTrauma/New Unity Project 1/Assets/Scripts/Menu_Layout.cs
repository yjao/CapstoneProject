using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Menu_Layout : MonoBehaviour {

    GameManager gameManager;
    QuestManager questManager;
    QuestLog quest_log;
    Menu gameMenu;
    Player player;
    private bool timeButton_show = false;
    private bool invenButton_show = false;
    private bool diaryButton_show = false;

    
    private Texture2D inventory_texture;
    private Texture2D diary_texture;
   // public GUIContent inventory_content;
   
	// Use this for initialization
	void Start () {
        gameManager = GameManager.Instance;
        questManager = QuestManager.Instance;
        //gameMenu = FindObjectOfType(typeof(Menu)) as Menu;
        quest_log = QuestLog.Instance; 
        player = FindObjectOfType(typeof(Player)) as Player;
        //show_button = gameMenu.In_Mode();
        inventory_texture = Resources.Load("Bag") as Texture2D;
        //inventory_content.inventory_texture = inventory_texture;
       
	}

    void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        
        string time = gameManager.GetTime();
        
        bool open_menu = GameObject.FindGameObjectWithTag("Menu");
        //GUI.skin.button.normal.background = inventory_texture;
        //GUI.skin.toggle.hover.background = inventory_texture;
        //GUI.skin.toggle.active.background = inventory_texture;
        
        timeButton_show = GUI.Button(new Rect(0,0,40,40), time);
        
        invenButton_show = GUI.Button(new Rect(0, 40, 40, 40), inventory_texture);

        if (invenButton_show && !open_menu)
        {
            player.InvenButton();
            invenButton_show = false;
        }

        diaryButton_show = GUI.Button(new Rect(0, 80, 40, 40), "Diary");

        //Find a place to put this:  Dataloader
       

        if (diaryButton_show && !open_menu)
        {
            //Insert open diary function here
            player.QuestButton();
            diaryButton_show = false;

        }

        
        
    }
	
	// Update is called once per frame
	void Update () {
        
	
	}
}
