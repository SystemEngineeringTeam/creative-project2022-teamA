#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectsUtility))]
public class ObjectsUtilityEditor : Editor
{
    string spawnObjectID="HogeMob";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        spawnObjectID=GUILayout.TextField(spawnObjectID);

        ObjectsUtility targetClass = target as ObjectsUtility;
        
        if (GUILayout.Button("hoge"))
        {   
            targetClass.spawnObject(spawnObjectID,targetClass.transform.position);
        }
    }
}

#endif
