using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemyAI : EnemyAI
{
    [SerializeField] private float shootRate = 8f;
    public GameObject projectile;
    [SerializeField] private GameObject spitParticles = null;
    protected override void Awake()
    {
        base.Awake();

        InvokeRepeating("Shoot",12f,shootRate);
    }
    private void Shoot(){
        if (!pathfinding.target || !projectile || target){
            return;
        }
        StartCoroutine("ShootWithAnimation");
    }
    IEnumerator ShootWithAnimation(){
        GetComponent<AnimateGiant>().PlaySpitAnimation();
        speedMultiplier = speedMultiplier/10f;
        yield return new WaitForSeconds(1);
        if (pathfinding.target){
            Vector2 lookPos = pathfinding.target.transform.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            Quaternion shootDir = Quaternion.Euler(0, 0, angle);

            if (spitParticles){
                Instantiate(spitParticles,transform.position,transform.rotation);
            }
            yield return new WaitForSeconds(0.1f);

            GameObject _projectile = Instantiate(projectile, transform.position, shootDir, transform.parent);
            ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
            _projectileAI.attackDmg = enemy.attackDmg;
            _projectileAI.range = 50;
            _projectileAI.friendly = false;
        }
        speedMultiplier = speedMultiplier*10f;
    }
    protected override void Die()
    {
        base.Die();
        if (!PauseManager.won){
            PauseManager.won = true;
            PauseManager.Pause_Static();
        }
    }
}
