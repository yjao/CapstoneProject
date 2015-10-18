using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "character")
        {
            Debug.Log("yes");
            Application.LoadLevel("Chelsey'sRoom");
        }
    }
}