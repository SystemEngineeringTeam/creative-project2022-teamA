using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoceSwitch : MonoBehaviour
{
    public bool FlgRight;
    private bool FlgSwich;
    public float force;
    private Transform child;
    private Vector2 smallSize = new Vector2(1.0f,0.5f);
    private Vector2 normalSize = new Vector2(1.0f,1.0f);
    private GameObject GimickManeger;
    private float timer;
    private bool FlgTime;
    
    // Start is called before the first frame update
    void Start()
    {
        GimickManeger = GameObject.FindWithTag("GimickManager");
        if(!FlgRight) force = -force;
        child = GetComponentInChildren<Transform>();
        FlgSwich = true;
    }

    // Update is called once per frame
    void Update()
    {
// プレイヤーが範囲外だと操作可能
        if(!GetComponent<PlayerIn>().FlgPlayerStay){
            child.transform.localScale = normalSize;
            FlgTime=true;
            // Debug.Log("出たよ");
        }
// 二重ボタン押し防止
        if(FlgTime){
            timer+=Time.deltaTime;
            if(timer<=0.5f){
                timer=0;
                FlgTime=false;
                FlgSwich=true;
            }
        }
// ボタン操作可能かつプレイヤーが範囲内
        if(FlgSwich && GetComponent<PlayerIn>().FlgPlayerStay){
            GimickManeger.GetComponent<GimickManager>().FoceSwich(force);
            // Debug.Log("力を加えた");
            child.transform.localScale = smallSize;
            FlgSwich=false;
        }

    }
}
