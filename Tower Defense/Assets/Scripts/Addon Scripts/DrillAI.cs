using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillAI : TowerAI
{
    public LayerMask enemyLayerMask;
    public float spread = 5;
    public override void FixedUpdate() {
        if (projectile){
            if (target){
                dir = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-spread,spread));
            } else {
                UpdateTarget();
            }
        }
    }

    public override void UpdateTarget(){
        target = null;
        RaycastHit2D ray = Physics2D.Raycast(transform.position+transform.right, transform.right, (tower.range*rangeMultiplier)-1, enemyLayerMask);
        if (ray.collider){
            if (ray.collider.gameObject.tag == "Enemy"){
                target = ray.collider.gameObject;
            }
        }
    }
}
