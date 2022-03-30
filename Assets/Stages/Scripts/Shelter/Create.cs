using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public KeyConfig key;
    //主人公が触れてキー入力をしたら
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Player"){
            if(key.action.Down()){
                Debug.Log("ものを作るよ");
            }
        }
    }
}
