using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuff : Debuff
{
    private bool applied = false;
    private Material coldMaterial = (Material)Resources.Load("materials/Cold", typeof(Material));
    private Material normalMaterial = (Material)Resources.Load("materials/Normal", typeof(Material));
    private SpriteRenderer spriteRenderer;
    public SlowDebuff(float duration, EnemyAI target) : base(target, duration){
        this.duration = duration;
    }
    public override void Update(){
        if (target != null && duration > 0){
            if (!applied){
                applied = true;
                target.speedMultiplier = target.speedMultiplier/2;
                spriteRenderer = target.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.material = coldMaterial;
            }
        }

        base.Update();
    }

    public override void Remove(){
        if (applied){
            applied = false;
            target.speedMultiplier = target.speedMultiplier*2;
            spriteRenderer.material = normalMaterial;
        }

        base.Remove();
    }
}
