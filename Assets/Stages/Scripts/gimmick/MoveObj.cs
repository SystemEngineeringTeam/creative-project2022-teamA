using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MovePoint,Obj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerIn>().FlgPlayerStay || GetComponent<PlayerIn>().FlgFoceObjStay)
        {
            Move();
        }
    }
    private void Move(){
        Obj.transform.Translate(MovePoint.transform.position);
    }
}
