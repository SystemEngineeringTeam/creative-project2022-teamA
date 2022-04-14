using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageToShelter : MonoBehaviour
{
    public KeyConfig key;
    public string scene;
    public Vector3 position;
    private bool OnMoveShelter;
    // public 
    GameObject GameManager;
    private void Start() {
        OnMoveShelter=false;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        scene = "Shelter";
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
}
