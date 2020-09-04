using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FastForwardButton : MonoBehaviour
{
    public static bool speedUp = false;
    public const float fastForwardSpeed = 1.5f;
    private TextMeshProUGUI textTMP;
    private void Awake() {
        textTMP = transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        SpeedDown();
    }
    private void OnDestroy() {
        SpeedDown();
    }
    public void ToggleSpeedUp(){
        if (speedUp){
            SpeedDown();
        }else{
            SpeedUp();
        }
    }
    public void SpeedDown(){
        speedUp = false;
        Time.timeScale = 1;
        textTMP.text = ">>";
    }
    public void SpeedUp(){
        speedUp = true;
        Time.timeScale = fastForwardSpeed;
        textTMP.text = "<<";
    }
}
