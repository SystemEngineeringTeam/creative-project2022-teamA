using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "MobExample", menuName="アクションゲーム/オブジェクト系/モブ")]
public class MobBasis : ObjectBasis
{
    public float mp =1;

    public float ofence =1;
    public GameDefines.attackAttribute defaultAttr = GameDefines.attackAttribute.Nomal;

    public TableFloat abilityDamages = new TableFloat();
    
    [HideInInspector]
    public float[] defenceList=new float[System.Enum.GetValues(typeof(GameDefines.attackAttribute)).Length];
    
    
    // public void attack2Other(ObjectBasis other){
    //     AttackEvent attack = new AttackEvent(this);
    //     other.attackEvents.Add(attack);
    // }
}

#region EditorGUI
#if UNITY_EDITOR

[CustomEditor(typeof(MobBasis))]
public class MobBasisEditor:Editor{
    bool defenceOpen=true;
    MobBasis targetObj;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        targetObj=(MobBasis)target;
        serializedObject.Update();

        if(defenceOpen=EditorGUILayout.Foldout(defenceOpen,"防御")){
            using (new EditorGUI.IndentLevelScope(1))
            {
                if(System.Enum.GetValues(typeof(GameDefines.attackAttribute)).Length!=targetObj.defenceList.Length){
                    System.Array.Resize(ref targetObj.defenceList,System.Enum.GetValues(typeof(GameDefines.attackAttribute)).Length);
                }
                for (int i=0;i<targetObj.defenceList.Length;i++)
                {
                    targetObj.defenceList[i]= EditorGUILayout.FloatField(System.Enum.GetName(typeof(GameDefines.attackAttribute),i),targetObj.defenceList[i]);
                }
            }
        }
        

        serializedObject.ApplyModifiedProperties();

    }
}

#endif
#endregion
