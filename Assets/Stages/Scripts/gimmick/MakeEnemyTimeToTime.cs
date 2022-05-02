using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemyTimeToTime : MonoBehaviour
{
    public GameObject GenelatePoint,Obj;
    public float coolTime;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime=0;
    }

    // Update is called once per frame
    private void OnBecameVisible()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=coolTime){
            currentTime = 0;
            Genelarte();
        }
    }
    private void Genelarte(){
        Instantiate(Obj, GenelatePoint.transform.position, Quaternion.identity);
    }
}
