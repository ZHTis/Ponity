using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class collideCoin : MonoBehaviour
{
    private bool bowlFullfilled = false;
    private int bowlFilledWith = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "CubeTar") {
            Debug.Log("collided");
        }
    }

    
}
