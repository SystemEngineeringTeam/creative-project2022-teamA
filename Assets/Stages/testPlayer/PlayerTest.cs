using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    public float jumpForce = 390.0f;       // ジャンプ時に加える力

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        //右入力で左向きに動く
        if(horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //左入力で左向きに動く
        else if(horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //ボタンを話すと止まる
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            this.rb.AddForce(transform.up * jumpForce);
        }
    }
    // 攻撃されたら動く仮置き
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="BreakWall"){
            other.gameObject.GetComponent<Break>().DamageWall(1);
        }
    }
}
