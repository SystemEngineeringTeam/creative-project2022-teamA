using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterToStage : MonoBehaviour
{
    public KeyConfig key;
    public string scene;
    private Vector3 position;
    private bool OnMoveShelter;
    GameObject GameManager;
    public TablePoint tablePoint = new TablePoint();
    private void Start() {
        OnMoveShelter=false;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        position = GameManager.GetComponent<ActionManager>().lastTransitionPosition;
        position.y = position.y + 1.0f;
        scene = GameManager.GetComponent<ActionManager>().beforeSceneName;
    }
    //主人公が触れてキー入力をしたら
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            OnMoveShelter=true;
            Debug.Log("ステージへ移動できるよ");
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
                GameManager.GetComponent<ActionManager>().TransitionScene(scene,tablePoint.GetTable()[scene]);
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
