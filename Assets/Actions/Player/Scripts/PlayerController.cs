using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 空中ジャンプ追加？
// ジャンプボタンの押す長さでジャンプの高さが変わるようにする
// ダッシュ追加
// ローリング追加

// node11.agames.jp:4062
// 117.102.213.104:4063

public class PlayerController : MonoBehaviour
{
	[Header("インスタンス")]
    public GroundCheck ground; //接地判定
	public WallJump wall; //壁ジャンプ判定

	[Header("移動値")]
    public float jumpForce = 680f;       // ジャンプ時に加える力
	// ジャンプ力は4.5ブロック分くらい
	public float runSpeed = 10.0f;       // 走っている間の速度
	public float walkSpeed = 5.0f;       // 歩いている間の速度
	[Header("↓ズサーの速度")]
	public float wallDownSpeed = -4.0f;   //ズサー



    private Animator anim = null;
    private Rigidbody2D rb = null;



	private int key = 0;                 // 左右の入力管理
    private string state;                // プレイヤーの状態管理
	private string prevState;            // 前の状態を保存
    private bool isGround = true;        // 地面と接地しているか管理するフラグ
	private bool isWall = true;        // 壁と接しているか管理するフラグ
	private bool jumpKeyDown = false; //ジャンプボタンを押した瞬間を管理
	private bool jumpKey = false; //ジャンプボタンを押してる間を管理
	private bool jumpKeyUp = false; //ジャンプボタンを離した瞬間を管理
	private float jumpTimer = 0;   //ジャンプボタンを押した秒数を記録するためのタイマー
	private float runTimer = 0;   //方向キーを"素早く"二回連続押したことを記録するためのタイマー
	private bool runTimer_flag = false;  //方向キーを"素早く"二回連続押したか判定するためのフラグ
	private bool runFlag = false;     // 走り状態かどうか判定するフラグ
	private int tmp = 0;
	private float speed = 0.0f;   //移動スピードを代入する（歩きか走りのスピードを代入）
	private bool wallJumpFlag = false;  // 壁ジャンの慣性を保つため



    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update
	void Update(){
		// keyの押下(特にKeyDown)はUpdateで判定(FixedUpdateでは絶対に行ってはいけない。)
		if(runTimer_flag){
			runTimer += Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			jumpKeyDown = true;
		}else if(Input.GetKey(KeyCode.Space)){
			jumpKey = true;
		}else if(Input.GetKeyUp(KeyCode.Space)){
			jumpKeyUp = true;
		}

		

		if (Input.GetKeyUp (KeyCode.RightArrow)||Input.GetKeyUp (KeyCode.D)){
			runTimer_flag = true;
			// runFlag = false;
			tmp = 1;
		}else if(Input.GetKeyUp (KeyCode.LeftArrow)||Input.GetKeyUp (KeyCode.A)){
			runTimer_flag = true;
			// runFlag = false;
			tmp = -1;
		}

		if(!Input.GetKey(KeyCode.RightArrow)&&!Input.GetKey(KeyCode.D)&&!Input.GetKey(KeyCode.LeftArrow)&&!Input.GetKey(KeyCode.A)){
			// 何のキーも押してないとき
			runFlag = false;
		}

		if(ground.EnterGround()){
			jumpTimer = 0.0f;
			jumpKeyDown = false;
			jumpKey = false;
			jumpKeyUp = false;
		}
	}


    void FixedUpdate(){
		GetInputKey ();
        ChangeState();
		ChangeAnimation();
		Move();
    }

	void GetInputKey(){
		key = 0;
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)){
			key = 1;
			if(tmp == key){
				if(runTimer > 0 && runTimer < 0.2){
					runFlag = true;
				}
				runTimer_flag = false;
			}
			runTimer = 0.0f;
		}
		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey (KeyCode.A)){
			if(key == 1){
				key = 0;
			}else{
				key = -1;
				if(tmp == key){
					if(runTimer > 0 && runTimer < 0.2){
						runFlag = true;
					}
					runTimer_flag = false;
				}
				runTimer = 0.0f;
			}
		}
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
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
			//待機中
			} else {
				state = "IDLE";
			}

		}
		// else if(isWall){
		// // 壁ジャンプ可能な状態（壁にくっついてる状態）

		// }
		else{
		// 空中にいる場合
			// 上昇中
			if(rb.velocity.y > 0){
				state = "JUMP";
				if(key != 0){
					transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				}
			// 下降中
			} else if(rb.velocity.y < 0) {
				state = "FALL";
				if(key != 0){
					transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", true);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					break;
				case "FALL":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", true);
					anim.SetBool ("rolling_flag", false);
					break;
				case "RUN":
					anim.SetBool ("run_flag", true);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					break;
				case "WALK":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", true);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					break;
				case "ROLLING":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", true);
					break;
				case "ATTACK":
					// 攻撃モーションが追加されたらここに
				default:
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					break;
			}
			// 状態の変更を判定するために状態を保存しておく
			prevState = state;
		}
    }

    void Move(){
		// 接地してる時にSpaceキー押下でジャンプ
		if(isGround){
			wallJumpFlag = false;
			if (jumpKeyDown) {
				jumpTimer = 0.0f;
				rb.velocity = new Vector2(rb.velocity.x,0);
				rb.AddForce (transform.up * this.jumpForce);

				isGround = false;
				isWall = false;
				jumpKeyDown = false;
			}
		}else if(isWall){
			if(jumpKeyDown){
				wallJumpFlag = true;
				rb.velocity = new Vector2(0,0);
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				// 壁ジャンプで向きを反転

				rb.AddForce (new Vector2(transform.localScale.x * 300,this.jumpForce));
				// 斜め上方向にジャンプ
				
				jumpKeyDown = false;
				isWall = false;
			}
		}
		
		// 長押しジャンプ処理
		if(jumpKey && !jumpKeyUp && jumpTimer < 0.3f && rb.velocity.y > 0){
			rb.AddForce (transform.up * this.jumpForce * Time.fixedDeltaTime * 2);
			jumpTimer += Time.fixedDeltaTime;
		}

		if(runFlag){
			speed = runSpeed;
		}else{
			speed = walkSpeed;
		}
		
		// 左右の移動
		if(!isWall){
			// 壁にいない時
			if(key != 0){
				// 入力あり
				if(isGround){
					// 壁にいなくて地面にいるとき
					transform.localScale = new Vector3 (key*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
					rb.velocity = new Vector2(key*speed, rb.velocity.y);		
				}else{
					// 壁にいなくて地面にいないとき（空中）
					rb.velocity = new Vector2(key*speed, rb.velocity.y);
				}
			}else if(key == 0){
				// 入力無しの時
				if(!wallJumpFlag){
					// 壁ジャンプしてないとき
					rb.velocity = new Vector2(0, rb.velocity.y);	
				}
			}
		}else if(isGround){
			// 壁に付いてて地面にも付いてるとき
			if(key == 1){
				transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}else if(key == -1){
				transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
		}else{
			// 壁にいて地面にいないとき
			if(key != 0 && rb.velocity.x <= 0 && rb.velocity.y <= 0){
				// 方向入力してるけど移動していない、かつ下に落ちてるとき（壁に向かって進んでるとき）
				transform.localScale = new Vector3 (key*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				rb.velocity = new Vector2(rb.velocity.x, wallDownSpeed);
			}else if(key != 0){
				transform.localScale = new Vector3 (key*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				rb.velocity = new Vector2(key*speed, rb.velocity.y);
			}
		}
	}
}
