using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel("WorldMap");
        }
    }
}