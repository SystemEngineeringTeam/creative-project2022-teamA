using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIn : MonoBehaviour
{
    public bool FlgPlayerIn;
    public GameObject OtherObj;
    private void Start() {
        FlgPlayerIn=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        OtherObj=other.gameObject;
        if(other.tag=="Player") FlgPlayerIn=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        OtherObj=other.gameObject;
        if(other.tag=="Player") FlgPlayerIn=false;
    }
}
