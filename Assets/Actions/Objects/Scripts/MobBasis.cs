using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName="アクションゲーム/オブジェクト系/モブ")]
public class MobBasis : ObjectBasis
{
    public float mp=1;
    public float ofence=1;
    public float defence=1;
    
    // public void attack2Other(ObjectBasis other){
    //     AttackEvent attack = new AttackEvent(this);
    //     other.attackEvents.Add(attack);
    // }
}
