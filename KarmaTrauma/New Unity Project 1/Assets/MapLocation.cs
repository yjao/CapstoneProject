using UnityEngine;
using System.Collections;

public class MapLocation : MonoBehaviour
{
	public string AlternativeDestination;
	public GameManager.AREA Destination;

	void OnMouseDown()
	{
		string sceneName = AlternativeDestination;
		if (Destination != GameManager.AREA.NONE)
			sceneName = Destination.ToString();
		Application.LoadLevel(sceneName);
	}
}
