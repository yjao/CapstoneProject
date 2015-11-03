using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour {
    public float speed;
    private bool player_arrived = false;
    private int end = 0;
    private bool check = false;
    private bool fallen = false;

    public GameObject cam;


    // Use this for initialization
    void Start () {
       cam.SetActive(false);
        Debug.Log(cam == null);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(cam == null);

        // start falling trigger
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > 2 && check == false && player_arrived == false)
        {
            Debug.Log("triggered");
            player_arrived = true;
            Player.Instance.PlayerCamera.SetActive(false);
            if (cam != null)
                cam.SetActive(true);
            check = true;
        }

        // falling
        if (end != 100 && player_arrived == true)
        {
            transform.Translate(0, -speed, 0);
            end += 1;
        }

        // he has fallen
        if (fallen == false && end == 100)
        {
            Player.Instance.PlayerCamera.SetActive(true);
            if (cam != null)
                cam.SetActive(false);
            
            fallen = true;
            Debug.Log("HELLO");
        }

    }
}
