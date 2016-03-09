using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	private GameManager gameManager;
    private AudioSource source;
    public AudioClip leaveArea;

	public string AltDestination;
	public bool increaseTime = true;
    //public PlayerData playerData;
    //public DayData dayData = new DayData();
    private bool colliding = false;
    private string destination;

    void Start()
    {
        source = GetComponent<AudioSource>();

        gameManager = GameManager.instance;
        //playerData = PlayerData.Instance;
    }

    void Update()
    {
        if (colliding == true && Input.GetKeyDown(KeyCode.Space) && gameManager.gameMode == GameManager.GameMode.PLAYING)
        {
			// Cooldown procedure
			//if (!gameManager.CheckKeyCooldown()) { return; } else { gameManager.SetKeyCooldown(); }

			bool midnight = false;
            if (increaseTime)
            {
                midnight = gameManager.SetTime(GameManager.TimeType.INCREASE);//, delay:true);
                //Debug.Log("gameClock : " + gameManager.GetTimeAsInt());
            }
            destination = SceneManager.SCENE_WORLDMAP;
            if (AltDestination != "")
            {
                destination = AltDestination;
            }

            if (midnight)
            {
                destination = SceneManager.SCENE_HOUSE;
            }
            if (!midnight)
            {
                source.PlayOneShot(leaveArea, 1);
                SceneManager.instance.LoadScene(destination);
            }
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