using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(DropDownTest))]
public class DropDownTestEditor : Editor
{
    DropDownTest targetObject;
    Component[] components={};
    List<string> menu=new List<string>();
    List<MethodInfo> methodsInComponent=new List<MethodInfo>();
    public void OnEnable(){
        targetObject = (DropDownTest)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("スクリプト",MonoScript.FromMonoBehaviour((MonoBehaviour)target),typeof(MonoScript),true);
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lookObject"));
        if(EditorGUI.EndChangeCheck()){
            targetObject.activeMethodIndex=-1;

        }

        EditorGUI.BeginChangeCheck();
        if(targetObject.lookObject!=null){ 
            components = targetObject.lookObject.GetComponents(typeof(MonoBehaviour));
        }
        
        targetObject.methods.Clear();
        foreach (Component component in components)
        {
            methodsInComponent.Clear();
            methodsInComponent.AddRange(component.GetType().GetMethods());
            targetObject.methods.AddRange(methodsInComponent.FindAll((m)=>(m.ReturnType.Equals(typeof(bool)))));
        }
        menu.Clear();
        foreach (MethodInfo method in targetObject.methods)
        {
            menu.Add(method.DeclaringType.Name+"/"+method.Name);
        }
        int changedIndex = EditorGUILayout.Popup("メソッド",targetObject.activeMethodIndex,menu.ToArray());
        if(EditorGUI.EndChangeCheck()){
            Undo.RecordObject(targetObject,"Method選択");
            targetObject.activeMethodIndex=changedIndex;
            Debug.Log("sentaku:"+targetObject.methodsAttribute.Count);
            targetObject.methodsAttribute.Clear();
            foreach (ParameterInfo param in targetObject.GetActiveMethod().GetParameters())
            {
                if(param.ParameterType.Equals(typeof(string))){
                    targetObject.methodsAttribute.Add("");
                }else{
                    targetObject.methodsAttribute.Add(System.Activator.CreateInstance(param.ParameterType));
                }
            }
            EditorUtility.SetDirty(targetObject);
        }
        

        // Debug.Log(targetObject.methodsAttribute.Count);
        
        using (new EditorGUI.IndentLevelScope(1))
        {
            for (int i=0;i < targetObject.methodsAttribute.Count;i++)
            {
                object item = targetObject.methodsAttribute[i];
                System.Type type = item.GetType();
                if(type.Equals(typeof(int))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.IntField(type.Name,(int)item);
                }else if(type.Equals(typeof(bool))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Toggle(type.Name,(bool)item);
                }else if(type.Equals(typeof(string))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.TextField(type.Name,item.ToString());
                }else if(type.Equals(typeof(float))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.FloatField(type.Name,(float)item);
                }else if(type.Equals(typeof(double))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.DoubleField(type.Name,(double)item);
                }else if(type.Equals(typeof(long))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.LongField(type.Name,(long)item);
                }else if(type.Equals(typeof(Rect))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.RectField(type.Name,(Rect)item);
                }else if(type.Equals(typeof(Color))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.ColorField(type.Name,(Color)item);
                }else if(type.Equals(typeof(AnimationCurve))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.CurveField(type.Name,(AnimationCurve)item);
                }else if(type.Equals(typeof(Bounds))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.BoundsField(type.Name,(Bounds)item);
                }else if(type.Equals(typeof(RectInt))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.RectIntField(type.Name,(RectInt)item);
                }else if(type.Equals(typeof(Vector2))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Vector2Field(type.Name,(Vector2)item);
                }else if(type.Equals(typeof(Vector3))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Vector3Field(type.Name,(Vector3)item);
                }else if(type.Equals(typeof(Vector4))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Vector4Field(type.Name,(Vector4)item);
                }else if(type.Equals(typeof(Vector2Int))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Vector2IntField(type.Name,(Vector2Int)item);
                }else if(type.Equals(typeof(Vector3Int))){
                    targetObject.methodsAttribute[i] = EditorGUILayout.Vector3IntField(type.Name,(Vector3Int)item);
                }else if(item is Object){
                    EditorGUILayout.ObjectField(type.Name,(Object)item,type,true);
                }
            }
        }
        
        

        EditorGUILayout.PropertyField(serializedObject.FindProperty("activeMethodIndex"));
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("hogstr"));
        EditorGUILayout.IntField(targetObject.methodsAttribute.Count);

        if(GUILayout.Button("テスト")){
            targetObject.hogstr="test";
            

            // Debug.Log(targetObject.GetActiveMethod().Invoke(targetObject.lookObject.GetComponent(targetObject.GetActiveMethod().DeclaringType),targetObject.methodsAttribute.ToArray()));
        }
        if(GUILayout.Button("test2")){
            Debug.Log(targetObject.hogstr);
        }

        serializedObject.ApplyModifiedProperties();
        // Editor.
    }
}
