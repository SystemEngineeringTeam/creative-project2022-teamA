using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_destroy : MonoBehaviour
{
    [Header("消える時間")] public float deleteTime = 5.0f;
    private float time = 0.0f;

 

    void Update()
    {
        time += Time.deltaTime;
        if (time >= deleteTime)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // 地面に衝突したら自オブジェクト削除
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);

            // Playerと衝突したら、自オブジェクトとPlayerオブジェクトを削除
        }
        else if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }


}

