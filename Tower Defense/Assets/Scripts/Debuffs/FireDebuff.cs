using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : Debuff
{
    private float tickTime = 0.3f;
    private float timeSinceLastTick = 100f;
    private GameObject fireParticles = (GameObject)Resources.Load("prefabs/FireParticles", typeof(GameObject));
    public FireDebuff(float duration, EnemyAI target) : base(target, duration){
        this.duration = duration;
    }
    public override void Update(){
        if (target != null){
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= tickTime){
                timeSinceLastTick = 0;
                target.TakeDamage(1, true, false);
                GameObject.Instantiate(fireParticles, target.gameObject.transform.position, Quaternion.identity);
            }
        }

        base.Update();
    }
}
