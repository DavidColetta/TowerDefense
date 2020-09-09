using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileAI : ProjectileAI
{
    GameObject target = null;
    private void Update() {
        if (target != null){
            Vector2 lookPos = target.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg);
        } else {
            target = FindClosestEnemy();
        }
    }
    GameObject FindClosestEnemy(){
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = range;
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            if (!ignore.Contains(potentialTarget)){
                float _distance = Vector2.Distance(potentialTarget.transform.position, position);
                if (_distance < distance)
                {
                    closest = potentialTarget;
                    distance = _distance;
                }
            }
        }
        return closest;
    }
    protected override void OnTriggerEnter2D(Collider2D other){
        base.OnTriggerEnter2D(other);

        target = null;
    }
    public override void OnHit(){
        base.OnHit();

        startPosition = rb.position;
    }
}
