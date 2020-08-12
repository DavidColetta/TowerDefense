using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    protected EnemyAI target;
    protected float duration;
    public Debuff(EnemyAI target, float duration){
        this.target = target;
    }
    public virtual void Update() {
        if (target != null){
            duration -= Time.deltaTime;

            if (duration <= 0){
                Remove();
            }
        }
    }
    public virtual void Remove(){
        if (target != null){
            target.RemoveDebuff(this);
        }
    }
}
