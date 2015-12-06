using UnityEngine;
using System.Collections;

public class InvisibleWallTop : MonoBehaviour {

    Player player;
    private bool colliding = false;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType(typeof(Player)) as Player;
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            //Debug.Log("hitting");
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            //Debug.Log("gettingout");
            colliding = false;
        }
    }
   
	// Update is called once per frame
	void Update () {
        float wall_y = this.transform.position.y;
        float scale_y = this.transform.localScale.y;

        wall_y = wall_y - scale_y;
        if (colliding)
        {
            player.onHitTop(wall_y);

        }
        //Debug.Log(colliding);
	
	}
}
