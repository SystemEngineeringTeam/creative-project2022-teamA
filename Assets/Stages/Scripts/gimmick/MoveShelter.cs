using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelter : MonoBehaviour
{
    private bool OnMoveShelter;
    private void Start() {
        OnMoveShelter=false;
    }
    //主人公が触れてキー入力をしたら
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            OnMoveShelter=true;
            Debug.Log("シェルターへ移動できるよ");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player"){
            OnMoveShelter=false;
            Debug.Log("シェルターへ移動できるよ");
        }
    }
    private void Update() {
        if(OnMoveShelter){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                Debug.Log("移動するよ");
            }
        }
    }
    // private void OnTriggerStay2D(Collider2D other) {
    //     if(other.tag=="Player"){
    //         if(Input.GetKeyDown(KeyCode.UpArrow)){
    //             Debug.Log("シェルターへ移動するよ");
    //         }
    //         Debug.Log("プレイヤーがシェルターに触れてるよ");
    //     }
    // }
}
