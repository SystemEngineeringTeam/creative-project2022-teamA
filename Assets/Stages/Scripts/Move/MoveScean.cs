using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScean : MonoBehaviour
{
    public string scene;
    private Vector3 position;
    public int x = 0;
    public int y = 0;
    public int z = 0;
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
