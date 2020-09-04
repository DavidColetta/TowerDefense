using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemyAI : EnemyAI
{
    [SerializeField] private float shootRate = 8f;
    public GameObject projectile;
    protected override void Awake()
    {
        base.Awake();

        InvokeRepeating("Shoot",1f,shootRate);
    }
    private void Shoot(){
        if (!pathfinding.target || !projectile || target){
            return;
        }
        Vector2 lookPos = pathfinding.target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        Quaternion shootDir = Quaternion.Euler(0, 0, angle);

        GameObject _projectile = Instantiate(projectile, transform.position, shootDir, transform.parent);
        ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
        _projectileAI.attackDmg = enemy.attackDmg;
        _projectileAI.range = 50;
        _projectileAI.friendly = false;
    }
}
