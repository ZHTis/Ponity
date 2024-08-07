using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SmogBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Camera me;
    private float Timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        me = transform.Find("meCamera").GetComponent<Camera>(); 
        Debug.Log(me);
        rb.velocity = new Vector3(2, 0, 0);
        //collider added will cause parent and children become spaceships

    }

    // Update is called once per frame
    void Update()
    {
       Timer += Time.deltaTime;
       
    }

    void RotateCamera(float Time,float ang ,int n){
        if(Timer > Time)
       {
         double angle = System.Math.Pow(-1, n)*ang;
          me.transform.Rotate(0, (float)angle, 0);
          Timer = 0;
       }

    }

}
