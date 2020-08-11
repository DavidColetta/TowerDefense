using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPanelTween : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 defaultScale;
    [SerializeField]
    Vector2 disabledScale = new Vector2(0f,0f);
    [SerializeField]
    float tweenDuration = 0.2f;
    int currentTweenID;
    bool Enabled;
    private void Awake() {
        rectTransform = gameObject.GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
    }
    public void Enable() {
        if (!Enabled){
            gameObject.SetActive(true);
            rectTransform.localScale = disabledScale;
            LeanTween.cancel(currentTweenID);
            Enabled = true;
            currentTweenID = LeanTween.scale(rectTransform, defaultScale, tweenDuration).id;
        }
    }
    public void Disable(){
        if (Enabled){
            Enabled = false;
            currentTweenID = LeanTween.scale(rectTransform, disabledScale, tweenDuration).setOnComplete(ActuallyDisable).id;
        }
    }
    private void ActuallyDisable(){
        LeanTween.cancel(currentTweenID);
        gameObject.SetActive(false);
    }
}
