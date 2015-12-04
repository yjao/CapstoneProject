using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string AltDestination;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
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