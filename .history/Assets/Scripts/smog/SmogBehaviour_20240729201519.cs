using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Camera me;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        me = GetComponent<Camera>();
        Debug.Log(me);
        rb.velocity = new Vector3(2, 0, 0);
        //collider added will cause parent and children become spaceships

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}