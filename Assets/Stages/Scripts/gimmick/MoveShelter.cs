using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelter : MonoBehaviour
{
    //主人公が触れてキー入力をしたら
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Player"){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                Debug.Log("シェルターへ移動するよ");
            }
        }
    }
}
