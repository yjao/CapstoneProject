using UnityEngine;
using System.Collections;

public class Menu_Layout : MonoBehaviour {

    GameManager gameManager;
    Menu gameMenu;
    Player player;
    private bool show_button;
   
	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        gameMenu = FindObjectOfType(typeof(Menu)) as Menu;
        player = FindObjectOfType(typeof(Player)) as Player;
        show_button = gameMenu.In_Mode();
       
	}

    void OnGUI()
    {
        //GUI.enabled = false;
        
        if (show_button && GUI.Button(new Rect(10, 10, 50, 50), "Inventory"))
        {
            
            //gameManager.MODE.MENU;
            show_button = false;
            player.InvenButton();
            //Debug.Log(GUI.enabled);

            
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
