using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string music = null;
    [SerializeField] private AudioMixerSnapshot Default = null;
    [SerializeField] private AudioMixerSnapshot NoMusic = null;
    private void Start() {
        if (music != "")
            Default.TransitionTo(0.6f);
            AudioManager.ChangeMusicPlaying(music);
    }
    public void GoToScene(int scene){
        NoMusic.TransitionTo(0.3f);
        SceneChanger.GoToScene_Static(scene);
    }
}
