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
		}
        StartCoroutine("LoadSceneCoroutine");
    }

    private void LoadParameters(InteractableObject IO, InteractableObject.Parameters parameter)
    {
        //InteractableObject io = npc.interactableObject.GetComponent<InteractableObject>();
        IO.dialogueIDType = parameter.dialogueIDType;
        IO.dialogueIDSingle = parameter.dialogueIDSingle;
        IO.dialogueIDMin = parameter.dialogueIDMin;
        IO.dialogueIDMax = parameter.dialogueIDMax;
        IO.dialogueIDMulti = parameter.dialogueIDMulti;
        if (IO.transform.parent.GetComponent<NPC>() != null)
        {
            NPC npc = IO.transform.parent.GetComponent<NPC>();
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
        if (GameObject.Find("InteractableList") != null)
        {
            foreach (Transform child in GameObject.Find("InteractableList").transform)
            {
                bool isActive = false;
                InteractableObject IO;
                if (child.childCount > 0)
                {
                    IO = child.GetComponentInChildren<InteractableObject>();
                }
                else
                {
                    IO = child.GetComponent<InteractableObject>();
                }
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

                child.gameObject.SetActive(isActive);
            }
        }
        yield break;
    }
}
