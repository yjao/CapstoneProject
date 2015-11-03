using UnityEngine;
using System.Collections;

public class InvisibleWall : MonoBehaviour
{
	Player player;
	private bool colliding = false;
	
	public enum WALL_DIRECTION
	{
		DEFAULT, TOP, LEFT, RIGHT, BOTTOM
	};
	public WALL_DIRECTION WallDirection;
	
	// Use this for initialization
	void Start()
	{
		player = Player.Instance;
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
		float wall_y = this.transform.position.y;
		float scale_y = this.transform.localScale.y;
		float wall_x = this.transform.position.x;
		float scale_x = this.transform.localScale.x;

		switch (WallDirection)
		{
		case WALL_DIRECTION.TOP:
			wall_y = wall_y - (scale_y/2);
			if (colliding)
				player.onHitTop(wall_y);
			break;
		case WALL_DIRECTION.BOTTOM:
			wall_y = wall_y + (scale_y/2);
			if (colliding)
				player.onHitBot(wall_y);
			break;
		case WALL_DIRECTION.LEFT:
			wall_x = wall_x + (scale_x/2);
			if (colliding)
				player.onHitLeft(wall_x);
			break;
		case WALL_DIRECTION.RIGHT:
			wall_x = wall_x - (scale_x/2);
			if (colliding)
				player.onHitRight(wall_x);
			break;
		default:
			break;
		}
	}
}
