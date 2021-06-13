using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] musics;
    public string startingMusic;
    private Sound music;
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.playOnAwake = false;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in musics)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.playOnAwake = false;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        //PlayMusic(startingMusic, false);
    }


    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) 
        {
            Debug.LogWarning("could not find sound " + soundName + " it might be msising or mispeleld");
            return;
        }
        s.source.Play();
    }

    public void PlayMusic(string musicName, bool continuous) 
    {
        Sound s = Array.Find(musics, sound => sound.name == musicName);
        float currentTime = 0.0f;
        if (continuous)
        {
            StartCoroutine(FadeMusicTracks(music, s));
        }
        

        
        
        if (s == null)
        {
            Debug.LogWarning("could not find music" + music + " it might be msising or mispeleld");
            return;
        }

        music = s;
        Debug.Log("PLAYING " + s.source.name);
        
        if (!continuous) 
        {
            s.source.Play();
            s.source.time = currentTime;
        }
    }

    IEnumerator FadeMusicTracks(Sound leaving, Sound entering)
    {
        
        entering.source.volume = 0.0f;
        entering.source.Play();
        entering.source.time = leaving.source.time;
        for (float i = 0; i < leaving.source.volume; i += 0.1f)
        {
            leaving.source.volume -= 0.1f;
            entering.source.volume += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    



}
