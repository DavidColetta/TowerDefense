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
        if (Selector.selectedObject){
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
    }
    public void UpgradeTower(){
        if (upgradeCost <= MoneyManager.money){
            Transform selectedTower = Selector.selectedObject.transform;
            GameObject _createdTower = Instantiate(upgradedTower.towerObj, selectedTower.position, selectedTower.rotation, selectedTower.parent);
            _createdTower.GetComponent<TowerAI>().hp = selectedTower.gameObject.GetComponent<TowerAI>().hp + (upgradedTower.maxHp-tower.maxHp);
            Destroy(selectedTower.gameObject);
            MoneyManager.GainMoney(-upgradeCost);
            Selector.SelectTower(_createdTower);
        }
    }

    public void CreateTooltip(){
        Tooltip.CreateTowerTooltip_Static(upgradedTower);
        Transform _range = Selector.selectedObject.transform.Find("Range");
        _range.localScale = Vector2.one*upgradedTower.range*2;
    }
    public void ResetRange(){
        if (Selector.selectedObject){
            Transform _range = Selector.selectedObject.transform.Find("Range");
            _range.localScale = Vector2.one*tower.range*2;
        } 
    }
}
