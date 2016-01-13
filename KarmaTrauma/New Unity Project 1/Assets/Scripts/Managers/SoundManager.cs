using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public AudioClip MainStreetMusic;
    public AudioClip MallMusic;

    private AudioSource currentSong;

    public static SoundManager instance;

    private Dictionary<string, AudioClip> mapSong;

	// Use this for initialization
	void Start () {
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        mapSong = new Dictionary<string, AudioClip>()
	    {
			{ "G_MainStreetSmall", MainStreetMusic},
            { "G_Mall", MallMusic}
        };

        currentSong = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void LoadSceneMusic(string mapName)
    {
        if (currentSong.isPlaying)
        {
            currentSong.Stop();
        }
        currentSong.clip = mapSong[mapName];
        currentSong.loop = true;
        currentSong.Play();
    }

    public void PauseSceneMusic()
    {
        if (currentSong.isPlaying)
        {
            currentSong.Pause();
        }
    }

    public void ResumeSceneMusic()
    {
        if (!currentSong.isPlaying)
        {
            currentSong.Play();
        }
    }
}
