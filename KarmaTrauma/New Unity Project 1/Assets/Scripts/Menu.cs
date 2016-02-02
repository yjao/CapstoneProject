using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    private GameManager gameManager;
    public static Menu Instance;

    int pointer;
    int pointer2;
    

    // Use this for initialization
    void Start()
    {
        pointer = 0;
        pointer2 = 0;
        gameManager = GameManager.instance;
        Inventory();
        DrawSelect();
        this.tag = "Menu";
        HideDescription();
        Instance = this;
    }

    void OnDestroy()
    {
        //EventManager.OnItemPickup -= ItemPickup;
    }

    void Inventory()
    {
        //transform.Find("Panel").gameObject.SetActive(false);
        //transform.Find("Text").gameObject.SetActive(false);
        //transform.Find("Pointer").gameObject.SetActive(false);
        transform.Find("ItemPanel0").gameObject.SetActive(true);

        
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!(x == 0 && y == 0))
                {
                   Transform z = (Instantiate(transform.Find("ItemPanel0"), new Vector3(0, 1, 0), Quaternion.identity)) as Transform;
                    z.transform.Translate(new Vector2(0.1f,0.2f));
                    z.transform.SetParent(transform, false);
                    z.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.3775f + .14f * x), (.95f - .25f * y));
                    z.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f + .14f * x), (.695f - .25f * y));
                    z.name = "ItemPanel" + x + y;
                    //Debug.Log("calling");
                }
            }
        }
    
        pointer = 0;
        pointer2 = 0;
        DrawSelect();
    }


    public void display()
    {
        for (int i = 0; i < gameManager.GetItemData().Length; ++i)
            if (gameManager.GetItemData()[i] != null)
            {
                int p = i % 3;
                int p1 = i / 3;
                DrawItem(gameManager.GetItemData()[i].Filename, p, p1);
            }
    }

    void DrawItem(string item, int p, int p1)
    {
        GameObject image = new GameObject(item);
        image.transform.SetParent(transform);
        image.AddComponent<RectTransform>();
        image.AddComponent<CanvasRenderer>();
        image.AddComponent<Image>();
        image.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(item);
        //image.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.154f + .14f * pointer), (.975f - .25f * pointer2));
        //image.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.014f + .14f * pointer), (.725f - .25f * pointer2));
        image.transform.GetComponent<RectTransform>().anchorMax = new Vector2((.3775f + .14f * p), (.95f - .25f * p1));
        image.transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f + .14f * p), (.695f - .25f * p1));
        image.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(.5f, .5f);
        image.transform.GetComponent<RectTransform>().localScale = new Vector2(.5f, .5f);
        image.transform.GetComponent<Image>().preserveAspect = true;
        //if (pointer2 < 2)
        //{
        //    if (pointer < 2)
        //        pointer += 1;
        //    else
        //    {
        //        pointer = 0;
        //        pointer2 += 1;
        //    }
        //}
    }

    void DrawSelect()
    {
        transform.Find("ItemSelect").gameObject.SetActive(true);
        transform.Find("ItemSelect").transform.GetComponent<RectTransform>().anchorMax = new Vector2((.3775f + .14f * pointer), (.95f - .25f * pointer2));
        transform.Find("ItemSelect").transform.GetComponent<RectTransform>().anchorMin = new Vector2((.234f + .14f * pointer), (.695f - .25f * pointer2));
        transform.Find("ItemSelect").SetAsLastSibling();
        if (gameManager.GetItemData()[pointer + pointer2 * 3] != null)
        {
            Item i = gameManager.GetItemData()[pointer + pointer2 * 3];
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



    // Update is called once per frame
    void Update()
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
    }

    
    //void OnClick()
    //{
    //    //Still need to fix the math...
    //       // Debug.Log(transform.Find("ItemPanel").position);
    //        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        float mousePos_x = Input.mousePosition.x/Screen.width;
    //        float mousePos_y = Input.mousePosition.y/Screen.height;
    //        Debug.Log(mousePos_x + ", " +mousePos_y);
    //        for (int x = 0; x < 3; x++)
    //        {
    //            for (int y = 0; y < 3; y++)
    //            {
    //                if (((.234f + .14f * x) < mousePos_x) && ((.95 - .25f * y) > mousePos_y))
    //                {
    //                    pointer = x;
    //                    pointer2 = y;
    //                    gameManager.ExitDialogue();
    //                    GameObject.Destroy(gameObject);
    //                    Debug.Log("working");
    //                    break;
    //                }
    //            }
    //        }
               
        
    //}
}
