using UnityEngine;
using System.Collections;

public class GameEventArgs
{
	public string DialogChoice { get; set; }
    public GameObject ThisGameObject { get; set; }

	public string String { get; set; }
	public int Integer { get; set; }
    public Vector2 Position { get; set; }
    public int IDNum { get; set; }
}
