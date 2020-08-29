using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    RectTransform rectTransform;
    [SerializeField]
    float tweenDuration = 0.2f;
    [SerializeField]
    Vector2 defaultScale = new Vector2(1f,1f);
    [SerializeField]
    Vector2 hoverScale = new Vector2(1.2f, 1.2f);
    [SerializeField]
    bool tweenOnEnable = true;
    [SerializeField]
    float enableTweenDelay = 0f;
    [SerializeField]
    int preferedSiblingIndex = -1;
    private Button button;
    private bool isButton = false;
    protected virtual void Awake() {
        rectTransform = gameObject.GetComponent<RectTransform>();
        if (GetComponent<Button>() != null){
            button = GetComponent<Button>();
            isButton = true;
            button.onClick.AddListener(() => _MouseClick());
        } else {
            isButton = false;
        }
    }
    public void OnEnable() {
        if (tweenOnEnable){
            rectTransform.localScale = new Vector2(0f,0f);
            LeanTween.scale(rectTransform, defaultScale, tweenDuration).setIgnoreTimeScale(true).setDelay(enableTweenDelay);
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData) {
        if (isButton){
            if (button.interactable){
                if (preferedSiblingIndex >= 0)
                    transform.SetSiblingIndex(preferedSiblingIndex+1);
                LeanTween.scale(rectTransform, hoverScale, tweenDuration).setIgnoreTimeScale(true);
            }
        }
    }
    public virtual void OnPointerExit(PointerEventData eventData) {
        if ((Vector2)rectTransform.localScale != defaultScale){
            if (preferedSiblingIndex >= 0)
                transform.SetSiblingIndex(preferedSiblingIndex);
            LeanTween.scale(rectTransform, defaultScale, tweenDuration).setIgnoreTimeScale(true);
        }
    }
    public void _MouseClick(){
        AudioManager.Play_Static("Click");
    }
    private void Update() {
        if (isButton){
            if (!LeanTween.isTweening(gameObject)){
                if (!button.interactable){
                    if ((Vector2)rectTransform.localScale != defaultScale){
                        if (preferedSiblingIndex >= 0)
                            transform.SetSiblingIndex(preferedSiblingIndex);
                        LeanTween.scale(rectTransform, defaultScale, tweenDuration).setIgnoreTimeScale(true);
                    }
                }
            }
        }
    }
}
