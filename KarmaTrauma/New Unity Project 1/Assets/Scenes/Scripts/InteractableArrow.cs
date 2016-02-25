using UnityEngine;
using System.Collections;

public class InteractableArrow : MonoBehaviour
{
    SpriteRenderer renderer;
    private bool turnOn = false;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (turnOn == true)
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            turnOn = true;

        /*if (!player.CollidingWithID.Contains(ID))
            player.CollidingWithID.Add(ID);*/
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            turnOn = false;

        /*if (player.CollidingWithID.Contains(ID))
            player.CollidingWithID.Remove(ID);*/
    }
}
