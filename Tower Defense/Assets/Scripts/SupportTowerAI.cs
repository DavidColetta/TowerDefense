using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTowerAI : TowerAI
{
    public float attackBoost = 0;
    public float speedBoost = 0;
    public float rangeBoost = 0;

    List<GameObject> towersInRange = new List<GameObject>();

    private void Start() {
        InvokeRepeating("UpdateSupportBuffs", 0 , 1);
    }
    private void UpdateSupportBuffs(){
        foreach (GameObject tower in towersInRange)
        {
            if (tower){
                TowerAI towerAI = tower.GetComponent<TowerAI>();
                if (towerAI.attackDmgMultiplier-1==attackBoost)
                    towerAI.attackDmgMultiplier = 1;
                if (towerAI.attackRateMultiplier-1==speedBoost)
                    towerAI.attackRateMultiplier = 1;
                if (towerAI.rangeMultiplier-1==rangeBoost)
                    towerAI.rangeMultiplier = 1;

                towerAI.target = null;
            }
        }

        towersInRange = FindTowersInRange();

        foreach (GameObject tower in towersInRange)
        {
            if (tower){
                TowerAI towerAI = tower.GetComponent<TowerAI>();
                if (towerAI.attackDmgMultiplier-1<attackBoost)
                    towerAI.attackDmgMultiplier = attackBoost+1;
                if (towerAI.attackRateMultiplier-1<speedBoost)
                    towerAI.attackRateMultiplier = speedBoost+1;
                if (towerAI.rangeMultiplier-1<rangeBoost)
                    towerAI.rangeMultiplier = rangeBoost+1;

                towerAI.target = null;
            }
        }
    }
    protected override void OnDestroy() {
        foreach (GameObject tower in towersInRange)
        {
            if (tower){
                TowerAI towerAI = tower.GetComponent<TowerAI>();
                if (towerAI.attackDmgMultiplier-1==attackBoost)
                    towerAI.attackDmgMultiplier = 1;
                if (towerAI.attackRateMultiplier-1==speedBoost)
                    towerAI.attackRateMultiplier = 1;
                if (towerAI.rangeMultiplier-1==rangeBoost)
                    towerAI.rangeMultiplier = 1;

                towerAI.target = null;
            }
        }
    }

    private List<GameObject> FindTowersInRange(){
        GameObject[] towers;
        towers = GameObject.FindGameObjectsWithTag("Tower");
        List<GameObject> towersInRange = new List<GameObject>();
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in towers)
        {
            Vector2 diff = potentialTarget.transform.position - position;
            float _distance = diff.magnitude;
            if (_distance < tower.range*rangeMultiplier)
            {
                if (potentialTarget != gameObject)
                    towersInRange.Add(potentialTarget);
            }
        }
        return towersInRange;
    }
}
