using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour
{
    private bool firstPush = false;

    //スタートボタンが押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start!");

        if(!firstPush)
        {
            Debug.Log("Go Next Scene!");
            //ここに次のシーンに飛ぶ命令を書く

            //
            firstPush = true;
        }

    }
}
