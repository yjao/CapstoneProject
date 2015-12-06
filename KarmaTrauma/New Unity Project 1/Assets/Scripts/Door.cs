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
        gameManager = GameManager.instance;
        //playerData = PlayerData.Instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameManager.IncreaseTime();
			bool midnight = gameManager.Midnight();
			string destination = "WorldMap";
            if (AltDestination != "")
            {
				destination = AltDestination;
            }

			if (midnight)
			{
				destination = "G_House";
			}

			SceneManager.instance.LoadScene(destination);
        }
    }
    void ChangeDestination(string newDestination)
    {
        AltDestination = newDestination;
    }
}