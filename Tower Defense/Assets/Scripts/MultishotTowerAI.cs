using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotTowerAI : TowerAI
{
    [SerializeField] private int shots = 2;
    [SerializeField] private float scatterDistance = 20;
    protected override void FireBullet(){
        Quaternion fireDir = dir;
        if (shots%2 == 0)
            fireDir = Quaternion.Euler(0,0,dir.eulerAngles.z - scatterDistance/(4f/shots));
        else
            fireDir = Quaternion.Euler(0,0,dir.eulerAngles.z - scatterDistance/(2f/(shots-1)));
        for (int i = 0; i < shots; i++)
        {
            GameObject _projectile = Instantiate(projectile, transform.position, fireDir, transform);
            ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
            _projectileAI.attackDmg = tower.attackDmg;
            _projectileAI.range = tower.range;

            fireDir = Quaternion.Euler(0,0,fireDir.eulerAngles.z + scatterDistance);
        }
    }
}
