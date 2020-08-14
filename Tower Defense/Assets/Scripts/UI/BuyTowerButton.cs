using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuyTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Tower tower;
    private MenuButtonTween menuButtonTween;
    private TextMeshProUGUI costText;
    private void Awake() {
        menuButtonTween = GetComponent<MenuButtonTween>();
        costText = transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        costText.SetText(tower.price.ToString());
    }
     public void OnPointerEnter (PointerEventData eventData) {
         Tooltip.CreateTowerTooltip_Static(tower);
         menuButtonTween._MouseEnter();
     }
     public void OnPointerExit (PointerEventData eventData) {
         Tooltip.HideTooltip_Static();
         menuButtonTween._MouseExit();
     }
}
