using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] 
    private GameObject pausePanel = null;
    [SerializeField] 
    private GameObject settingsPanel = null;
    [SerializeField] 
    private GameObject gameOverPanel = null;
    private static PauseManager instance;
    public static void Pause_Static(){
        instance.Pause();
    }
    public void Pause(){
        paused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
        if (GameOver.isGameOver){
            settingsPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            TextMeshProUGUI GameOverWaveText = gameOverPanel.transform.Find("GameOverWaveText").gameObject.GetComponent<TextMeshProUGUI>();
            GameOverWaveText.text = "Wave: "+EnemySpawner.wave;

        } else {
            gameOverPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
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
        if (!instance){
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        Unpause();
    }
    void Update()
    {
        if (!GameOver.isGameOver){
            if (Input.GetKeyDown(KeyCode.Escape)){
                TogglePause();
            }
        }
    }
}
