using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    public int hp;
    public GameObject projectile;
    [HideInInspector]
    public EnemyPathfinding pathfinding;
    public GameObject target;
    private Rigidbody2D rb;
    private float difficulty;
    public GameObject hurtParticles;


    void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        pathfinding.enemy = enemy;
        rb = GetComponent<Rigidbody2D>();
        difficulty = transform.root.GetComponent<DifficultyManager>().difficulty;

        hp = Mathf.RoundToInt(enemy.maxHp * difficulty);
        rb.mass = enemy.maxHp/10;
    }
    void FixedUpdate()
    {
        if (pathfinding.reachedEndOfPath){
            target = pathfinding.target;
        } else if (pathfinding.reachedTower && !target){
            pathfinding.reachedTower = false;
        }
    }
    void Update() {
        if (target){
            //Set Rotation
            Vector2 lookPos = target.transform.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 720*Time.deltaTime);

            if (!IsInvoking("Attack")){
                InvokeRepeating("Attack", enemy.attackRate, enemy.attackRate);
            }
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, pathfinding.direction, 360*Time.deltaTime);
            if (IsInvoking("Attack")){
                CancelInvoke("Attack");
            }
        }
    }


    void Attack(){
        if (target){
            if (!projectile){
                target.GetComponent<TowerAI>().TakeDamage(enemy.attackDmg);
            } else {
                GameObject _projectile = Instantiate(projectile, transform.position, transform.rotation, transform.parent);
                ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
                _projectileAI.attackDmg = enemy.attackDmg;
                _projectileAI.range = enemy.range;
                _projectileAI.friendly = false;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        GameObject collidingObj = collision.gameObject;
        if (collidingObj.tag == "Wall" || collidingObj.tag == "Tower"){
            pathfinding.reachedTower = true;
            target = collidingObj;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        GameObject collidingObj = collision.gameObject;
        if (collidingObj == target){
            pathfinding.reachedTower = false;
            target = null;
            pathfinding.cannotTarget.Clear();
        }
    }
    
    #region Take Damage
    public void TakeDamage(int damage, bool armorPiercing = false){
        if (hp > 0){
            if (!armorPiercing)
                hp -= Mathf.Max(damage - enemy.defense, 0);
            else
                hp -= damage;
            if (hp <= 0){
                Die();
            }
            if (damage - enemy.defense > 0 && hurtParticles){
                Instantiate(hurtParticles, transform.position, Quaternion.identity);
            }
        }
    }
    void Die(){
        Destroy(gameObject);
        MoneyManager.GainMoney(enemy.spawnCost);
    }
    #endregion
}