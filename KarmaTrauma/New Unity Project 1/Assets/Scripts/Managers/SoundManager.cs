using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public AudioClip MainStreetMusic;
    public AudioClip MallMusic;
    public AudioClip MidnightSound;

    private AudioSource currentSong;

    public static SoundManager instance;

    private Dictionary<string, AudioClip> mapSong;
    public List<AudioSource> backgroundSounds;

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
            { "G_Mall", MallMusic},
            { "WorldMapMidnight", MidnightSound}
        };

        backgroundSounds = new List<AudioSource>();

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

    public void LoadSceneSound(string mapName, float volume, bool loop = false)
    {
        AudioSource tempsource = gameObject.AddComponent<AudioSource>();
        if (mapSong.ContainsKey(mapName))
        {
            tempsource.clip = mapSong[mapName];
        }
        else
        {
            tempsource.clip = Resources.Load<AudioClip>(mapName);
        }
        tempsource.volume = volume;
        tempsource.loop = true;
        tempsource.Play();
        backgroundSounds.Add(tempsource);
    }

    public void StopAllBackgroundSounds()
    {
        for (int i = 0; i < backgroundSounds.Count; i++)
        {
            GameObject.Destroy(backgroundSounds[i]);
        }
        backgroundSounds.Clear();
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
