using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public KeyConfig key;
    private bool OnMoveElevater;
    private bool DoMoveElevater;
    private bool UnLock;
    public float currentTime = 0f;
    public float DelayTime;
    public float MoveDis;
    private void Start() {
        OnMoveElevater=false;
        DoMoveElevater=false;
        UnLock =true;
    }
    //主人公が触れたら
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            OnMoveElevater=true;
            Debug.Log("エレベータが作動できるよ");
        }
    }
    // 主人公が離れたら
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            OnMoveElevater=false;
            Debug.Log("エレベータが作動できないよ");
        }
    }

    private void Update() {
        if(OnMoveElevater && UnLock){
            // キー入力
            if(key.action.Down()){
                Debug.Log("作動するよ");
                DoMoveElevater = true;
                UnLock=false;
                currentTime = 0f;
            }
        }
        if(DoMoveElevater){
            transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, MoveDis/DelayTime);
            currentTime += Time.deltaTime;
            if(DelayTime <= currentTime){
                transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                DoMoveElevater = false;
                UnLock = true;
                MoveDis = MoveDis * -1;
            }
        }

    }
}
