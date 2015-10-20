using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    private bool collided = false;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			collided = true;
		}

	}
    void Update()
    {
        if (collided && Input.GetKey(KeyCode.Space))
        {
            //call dialogue box;
            Debug.Log("Interacting");
            collided = false;
        }
    }
}
