using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartWaveButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI nextWaveTextTMP;
    public static bool CanRestart = false;
    public static RestartWaveButton instance;
    void Awake()
    {
        CanRestart = false;
        instance = this;
        button = GetComponent<Button>();
        nextWaveTextTMP = transform.Find("RestartWaveText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public static void GainRestartWave(){
        CanRestart = true;
        if (instance){
            instance.gameObject.SetActive(true);
            instance.nextWaveTextTMP.SetText("Restart Wave " + (EnemySpawner.wave));
        }
    }
    void Update()
    {
        if (!CanRestart){
            instance.gameObject.SetActive(false);
        }
        if (EnemySpawner.waveInProgress){
            button.interactable = false;
        } else if (!button.interactable){
            nextWaveTextTMP.SetText("Restart Wave " + (EnemySpawner.wave));
            button.interactable = true;
        }
    }   
}
