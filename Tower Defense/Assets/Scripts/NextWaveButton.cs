using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveButton : MonoBehaviour
{
    public EnemySpawner enemySpawner;
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
        if (!enemySpawner.waveInProgress && !button.interactable){
            button.interactable = true;
            nextWaveTextTMP.SetText("Wave " + (enemySpawner.wave + 1));
        }
    }   
}
