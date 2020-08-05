using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WallBreakerAI : MonoBehaviour
{
    private EnemyAI enemyAI;
    private bool selfDestructing = false;
    private Animator animator;
    public GameObject projectile;
    public int explosionDamage = 15;
    public float range = 0.5f;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.target && !selfDestructing){
            selfDestructing = true;
            StartCoroutine("SelfDestruct", enemyAI.enemy.attackRate);
        } else if (!enemyAI.target && selfDestructing){
            selfDestructing = false;
        }
    }

    IEnumerator SelfDestruct(float timer){
        animator.SetFloat("FillSpeed", 1f/enemyAI.enemy.attackRate);
        yield return new WaitForSeconds(timer);
        if (selfDestructing){
            Destroy(gameObject, 0.05f);
            GameObject _projectile = Instantiate(projectile, transform.position, transform.rotation, transform.parent);
            ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
            _projectileAI.attackDmg = explosionDamage;
            _projectileAI.range = range;
            _projectileAI.friendly = false;
        } else {
            animator.SetFloat("FillSpeed", -1f/enemyAI.enemy.attackRate);
        }
    }
}
