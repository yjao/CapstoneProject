using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSprite : MonoBehaviour {

	[HideInInspector]
	public Sprite sprite;
	[HideInInspector]
	public int x = 1, y = 1;
	[HideInInspector]
	public float offsetX = 0, offsetY = 0;
    [HideInInspector]
	public List<GameObject> spritesInScene = new List<GameObject>();
	
	/// <summary>
	/// Creates the sprites.
	/// </summary>
	/// <param name="_axisX">_axis x.</param>
	/// <param name="_axisY">_axis y.</param>
	public void CreateSprites(int _axisX, int _axisY)
	{
		//spritesInScene = new GameObject[_axisX,_axisY];

		for (int i = 0; i < _axisX; i++)
		{
			for (int j = 0; j < _axisY; j++)
			{
				GameObject newSprite = new GameObject("sprite_" + i.ToString() + "_" + j.ToString());
				newSprite.transform.parent = this.transform;
				newSprite.transform.localPosition = new Vector3(i * sprite.textureRect.width / sprite.pixelsPerUnit + (i * offsetX), 
				                                                j * sprite.textureRect.height / sprite.pixelsPerUnit + (j *offsetY));
                newSprite.transform.localScale = Vector3.one;
				newSprite.AddComponent<SpriteRenderer>().sprite = sprite;
				spritesInScene.Add(newSprite);
			}
		}
	}

	/// <summary>
	/// Cleans up.
	/// </summary>
	public void CleanUp()
	{ 
		if (spritesInScene.Count > 0)
		{
			for (int i = 0; i < spritesInScene.Count;)
			{
				DestroyImmediate(spritesInScene[i]);
				spritesInScene.RemoveAt(i);
			}
		}

	}
}

