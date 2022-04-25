using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyosTestPlayer : MonoBehaviour
{

    public KeyConfig keyConfig;
    [Header("移動速度関連")]
    public float moveMaxSpeed = 3; // 移動速度
    public float controllGrip = 50; // 移動操作の加速度
    public float controllGripAir = 30; // 空中での移動加速度
    public Vector2 parentVelocity = new Vector2(); // 動く足場などに追従する速度

    [Header("ジャンプ関連")]
    public float groundJumpSpeed = 30; // 最小ジャンプの速度加算
    public float keepJumpFroce = 30; // 大ジャンプ時に加わる加速度(JumpTimeによって減衰)
    public float jumpMaxTime =0.5f; // 大ジャンプで加速度が加わる時間
    public float jumpCoolTime=0.2f; // ジャンプ後再度ジャンプする為に必要な時間
    float jumpTime=0; // 大ジャンプするときの残り時間
    bool canJump=false; // ジャンプが可能かどうか
    bool isJump = false; // ジャンプにより上昇中かどうか
    bool isFirstJump=false; // 大小ジャンプ区別のためのフラグ

    [Header("地面判定関連")]
    public PhysicsMaterial2D PhNof;
    public PhysicsMaterial2D PhMaxf;
    
    public GroundsCounter groundsCnt = new GroundsCounter(); // 上下左右の接地数
    [System.Serializable]
    public struct GroundsCounter
    {
        public int up,down,left,right;
    }
    int GroundCnt=0; // 接地数
    bool onGround = false; // 接地しているかどうか
    bool onWall=false; // 壁に接しているか

    [Header("アクション追加要素")]
    public bool wallSticky =true; // 壁ズザーあり
    public float wallStickSpeed = 3; // 壁ズザーの速度
    bool isWallStick=false; // 壁ズザーのフラグ
    public bool wallJump = true;
    public float wallJumpHorizonSpeed=20;
    
    // その他変数
    Rigidbody2D rig; // プレイヤーのrigidBody2D
    BoxCollider2D boxCol; // プレイヤーのボックスコライダ
    float downAngle; // プレイヤーの下方向の角度
    Vector2 horizonDirection,verticalDirection; // プレイヤーの水平垂直方向
    float horizontalSpeed,verticalSpeed; // プレイヤーの水平垂直方向の速度
    bool wallSideRight=false;
    bool isMove=false;
    public List<float> coolTimes=new List<float>();
    public List<float> nowCoolTimes= new List<float>();
    public List<Collider2D> attackTriggers=new List<Collider2D>();

// 最初の１フレームでの処理
    void Start()
    {
        rig=GetComponent<Rigidbody2D>(); // rigidbodyを読み込む
        if(!rig){
            rig = gameObject.AddComponent<Rigidbody2D>();
        }
        boxCol=GetComponent<BoxCollider2D>(); // コライダを読み込む
        if(!boxCol){
            boxCol = gameObject.AddComponent<BoxCollider2D>();
        }
        nowCoolTimes=new List<float>(new float[coolTimes.Count]);
        
        
    }

// 変動fpsの毎フレーム処理
    void Update()
    {
        Debug.Log(boxCol.sharedMaterial.name);
    // キーコンフィグをテストした
        // Debug.Log(keyConfig.up.Up()+":"+keyConfig.up.Stay()+":"+keyConfig.up.Down()+":"+keyConfig.up.All()+":"+keyConfig.up.AllDown());
    // 準備
        
        // 速度をプレイヤーの水平と垂直に分離する
        downAngle = Mathf.Atan2(Physics2D.gravity.y,Physics2D.gravity.x);
        float horizontalAngle = downAngle+Mathf.PI/2;
        horizonDirection = new Vector2(Mathf.Cos(horizontalAngle),Mathf.Sin(horizontalAngle));
        verticalDirection = new Vector2(Mathf.Cos(downAngle+Mathf.PI),Mathf.Sin(downAngle+Mathf.PI));
        horizontalSpeed = Vector2.Dot(rig.velocity-parentVelocity,horizonDirection); //水平方向の速度
        verticalSpeed = Vector2.Dot(rig.velocity-parentVelocity,verticalDirection); //垂直方向の速度
        

    // 操作部分
        // 移動操作
        float horizontalMove =0;
        if (keyConfig.left.Stay()){
            horizontalMove-=1;
        }
        if (keyConfig.right.Stay()){
            horizontalMove+=1;
        }
        isWallStick=false;
        if(onWall){
            wallSideRight=groundsCnt.right>0;
            if((horizontalMove>0&&groundsCnt.right>0)||(horizontalMove<0&&groundsCnt.left>0)){
                horizontalMove=0;
                if(wallSticky){
                    isWallStick=(verticalSpeed<=-wallStickSpeed);
                    verticalSpeed=Mathf.Max(verticalSpeed,-wallStickSpeed);
                }
            }
        }
        isMove = (horizontalMove!=0);
        
        // ジャンプ操作
        if(!isJump){
            if(canJump&&keyConfig.jump.Stay()){
                canJump=false;
                isJump=true;
                isFirstJump=true;
                jumpTime=jumpMaxTime;
            }
        }else{
            if(jumpTime>0&&!keyConfig.jump.Stay()){
                jumpTime=0;
            }
        }

        if(nowCoolTimes[0]<=0.3f){
            attackTriggers[0].gameObject.SetActive(false);
        }
        if(nowCoolTimes[0]<=0){
            if(keyConfig.attack.Down()){
                attackTriggers[0].gameObject.SetActive(true);
                nowCoolTimes[0]=coolTimes[0];
            }
        }

    // 見た目処理
    if(isMove){
        if(horizontalSpeed<0){
            transform.rotation=Quaternion.Euler(0,180,0);
        }else{
            transform.rotation=Quaternion.identity;
        }
    }
    transform.rotation*=Quaternion.Euler(0,0,Mathf.Rad2Deg*downAngle+90);



    // 処理部分

        // プレイヤーの移動方向に合わせて水平速度を変更
        if(canJump){
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed,horizontalMove*moveMaxSpeed,controllGrip*Time.deltaTime);
        }else{
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed,horizontalMove*moveMaxSpeed,controllGripAir*Time.deltaTime);
        }
        // horizontalSpeed = horizontalMove*moveMaxSpeed; // 簡易版

        // ジャンプ処理
        if(isJump){
            if(jumpTime>0){
                if (isFirstJump){
                    // 小ジャンプの最初の処理
                    verticalSpeed=Mathf.Min(verticalSpeed+groundJumpSpeed,groundJumpSpeed);
                    isFirstJump=false;
                    if(wallJump&&isWallStick){
                        horizontalSpeed+=(wallSideRight?-1:1)*wallJumpHorizonSpeed;
                    }
                }else{
                    // 大ジャンプの処理
                    verticalSpeed+=keepJumpFroce*(jumpTime/jumpMaxTime)*Time.deltaTime;
                    // verticalSpeed+=keepJumpFroce*Time.deltaTime; // 簡易版
                }
                

                jumpTime-=Time.deltaTime;
            }else{
                isJump=false;
            }
        }

        // 水平と垂直の速度を統合
        rig.velocity = horizonDirection*horizontalSpeed+verticalDirection*verticalSpeed+parentVelocity;
        
        // クールタイム処理
        for (int i = 0; i < nowCoolTimes.Count; i++){
            if(nowCoolTimes[i]>0){
                nowCoolTimes[i]-=Time.deltaTime;
            }
        }

    }

