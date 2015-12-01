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
                Application.LoadLevel(AltDestination);        
            }
            else
            {
                Application.LoadLevel("WorldMap");
            }
        }
    }
    void ChangeDestination(string newDestination)
    {
        AltDestination = newDestination;
    }
}