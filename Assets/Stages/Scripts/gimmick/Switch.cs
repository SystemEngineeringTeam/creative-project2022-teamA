using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // 力スイッチ
    public List<GameObject> foceObjList = new List<GameObject>(){};
    public float force;
    public bool ForceOn, ForceFlgRight;
    // 開けるスイッチ
    public bool OpenOn;
    public GameObject OpenObj;

    // 二つ同時押しスイッチ
    public GameObject twinSwich;
    public bool twinSwichOn;


    public float timeWait;
    private bool OperationPossible, SwichOn, isVision;
    private Transform child;
    private Vector2 smallSize = new Vector2(1.0f, 0.5f);
    private Vector2 normalSize = new Vector2(1.0f, 1.0f);
    private GameObject GimickManagerObj;
    private float timer;
    private bool FlgTime, Flg;
    public KeyConfig key;

    // Start is called before the first frame update
    void Start()
    {
        GimickManagerObj = GameObject.FindWithTag("GimickManager");
        if (GimickManagerObj == null)
            ForceOn = false;
        if (!ForceFlgRight)
            force = -force;
        child = GetComponentInChildren<Transform>();
        OperationPossible = true;
        SwichOn = false;
        twinSwichOn = false;
    }
    // カメラ中か外か
    void OnBecameInvisible()
    {
        isVision = false;
    }
    void OnBecameVisible()
    {
        isVision = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVision)
            return;
// 押してる処理
#region
        SwichOn = false;
        if (GetComponent<PlayerIn>().FlgPlayerStay || GetComponent<PlayerIn>().FlgFoceObjStay)
        {
            Flg = true;
            twinSwichOn = true;
        }
        else
        {
            Flg = false;
            FlgTime = true;
            twinSwichOn = false;
        }
        // 二重ボタン押し防止
        if (FlgTime)
        {
            timer += Time.deltaTime;
            if (timer >= timeWait)
            {
                timer = 0;
                FlgTime = false;
                OperationPossible = true;
                child.transform.localScale = normalSize;
            }
        }

        if (OperationPossible && Flg)
        {
            OnSwich();
        }
        else
        {
            SwichOn = false;
        }
        if(Flg){
            if(key.action.Down()){
                OnSwich();
                // Debug.Log("key押してる");
            }
        }
#endregion
// Foceの処理
#region
        // 物が押してるかどうかどうか
        if (ForceOn)
        {
            // ボタン操作可能かつプレイヤーが範囲内
            if (SwichOn)
            {
                GimickManagerObj.GetComponent<GimickManager>().FoceSwichMethod(force);
                // Debug.Log("力を加えた");
            }
        }
#endregion
// 別の処理
#region
        if (OpenOn)
        {
            if(SwichOn){
            if(OpenObj == null){
                Debug.Log("開ける物がない");
            }else{
                GimickManagerObj.GetComponent<GimickManager>().OpenMethod(OpenObj);
            }
            }
        }
#endregion
    }
    void OnSwich(){
            child.transform.localScale = smallSize;
            OperationPossible = false;
            SwichOn = true;
            // 二つ押しの処理
            if(twinSwich != null){
                if(twinSwich.GetComponent<Switch>().twinSwich == null) Debug.Log("相手のオブジェクトが指定されてないよ");
                if(!twinSwich.GetComponent<Switch>().twinSwichOn){
                    SwichOn = false;
                    // Debug.Log("ダメだった");
                }
                // Debug.Log("ダブルボタン");
            }
    }
}
