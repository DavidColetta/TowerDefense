using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonTween : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private float rightPosition = -32;
    [SerializeField] private float leftPosition = -132;
    [SerializeField] private float tweenDuration = 0.2f;
    bool OnRight = false;
    private void Awake() {
        rectTransform = gameObject.GetComponent<RectTransform>();

        if (OnRight){
            LeanTween.moveX(rectTransform,rightPosition,0);
        } else {
            LeanTween.moveX(rectTransform,leftPosition,0);
        }
    }
    public void MoveRight(){
        if (!OnRight){
            LeanTween.moveX(rectTransform,rightPosition,tweenDuration).setIgnoreTimeScale(true);
            OnRight = true;
        }
    }
    public void MoveLeft(){
        if (OnRight){
            LeanTween.moveX(rectTransform,leftPosition,tweenDuration).setIgnoreTimeScale(true);
            OnRight = false;
        }
    }
    public void TogglePos(){
        if (OnRight){
            MoveLeft();
        } else {
            MoveRight();
        }
    }
}
