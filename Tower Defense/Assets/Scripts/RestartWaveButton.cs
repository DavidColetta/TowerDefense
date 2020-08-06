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
    void Start()
    {
        button = GetComponent<Button>();
        nextWaveTextTMP = transform.Find("RestartWaveText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.waveInProgress || !CanRestart){
            button.interactable = false;
        }
        if (!EnemySpawner.waveInProgress && !button.interactable && CanRestart){
            button.interactable = true;
            nextWaveTextTMP.SetText("Restart Wave " + (EnemySpawner.wave));
        }
    }   
}
