using UnityEngine;
using System.Collections;

public class ClassSceneManager : MonoBehaviour {

    private GameManager gameManager;
	
    // Use this for initialization
	void Start () {

        gameManager = GameManager.instance;
	}
    void Wake()
    {

        StartCoroutine(gameManager.ClassFade());
    }
	
	// Update is called once per frame
    void Update()
    {

	}
}
