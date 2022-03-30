using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaToPlayer : MonoBehaviour
{

    //プレイヤーオブジェクト
    public GameObject player;
    //弾のプレハブオブジェクト
    public GameObject tama;

    [Header("弾の速さ")][Range(1,70)] public float BulletSpeed = 1.0f;

    //一秒ごとに弾を発射するためのもの
    [Header("弾を打つ間隔")][Range(0, 1)] public float targetTime = 0.5f;
    private float currentTime = 0;



    // Update is called once per frame
    void Update()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        float dist = -1;
        foreach(GameObject p in players)
        {
            float d = Vector3.Distance(p.transform.position, transform.position);
            if (dist == -1 || dist > d)
            {
                dist = d;
                player = p;
            }
        }
        //一秒経つごとに弾を発射する
        if(player!=null){
            currentTime += Time.deltaTime;
            if (targetTime < currentTime)
            {
                currentTime = 0;
                //敵の座標を変数posに保存
                var pos = this.gameObject.transform.position;
                //弾のプレハブを作成
                var t = Instantiate(tama) as GameObject;
                //弾に攻撃のプログラムを注入
                t.GetComponent<Hit_destroy>().mob = GetComponent<MobBehaviour>();
                //弾のプレハブの位置を敵の位置にする
                t.transform.position = pos;
                //敵からプレイヤーに向かうベクトルをつくる
                //プレイヤーの位置から敵の位置（弾の位置）を引く
                Vector2 vec = player.transform.position - pos;
                //弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
                t.GetComponent<Rigidbody2D>().velocity = vec.normalized * BulletSpeed;
            }
        }
        
    }
}