using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuyTowerButton : MenuButtonTween{
    public Tower tower;
    private TextMeshProUGUI costText;
    protected override void Awake() {
        costText = transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        costText.SetText(tower.price.ToString());

        base.Awake();
    }
     public override void OnPointerEnter (PointerEventData eventData) {
         Tooltip.CreateTowerTooltip_Static(tower);

         base.OnPointerEnter(eventData);
     }
     public override void OnPointerExit (PointerEventData eventData) {
         Tooltip.HideTooltip_Static();

         base.OnPointerExit(eventData);
     }
}
