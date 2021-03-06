﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public AudioClip[] BGMs;
    public bool playMusic;
    public float musicVolume;
    private bool fadeOut;
    public AudioClip[] OtherSongs;
    public AudioClip MidnightSound;
    public AudioClip Tock;

    public AudioSource currentSong;

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

        playMusic = false;
        mapSong = new Dictionary<string, AudioClip>()
	    {
            { "BadThingMusic", OtherSongs[0]},
            { "WorldMapMidnight", MidnightSound},
			{ "EndingMusic", OtherSongs[1]},
            { "Tock", Tock}
        };

        backgroundSounds = new List<AudioSource>();

        currentSong = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator LoadSceneMusic()
    {
        if (currentSong.isPlaying || GameManager.instance.GetTimeAsInt() >= 22)
        {
            yield break;
        }
        playMusic = true;
        while (playMusic)
        {
            if (!currentSong.isPlaying)
            {
                int x = Random.Range(0, BGMs.Length);
                while(currentSong.clip == BGMs[x])
                {
                    x = Random.Range(0, BGMs.Length);
                }
                currentSong.clip = BGMs[x];
                currentSong.volume = 0;
                StartCoroutine(FadeInAudioSource(currentSong));
                currentSong.Play();
            }
            if ((currentSong.clip.length - currentSong.time) < 10f && !fadeOut)
            {
                StartCoroutine(FadeOutAudioSource(currentSong));
            }
            yield return null;
        }
    }

    public void PlayOtherSong(string name)
    {
        if (currentSong.clip == mapSong[name])
        {
            return;
        }
        if (currentSong.isPlaying)
        {
            playMusic = false;
        }
        currentSong.clip = mapSong[name];
        currentSong.volume = 0;
        StartCoroutine(FadeInAudioSource(currentSong));
        currentSong.Play();
    }

    public void LoadSceneSound(string mapName, float volume, bool loop = false)
    {
        bool isplaying = false;
        for (int i = 0; i < backgroundSounds.Count; i++)
        {
            if (backgroundSounds[i].clip == mapSong[mapName] || backgroundSounds[i].clip == Resources.Load<AudioClip>(mapName))
            {
                isplaying = true;
            }
        }
        if (!isplaying)
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
            tempsource.loop = loop;
            tempsource.Play();
            backgroundSounds.Add(tempsource);
        }
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

    public IEnumerator ResumeSceneMusic()
    {
        if (currentSong.isPlaying)
        {
            yield return StartCoroutine(FadeOutAudioSource(currentSong));
        }
        StartCoroutine(LoadSceneMusic());
    }

    public IEnumerator FadeInAudioSource(AudioSource source)
    {
        while (source.volume < musicVolume)
        {
            if (fadeOut)
            {
                yield break;
            }
            yield return new WaitForSeconds(.05f);
            source.volume += .01f;
            yield return null;
        }
    }
    public IEnumerator FadeOutAudioSource(AudioSource source, bool stopMusicLoop = false, float rate=0.01f)
    {
        if (source.volume > 0)
        {
            fadeOut = true;
        }
        while (source.volume > 0)
        {
            yield return new WaitForSeconds(.05f);
			source.volume -= rate;
            if (source.volume <= 0)
            {
                if (stopMusicLoop)
                {
                    playMusic = false;
                }
                fadeOut = false;
                source.Stop();
                yield break;
            }
            yield return null;
        }
    }
}
