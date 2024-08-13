using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        me = transform.Find("meCamera").GetComponent<Camera>(); 
        Debug.Log(me);
        rb.velocity = new Vector3(2, 0, 0);
        StartCoroutine(RotateCamera(1f,55));
        //collider added will cause parent and children become spaceships

    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    IEnumerator RotateCamera(float Time,float ang ){
        int n = 0;
       
        double angle = System.Math.Pow(-1, n)*ang;
        me.transform.Rotate(0, (float)angle, 0);
        n++;
       yield return new WaitForSeconds(Time);

    }

}