// 固定fps物理演算前の処理
    void FixedUpdate(){
        parentVelocity = Vector2.zero;
        groundsCnt=new GroundsCounter(); // OnCollision前に接地数を0に初期化
        onGround = (GroundCnt>0);
        StartCoroutine(AFUCoroutine()); // 物理演算後の処理を予約
    }

// 物理演算後の処理を実装するためのコルーチン
    IEnumerator AFUCoroutine(){
        yield return new WaitForFixedUpdate();
        AfterFixedUpdate();
    }

// 固定fps物理演算後の処理(コルーチンにより実装)
    void AfterFixedUpdate(){
        if(groundsCnt.down>0&&!isMove){
            boxCol.sharedMaterial=PhMaxf;
        }else{
            boxCol.sharedMaterial=PhNof;
        }
        // フラグ関連
        canJump = ((groundsCnt.down>0||(isWallStick&&wallJump))&&jumpMaxTime-jumpTime>=jumpCoolTime);
        if(canJump){
            isJump=false;
        }
        onGround = (GroundCnt>0);
        onWall = (groundsCnt.left>0||groundsCnt.right>0);
        
    }
    void OnCollisionStay2D(Collision2D collision2D){
        // 上下左右判定の処理
        // if(collision2D.transform.CompareTag("Ground")){
            float GroundThreshold=(boxCol.size.y+boxCol.edgeRadius)/2;
            float WallThreshold=(boxCol.size.x+boxCol.edgeRadius)/2;
            foreach (ContactPoint2D contact in collision2D.contacts)
            {
                Vector3 contactPos = new Vector3(contact.point.x,contact.point.y,transform.position.z);
                Debug.DrawLine(contactPos+Vector3.up,contactPos+Vector3.down,Color.red,.1f);
                Debug.DrawLine(contactPos+Vector3.left,contactPos+Vector3.right,Color.red,.1f);

                Vector2 localContact=contact.point-new Vector2(transform.position.x,transform.position.y)+rig.velocity*Time.fixedDeltaTime;
                Vector2 RotatedContact = new Vector2(Vector2.Dot(localContact,horizonDirection),Vector2.Dot(localContact,verticalDirection));
                if(Mathf.Abs(RotatedContact.y)>GroundThreshold*transform.localScale.y){
                    if(RotatedContact.y<0){
                        groundsCnt.down++;
                    }else{
                        groundsCnt.up++;
                    }
                }else if(Mathf.Abs(RotatedContact.x)>WallThreshold*transform.localScale.x){
                    if(RotatedContact.x<0){
                        groundsCnt.left++;
                    }else{
                        groundsCnt.right++;
                    }
                }
            }
        
        // }
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.transform.CompareTag("Ground")){
            GroundCnt++;
        }
    }
    void OnCollisionExit2D(Collision2D collision2D){
        if(collision2D.transform.CompareTag("Ground")){
            GroundCnt--;
        }
    }
}
