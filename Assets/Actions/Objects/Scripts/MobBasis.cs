using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobExample", menuName="アクションゲーム/オブジェクト系/モブ")]
public class MobBasis : ObjectBasis
{
    [System.Serializable]
    public struct defenceList
    {

        public float Nomal,
        Fire,
        Thunder,
        Ice,
        Sacred,
        Dark;
    }
    
    public float mp=1;
    public float ofence=1;
    
    public TableFloat abilityDamages = new TableFloat();
    public defenceList defence = new defenceList();
    
    
    // public void attack2Other(ObjectBasis other){
    //     AttackEvent attack = new AttackEvent(this);
    //     other.attackEvents.Add(attack);
    // }
}

