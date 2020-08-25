using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string music = "";
    private void Start() {
        if (music != "")
            AudioManager.ChangeMusicPlaying(music);
    }
    public void GoToScene(int scene){
        SceneChanger.GoToScene_Static(scene);
    }
}
