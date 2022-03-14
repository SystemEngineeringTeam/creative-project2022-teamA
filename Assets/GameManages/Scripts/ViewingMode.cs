using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingMode : MonoBehaviour
{
    public float speed=10;
    Rigidbody2D rig;
    void Start()
    {
        rig=GetComponent<Rigidbody2D>();
        if(rig!=null){
            rig.gravityScale=0;
        }
    }

    void Update()
    {
        Vector2 move= Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            move.y+=1;
        }
        if(Input.GetKey(KeyCode.S)){
            move.y-=1;
        }
        if(Input.GetKey(KeyCode.A)){
            move.x-=1;
        }
        if(Input.GetKey(KeyCode.D)){
            move.x+=1;
        }

        if (rig==null)
        {
            transform.position+=new Vector3(move.x,move.y,0) * (speed * Time.deltaTime);
        }else{
            rig.velocity=move*speed;
        }

        

    }

}
