using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public int upgradeNumb;
    private Button button;
    private TextMeshProUGUI upgradeCostDisplay;
    private Tower tower;
    private Tower upgradedTower;
    private int upgradeCost;
    void Awake()
    {
        button = GetComponent<Button>();
        upgradeCostDisplay = transform.Find("Upgrade Cost Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        tower = Selector.selectedObject.GetComponent<TowerAI>().tower;
        if (tower.upgrade.Length > upgradeNumb){
            upgradedTower = tower.upgrade[upgradeNumb];
            upgradeCost = upgradedTower.price - tower.price;
            upgradeCostDisplay.SetText(upgradeCost.ToString());
        } else {
            gameObject.SetActive(false);
        }

        if (upgradeCost <= MoneyManager.money){
            button.interactable = true;
        } else {
            button.interactable = false;
        }
    }
    public void UpgradeTower(){
        if (upgradeCost <= MoneyManager.money){
            Transform selectedTower = Selector.selectedObject.transform;
            GameObject _createdTower = Instantiate(upgradedTower.towerObj, selectedTower.position, selectedTower.rotation, selectedTower.parent);
            _createdTower.GetComponent<TowerAI>().hp = selectedTower.gameObject.GetComponent<TowerAI>().hp + (upgradedTower.maxHp-tower.maxHp);
            Debug.Log(selectedTower.gameObject.GetComponent<TowerAI>().hp + (upgradedTower.maxHp-tower.maxHp));
            Destroy(selectedTower.gameObject);
            MoneyManager.GainMoney(-upgradeCost);
        }
    }

    public void CreateTooltip(){
        string tooltipString = upgradedTower.name+"\nDamage: "+upgradedTower.attackDmg.ToString()+"\nAttack Rate: "+upgradedTower.attackRate.ToString()+"\nHp: "+upgradedTower.maxHp.ToString();
        Tooltip.ShowTooltip_Static(tooltipString);
    }

    private void OnDisable() {
        Tooltip.HideTooltip_Static();
    }
}
