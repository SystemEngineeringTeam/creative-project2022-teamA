using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSceanStage : MonoBehaviour
{
    public KeyConfig key;
    public string scene;
    private Vector3 position;
    public int x = 0;
    public int y = 0;
    public int z = 0;
    private bool OnMoveShelter;
    GameObject GameManager;
    private void Start() {
        OnMoveShelter=false;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        position = new Vector3(x, y, z);
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
            Debug.Log("移動できるよ");
        }
    }
    private void Update() {
        if(OnMoveShelter){
            if(key.action.Down()){
                Debug.Log("移動するよ");
                GameManager.GetComponent<ActionManager>().TransitionScene(scene,position);
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
