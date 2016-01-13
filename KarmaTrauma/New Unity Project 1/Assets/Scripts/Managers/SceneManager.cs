using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        SoundManager.instance.LoadSceneMusic(name);
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
        yield break;
    }
}
