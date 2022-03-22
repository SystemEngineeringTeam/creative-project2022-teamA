using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDB", menuName="アクションゲーム/オブジェクト系/データベース")]

public class ObjectDB : ScriptableObject
{
    [SerializeField]
    private List<ObjectBasis> objectList = new List<ObjectBasis>();
    public List<ObjectBasis> getObjectList(){
        return objectList;
    }
}