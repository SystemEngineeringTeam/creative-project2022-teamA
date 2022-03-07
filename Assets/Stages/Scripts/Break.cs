using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public int hp;
    void Start() {
        DamageWall(11);
    }

    // Update is called once per frame
    void Update()
    {
        // this.GetComponent<ParticleSystem>().Play(); test
    }
    public void DamageWall(int loss) {
        hp -= loss;

        this.GetComponentInChildren<ParticleSystem>().Play();

        if (hp <= 0) {
            gameObject.SetActive(false);
        }
    }
}
