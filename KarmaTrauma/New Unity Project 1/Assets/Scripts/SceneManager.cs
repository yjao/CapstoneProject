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

    private void loadParameters(NPC npc, NPC.NPCParameters parameter)
    {
        InteractableObject io = npc.interactableObject.GetComponent<InteractableObject>();
        io.DialogueIDType = parameter.DialogueIDType;
        io.DialogueIDSingle = parameter.DialogueIDSingle;
        io.DialogueIDMin = parameter.DialogueIDMin;
        io.DialogueIDMax = parameter.DialogueIDMax;
        io.DialogueIDMulti = parameter.DialogueIDMulti;
        //npc.characterAnimations.setAnimationOnLoad(parameter.StartingAnimationState);
        npc.wanderDistanceX = parameter.wanderDistanceX;
        npc.wanderDirectionX = parameter.wanderDirectionX;
        npc.wanderDistanceY = parameter.wanderDistanceY;
        npc.wanderDirectionY = parameter.wanderDirectionY;
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        if (GameObject.Find("NPCList") != null)
        {
            foreach (Transform child in GameObject.Find("NPCList").transform)
            {
                bool isActive = false;
                foreach (NPC.NPCParameters p in child.GetComponent<NPC>().NPCParameter)
                {
                    if (p.Time.Contains(GameManager.Instance.GetTimeAsInt()))
                    {
                        isActive = true;
                        loadParameters(child.GetComponent<NPC>(), p);
                        break;
                    }
                }
                /*
                for (int times = 0; times < child.GetComponent<NPC>().time.Length; times++)
                {
                    Debug.Log(times);
                    if (child.GetComponent<NPC>().time[times] == GameManager.Instance.GetTimeAsInt())
                    {
                        isActive = true;
                        break;
                    }
                }*/
                child.gameObject.SetActive(isActive);
            }
        }
        yield break;
    }
}
