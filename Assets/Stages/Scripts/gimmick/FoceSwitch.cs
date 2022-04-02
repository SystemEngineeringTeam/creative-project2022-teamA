using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoceSwitch : MonoBehaviour
{
    public bool FlgRight;
    public float power;
    private bool flg;
    private GameObject[] FoceObjs;
    private bool FlgSwich;
    private Vector2 force;
    private Transform child;
    private Vector2 smallSize = new Vector2(1.0f,0.5f);

    
    // Start is called before the first frame update
    void Start()
    {
        FoceObjs = GameObject.FindGameObjectsWithTag("ForceObj");
        if(!FlgRight) power = power*-1.0f;
        force = new Vector2(power, 0.0f);
        child = GetComponentInChildren<Transform>();
        FlgSwich = false;
        flg=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerIn>().FlgPlayerIn && flg){
            flg=false;
            FlgSwich=true;
        }else{
            flg=true;
        }

        if(FlgSwich){
            foreach (GameObject FoceObj in FoceObjs) {
                FoceObj.GetComponent<Rigidbody2D>().AddForce(force);
            }
            child.transform.localScale = smallSize;
            FlgSwich=false;
        }

    }
}
