using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuff : Debuff
{
    private bool applied = false;
    public SlowDebuff(float duration, EnemyAI target) : base(target, duration){
        this.duration = duration;
    }
    public override void Update(){
        if (target != null && duration > 0){
            if (!applied){
                applied = true;
                target.speedMultiplier = target.speedMultiplier/2;
            }
        }

        base.Update();
    }

    public override void Remove(){
        if (applied){
            applied = false;
            target.speedMultiplier = target.speedMultiplier*2;
        }

        base.Remove();
    }
}
