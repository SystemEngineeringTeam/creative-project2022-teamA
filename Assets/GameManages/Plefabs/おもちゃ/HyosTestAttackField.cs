using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyosTestAttackField : MonoBehaviour
{
    MobBehaviour mob;
    void Start(){
        mob=GetComponentInParent<MobBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.CompareTag("Defence"))
        {
            MobBehaviour otherMob;
            if(other.TryGetComponent<MobBehaviour>(out otherMob)){
                
            }else if((otherMob=other.GetComponentInParent<MobBehaviour>())!=null){
                mob.attackToOther(otherMob,"通常攻撃");
                Debug.Log("AAA");
            }
            
        }
    }
}
