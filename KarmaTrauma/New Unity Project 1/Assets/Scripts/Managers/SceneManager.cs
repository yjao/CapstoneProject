using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

	private GameManager gameManager;

	#region SCENE CONSTANTS

	public const string SCENE_HOUSE = "G_House";
	public const string SCENE_CLASS = "G_Class";
	public const string SCENE_MAINSTREET = "G_MainStreetSmall";
	public const string SCENE_MALL = "G_Mall";
	public const string SCENE_HOSPITAL = "G_MentalHospital";
	public const string SCENE_PARK = "G_Park";
    public const string SCENE_WORLDMAP = "WorldMapFallDemo";

	#endregion

    Dictionary<string, bool> outdoors;

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
            {SCENE_WORLDMAP, true}
        };
        //gameManager.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(false);
	}

    public void LoadScene(string name=null)
    {
		if (name != null)
		{
			Application.LoadLevel(name);
            gameManager.transform.GetComponentInChildren<Menu_Layout>().Fast_Forward_Label(false);
		}

        if (name == SCENE_WORLDMAP)
        {
            gameManager.transform.GetComponentInChildren<Menu_Layout>().Fast_Forward_Label(true);
        }
        StartCoroutine("LoadSceneCoroutine");
        //SoundManager.instance.LoadSceneMusic(name);
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
        }
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;
		GameObject interactableList = GameObject.Find("InteractableList");
		if (interactableList!= null)
        {
			foreach (Transform child in interactableList.transform)
            {
                bool isActive = false;
                InteractableObject IO = child.GetComponent<InteractableObject>();
				if (IO == null)
				{
					break;
				}
                if (IO.interactionType == InteractableObject.InteractionType.ITEM)
                {
                    if (gameManager.dayData.DataDictionary.ContainsKey(gameManager.allItems[IO.iD].Name))
                    {
                        if (!gameManager.dayData.DataDictionary[gameManager.allItems[IO.iD].Name])
                        {
                            isActive = true;
                            break;
                        }
                    }
                    else
                    {
                        isActive = true;
                        break;
                    }
                }
                else if (gameManager.dayData.DataDictionary.ContainsKey(IO.activeBool))
                {
                    if (gameManager.dayData.DataDictionary[IO.activeBool])
                    {
                        isActive = true;
                        break;
                    }
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

                    // Check GameManager's sceneParameters
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
                child.gameObject.SetActive(isActive);
            }
        }
        tint_screen(Application.loadedLevelName, gameManager.GetTimeAsInt());
        yield break;
    }

    private void tint_screen(string scene, int time)
    {
        if (outdoors.ContainsKey(scene))
        {
            if (time == 18)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = new Color(255f / 255f, 153f / 255f, 0f, 50f / 255f);
            }
            else if (time == 20)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = new Color(107f / 255f, 107f / 255f, 107f / 255f, 100f / 255f);
            }
            else if (time == 22)
            {
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").gameObject.SetActive(true);
                GameManager.instance.transform.Find("Menu_layout").transform.Find("Time_Tint").GetComponent<Image>().color = new Color(107f / 255f, 107f / 255f, 107f / 255f, 150f / 255f);
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
}