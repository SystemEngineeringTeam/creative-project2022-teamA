using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    MobBehaviour mob;
    BoxCollider2D attackBoxCollider;
    void Start(){
        mob = GetComponentInParent<MobBehaviour>();
        attackBoxCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.CompareTag("Defence"))
        // Defenceタグがついていた時
        {
            MobBehaviour otherMob;
            if(other.TryGetComponent<MobBehaviour>(out otherMob)){
                
            }else if((otherMob=other.GetComponentInParent<MobBehaviour>())!=null){
                mob.attackToOther(otherMob,"通常攻撃");

                // 攻撃種類による当たり判定の変形処理
                // attackBoxCollider.size = new Vector2(1,1);

            }
        }
    }
}