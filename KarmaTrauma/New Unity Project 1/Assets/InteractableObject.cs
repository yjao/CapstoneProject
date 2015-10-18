using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			Debug.Log("Collided");
		}
	}

}
