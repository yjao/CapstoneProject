using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public static SceneManager Instance;
	// Use this for initialization
	void Start () {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
        StartCoroutine("LoadSceneCoroutine");
    }

    private void loadParameters(InteractableObject IO, InteractableObject.Parameters parameter)
    {
        //InteractableObject io = npc.interactableObject.GetComponent<InteractableObject>();
        IO.DialogueIDType = parameter.DialogueIDType;
        IO.DialogueIDSingle = parameter.DialogueIDSingle;
        IO.DialogueIDMin = parameter.DialogueIDMin;
        IO.DialogueIDMax = parameter.DialogueIDMax;
        IO.DialogueIDMulti = parameter.DialogueIDMulti;
        if (IO.transform.parent.GetComponent<NPC>() != null)
        {
            NPC npc = IO.transform.parent.GetComponent<NPC>();
            npc.characterAnimations.AnimationState = parameter.StartingAnimationState;
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
                foreach (InteractableObject.Parameters p in IO.Parameter)
                {
                    if (p.Time.Contains(GameManager.Instance.GetTimeAsInt()))
                    {
                        isActive = true;
                        loadParameters(IO, p);
                        break;
                    }
                }
                child.gameObject.SetActive(isActive);
            }
        }
        yield break;
    }
}
