using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenelateObj : MonoBehaviour
{
    public GameObject GenelatePoint;
    public GameObject Obj;
    public Vector3 size;
    private GameObject GimickManagerObj;

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
        GameObject Object = Instantiate(Obj, GenelatePoint.transform.position, Quaternion.identity);
        Object.transform.localScale = size;
        this.gameObject.SetActive (false);
        GimickManagerObj.GetComponent<GimickManager>().GenelateObject();
    }
}
