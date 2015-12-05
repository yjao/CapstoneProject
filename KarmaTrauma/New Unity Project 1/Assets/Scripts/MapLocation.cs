using UnityEngine;
using System.Collections;

public class MapLocation : MonoBehaviour
{
	public string AlternativeDestination;
	public GameManager.AREA Destination;
    private GameManager gameManager;

    void Start()
    {
         gameManager = GameManager.Instance;
    }
	void OnMouseDown()
	{
		string sceneName = AlternativeDestination;
		if (Destination != GameManager.AREA.NONE)
			sceneName = Destination.ToString();
        SceneManager.Instance.LoadScene(sceneName);
		//Application.LoadLevel(sceneName);
       
	}
}
