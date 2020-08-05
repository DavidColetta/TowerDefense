using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemyPunch : MonoBehaviour
{
    private Animator animator;
    private EnemyAI enemyAI;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (enemyAI.target){
            animator.SetFloat("AttackSpeed", 1f/enemyAI.enemy.attackRate);
        } else {
            animator.SetFloat("AttackSpeed", -2f);
        }
    }
}