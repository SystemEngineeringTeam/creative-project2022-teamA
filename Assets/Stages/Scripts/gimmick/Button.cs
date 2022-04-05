using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GimickManager))] // 拡張するクラスを指定
public class Button : Editor { // 継承しているクラスがMonoBehaviourでないところに注意！

    public override void OnInspectorGUI()
    {
        // 元のインスペクター部分を表示
        base.OnInspectorGUI();

        // targetを変換して対象を取得
        GimickManager GimickManager = target as GimickManager;

        // publicなメソッドを実行するボタン
        if (GUILayout.Button("foceButton!"))
        {
            GimickManager.FoceSwichMethod(10.5f);
        }
    }
}
