using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    static AudioManager instance;
    public static Sound music;
    void Awake()
    {
        if (!instance){
            instance = this;
        } else {
            Destroy(this);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
        }
    }

    public void Play(string name, bool CanOverlap = false){
        if (name == "")
            return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound "+name+" not found!");
            return;
        }
        s.source.volume = s.volume;
        s.source.pitch = s.pitch + UnityEngine.Random.Range(-s.pitchVariance,s.pitchVariance);
        if (CanOverlap){
            s.source.PlayOneShot(s.clip);
            return;
        }
        s.source.Play();
    }
    public static void Play_Static(string name, bool CanOverlap = false){
        instance.Play(name, CanOverlap);
    }
    public void StopPlaying(string name){
        if (name == "")
            return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound "+name+" not found!");
            return;
        }
        s.source.Stop();
    }
    public static void StopPlaying_Static(string name){
        instance.StopPlaying(name);
    }
    public static void ChangeMusicPlaying(string name){
        if (music != null)
            StopPlaying_Static(music.name);
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Music "+name+" not found!");
            return;
        }
        music = s;
        Play_Static(music.name);
    }
}
