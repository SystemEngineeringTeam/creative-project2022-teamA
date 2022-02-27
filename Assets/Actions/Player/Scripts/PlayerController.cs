using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// node11.agames.jp:4062
// 117.102.213.104:4063

public class PlayerController : MonoBehaviour
{
    // インスペクターで変更可能
    public GroundCheck ground; //接地判定
    public float jumpForce = 22f;       // ジャンプ時に加える力
	public float jumpThreshold = 1f;    // ジャンプ中か判定するための閾値
	public float runForce = 1.5f;       // 走り始めに加える力
	public float runSpeed = 0.5f;       // 走っている間の速度
	public float runThreshold = 2.2f;   // 速度切り替え判定のための閾値


    private Animator anim = null;
    private Rigidbody2D rb = null;

    private string state;                // プレイヤーの状態管理
	private string prevState;            // 前の状態を保存
	private float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数
    private bool isGround = true;        // 地面と接地しているか管理するフラグ

    // Unityで設定されている横移動用のキーを取得
    // この値が1なら右入力がされていて、‐1なら左入力がされている
    // 0なら何も入力されていない
    // コントローラーなどのスティック操作の場合、スティックの倒し加減で小数を取るためfloat
    float horizontalKey;

    // Unityで設定されている上下移動用のキーを取得
    // この値が1なら上入力がされていて、‐1なら下入力がされている
    float verticalKey;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        ChangeState();
		ChangeAnimation();
		Move();
    }

    void ChangeState(){
		// 接地判定受け取り
        isGround = ground.IsGround();

        // 入力方向受け取り
        horizontalKey = Input.GetAxis("Horizontal");
        verticalKey = Input.GetAxis("Vertical");
 
		// 接地している場合
		if (isGround) {
			// 移動中
			if (horizontalKey != 0) {
				state = "RUN";
			//待機中
			} else {
				state = "IDLE";
			}
		// 空中にいる場合
		} else  {
			// 上昇中
			if(rb.velocity.y > 0){
				state = "JUMP";
			// 下降中
			} else if(rb.velocity.y < 0) {
				state = "FALL";
			}
		}
	}

    void ChangeAnimation(){
        if (prevState != state) {
			switch (state) {
			case "JUMP":
				// anim.SetBool ("isFall", true);
				// anim.SetBool ("isJump", false);
				anim.SetBool ("run_flag", false);
				// anim.SetBool ("isIdle", false);
				stateEffect = 0.5f;
				break;
			case "FALL":
				// anim.SetBool ("isFall", true);
				// anim.SetBool ("isJump", false);
				anim.SetBool ("run_flag", false);
				// anim.SetBool ("isIdle", false);
				stateEffect = 0.5f;
				break;
			case "RUN":
				anim.SetBool ("run_flag", true);
				// anim.SetBool ("isFall", false);
				// anim.SetBool ("isJump", false);
				// anim.SetBool ("isIdle", false);
				stateEffect = 1f;
				
                if(Input.GetAxis("Horizontal") > 0){
                    transform.localScale = new Vector3 (Input.GetAxis("Horizontal"), 1, 1); // 向きに応じてキャラクターのspriteを反転
                }
				
				break;
			default:
				// // anim.SetBool ("isIdle", true);
				// anim.SetBool ("isFall", false);
				anim.SetBool ("run_flag", false);
				// anim.SetBool ("isJump", false);
				stateEffect = 1f;
				break;
			}
			// 状態の変更を判定するために状態を保存しておく
			prevState = state;
		}
    }

    void Move(){
		// 設置している時にSpaceキー押下でジャンプ
		if (isGround) {
			if (verticalKey < 0) {
				rb.AddForce (transform.up * this.jumpForce);
				isGround = false;
			}
		}
 
		// 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
		float speedX = Mathf.Abs (this.rb.velocity.x);
		if (speedX < this.runThreshold) {
			this.rb.AddForce (transform.right * horizontalKey * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
		} else {
			this.transform.position += new Vector3 (runSpeed * Time.deltaTime * horizontalKey * stateEffect, 0, 0);
		}
	
	}
}
