using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class MobBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct DefenceFiled
    {
        public float damageFactor;
        public Collider2D collider;
    }
    public List<DefenceFiled> defenceFileds=new List<DefenceFiled>();
    public MobBasis status;
    public TableEvent eventsList = new TableEvent();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void attackToOther(){

    }
    public bool boolTrue(bool input,string str,int i){
        return input;
    }
}
