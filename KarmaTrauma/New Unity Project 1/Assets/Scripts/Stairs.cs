using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

    public float char_positionx;
    public float char_positiony;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.position = new Vector2(char_positionx, char_positiony);
        }
    }
}
