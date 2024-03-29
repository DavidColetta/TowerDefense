﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAI : MonoBehaviour
{
    public int attackDmg = 1;
    public float range = 64;
    public float speed = 6;
    public int piercing = 1;
    public bool friendly = true;
    public bool armorPiercing = false;
    public List<Vector2> debuffs = new List<Vector2>();
    public GameObject onDeath;
    public string hitSound = "Hit";
    public Vector2 expansion = new Vector2(0f, 0f);
    protected Rigidbody2D rb;
    protected Vector2 startPosition;
    [HideInInspector]
    public List<GameObject> ignore = new List<GameObject>();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    void FixedUpdate()
    {
        if (speed != 0){
            Vector2 moveVector = transform.right * speed * Time.deltaTime;
            rb.position += moveVector;

            if (Vector2.Distance(startPosition, rb.position) > range){
                Destroy(gameObject);
            }
        }

        if (expansion != new Vector2(0f, 0f)){
            transform.localScale += (Vector3)expansion * Time.deltaTime;
            if (transform.localScale.x > range*2 || transform.localScale.y > range*2){
                Die();
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        GameObject collidingObj = other.gameObject;
        if (friendly){
            if (collidingObj.tag == "Enemy"){
                EnemyAI enemyAI = collidingObj.GetComponent<EnemyAI>();
                if (!ignore.Contains(collidingObj)){
                    ignore.Add(collidingObj);
                    int _damage  = enemyAI.TakeDamage(attackDmg, armorPiercing);
                    if (_damage > 0 || attackDmg == 0){
                        AudioManager.Play_Static(hitSound, true);
                    } else if (_damage <= 0 && attackDmg != 0){
                        AudioManager.Play_Static("MetalHit", true);
                    }
                    foreach (Vector2 debuffID in debuffs)
                    {
                        switch (debuffID.x)
                        {
                            case 0: 
                                enemyAI.AddDebuff(new FireDebuff(debuffID.y, enemyAI));
                                break;
                            case 1:
                                enemyAI.AddDebuff(new SlowDebuff(debuffID.y, enemyAI));
                                break;
                            default:
                                break;
                        }
                    }
                    OnHit();
                }
            }
        } 
        if (!friendly){
            if (collidingObj.tag == "Tower" || collidingObj.tag == "Wall"){
                TowerAI towerAI = collidingObj.GetComponent<TowerAI>();
                if (!ignore.Contains(collidingObj)){
                    ignore.Add(collidingObj);
                    AudioManager.Play_Static(hitSound, true);
                    towerAI.TakeDamage(attackDmg);
                    OnHit();
                }
            }
        }
    }
    public virtual void OnHit(){
        piercing -= 1;
        if (piercing == 0){
            Die();
        }
    }
    private void Die(){
        if (onDeath != null){
            GameObject _onDeath = Instantiate(onDeath, transform.position, Quaternion.identity, transform.parent);
            ProjectileAI _projectileAI = onDeath.GetComponent<ProjectileAI>();
            if (_projectileAI != null && ignore.Count > 0){
                _projectileAI.ignore.AddRange(ignore);
            }
        }
        Destroy(gameObject);
    }
}
