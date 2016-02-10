using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	private GameManager gameManager;

	public string AltDestination;
	public bool increaseTime = true;
    //public PlayerData playerData;
    //public DayData dayData = new DayData();
    private bool colliding = false;
    private string destination;

    void Start()
    {
        gameManager = GameManager.instance;
        //playerData = PlayerData.Instance;
    }

    void Update()
    {
        if (colliding == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (increaseTime)
            {
                gameManager.IncreaseTime();
                //Debug.Log("gameClock : " + gameManager.GetTimeAsInt());
            }
            bool midnight = gameManager.Midnight();
            destination = "WorldMap";
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            colliding = false;

        /*if (player.CollidingWithID.Contains(ID))
            player.CollidingWithID.Remove(ID);*/
    }


    void ChangeDestination(string newDestination)
    {
        AltDestination = newDestination;
    }
}