using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Menu_Layout : MonoBehaviour
{
    GameManager gameManager;
    QuestLog quest_log;
    Menu gameMenu;
    Player player;
    private const string SCENE_WORLD_MAP = "G_WorldMap";
    private const string SCENE_MAIN_MENU = "KarmaTrauma";

	public GameObject mapPanel;
	public GameObject mapText;
	public GameObject bagPanel;
	public GameObject bagText;
	public GameObject memoryPanel;
	public GameObject memoryText;
	public GameObject fastForwardPanel;
    public GameObject fastForwardPicture;
	public GameObject clockPanel;
	public GameObject clockText;
	public GameObject timeTint;

    private bool timeButton_show = false;
    private bool invenButton_show = false;
    private bool diaryButton_show = false;
    private bool open_menu = false;
    public int startTime;

    private AudioClip bagSound;
    private AudioClip logSound;
    private AudioClip logSoundClose;
    private AudioSource source;

    private int world_map_level = 17;
    private Texture2D inventory_texture;
    private Texture2D diary_texture;
    

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.instance;
        quest_log = QuestLog.instance;
        gameMenu = Menu.Instance;
        bagSound = Resources.Load<AudioClip>("Zipper1");
        logSound = Resources.Load<AudioClip>("MemoryLog");
        logSoundClose = Resources.Load<AudioClip>("MemoryLogReverse");
        source = gameObject.GetComponent<AudioSource>();
        //gameMenu = FindObjectOfType(typeof(Menu)) as Menu;
        //quest_log = QuestLog.Instance; 
        player = FindObjectOfType(typeof(Player)) as Player;
        ////show_button = gameMenu.In_Mode();
        //inventory_texture = Resources.Load("Bag") as Texture2D;
        //inventory_content.inventory_texture = inventory_texture;
        if (startTime != 0)
        {
            gameManager.SetTime(GameManager.TimeType.SET, startTime);
        }

        transform.FindChild("QuestBook").gameObject.SetActive(false);
        transform.FindChild("Inventory").gameObject.SetActive(false);
    }

    void M_Clock()
    {
        //Debug.Log(transform.FindChild("Clock_display").GetComponent<Text>().text);
        transform.FindChild("Clock_display").GetComponent<Text>().text = gameManager.GetTime();
    }

    public void M_Fastforward()
    {
        if (gameManager.GetTimeAsInt() < 22)
        {
            gameManager.SetTime(GameManager.TimeType.INCREASE);
            if (gameManager.GetTimeAsInt() >= 20)
            {
                SoundManager.instance.PlayOtherSong("BadThingMusic");
            }
            SceneManager.instance.tint_screen(Application.loadedLevelName, gameManager.GetTimeAsInt());
        }
    }

    public void M_Bag()
    {
        //player.InvenButton();
        transform.FindChild("Inventory").gameObject.SetActive(true);
        invenButton_show = true;
        open_menu = true;
        gameManager.Wait();
        gameMenu.display();
        gameMenu.DrawSelect();
    }


    public void Close_Bag()
    {
        transform.FindChild("Inventory").gameObject.SetActive(false);
        invenButton_show = false;
        open_menu = false;
        gameManager.Play();
    }

    public void M_Diary()
    {
        //player.QuestButton();
        transform.FindChild("QuestBook").gameObject.SetActive(true);
        diaryButton_show = true;
        open_menu = true;
        gameManager.Wait();
        quest_log.display() ;
        quest_log.DrawSelect();
        //gameManager.gameMode = GameManager.GameMode.LOG;
        //gameManager.prevMode = GameManager.GameMode.PLAYING;
    }

    public void Close_Diary()
    {
        transform.FindChild("QuestBook").gameObject.SetActive(false);
        diaryButton_show = false;
        open_menu = false;
        gameManager.Play();
    }

    public void Fast_Forward_Label(bool setToTrue)
    {
        if (setToTrue)
        {
            transform.FindChild("Fast_forward").gameObject.SetActive(true);
            transform.FindChild("Fast_forward_background").gameObject.SetActive(true);
        }
        else
        {
            transform.FindChild("Fast_forward").gameObject.SetActive(false);
            transform.FindChild("Fast_forward_background").gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update() {
        M_Clock();
        if (!open_menu && !gameManager.has_text_box && gameManager.gameMode != GameManager.GameMode.NONE)
        {
            if (Input.GetKeyDown(KeyCode.B) && Application.loadedLevelName != SCENE_MAIN_MENU)
            {
                source.clip = bagSound;
                source.Play();
                M_Bag();

            }

            else if (Input.GetKeyDown(KeyCode.M) && Application.loadedLevelName != SCENE_MAIN_MENU)
            {
                source.clip = logSound;
                source.Play();
                M_Diary();

            }
            else if (Input.GetKeyDown(KeyCode.F) && (Application.loadedLevelName == SCENE_WORLD_MAP))
            {
                M_Fastforward();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (invenButton_show)
            {
                source.clip = bagSound;
                source.Play();
                Close_Bag();
            }
            if (diaryButton_show)
            {
                source.clip = logSoundClose;
                source.Play();
                Close_Diary();
            }
        }

        else if (invenButton_show && Input.GetKeyDown(KeyCode.B))
        {
            source.clip = bagSound;
            source.Play();
            Close_Bag();
        }
        else if (diaryButton_show && Input.GetKeyDown(KeyCode.M)){
            source.clip = logSoundClose;
            source.Play();
            Close_Diary();
        }

    
	}

	#region Getters & Setters

	public bool GetMemoryLogOpen()
	{
		return diaryButton_show;
	}

	public void GameMenus(bool setActive)
	{
		mapPanel.SetActive(setActive);
		mapText.SetActive(setActive);
		bagPanel.SetActive(setActive);
		bagText.SetActive(setActive);
		memoryPanel.SetActive(setActive);
		memoryText.SetActive(setActive);
		clockPanel.SetActive(setActive);
		clockText.SetActive(setActive);
	}

	#endregion

}
