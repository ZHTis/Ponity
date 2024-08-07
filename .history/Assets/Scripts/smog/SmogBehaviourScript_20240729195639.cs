using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogBehaviourScript : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        rb.isKinematic = true;
        rb.velocity = new Vector3(20, 0, 0);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
