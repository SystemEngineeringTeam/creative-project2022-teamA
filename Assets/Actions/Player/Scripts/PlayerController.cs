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
    // インスペクターで変更可能
    public GroundCheck ground; //接地判定
	public WallJump wall; //壁ジャンプ判定
    public float jumpForce = 700f;       // ジャンプ時に加える力
	public float runSpeed = 10.0f;       // 走っている間の速度
	public float runThreshold = 2.2f;   // 速度切り替え判定のための閾値
	public float timer = 0;

    private Animator anim = null;
    private Rigidbody2D rb = null;


	private int key = 0;                 // 左右の入力管理
    private string state;                // プレイヤーの状態管理
	private string prevState;            // 前の状態を保存
    private bool isGround = true;        // 地面と接地しているか管理するフラグ
	private bool isWall = true;        // 壁と接しているか管理するフラグ
	private bool isJump = false;      // ジャンプしている最中か管理するフラグ
	public bool jumpKeyDown = false; //ジャンプボタンを押した瞬間を管理
	private bool jumpKey = false; //ジャンプボタンを押してる間を管理
	private bool jumpKeyUp = false; //ジャンプボタンを離した瞬間を管理

	private int tmp = 0;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update
	void Update(){
		// keyの押下はUpdateで判定(FixedUpdateでは絶対に行ってはいけない。)

		if(Input.GetKeyDown(KeyCode.Space)){
			jumpKeyDown = true;
		}else if(Input.GetKey(KeyCode.Space)){
			jumpKey = true;
		}else if(Input.GetKeyUp(KeyCode.Space)){
			jumpKeyUp = true;
		}

		if(ground.EnterGround()){
			timer = 0.0f;
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
		if(key != 0){
			tmp = key;
		}
		key = 0;
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)){
			key = 1;
		}
		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey (KeyCode.A)){
			key = -1;
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

		}else if(isWall){
		// 壁ジャンプ可能な状態（壁にくっついてる状態）

		}else{
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
					break;
				case "FALL":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", true);
					break;
				case "RUN":
					anim.SetBool ("run_flag", true);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					break;
				case "WALK":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", true);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					break;
				default:
					anim.SetBool ("run_flag", false);
					anim.SetBool ("walk_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					break;
			}
			// 状態の変更を判定するために状態を保存しておく
			prevState = state;
		}
    }

    void Move(){
		// 接地してる時にSpaceキー押下でジャンプ
		if(isGround){
			if (jumpKeyDown) {
				timer = 0.0f;
				rb.velocity = new Vector2(rb.velocity.x,0);
				rb.AddForce (transform.up * this.jumpForce);

				isGround = false;
				isWall = false;
				jumpKeyDown = false;
			}
		}else if(isWall){
			if(jumpKeyDown){
				rb.velocity = new Vector2(0,0);
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				// 壁ジャンプで向きを反転

				rb.AddForce (new Vector2(transform.localScale.x * 150,(this.jumpForce/4)*3));
				// 斜め上方向にジャンプ
				// 進みたい方向キーを入力しながら壁ジャンプすると、壁ジャンプの飛距離が増加
				
				jumpKeyDown = false;
				isWall = false;
				// Debug.Log("wall");
			}
		}
		
		// 長押しジャンプ処理
		if(jumpKey && !jumpKeyUp && timer < 0.3f && rb.velocity.y > 0){
			rb.AddForce (transform.up * this.jumpForce * Time.deltaTime * 2);
			timer += Time.deltaTime;
		}
		
		// 左右の移動
		if(!isWall){
			// 壁にいない時

			if(key == 1){
				// 右を入力している時
				if(isGround){
					// 壁にいなくて地面にいるとき
					transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
					rb.velocity = new Vector2(runSpeed, rb.velocity.y);
				}else{
					// 壁にいなくて地面にいないとき（空中）
					rb.velocity = new Vector2(runSpeed/2, rb.velocity.y);
				}
			}else if(key == -1){
				if(isGround){
					transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
					rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
				}else{
					rb.velocity = new Vector2(-runSpeed/2, rb.velocity.y);
				}
			}else{
				// 入力無しの時
				if(isGround){
					// 地面にいるとき
					rb.velocity = new Vector2(0, 0);
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
			// 壁にいて地面にいないとき（壁をズサーしてるとき）
			if(rb.velocity.y < 0){
				rb.velocity = new Vector2(rb.velocity.x, -5.0f);
			}
		}
	}
}
