using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 空中ジャンプ、壁ジャンプに回数制限、もしくは壁と反対に飛んでくように
// ダッシュ追加
// ローリング追加

// node11.agames.jp:4062
// 117.102.213.104:4063

public class PlayerController : MonoBehaviour
{
    // インスペクターで変更可能
    public GroundCheck ground; //接地判定
	public WallJump wall; //壁ジャンプ判定
    public float jumpForce = 1000f;       // ジャンプ時に加える力
	public float jumpThreshold = 22f;    // ジャンプ中か判定するための閾値
	public float runForce = 1.5f;       // 走り始めに加える力
	public float runSpeed = 0.5f;       // 走っている間の速度
	public float runThreshold = 2.2f;   // 速度切り替え判定のための閾値


    private Animator anim = null;
    private Rigidbody2D rb = null;


	private int key = 0;                 // 左右の入力管理
    private string state;                // プレイヤーの状態管理
	private string prevState;            // 前の状態を保存
	private float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数
    private bool isGround = true;        // 地面と接地しているか管理するフラグ
	private bool isWall = true;        // 壁と接しているか管理するフラグ

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
		GetInputKey ();
        ChangeState();
		ChangeAnimation();
		Move();
    }

	void GetInputKey(){
		key = 0;
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D))
			key = 1;
		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey (KeyCode.A))
			key = -1;
	}

    void ChangeState(){
		// 接地判定受け取り
        isGround = ground.IsGround();
		isWall = wall.IsWall();

		// 接地している場合
		if(isGround){
			// 移動中
			if (key != 0) {
				state = "RUN";
				transform.localScale = new Vector3 (key, 1, 1);
			//待機中
			} else {
				state = "IDLE";
			}

		}else if(isWall){
		// 壁ジャンプ可能な状態（壁にくっついてる状態）
			
		}else{
		// 空中にいる場合
			// 上昇中
			if(rb.velocity.y > 0){
				state = "JUMP";
				if(key != 0){
					transform.localScale = new Vector3 (key, 1, 1);
				}
			// 下降中
			} else if(rb.velocity.y < 0) {
				state = "FALL";
				if(key != 0){
					transform.localScale = new Vector3 (key, 1, 1);
				}
			}
		}
	}

    void ChangeAnimation(){
        if (prevState != state) {
			// Debug.Log(state);
			switch (state) {
				case "JUMP":
					anim.SetBool ("run_flag", false);
					// anim.SetBool ("isFall", true);
					// anim.SetBool ("isJump", false);
					// anim.SetBool ("isIdle", false);
					stateEffect = 0.5f;
					break;
				case "FALL":
					anim.SetBool ("run_flag", false);
					// anim.SetBool ("isFall", true);
					// anim.SetBool ("isJump", false);
					// anim.SetBool ("isIdle", false);
					stateEffect = 0.5f;
					break;
				case "RUN":
					anim.SetBool ("run_flag", true);
					// anim.SetBool ("isFall", false);
					// anim.SetBool ("isJump", false);
					// anim.SetBool ("isIdle", false);
					stateEffect = 1f;
					
					break;
				default:
					anim.SetBool ("run_flag", false);
					// // anim.SetBool ("isIdle", true);
					// anim.SetBool ("isFall", false);
					// anim.SetBool ("isJump", false);
					stateEffect = 1f;
					break;
			}
			// 状態の変更を判定するために状態を保存しておく
			prevState = state;
		}
    }

    void Move(){
		// 接地してる時にSpaceキー押下でジャンプ
		float speedX = Mathf.Abs (this.rb.velocity.x);

		if(isGround){
			if (Input.GetKeyDown(KeyCode.Space)) {
				// // 以下を追加するとジャンプ中にも方向転換可能に
				// if(key != 0){
				// 	speedX = Mathf.Abs (this.rb.velocity.x);
				// 	if (speedX < this.runThreshold) {
				// 		this.rb.AddForce (transform.right * key * this.runForce * stateEffect);
				// 	} else {
				// 		this.transform.position += new Vector3 (runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
				// 	}
				// }

				rb.velocity = new Vector2(0,0);
				rb.AddForce (transform.up * this.jumpForce);
				isGround = false;
				// Debug.Log("ground");
			}
		}else if(isWall){
			// ズサーを追加したい
			if (Input.GetKeyDown(KeyCode.Space)){
				rb.velocity = new Vector2(0,0);
				transform.localScale = new Vector3 (-transform.localScale.x, 1, 1);
				// 壁ジャンプで向きを反転
				rb.AddForce (new Vector2(transform.localScale.x * 150,(this.jumpForce/4)*3));
				// 斜め上方向にジャンプ
				
				isWall = false;
				// Debug.Log("wall");
			}
		}
		
		if(!isWall){
			// 左右の移動
			speedX = Mathf.Abs (this.rb.velocity.x);
			if (speedX < this.runThreshold) {
				this.rb.AddForce (transform.right * key * this.runForce * stateEffect);
			} else {
				this.transform.position += new Vector3 (runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
			}
		}
	}
}
