using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillAI : TowerAI
{
    public LayerMask enemyLayerMask;
    public override void FixedUpdate() {
        if (projectile){
            if (target){

            } else {
                UpdateTarget();
            }
        }
    }

    public override void UpdateTarget(){
        RaycastHit2D ray = Physics2D.Raycast(transform.position + transform.right, transform.right, tower.range, enemyLayerMask);
        if (ray.collider){
            if (ray.collider.gameObject.tag == "Enemy"){
                target = ray.collider.gameObject;
            }
        }
    }
}
