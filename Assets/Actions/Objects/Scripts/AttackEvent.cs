using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent: ScriptableObject
{
    ObjectBasis attackFrom;
    float ofence;
    float addOfence;
    List<float> ofenceBuff;
    float defence;
    float addDefence;
    List<float> defenceBuff;
    float abilityPower;
    // public float

    public AttackEvent(ObjectBasis attackFrom){
        this.attackFrom=attackFrom;
    }
    public static float damageCalc(float ofence,float addOfence,List<float> ofenceBuff,float defence,float addDefence,List<float> defenceBuff,float abilityPower,float fromLevel){
        float oBuffSum=0;
        float dBuffSum=0;
        ofenceBuff.ForEach((b)=>{oBuffSum+=b;});
        defenceBuff.ForEach((b)=>{dBuffSum+=b;});
        return fromLevel*abilityPower*((ofence+addOfence)*oBuffSum)/((defence+addDefence)*dBuffSum);
    }
    public float damageCalc(){
        return damageCalc(ofence,addOfence,ofenceBuff,defence,addDefence,defenceBuff,abilityPower,attackFrom.lv);
    }
}
