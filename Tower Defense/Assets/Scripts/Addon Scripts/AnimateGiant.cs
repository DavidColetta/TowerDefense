using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGiant : AnimateEnemyPunch
{
    protected override void Update() {
        base.Update();

    }
    public void PlaySpitAnimation(){
        animator.SetTrigger("Spit");
    }
}
