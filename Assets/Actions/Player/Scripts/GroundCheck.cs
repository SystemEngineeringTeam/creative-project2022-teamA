using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "Ground";    // Tag名
    private bool isGround,isGroundEnter,isGroundStay,isGroundExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGround(){
        if(isGroundEnter || isGroundStay){
            isGround = true;
        }else if(isGroundExit){
            isGround = false;
        }
        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }
    public bool EnterGround(){
        return isGroundEnter;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定内に入った");
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定に入ったまま");
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定から出た");
            isGroundExit = true;
        }
    }
}
