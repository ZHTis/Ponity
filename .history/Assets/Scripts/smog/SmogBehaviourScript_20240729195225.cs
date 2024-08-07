using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = new Vector3(20, 0, 0);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
