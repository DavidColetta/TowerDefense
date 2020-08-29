using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyButton : MonoBehaviour
{
    private TextMeshProUGUI destroyCostDisplay;
    private int destroyCost;
    private TowerAI towerAI;
    private Button button;

    void Start()
    {
        destroyCostDisplay = transform.Find("Destroy Cost Text").gameObject.GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Selector.selectedObject){
            towerAI = Selector.selectedObject.GetComponent<TowerAI>();
            destroyCost = Mathf.FloorToInt(0.75f * towerAI.tower.price * towerAI.hp / towerAI.tower.maxHp);
            destroyCostDisplay.SetText(destroyCost.ToString());

            bool IsLastTower = false;
            if (GameOver.NumberOfTowers <= 1){
                if (towerAI.gameObject.tag != "Wall"){
                   IsLastTower = true;
                }
            }
            if (IsLastTower)
                button.interactable = false;
            else 
                button.interactable = true;
        }
    }

    public void DestroyTower(){
        Destroy(towerAI.gameObject);
        AudioManager.Play_Static("Break");
        MoneyManager.GainMoney(destroyCost);
    }
}
