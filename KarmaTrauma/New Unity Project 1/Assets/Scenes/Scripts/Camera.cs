using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    private float distance = 10;
    private static Player player;
	// Use this for initialization
	void Start () {
    player = FindObjectOfType(typeof(Player)) as Player;

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z-distance);

	
	}
}
