using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerBehaviour : MobBehaviour
{
    public override void OnDied(){
        Debug.Log("プレイヤーの死");
        hp=status.hp;
    }
}
