using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Animator))]
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
            animator.SetBool("Attacking", true);
            animator.SetFloat("Speed", enemyAI.speedMultiplier/enemyAI.enemy.attackRate);
        } else {
            animator.SetBool("Attacking", false);
            animator.SetFloat("Speed", enemyAI.speedMultiplier);
        }
    }
}