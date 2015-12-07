using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	private GameManager gameManager;

	public string AltDestination;
	public bool increaseTime = true;
    //public PlayerData playerData;
    //public DayData dayData = new DayData();

    void Start()
    {
        gameManager = GameManager.instance;
        //playerData = PlayerData.Instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			if (increaseTime)
			{
	            gameManager.IncreaseTime();
			}
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