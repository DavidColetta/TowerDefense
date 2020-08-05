using System.Collections;
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
    public GameObject onDeath;
    public Vector2 expansion = new Vector2(0f, 0f);
    private Rigidbody2D rb;
    private Vector2 startPosition;
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
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject collidingObj = other.gameObject;
        if (friendly){
            if (collidingObj.tag == "Enemy"){
                EnemyAI enemyAI = collidingObj.GetComponent<EnemyAI>();
                if (!ignore.Contains(collidingObj)){
                    enemyAI.TakeDamage(attackDmg, armorPiercing);
                    OnHit();
                }
            }
        } else if (!friendly){
            if (collidingObj.tag == "Tower" || collidingObj.tag == "Wall"){
                TowerAI towerAI = collidingObj.GetComponent<TowerAI>();
                if (!ignore.Contains(collidingObj)){
                    towerAI.TakeDamage(attackDmg);
                    OnHit();
                }
            }
        }
    }
    public void OnHit(){
        piercing -= 1;
        if (piercing == 0){
            Die();
        }
    }
    private void Die(){
        if (onDeath != null){
            Instantiate(onDeath, transform.position, Quaternion.identity, transform.parent);
        }
        Destroy(gameObject);
    }
}
