using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : Debuff
{
    private float tickTime = 1f;
    private float timeSinceLastTick = 0;
    public FireDebuff(float duration, EnemyAI target) : base(target, duration){
        this.duration = duration;
    }
    public override void Update(){
        if (target != null){
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= tickTime){
                timeSinceLastTick = 0;
                Debug.Log("FireTick");
                target.TakeDamage(1, true);
            }
        }

        base.Update();
    }
}
