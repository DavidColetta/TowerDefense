using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemyAttack : MonoBehaviour
{
    public Sprite[] animatedSprites;
    public float[] animationDelays;
    private SpriteRenderer spriteRenderer;
    private EnemyAI enemyAI;
    private float animationTime;
    private int animationFrame;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
    }
    void Update()
    {
        if (enemyAI.target){
            animationTime += (enemyAI.enemy.attackRate/enemyAI.speedMultiplier)*Time.deltaTime;
            if (animationTime > animationDelays[animationFrame]){
                animationFrame ++;
                animationTime = 0;
                if (animationFrame >= animatedSprites.Length){
                    animationFrame = 0;
                }
                spriteRenderer.sprite = animatedSprites[animationFrame];
            }
            
        } else {
            animationTime = 0;
            animationFrame = 0;
        }
    }
}
