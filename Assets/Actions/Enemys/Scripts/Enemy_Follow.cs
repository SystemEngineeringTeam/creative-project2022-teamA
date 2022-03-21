using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pv = player.transform.position;
        Vector3 ev = transform.position;

        float p_vX = pv.x - ev.x;
        float p_vY = pv.y - ev.y;

        float vx = 0f;
        float vy = 0f;

        float sp = 10f;

        // 減算した結果がマイナスであればXは減算処理
        if (p_vX < 0)
        {
            vx = -sp;
        }
        else
        {
            vx = sp;
        }

        // 減算した結果がマイナスであればYは減算処理
        if (p_vY < 0)
        {
            vy = -sp;
        }
        else
        {
            vy = sp;
        }

        transform.Translate(vx / 50, vy / 50, 0);

    }

}