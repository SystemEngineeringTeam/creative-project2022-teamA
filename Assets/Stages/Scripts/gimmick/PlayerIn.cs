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
        // FlgPlayerStay=false;
        // FlgFoceObjStay = false;
        if(other.tag=="Player"){
            FlgPlayerStay=true;
            OtherObj=other.gameObject;
        }
        if(other.tag=="ForceObj"){
            FlgFoceObjStay = true;
            OtherObj=other.gameObject;
        }
        // if(FlgPlayerStay || FlgFoceObjStay){
        //     Debug.Log("スイッチ踏んでるぞぉぉぉ");
        // }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player") FlgPlayerStay=false;
        if(other.tag=="ForceObj") FlgFoceObjStay = false;
        if(!FlgFoceObjStay || !FlgPlayerStay) OtherObj=other.gameObject;
    }
}
