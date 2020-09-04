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
    [HideInInspector]
    public GameObject target;
    public int hp;
    [HideInInspector] public float attackDmgMultiplier = 1;
    [HideInInspector] public float attackRateMultiplier = 1;
    [HideInInspector] public float rangeMultiplier = 1;
    [SerializeField] private string attackSound = "";
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        SpriteR = GetComponent<SpriteRenderer>();

        hp = tower.maxHp;

        AudioManager.Play_Static("Build", true);

        dir = transform.rotation;
    }
    public virtual void FixedUpdate()
    {
        if (projectile){
            if (target){
                //Set Rotation
                Vector2 lookPos = target.transform.position - transform.position;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                dir = Quaternion.Euler(0, 0, angle);
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
        AudioManager.Play_Static("Damage");
        if (hurtParticles)
            Instantiate(hurtParticles, transform.position, Quaternion.identity, transform);
        SpriteR.color = new Color(1f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(0.2f);
        SpriteR.color = new Color(1f, 1f, 1f, 1f);
    }
    public void Die(){
        if (tower.price >= 120*DifficultyManager.localDifficulty)
            RestartWaveButton.GainRestartWave();
        Destroy(gameObject);
        AudioManager.Play_Static("Break");
    }
    protected virtual void OnDestroy() {
        if (AstarPath.active){
            AstarPath.active.GetNearest(transform.position).node.Tag = 0;
        }
    }
    #endregion
    #region Attack
    protected virtual void Update() {
        if (projectile){
            if (target){
                if (!IsInvoking("FireBullet")){
                    InvokeRepeating("FireBullet", tower.attackRate/attackRateMultiplier, tower.attackRate/attackRateMultiplier);
                }
            } else {
                UpdateTarget();
                if (IsInvoking("FireBullet") && !target){
                    CancelInvoke("FireBullet");
                }
            }
        }
    }
    protected virtual void FireBullet(){
        AudioManager.Play_Static(attackSound, true);
        GameObject _projectile = Instantiate(projectile, transform.position, dir, transform);
        ProjectileAI _projectileAI = _projectile.GetComponent<ProjectileAI>();
        _projectileAI.attackDmg = Mathf.RoundToInt(tower.attackDmg * attackDmgMultiplier);
        _projectileAI.range = tower.range * rangeMultiplier;
    }

    public virtual void UpdateTarget(){
        target = FindClosestEnemy();
    }
    protected GameObject FindClosestEnemy(){
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = tower.range * rangeMultiplier;
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in enemies){
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
