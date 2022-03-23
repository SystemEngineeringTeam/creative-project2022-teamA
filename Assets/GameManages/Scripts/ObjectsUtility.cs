using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsUtility : MonoBehaviour
{
    [SerializeField]
    private ObjectDB objectDB;
    
    public ObjectBasis GetObject(string ID){
        return objectDB.getObjectList().Find((obj)=>(string.Compare(obj.objectID,ID)==0));
    }

    public void spawnObject(ObjectBasis obj,Vector3 pos){
        GameObject mobObject = Instantiate(obj.objectPrefab,pos,Quaternion.identity);
    }
    public void spawnObject(string objID,Vector3 pos){
        spawnObject(GetObject(objID),pos);
    }
    

}
