using UnityEngine;
using System.Collections;

public class Menu_Layout : MonoBehaviour {

    GameManager gameManager;
    Menu gameMenu;
    Player player;
   
	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        gameMenu = FindObjectOfType(typeof(Menu)) as Menu;
        player = FindObjectOfType(typeof(Player)) as Player;
       
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Bag"))
        {
            //gameManager.MODE.MENU;
            player.InvenButton();
            
            
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
