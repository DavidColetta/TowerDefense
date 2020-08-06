using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RepairButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI repairCostDisplay;
    private int repairCost;
    private TowerAI towerAI;

    void Start()
    {
        button = GetComponent<Button>();
        repairCostDisplay = transform.Find("Repair Cost Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        towerAI = Selector.selectedObject.GetComponent<TowerAI>();
        repairCost = Mathf.CeilToInt(towerAI.tower.price * (towerAI.tower.maxHp-towerAI.hp) / towerAI.tower.maxHp);
        if (0 < repairCost && repairCost <= MoneyManager.money){
            button.interactable = true;
        } else {
            button.interactable = false;
        }
        repairCostDisplay.SetText(repairCost.ToString());
    }

    public void RepairTower(){
        if (repairCost <= MoneyManager.money){
            towerAI.hp = towerAI.tower.maxHp;
            MoneyManager.GainMoney(-repairCost);
        }
    }
}
