using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    public int hp;
    [HideInInspector]
    public EnemyPathfinding pathfinding;
    public GameObject target;
    private Rigidbody2D rb;
    public GameObject hurtParticles;
    public List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> debuffsToAdd = new List<Debuff>();
    public float speedMultiplier = 1;
    private Transform hpDisplay;
    private int maxHp;


    protected virtual void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        pathfinding.enemy = enemy;
        rb = GetComponent<Rigidbody2D>();
        hpDisplay = transform.Find("HpDisplay");

        maxHp = Mathf.RoundToInt(enemy.maxHp * DifficultyManager.difficulty);
        hp = maxHp;
        rb.mass = (enemy.maxHp + enemy.defense*2)/10;
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
                InvokeRepeating("Attack", enemy.attackRate / speedMultiplier, enemy.attackRate / speedMultiplier);
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
            target.GetComponent<TowerAI>().TakeDamage(enemy.attackDmg);
        }
    }
    protected void OnCollisionStay2D(Collision2D collision) {
        GameObject collidingObj = collision.gameObject;
        if (collidingObj.tag == "Wall" || collidingObj.tag == "Tower"){
            pathfinding.reachedTower = true;
            target = collidingObj;
        }
    }
    protected void OnCollisionExit2D(Collision2D collision) {
        GameObject collidingObj = collision.gameObject;
        if (collidingObj == target){
            pathfinding.reachedTower = false;
            target = null;
            pathfinding.cannotTarget.Clear();
        }
    }
    
    #region Take Damage
    public int TakeDamage(int damage, bool armorPiercing = false, bool showHurtParticles = true){
        if (hp > 0){
            if (!armorPiercing)
                hp -= Mathf.Max(damage - enemy.defense, 0);
            else
                hp -= damage;
            if (hp <= 0){
                Die();
            }
            if (hurtParticles && showHurtParticles){
                if (damage - enemy.defense > 0 || armorPiercing)
                    Instantiate(hurtParticles, transform.position, Quaternion.identity);
            }
            float hpPercentage = 1 - (float)hp/(float)maxHp;
            hpDisplay.localScale = new Vector3(hpPercentage, hpPercentage, 1f);
            if (!armorPiercing)
                return Mathf.Max(damage - enemy.defense, 0);
            else
                return damage;
        }
        return 1;
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