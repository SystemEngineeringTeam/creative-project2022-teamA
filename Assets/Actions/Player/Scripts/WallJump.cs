using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private string groundTag = "Ground";    // Tag名
    private bool isWall,isWallEnter,isWallStay,isWallExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsWall(){
        // Debug.Log(isWallEnter+":"+isWallStay+":"+isWallExit+" // isWall:"+isWall);
        if(isWallExit){
            isWall = false;
        }else if(isWallEnter || isWallStay){
            isWall = true;
        }
        isWallEnter = false;
        isWallStay = false;
        isWallExit = false;
        return isWall;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定内に入った");
            isWallEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定に入ったまま");
            isWallStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == groundTag){
            // Debug.Log("判定から出た");
            isWallStay = false;
            // ↑これがないと壁ジャンプ連打でバグる
            isWallExit = true;
        }
    }
}

