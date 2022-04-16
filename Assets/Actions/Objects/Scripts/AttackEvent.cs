using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent
{
    MobBehaviour attackFrom;
    public float ofence;
    public float addOfence=0;
    public List<float> ofenceBuff;
    public float defence;
    public float addDefence=0;
    public List<float> defenceBuff;
    public float abilityPower=1;
    // public float

    public AttackEvent(MobBehaviour attackFrom,MobBehaviour attackTo,string ability){
        this.attackFrom=attackFrom;
        ofence = attackFrom.status.ofence;
        GameDefines.attackAttribute attr = attackFrom.status.defaultAttr;
        if(attackFrom.HandedItem!=null){
            if (attackFrom.HandedItem.GetType()==typeof(WeaponBasis)){
                WeaponBasis weapon = attackFrom.HandedItem as WeaponBasis;
                ofence+=weapon.attackPower;
                attr = weapon.attr;
            }
        }
        defence = attackTo.status.defenceList[(int)attr];
        abilityPower=attackFrom.status.abilityDamages.GetTable()[ability];
    }
    public AttackEvent(MobBehaviour attackFrom,MobBehaviour attackTo,WeaponBasis weapon,string ability){
        this.attackFrom=attackFrom;
        ofence = attackFrom.status.ofence+weapon.attackPower;
        GameDefines.attackAttribute attr = weapon.attr;
        defence = attackTo.status.defenceList[(int)attr];
        abilityPower=attackFrom.status.abilityDamages.GetTable()[ability];
    }
    public void processEvent(MobBehaviour attackTo){
        float damage = damageCalc();
        Debug.Log("ダメージ："+damage);
        attackTo.hp-=damage;
        attackTo.OnDamaged();
    }
    public static float damageCalc(float ofence,float addOfence,List<float> ofenceBuff,float defence,float addDefence,List<float> defenceBuff,float abilityPower,float fromLevel){
        float oBuffSum=1;
        float dBuffSum=1;
        if(ofenceBuff!=null){
            ofenceBuff.ForEach((b)=>{oBuffSum+=b;});
        }
        if(defenceBuff!=null){
            defenceBuff.ForEach((b)=>{dBuffSum+=b;});
        }
        return Mathf.Max(fromLevel*abilityPower*((ofence+addOfence)*oBuffSum)/((defence+addDefence)*dBuffSum),1);
    }
    public float damageCalc(){
        return damageCalc(ofence,addOfence,ofenceBuff,defence,addDefence,defenceBuff,abilityPower,attackFrom.status.lv);
    }
}
