using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTowerAI : TowerAI
{
    [SerializeField]
    private int debuffID = 0;
    private Debuff debuff;
    private void Start() {
        switch (debuffID)
        {
            case 0:
                debuff = new FireDebuff(0f, null);
                break;
            case 1:
                debuff = new SlowDebuff(0f, null);
                break;
            default:
                debuff = new SlowDebuff(0f, null);
                break;
        }
    }
    public override void UpdateTarget(){
        target = FindClosestNoDebuff();
    }
    private GameObject FindClosestNoDebuff(){
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = tower.range;
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in enemies){
            float _distance = Vector2.Distance(potentialTarget.transform.position, position);
            if (_distance < distance){
                EnemyAI enemyAI = potentialTarget.GetComponent<EnemyAI>();
                if (enemyAI != null){
                    if (!enemyAI.debuffs.Exists(x => x.GetType() == debuff.GetType())){
                        closest = potentialTarget;
                        distance = _distance;
                    }
                }
            }
        }
        return closest;
    }
}
