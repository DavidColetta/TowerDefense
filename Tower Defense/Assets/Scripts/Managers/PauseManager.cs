using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] 
    private GameObject winPanel = null;
    private static PauseManager instance;
    [SerializeField] private AudioMixerSnapshot Default = null;
    [SerializeField] private AudioMixerSnapshot DampenedMusic = null;
    public static bool won = false;
    public static void Pause_Static(){
        instance.Pause();
    }
    public void Pause(){
        paused = true;
        Time.timeScale = 0;
        DampenedMusic.TransitionTo(0);
        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
        if (GameOver.isGameOver){
            settingsPanel.SetActive(false);
            winPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            TextMeshProUGUI GameOverWaveText = gameOverPanel.transform.Find("GameOverWaveText").gameObject.GetComponent<TextMeshProUGUI>();
            GameOverWaveText.text = "Wave: "+EnemySpawner.wave;
        } else if (won){
            gameOverPanel.SetActive(false);
            winPanel.SetActive(true);
            settingsPanel.SetActive(false);
        } 
        else {
            gameOverPanel.SetActive(false);
            winPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }
    public void Unpause(bool dontUndampenAudio = false){
        paused = false;
        if (FastForwardButton.speedUp)
            Time.timeScale = FastForwardButton.fastForwardSpeed;
        else 
            Time.timeScale = 1;
        if (!dontUndampenAudio)
            Default.TransitionTo(0.2f);
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
        won = false;
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
    public void DisplaySettings(){
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
