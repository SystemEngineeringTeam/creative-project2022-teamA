using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GenelateObj : MonoBehaviour
{
    public GameObject GenelatePoint,Obj;
    private GameObject Object;
    // public Transform Obj;
    public Vector3 size;
    private GameObject GimickManagerObj;
    GameObject[] boxes;

    private void Start() {
        GimickManagerObj = GameObject.FindWithTag("GimickManager");
    }

    private void Update() {
        if (GetComponent<PlayerIn>().FlgPlayerStay || GetComponent<PlayerIn>().FlgFoceObjStay)
        {
            Genelete();
        }
    }
    private void Genelete(){
        Object = Instantiate(Obj, GenelatePoint.transform.position, Quaternion.identity) as GameObject;
        Object.transform.localScale = size;
        this.gameObject.SetActive (false);
        GimickManagerObj.GetComponent<GimickManager>().GenelateObject();

        if(Object.tag == "ForceObj"){

            boxes = new GameObject[ Object.transform.childCount ];
            for(int i=0;Object.transform.childCount>i;i++){
                if(Object.transform.GetChild(i).gameObject.TryGetComponent(out Light2D Light)){
                    Light.pointLightOuterRadius = size.x;

                }else{

                };
            }
        }
    }
}
