using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "coin(Clone)") {
            Debug.Log("hit");
        }
    }
}
