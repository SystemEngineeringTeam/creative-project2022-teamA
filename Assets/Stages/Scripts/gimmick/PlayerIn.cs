using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIn : MonoBehaviour
{
    public bool FlgPlayerStay;
    public bool FlgFoceObjStay;
    public GameObject OtherObj;
    private void Start() {
        FlgPlayerStay=false;
        FlgFoceObjStay=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        OtherObj=other.gameObject;
        if(other.tag=="Player") FlgPlayerStay=true;
        if(other.tag=="ForceObj") FlgFoceObjStay = true;
        // Debug.Log("スイッチ踏んでるぞぉぉぉ");
    }
    private void OnTriggerExit2D(Collider2D other) {
        OtherObj=other.gameObject;
        if(other.tag=="Player") FlgPlayerStay=false;
        if(other.tag=="ForceObj") FlgFoceObjStay = false;
    }
}
