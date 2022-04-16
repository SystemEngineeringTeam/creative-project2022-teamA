using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GimickManager : MonoBehaviour
{
    private bool Open,cooltime;
    private GameObject OpenObj;
    private float timer;
    public float OpenTime;
    private GameObject[] FoceObjs;
    // Start is called before the first frame update
    void Start(){
        GenelateObject();
        Open = false;
        cooltime = false;
    }

    // Update is called once per frame
    void Update(){
        if(Open && cooltime){
            if(!OpenObj){
                Open = false;
                Debug.Log("OpenObj未指定");
                return;
            }
            timer += Time.deltaTime;
            OpenObj.transform.position += new Vector3(0, -Time.deltaTime, 0);
            if(timer >= OpenTime) OpenEnd();
        }
    }
    public void FoceSwichMethod(float force){
        foreach (GameObject FoceObj in FoceObjs) {
            FoceObj.GetComponent<Rigidbody2D>().velocity= new Vector2(force,0); 
            // Debug.Log("力が加わっているはず…");
        }
    }
    public void OpenMethod(GameObject Obj){
        // 2回呼び出し禁止
        if(cooltime) return;
        cooltime = true;
        OpenObj = Obj;
        Open = true;
    }
    void OpenEnd(){
        Destroy(OpenObj);
        Open = false;
        timer = 0;
        // 2回呼び出し防止
        cooltime = false;
    }
    public void GenelateObject(){
        FoceObjs = GameObject.FindGameObjectsWithTag("ForceObj");
    }
}
