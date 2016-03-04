using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public string startScene;

    void Start()
    {
        //quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        //exitText = exitText.GetComponent<Button>();
    //    quitMenu.enabled = false;
        DontDestroyOnLoad(this);
		Cursor.visible = false;
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			MainGameDemo();
		}
	}

    public void ExitPress() //this function will be used on our Exit button
    {
      //  quitMenu.enabled = true; //enable the Quit menu when we click the Exit button
        startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
        //exitText.enabled = false;

    }

    public void NoPress() //this function will be used for our "NO" button in our Quit Menu
    {
    //    quitMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
        startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
      //  exitText.enabled = true;

    }

    public void StartLevel() //this function will be used on our Play button
    {
        Application.LoadLevel("prologue"); //this will load our first level from our build settings. "1" is the second scene in our game

    }
    public void LoadLevel()
    {
        // This will require some serious coding 

    }

    public void ShortPrologueDemo()
    {
        Application.LoadLevel("ShortPrologue");
    }

    public void MainGameDemo()
    {
        //StartCoroutine("LoadDemoScene");
		StartCoroutine("ExitPrologue");
    }

    IEnumerator LoadDemoScene()
    {
		Debug.Log ("You're not supposed to be here.");
        Application.LoadLevel(SceneManager.SCENE_MAINSTREET);
        yield return null;
		GameManager.instance.SetTime(GameManager.TimeType.SET, 20);
		yield return null;
		SceneManager.instance.LoadScene();
		yield return null;
        Destroy(this);
    }

	IEnumerator ExitPrologue()
	{
        string sceneName = SceneManager.SCENE_MAINSTREET;
        if (startScene != null)
        {
            sceneName = startScene;
        }
		Application.LoadLevel(sceneName);
		yield return null;

		GameManager.instance.SetTime(GameManager.TimeType.SET, 20);
		yield return null;

		SceneManager.instance.LoadScene(sceneName);
		yield return null;

		Destroy(this);
		yield break;
	}

    public void ExitGame() //This function will be used on our "Yes" button in our Quit menu
    {
        Application.Quit(); //this will quit our game. Note this will only work after building the game
    }

}