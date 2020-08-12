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
    public GameObject hurtParticles;
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> debuffsToAdd = new List<Debuff>();
    public float speedMultiplier = 1;


    void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        pathfinding.enemy = enemy;
        rb = GetComponent<Rigidbody2D>();

        hp = Mathf.RoundToInt(enemy.maxHp * DifficultyManager.difficulty);
        rb.mass = enemy.maxHp/10;
        pathfinding.speed = speedMultiplier * enemy.speed;
    }
    void FixedUpdate()
    {
        HandleDebuffs();
        pathfinding.speed = speedMultiplier * enemy.speed;

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
                InvokeRepeating("Attack", enemy.attackRate / speedMultiplier, enemy.attackRate);
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
            if (hurtParticles){
                if (damage - enemy.defense > 0 || armorPiercing)
                    Instantiate(hurtParticles, transform.position, Quaternion.identity);
            }
        }
    }
    public void AddDebuff(Debuff debuff){
        if (!debuffsToAdd.Exists(x => x.GetType() == debuff.GetType())){
            debuffsToAdd.Add(debuff);
            if (debuffs.Exists(x => x.GetType() == debuff.GetType()))
                debuffs.Find(x => x.GetType() == debuff.GetType()).Remove();
        }
    }
    public void RemoveDebuff(Debuff debuff){
        if (!debuffsToRemove.Exists(x => x.GetType() == debuff.GetType()))
            debuffsToRemove.Add(debuff);
    }
    private void HandleDebuffs(){
        if (debuffsToRemove.Count > 0){
            foreach (Debuff debuff in debuffsToRemove)
            {
                debuffs.Remove(debuff);
            }
            debuffsToRemove.Clear();
        }
        if (debuffsToAdd.Count > 0){;
            foreach (Debuff debuff in debuffsToAdd)
            {
                if (!debuffs.Exists(x => x.GetType() == debuff.GetType())){
                    debuffs.Add(debuff);
                }
            }
            debuffsToAdd.Clear();
        }

        foreach (Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }
    void Die(){
        Destroy(gameObject);
        MoneyManager.GainMoney(enemy.spawnCost);
    }
    #endregion
}