using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume = 0.5f;
    [Range(0.2f,3f)]
    public float pitch = 1;
    [Range(0f,1f)]
    public float pitchVariance = 0.2f;
    public bool loop = false;
    public AudioMixerGroup output;


    [HideInInspector]
    public AudioSource source;
}
