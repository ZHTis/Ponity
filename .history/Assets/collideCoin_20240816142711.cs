using System.Collections;
using System.Collections.Generic;
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

    void OnCollisionEnter(Collider other) {
        if (other.gameObject.name == "CubeTar") {
            bowlFilledWith++;
            if (bowlFilledWith == 5) { 
                bowlFullfilled = true;
                Debug.Log("bowl filled with 5 coins");
        }
    }}

}
