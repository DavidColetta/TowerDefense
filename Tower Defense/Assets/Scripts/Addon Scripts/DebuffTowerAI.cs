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
                debuff = new FireDebuff(1, null);
                break;
            case 1:
                debuff = new SlowDebuff(1, null);
                break;
            default:
                debuff = new SlowDebuff(1, null);
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
        float distance = tower.range * rangeMultiplier;
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
        if (!closest){
            closest = FindClosestEnemy();
        }
        return closest;
    }
    protected override void FireBullet()
    {
        base.FireBullet();
        Invoke("UpdateTarget",1);
    }
}
