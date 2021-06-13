using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] musics;
    public string startingMusic;
    private Sound music;
    private static AudioManager instance;
    public void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in musics)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
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
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        {
            Debug.LogWarning("could not find sound " + soundName + " it might be msising or mispeleld");
            return;
        }
        s.source.Play();
    }

    public void PlayMusic(string musicName, bool continuous) 
    {
        Sound s = Array.Find(musics, sound => sound.name == name);
        float currentTime = music.source.time;

        music.source.Stop();
        
        if (s == null)
        {
            Debug.LogWarning("could not find music" + music + " it might be msising or mispeleld");
            return;
        }

        music = s;
        s.source.Play();
        if (continuous) 
        {
            s.source.time = currentTime;
        }
    }



}
