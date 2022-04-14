using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public int hp;
    public GameObject efectA,efectB;
    void Start() {
        // DamageWall(11); test
    }
    // Update is called once per frame
    public void DamageWall(int loss) {
        hp -= loss;
        //パーティクル生成
        Instantiate(efectA, this.transform.position, Quaternion.identity);
        Debug.Log("壁"+loss+"のダメージ");
        if (hp <= 0) {
            Instantiate(efectA, this.transform.position, Quaternion.identity);
            Instantiate(efectB, this.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Debug.Log("壁は壊れた");
        }
    }
}
