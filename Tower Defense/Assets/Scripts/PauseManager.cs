using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] 
    private GameObject pausePanel = null;
    public void Pause(){
        paused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
    }
    public void Unpause(){
        paused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void TogglePause(){
        if (paused){
            Unpause();
        } else {
            Pause();
        }
    }
    private void Awake() {
        Unpause();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }
}
