using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class MobBehaviour : MonoBehaviour
{
    public float hp,mp;
    public MobBasis status;
    public List<AttackEvent> attackEvents=new List<AttackEvent>();
    public ItemBasis HandedItem;
    
    void Start()
    {
        hp=status.hp;
        mp=status.mp;
    }

    void Update()
    {
        if(hp<=0){
            OnDied();
        }
    }
    void LateUpdate(){
        // attackToOtherによって他から与えられたattackEventを一括で処理する
        foreach (AttackEvent attackEvent in attackEvents)
        {
            attackEvent.processEvent(this);
        }
        attackEvents.Clear();
    }

    // 攻撃を受けたときのコールバック関数.オーバーライドして使ってほしいやつ
    public virtual void OnDamaged(){

    }

    // 体力が0になったときの関数.オーバーライドして使ってほしいやつ
    public virtual void OnDied(){
        Destroy(gameObject);
    }

    // 攻撃を行う時に外部スクリプトからこの関数を実行する
    public virtual void attackToOther(GameObject mob,string ability){
        MobBehaviour mobB;
        if(mob.TryGetComponent<MobBehaviour>(out mobB)){
            attackToOther(mobB,ability);
        }else if(mob.transform.parent.TryGetComponent<MobBehaviour>(out mobB)){
            attackToOther(mobB,ability);
        }
        
    }

    // 攻撃を行う時に外部スクリプトからこの関数を実行する
    public virtual void attackToOther(MobBehaviour mob,string ability){
        AttackEvent attackEvent=new AttackEvent(this,mob,ability);

        mob.attackEvents.Add(attackEvent);
    }
    

}
