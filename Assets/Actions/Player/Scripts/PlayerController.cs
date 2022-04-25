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
    public GroundCheck ground; 			// 接地判定
	public WallJump wall; 				// 壁ジャンプ判定
	public KeyConfig keyConfig; 		// キーコンフィグ
	public GameObject attackBox;        //AttackBox

	[Header("移動値")]
    public float jumpForce = 680f;       // ジャンプ時に加える力
	// ジャンプ力は4.5ブロック分くらい
	public float runSpeed = 10.0f;       // 走っている間の速度
	public float dashPower = 20.0f;      // ダッシュのパワー
	[Header("↓ズサーの速度")]
	public float wallDownSpeed = -4.0f;  // ズサー



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
	private float dashTimer = 0;   //方向キーを"素早く"二回連続押したことを記録するためのタイマー
	private bool dashTimer_flag = false;  //方向キーを"素早く"二回連続押したか判定するためのフラグ
	private bool canDashFlag = false;     // ダッシュできるかどうか判定するフラグ
	private bool dashFlag = false;     // ダッシュ状態かどうか判定するフラグ
	private bool runFlag = true;     // 走り状態を制御するためのフラグ
	private int tmp = 0;
	private float speed = 0.0f;   //移動スピードを代入する（歩きか走りのスピードを代入）
	private bool wallJumpFlag = false;  // 壁ジャンの慣性を保つため
	private bool normalAttackFlag = false;    //通常攻撃のフラグ
	private bool canInputKeyFlag = true;  //キー入力を受け付けるフラグ



    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update
	void Update(){
		if(canInputKeyFlag){
			GetInputKey();
			// keyの押下(特にKeyDown)はUpdateで判定(FixedUpdateでは絶対に行ってはいけない。)
		}

		if(dashTimer_flag){
			dashTimer += Time.deltaTime;
		}

		if(!keyConfig.right.Stay()&&!keyConfig.left.Stay()){
			// 何のキーも押してないとき
			canDashFlag = false;
		}

		if(ground.EnterGround()){
			jumpTimer = 0.0f;
			jumpKeyDown = false;
			jumpKey = false;
			jumpKeyUp = false;
		}
	}

	void GetInputKey(){
		key = 0;
		if (keyConfig.right.Down() || keyConfig.right.Stay()){
			key = 1;
			if(tmp == key){
				if(dashTimer > 0 && dashTimer < 0.2){
					canDashFlag = true;
				}
				dashTimer_flag = false;
			}
			dashTimer = 0.0f;
		}
		if (keyConfig.left.Stay()){
			if(key == 1){
				key = 0;
			}else{
				key = -1;
				if(tmp == key){
					if(dashTimer > 0 && dashTimer < 0.2){
						canDashFlag = true;
					}
					dashTimer_flag = false;
				}
				dashTimer = 0.0f;
			}
		}

		if(keyConfig.attack.Down() && isGround && !anim.GetBool("jump_up_flag") && !anim.GetBool("jump_down_flag") ){
			// 通常攻撃キーを押下時　かつ　地面に接地しているとき　かつ　ジャンプ関連のアニメーションフラグが全てフォルスの時に
			// このフラグを立てる
			normalAttackFlag = true;
        }

		if(keyConfig.jump.Down()){
			jumpKeyDown = true;
		}else if(keyConfig.jump.Stay()){
			jumpKey = true;
		}else if(keyConfig.jump.Up()){
			jumpKeyUp = true;
		}

		if (keyConfig.right.Up()){
			dashTimer_flag = true;
			// canDashFlag = false;
			tmp = 1;
		}else if(keyConfig.left.Up()){
			dashTimer_flag = true;
			// canDashFlag = false;
			tmp = -1;
		}

	}


    void FixedUpdate(){
		if(!runFlag){
			// 空中ダッシュをしたときに重力を無くす
			rb.velocity = new Vector2(rb.velocity.x,0);
		}
		ChangeState();
		ChangeAnimation();
		Move();
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
		if(dashFlag){
			state = "DASH";
		}else if(normalAttackFlag){
			state = "ATTACK";
		}


	}

    void ChangeAnimation(){
		// Debug.Log(anim.GetBool ("run_flag")+":"+anim.GetBool ("dash_flag")+":"+anim.GetBool("jump_up_flag")+":"+anim.GetBool ("jump_down_flag")+":"+anim.GetBool ("attack_normal_flag"));
        if (prevState != state) {
			Debug.Log(state);
			switch (state) {
				case "JUMP":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", true);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", false);
					break;
				case "FALL":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", true);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", false);
					break;
				case "RUN":
					anim.SetBool ("run_flag", true);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", false);
					break;
				case "DASH":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", true);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", false);
					break;
				case "ROLLING":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", true);
					anim.SetBool ("attack_normal_flag", false);
					break;
				case "ATTACK":
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", true);
					Debug.Log("AnimAttack");
					break;
				default:
					anim.SetBool ("run_flag", false);
					anim.SetBool ("dash_flag", false);
					anim.SetBool ("jump_up_flag", false);
					anim.SetBool ("jump_down_flag", false);
					anim.SetBool ("rolling_flag", false);
					anim.SetBool ("attack_normal_flag", false);
					break;
			}
			// 状態の変更を判定するために状態を保存しておく
			prevState = state;
		}
    }

    void Move(){
		// ダッシュが可能状態である　かつ　ダッシュ中ではない　かつ　入力方向とプレイヤーの向きが同じ場合
		if(canDashFlag && !dashFlag && key == transform.localScale.x && key == tmp){
			Dash();
		}else{
			speed = runSpeed;
		}

		if(normalAttackFlag){
			NormalAttack();
		}else if(runFlag){

			if(isGround){
				// 接地してる時に連続ダッシュを可能に
				// 空中では一回のみ
				dashFlag = false;

				// 接地してる時にSpaceキー押下でジャンプ
				wallJumpFlag = false;
				if (jumpKeyDown) {
					jumpTimer = 0.0f;
					rb.velocity = new Vector2(rb.velocity.x,0);
					rb.AddForce (transform.up * this.jumpForce);

					isGround = false;
					isWall = false;
					jumpKeyDown = false;
				}

				// シフトキー押下で
				if(keyConfig.dash.Down()){
					Rolling();
				}

			}else if(isWall){
				if(jumpKeyDown){
					wallJumpFlag = true;
					// runFlag = false;
					rb.velocity = new Vector2(0,0);
					transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
					// 壁ジャンプで向きを反転

					rb.AddForce (new Vector2(transform.localScale.x * 500,this.jumpForce));
					// 斜め上方向にジャンプ

					Invoke("runFlagToTrue",0.3f);
					
					jumpKeyDown = false;
					isWall = false;
				}
			}
			
			// 長押しジャンプ処理
			if(jumpKey && !jumpKeyUp && jumpTimer < 0.3f && rb.velocity.y > 0){
				rb.AddForce (transform.up * this.jumpForce * Time.fixedDeltaTime * 2);
				jumpTimer += Time.fixedDeltaTime;
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

	void Rolling(){
		// f(x) = -40(x-0.5)^2 + 10 
		// runFlag = false;

	}

	void Dash(){
		runFlag = false;
		dashFlag = true;
		rb.velocity = new Vector2(0, 0);
		Vector2 force = new Vector2(key * dashPower,0);
		rb.AddForce (force,ForceMode2D.Impulse);
		Invoke("runFlagToTrue",0.3f);
		// Debug.Log("Dash中だよ！");
	}

	void NormalAttack(){
		canInputKeyFlag = false;
		runFlag = false;
		rb.velocity = new Vector2(0,0);
		attackBox.gameObject.SetActive(true);
	}
	
	void AttackAnimationCompleted(){
		canInputKeyFlag = true;
		runFlag = true;
		attackBox.gameObject.SetActive(false);
		if(anim.GetBool("attack_normal_flag")){
			anim.SetBool("attack_normal_flag",false);
		}
		normalAttackFlag = false;
	}

	void runFlagToTrue(){
		// 通常の移動モーションに遷移可能
		runFlag = true;
		canDashFlag = false;
		if(isGround){
			// 地面でダッシュをしたときは連続でダッシュが可能だが、空中では連続で行えないようにするため、
			// 地面にいる場合のみdashFlagを戻している
			dashFlag = false;
		}
	}
}
