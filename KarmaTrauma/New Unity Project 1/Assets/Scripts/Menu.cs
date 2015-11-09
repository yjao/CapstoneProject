using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    private GameManager gameManager;

    public enum Modes
    {
        MAIN, INVENTORY, LOG
    };

    Modes MenuMode = Modes.MAIN;
    int pointer;
    int pointer2;

    Item[] items;

	// Use this for initialization
	void Start () {
        items = new Item[9];
        items[0] = new Item("Momo", "The original soul of Chelsey", "sprite1");
        items[1] = new Item("Jewel", "Contains the soul of a demon. Or just a jewel for debugging", "jewel");
        items[2] = new Item("Bacon and Eggs", "Yum", "baconAndEggs");
        items[3] = new Item("Stairs", "How did you steal the stairs?!", "stairs");
        pointer = 0;
        pointer2 = 0;
        gameManager = GameManager.Instance;
        transform.Find("ItemPanel").gameObject.SetActive(false);
        transform.Find("ItemSelect").gameObject.SetActive(false);
        HideDescription();
	}
	
	// Update is called once per frame
	void Update () {
        if (MenuMode == Modes.MAIN)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (pointer > 0)
                    pointer -= 1;
                else if (pointer == 0)
                    pointer = 4;
                transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.75f, .86f - .055f * pointer);
                transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.79f, .92f - .055f * pointer);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (pointer < 4)
                    pointer += 1;
                else if (pointer == 4)
                    pointer = 0;
                transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMin = new Vector2(.75f, .86f - .055f * pointer);
                transform.Find("Pointer").transform.GetComponent<RectTransform>().anchorMax = new Vector2(.79f, .92f - .055f * pointer);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (pointer == 0)
                {
                    MenuMode = Modes.INVENTORY;
                    Inventory();
                }
                else if (pointer == 3)
                {
                    Application.LoadLevel(Application.loadedLevel);
                    gameManager.ExitDialogue();
                    GameObject.Destroy(gameObject);
                }
                else if (pointer == 4)
                {
                    gameManager.ExitDialogue();
                    GameObject.Destroy(gameObject);
                }
            }
        }
        else if (MenuMode == Modes.INVENTORY)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (pointer2 > 0)
                {
                    pointer2 -= 1;
                    DrawSelect();
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (pointer2 < 2)
                {
                    pointer2 += 1;
                    DrawSelect();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (pointer > 0)
                {
                    pointer -= 1;
                    DrawSelect();
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (pointer < 2)
                {
                    pointer += 1;
                    DrawSelect();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.ExitDialogue();
                GameObject.Destroy(gameObject);
            }
        }
	}

    void Inventory()
    {
        transform.Find("ItemPanel").gameObject.SetActive(true);
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Transform z = (Instantiate(transform.Find("ItemPanel"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
                    z.transform.SetParent(transform, false);
                    z.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.154f + .14f * x), (.975f - .25f * y));
                    z.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.014f + .14f * x), (.725f - .25f * y));
                }
            }
        }
        for (int i = 0; i < items.Length; ++i)
            if (items[i] != null)
                DrawItem(items[i].Filename);
        pointer = 0;
        pointer2 = 0;
        DrawSelect();
    }

    void DrawItem(string item)
    {
        GameObject image = new GameObject(item);
        image.transform.SetParent(transform);
        image.AddComponent<RectTransform>();
        image.AddComponent<CanvasRenderer>();
        image.AddComponent<Image>();
        image.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(item);
        image.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.154f + .14f * pointer), (.975f - .25f * pointer2));
        image.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.014f + .14f * pointer), (.725f - .25f * pointer2));
        image.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(.5f, .5f);
        image.transform.GetComponent<RectTransform>().localScale = new Vector2(.5f, .5f);
        image.transform.GetComponent<Image>().preserveAspect = true;
        if (pointer2 < 2)
        {
            if (pointer < 2)
                pointer += 1;
            else
            {
                pointer = 0;
                pointer2 += 1;
            }
        }
    }

    void DrawSelect()
    {
        transform.Find("ItemSelect").gameObject.SetActive(true);
        transform.Find("ItemSelect").transform.GetComponent<RectTransform>().anchorMax = new Vector2((.154f + .14f * pointer), (.975f - .25f * pointer2));
        transform.Find("ItemSelect").transform.GetComponent<RectTransform>().anchorMin = new Vector2((.014f + .14f * pointer), (.725f - .25f * pointer2));
        transform.Find("ItemSelect").SetAsLastSibling();
        if (items[pointer + pointer2 * 3] != null)
        {
            Item i = items[pointer + pointer2 * 3];
            DrawDescription(i.Name, i.Description);
        }
        else
            HideDescription();
    }

    void DrawDescription(string name, string description)
    {
        transform.Find("ItemTextPanel").gameObject.SetActive(true);
        transform.Find("ItemNamePanel").gameObject.SetActive(true);
        transform.Find("ItemText").gameObject.SetActive(true);
        transform.Find("ItemName").gameObject.SetActive(true);
        transform.Find("ItemText").transform.GetComponent<Text>().text = description;
        transform.Find("ItemName").transform.GetComponent<Text>().text = name;
    }

    void HideDescription()
    {
        transform.Find("ItemTextPanel").gameObject.SetActive(false);
        transform.Find("ItemNamePanel").gameObject.SetActive(false);
        transform.Find("ItemText").gameObject.SetActive(false);
        transform.Find("ItemName").gameObject.SetActive(false);
    }
}
