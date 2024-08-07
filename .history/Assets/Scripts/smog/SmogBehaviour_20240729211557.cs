using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SmogBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Camera me;
    private float Timer; private int rotateHistory;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        me = transform.Find("meCamera").GetComponent<Camera>(); 
        Debug.Log(me);
        rb.velocity = new Vector3(2, 0, 0);
        Timer = 0;rotateHistory = 0;
        //collider added will cause parent and children become spaceships

    }

    // Update is called once per frame
    void Update()
    {
       Timer += Time.deltaTime;
       if(Timer > 1f){  
       RotateCamera(new float[]{55,90,125},me);
       Debug.Log(rotateHistory);
       Timer = 0;}
    }

    void RotateCamera(float[] ang,Camera me){
        me.transform.rotation = Quaternion.Euler(0, ang[rotateHistory], 0);
        rotateHistory++; if(rotateHistory==ang.Length){rotateHistory=0;}
       }

}
