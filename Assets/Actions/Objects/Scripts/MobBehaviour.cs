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
    public virtual void OnDied(){
        Destroy(gameObject);
    }


    void LateUpdate(){
        foreach (AttackEvent attackEvent in attackEvents)
        {
            attackEvent.processEvent(this);
        }
        attackEvents.Clear();
    }

    public virtual void attackToOther(GameObject mob,string ability){
        MobBehaviour mobB;
        if(mob.TryGetComponent<MobBehaviour>(out mobB)){
            attackToOther(mobB,ability);
        }else if(mob.transform.parent.TryGetComponent<MobBehaviour>(out mobB)){
            attackToOther(mobB,ability);
        }
        
    }
    public virtual void attackToOther(MobBehaviour mob,string ability){
        AttackEvent attackEvent=new AttackEvent(this,mob,ability);

        mob.attackEvents.Add(attackEvent);
    }
}
