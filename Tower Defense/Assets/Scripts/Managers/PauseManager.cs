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
    private static PauseManager instance;
    [SerializeField] private AudioMixerSnapshot Default = null;
    [SerializeField] private AudioMixerSnapshot DampenedMusic = null;
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
            gameOverPanel.SetActive(true);
            TextMeshProUGUI GameOverWaveText = gameOverPanel.transform.Find("GameOverWaveText").gameObject.GetComponent<TextMeshProUGUI>();
            GameOverWaveText.text = "Wave: "+EnemySpawner.wave;

        } else {
            gameOverPanel.SetActive(false);
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
