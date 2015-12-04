using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string AltDestination;
    private GameManager gameManager;
    public PlayerData Data;
    public DayData dayData = new DayData();

    void Start()
    {
        gameManager = GameManager.Instance;
        //Data = PlayerData.Instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (AltDestination != "")
            {
                gameManager.IncreaseTime();
                if (gameManager.Midnight())
                {
                    Application.LoadLevel("G_House");
                    //Loop back to where mom yelling at you on Scene Manager or somewhere...                        
                }
                //SceneManager.Instance.LoadScene(AltDestination);
                Application.LoadLevel(AltDestination);

            }
            else
            {
                gameManager.IncreaseTime();
                if (gameManager.Midnight())
                {
                    Application.LoadLevel("G_House");
                    Data.daysPassed++;
                    Debug.Log("day: " + Data.daysPassed);
                    //gameManager.CreateMessage("Oops, another day had passed. Try to clear all quests in one go. You're now on Day " + (Data.daysPassed + 1), true);
                    //dayData.Wipe();
                    //Data.daysPassed++;
                    //gameManager.CreateMessage("Oops, another day had passed. Try to clear all quests in one go. You're now on Day " + (Data.daysPassed + 1), true);
                }
                else {
                    Application.LoadLevel("WorldMap");
                }
                
                
            }
        }
    }
    void ChangeDestination(string newDestination)
    {
        AltDestination = newDestination;
    }
}