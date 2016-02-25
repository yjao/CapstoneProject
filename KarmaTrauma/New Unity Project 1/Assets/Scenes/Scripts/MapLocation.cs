using UnityEngine;
using System.Collections;

public class MapLocation : MonoBehaviour
{
	public string AlternativeDestination;
	public GameManager.Area Destination;
    private GameManager gameManager;

    void Start()
    {
         gameManager = GameManager.instance;
    }
	void OnMouseDown()
	{
		string sceneName = AlternativeDestination;
		if (Destination != GameManager.Area.NONE)
			sceneName = Destination.ToString();
        SceneManager.instance.LoadScene(sceneName);
		//Application.LoadLevel(sceneName);
       
	}
}
