using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

	private GameManager gameManager;

    private string prev_map;

    private string current_map;

	#region SCENE CONSTANTS

	public const string SCENE_TITLESCREEN = "KarmaTrauma";
	public const string SCENE_CREDITS = "E_Credits";
	public const string SCENE_TUTORIAL = "T_Main";

	public const string SCENE_HOUSE = "G_House";
	public const string SCENE_CLASS = "G_Class";
	public const string SCENE_MAINSTREET = "G_MainStreet";
	public const string SCENE_MALL = "G_Mall";
	public const string SCENE_HOSPITAL = "G_Hospital";
	public const string SCENE_PARK = "G_Park";
	public const string SCENE_POLICE = "G_PoliceStation";
	public const string SCENE_APARTMENT = "G_Apartment";
	public const string SCENE_WORLDMAP = "G_WorldMap";

	#endregion

    #region COLOR CONSTANTS

    Color sunset = new Color(255f / 255f, 153f / 255f, 0f, 50f / 255f);
    Color night = new Color(138f / 255f, 138f / 255f, 203f / 255f, 100f / 255f);
    Color late_night = new Color(38f / 255f, 38f / 255f, 137f/255f, 100f / 255f);

    #endregion

    Dictionary<string, bool> outdoors;
    Dictionary<string, string> scene_display_names;

	void Start()
	{
		gameManager = GameManager.instance;


        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        outdoors = new Dictionary<string, bool>()
        {
            {SCENE_MAINSTREET, true},
            {SCENE_PARK, true},
            {SCENE_WORLDMAP, true},
            {"T_MainStreet", true}
        };

        scene_display_names = new Dictionary<string, string>()
        {
            {SCENE_HOUSE, "Home"},
	        {SCENE_CLASS, "School"},
	        {SCENE_MAINSTREET, "Main Street"},
	        {SCENE_MALL, "The Donut Hole"},
	        {SCENE_HOSPITAL, "Punxsu Hospital"},
            {SCENE_PARK, "Punxsu Park"},
            {SCENE_POLICE, "Police Station"},
            {SCENE_WORLDMAP, "World Map"},
            {"T_Class", "Classroom"},
            {"T_MainStreet", "Main Street"},
			{SCENE_CREDITS, "Meet the Team!"},
			{SCENE_TITLESCREEN, ""},
			{SCENE_TUTORIAL, ""}
        };
        //gameManager.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
	}
    public string SceneName(string name)
    {
        return scene_display_names[name];
    }
    public void LoadScene(string name=null)
    {
        prev_map = current_map;
        current_map = name;
        StartCoroutine(LoadSceneCoroutine());
        //SoundManager.instance.LoadSceneMusic(name);
    }

    public void LoadEnding()
    {
        Application.LoadLevel("Outcome Screen");
    }

    private void LoadParameters(InteractableObject IO, InteractableObject.Parameters parameter)
    {
        //InteractableObject io = npc.interactableObject.GetComponent<InteractableObject>();
        IO.dialogueIDType = parameter.dialogueIDType;
        IO.dialogueIDSingle = parameter.dialogueIDSingle;
        IO.dialogueIDMin = parameter.dialogueIDMin;
        IO.dialogueIDMax = parameter.dialogueIDMax;
        IO.dialogueIDMulti = parameter.dialogueIDMulti;
		NPC npc = IO.transform.GetComponent<NPC>();
        if (npc != null)
        {
            npc.characterAnimations.AnimationState = parameter.startingAnimationState;
			npc.characterAnimations.animationSpeed = parameter.animationSpeed;
			npc.wanderDistanceX = parameter.wanderDistanceX;
            npc.wanderDirectionX = parameter.wanderDirectionX;
            npc.wanderDistanceY = parameter.wanderDistanceY;
            npc.wanderDirectionY = parameter.wanderDirectionY;
            if (!parameter.turnOnInteract)
            {
                npc.characterAnimations.facePlayerOnInteraction = false;
            }
        }
    }

    public IEnumerator LoadSceneCoroutine(string mapName=null)
    {
		if (mapName != null)
		{
			current_map = mapName;
		}

        yield return null;
        yield return StartCoroutine(fade_black());
        if (current_map != null)
        {
            Application.LoadLevel(current_map);
            gameManager.transform.Find("Menu_layout/MapName").GetComponent<Text>().text = scene_display_names[current_map];
            gameManager.transform.GetComponentInChildren<Menu_Layout>().Fast_Forward_Label(false);
        }

        yield return null;
        yield return StartCoroutine(map_name());
		GameObject interactableList = GameObject.Find("InteractableList");
		if (interactableList!= null)
        {
			foreach (Transform child in interactableList.transform)
            {
                bool isActive = false;
                InteractableObject IO = child.GetComponent<InteractableObject>();
				if (IO == null)
				{
					continue;
				}
				if (gameManager.GetData(IO.disableBool))
                {
                    isActive = false;
                }
                else
                {
                    foreach (InteractableObject.Parameters p in IO.parameter)
                    {
                        if (p.timeBlocks.Contains(GameManager.instance.GetTimeAsInt()))
                        {
                            isActive = true;
                            LoadParameters(IO, p);
                            break;
                        }
                    }

					// Check GM's quest bool scene params first, in prioritized order
					bool paramsAlreadySet = false;
					for (int i = 0; i < gameManager.boolSceneParameters.Count; i++)
					{
						if (GameManager.instance.dayData.GetBool(gameManager.boolSceneParameters[i].boolName))
						{
							InteractableObject.Parameters p = gameManager.boolSceneParameters[i].GetParamData(Application.loadedLevelName, IO.iD, GameManager.instance.GetTimeAsInt());
							if (p != null)
							{
								paramsAlreadySet = true;
								isActive = true;
								LoadParameters(IO, p);
								break;
							}
						}
					}

                    // Check GameManager's sceneParameters
					if (!paramsAlreadySet)
					{
	                    if (gameManager.sceneParameters.ContainsKey(Application.loadedLevelName) != false)
	                    {
	                        List<InteractableObject.Parameters> list = gameManager.sceneParameters[Application.loadedLevelName];
	                        foreach (InteractableObject.Parameters p in list)
	                        {
	                            if (IO.iD == p.NpcID)
	                            {
	                                if (p.timeBlocks.Contains(GameManager.instance.GetTimeAsInt()))
	                                {
	                                    isActive = true;
	                                    LoadParameters(IO, p);
	                                    break;
	                                }
	                            }
	                        }
	                    }
                	}
				}
                child.gameObject.SetActive(isActive);
            }
        }

        // This if block was below "if (current_map != null)" block but moved here for player's world map position.
        if (current_map == SCENE_WORLDMAP)
        {
			GameObject.Find("WorldMap").GetComponent<WorldMapManager>().LoadMapInfo(prev_map);
        }

        tint_screen(Application.loadedLevelName, gameManager.GetTimeAsInt());
        yield return StartCoroutine(fade_out());
		if (gameManager.GetTimeAsInt() >= 20 && 
		    !(GameManager.instance.dayData.DataDictionary.ContainsKey("AlfredSaved") && GameManager.instance.dayData.GetBool("AlfredSaved")))
        {
            if (SoundManager.instance.currentSong.clip != SoundManager.instance.OtherSongs[0])
            {
                yield return StartCoroutine(SoundManager.instance.FadeOutAudioSource(SoundManager.instance.currentSong, true, .03f));
            }
            SoundManager.instance.PlayOtherSong("BadThingMusic");
        }
        else
        {
            StartCoroutine(SoundManager.instance.LoadSceneMusic());
        }
        yield return null;
        yield break;
    }

    public void tint_screen(string scene, int time)
    {
        if (outdoors.ContainsKey(scene))
        {
            if (time == 18)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = sunset;
            }
            else if (time == 20)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = night;
            }
            else if (time == 22)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = late_night;
            }
            else
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
            }
        }
        else
        {
            GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
        }
    }

    public IEnumerator fade_black()
    {
        gameManager.prevMode = gameManager.gameMode;
        gameManager.gameMode = GameManager.GameMode.NONE;
        gameManager.transform.Find("Menu_layout").transform.Find("Fade_Panel").gameObject.SetActive(true);
        Image fade_panel = gameManager.transform.Find("Menu_layout").transform.Find("Fade_Panel").GetComponent<Image>();
        Color current_color = fade_panel.color;
        while (fade_panel.color.a <= 1f)
        {
            current_color.a += 5f/255f;
            fade_panel.color = current_color;
            yield return null;
        }
    }

    public IEnumerator fade_out()
    {
        Image fade_panel = gameManager.transform.Find("Menu_layout").transform.Find("Fade_Panel").GetComponent<Image>();
        Color current_color = fade_panel.color;
        while (fade_panel.color.a >= 0f)
        {
            current_color.a -= 5f / 255f;
            fade_panel.color = current_color;
            yield return null;
        }
        gameManager.transform.Find("Menu_layout").transform.Find("Fade_Panel").gameObject.SetActive(false);
        gameManager.prevMode = gameManager.gameMode;
        gameManager.gameMode = GameManager.GameMode.PLAYING;
    }

    public IEnumerator map_name(string day = null, float timer = 1f)
    {
        string current_string;
        if (day != null)
        {
            current_string = day;
        }
        else
        {
            current_string = scene_display_names[current_map];
        }
        GameObject name = new GameObject();
        name.AddComponent<CanvasRenderer>();
        name.AddComponent<Text>();
        Text t = name.transform.GetComponent<Text>();
        t.text = current_string;
        name.transform.SetParent(gameManager.transform.Find("Menu_layout"), false);
        t.rectTransform.anchorMin = new Vector2(0f, 0f);
        t.rectTransform.anchorMax = new Vector2(1f, 1f);
        t.alignment = TextAnchor.MiddleCenter;
        t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        t.resizeTextForBestFit = true;
        t.resizeTextMaxSize = 60;
        yield return new WaitForSeconds(timer);
        GameObject.Destroy(name);
    }

	public IEnumerator display_text(string text, float timer = 1f)
	{
		GameObject name = new GameObject();
		name.AddComponent<CanvasRenderer>();
		name.AddComponent<Text>();
		Text t = name.transform.GetComponent<Text>();
		t.text = text;
		name.transform.SetParent(gameManager.transform.Find("Menu_layout"), false);
		t.rectTransform.anchorMin = new Vector2(0f, 0f);
		t.rectTransform.anchorMax = new Vector2(1f, 1f);
		t.alignment = TextAnchor.MiddleCenter;
		t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		t.resizeTextForBestFit = true;
		t.resizeTextMaxSize = 45;
		yield return new WaitForSeconds(timer);
		GameObject.Destroy(name);
	}
}