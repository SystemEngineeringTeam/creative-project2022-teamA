using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimickManager : MonoBehaviour
{
    private GameObject[] FoceObjs;
    // Start is called before the first frame update
    void Start()
    {
        FoceObjs = GameObject.FindGameObjectsWithTag("ForceObj");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FoceSwich(float force){
        foreach (GameObject FoceObj in FoceObjs) {
            FoceObj.GetComponent<Rigidbody2D>().velocity= new Vector2(force,0); 
            // Debug.Log("力が加わっているはず…");
        }
    }
}
