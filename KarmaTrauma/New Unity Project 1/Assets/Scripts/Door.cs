using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string AltDestination;

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
                Application.LoadLevel("World Map");
            }
        }
    }
}