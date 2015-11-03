using UnityEngine;
using System.Collections;

public class InvisibleWallLeft : MonoBehaviour {

    Player player;
    private bool colliding = false;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        float wall_x = this.transform.position.x;
        float scale_x = this.transform.localScale.x;
        //Debug.Log(wall_x);
        wall_x = wall_x + scale_x / 2;
        if (colliding)
        {
            player.onHitLeft(wall_x);

        }
        //Debug.Log(colliding);

    }
}

