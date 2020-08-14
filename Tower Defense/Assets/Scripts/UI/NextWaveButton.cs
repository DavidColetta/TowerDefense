using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI nextWaveTextTMP;
    void Start()
    {
        button = GetComponent<Button>();
        nextWaveTextTMP = transform.Find("NextWaveText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.waveInProgress){
            button.interactable = false;
        } else if (!button.interactable){
            button.interactable = true;
            nextWaveTextTMP.SetText("Wave " + (EnemySpawner.wave + 1));
        }
    }   
}
