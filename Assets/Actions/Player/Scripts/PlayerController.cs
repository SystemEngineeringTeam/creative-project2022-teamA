using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // インスペクターで変更可能
    public float speed; //速度
    public float jumpSpeed; //ジャンプの速度
    public float gravity; //重力
    public GroundCheck ground; //接地判定

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 接地判定受け取り
        isGround = ground.IsGround();

        // Unityで設定されている横移動用のキーを取得
        // この値が1なら右入力がされていて、‐1なら左入力がされている
        // 0なら何も入力されていない
        // コントローラーなどのスティック操作の場合、スティックの倒し加減で小数を取るためfloat
        float horizontalKey = Input.GetAxis("Horizontal");

        // Unityで設定されている上下移動用のキーを取得
        // この値が1なら上入力がされていて、‐1なら下入力がされている
        float verticalKey = Input.GetAxis("Vertical");

        float xSpeed = 0.0f;

        if(horizontalKey > 0){ //右移動
            transform.localScale = new Vector3(1,1,1);
            anim.SetBool("run_flag",true);
            xSpeed = speed;

        }else if(horizontalKey < 0){ //左移動
            transform.localScale = new Vector3(-1,1,1);
            anim.SetBool("run_flag",true);
            xSpeed = -speed;

        }else{
            anim.SetBool("run_flag",false);
            xSpeed = 0.0f;
        }

        if(isGround){
            if(verticalKey > 0){ //ジャンプ
                isJump = true;
                jumpSpeed = 40.0f;
            }else{
                isJump = false;
            }
        }else if(isJump){
            jumpSpeed *= 0.5f;
        }else{
            jumpSpeed = 0.0f;
        }

        rb.velocity = new Vector2(xSpeed,rb.velocity.y);
        rb.AddForce(new Vector2(0,jumpSpeed-gravity));
    }
}
