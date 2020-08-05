using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TowerAI : MonoBehaviour
{
    public Tower tower;
    public GameObject projectile;
    public GameObject hurtParticles;
    [HideInInspector]
    public Quaternion dir;
    private BoxCollider2D bc;
    private SpriteRenderer SpriteR;
    private GameObject target;
    public int hp;
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        SpriteR = GetComponent<SpriteRenderer>();

        hp = tower.maxHp;

        //Update A* path to include this Tower
        //AstarPath.active.UpdateGraphs (bc.bounds);

        InvokeRepeating("UpdateTarget", 0f, 1f);
    }
    void FixedUpdate()
    {
        if (projectile){
            if (target){
                //Set Rotation
                Vector2 lookPos = target.transform.position - transform.position;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                dir = Quaternion.Euler(0, 0, angle);
            } else {
                UpdateTarget();
            }
        }
    }
    #region Take Damage
    public void TakeDamage(int damage){
        if (damage > 0)
            StartCoroutine("HurtEffect");
        hp -= damage;
        if (hp <= 0){
            Die();
        }
    }
    IEnumerator HurtEffect(){
        if (hurtParticles)
            Instantiate(hurtParticles, transform.position, Quaternion.identity, transform);
        SpriteR.color = new Color(1f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(0.2f);
        SpriteR.color = new Color(1f, 1f, 1f, 1f);
    }
    public void Die(){
        Destroy(gameObject);
    }
    private void OnDestroy() {
        if (AstarPath.active)
            AstarPath.active.GetNearest(transform.position).node.Tag = 0;
    }
    #endregion
    #region Attack
    private void Update() {
        if (target){
            if (!IsInvoking("FireBullet")){
                InvokeRepeating("FireBullet", tower.attackRate, tower.attackRate);
            }
        } else {
            UpdateTarget();
            if (IsInvoking("FireBullet") && !target){
                CancelInvoke("FireBullet");
            }
        }
    }
    void FireBullet(){
        GameObject _projectile = Instantiate(projectile, transform.position, dir, transform);
        ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
        _projectileAI.attackDmg = tower.attackDmg;
        _projectileAI.range = tower.range;
    }

    void UpdateTarget(){
        target = FindClosestEnemy();
    }
    GameObject FindClosestEnemy(){
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = tower.range;
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            float _distance = Vector2.Distance(potentialTarget.transform.position, position);
            if (_distance < distance)
            {
                closest = potentialTarget;
                distance = _distance;
            }
        }
        return closest;
    }
    #endregion
}
