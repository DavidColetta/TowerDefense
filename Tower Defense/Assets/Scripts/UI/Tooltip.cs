using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake() {
        instance = this;
        backgroundRectTransform = transform.Find("Tooltip Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Tooltip Text").GetComponent<TextMeshProUGUI>();

        HideTooltip();
    }
    private void Update() {
        transform.position = Input.mousePosition;

        RectTransform canvasRectTransform = transform.root.GetComponent<RectTransform>();
        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width){
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height){
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)){
            HideTooltip();
        }
    }
    public void ShowTooltip(string tooltipString){
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        tooltipText.SetText(tooltipString);
        tooltipText.ForceMeshUpdate();
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth, tooltipText.preferredHeight);
        backgroundRectTransform.sizeDelta = backgroundSize;

        Update();
    }
    public void HideTooltip(){
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString){
        instance.ShowTooltip(tooltipString);
    }
    public static void HideTooltip_Static(){
        instance.HideTooltip();
    }
    public void CreateTowerTooltip(Tower tower){
        if (tower != null){
            string tooltipString = tower.name+"\nDamage: "+tower.attackDmg.ToString()+"\nAttack Rate: "+tower.attackRate.ToString()+"\nHp: "+tower.maxHp.ToString();
            ShowTooltip(tooltipString);
        }
    }
    public static void CreateTowerTooltip_Static(Tower tower){
        instance.CreateTowerTooltip(tower);
    }
}
