using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageToStage : MonoBehaviour
{
    public string scene;
    private Vector3 position;
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    GameObject GameManager;
    private void Start() {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        position = new Vector3(x, y, z);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            Debug.Log("シェルターへ移動できるよ");
            GameManager.GetComponent<ActionManager>().TransitionScene(scene,position);
        }
    }
}
