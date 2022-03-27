using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class DropDownTest : MonoBehaviour
{
    public GameObject lookObject;
    public List<MethodInfo> methods=new List<MethodInfo>();
    public int activeMethodIndex=0;
    [System.Serializable]
    public struct ObjectList
    {
        public List<System.Object> objList;
        public ObjectList(List<System.Object> list){
            objList=list;
        }
    }
    public struct Attr
    {
        public System.Type type;
        public object attrObj;
        public Attr(object val){
            type=val.GetType();
            attrObj=val;
            // attrObj = System.BitConverter.GetBytes((dynamic)val);
        }
    }
    public object hogstr;
    public List<object> methodsAttribute=new List<object>();
    public MethodInfo GetActiveMethod(){
        return methods[activeMethodIndex];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(hogstr);
    }
}

