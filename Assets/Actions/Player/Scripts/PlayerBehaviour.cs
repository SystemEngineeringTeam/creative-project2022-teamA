using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MobBehaviour
{
    public override void OnDied(){
        // 死亡時アクション
        hp=status.hp;
    }
}
