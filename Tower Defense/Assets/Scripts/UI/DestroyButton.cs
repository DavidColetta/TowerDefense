using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyButton : MonoBehaviour
{
    private TextMeshProUGUI destroyCostDisplay;
    private int destroyCost;
    private TowerAI towerAI;

    void Start()
    {
        destroyCostDisplay = transform.Find("Destroy Cost Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        towerAI = Selector.selectedObject.GetComponent<TowerAI>();
        destroyCost = Mathf.FloorToInt(0.75f * towerAI.tower.price * towerAI.hp / towerAI.tower.maxHp);
        destroyCostDisplay.SetText(destroyCost.ToString());
    }

    public void DestroyTower(){
        towerAI.Die();
        MoneyManager.GainMoney(destroyCost);
    }
}
