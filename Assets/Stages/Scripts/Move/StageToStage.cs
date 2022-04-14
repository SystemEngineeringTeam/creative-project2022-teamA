using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageToStage : MonoBehaviour
{
    public string scene;
    public Vector3 position;
    GameObject GameManager;
    private void Start() {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            Debug.Log("ステージ移動できるよ");
            GameManager.GetComponent<ActionManager>().TransitionScene(scene,position);
        }
    }
}
