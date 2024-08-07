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
        me = transform.Find("meCamera").GetComponent<Camera>(); 
        Debug.Log(me);
        rb.velocity = new Vector3(2, 0, 0);
        StartCoroutine(wait(1.5f));
        //collider added will cause parent and children become spaceships

    }

    // Update is called once per frame
    void Update()
    {
       
       
      
    }

    private IEnumerator wait(float time){
        me.transform.Rotate(0, 55, 0);
        yield return new WaitForSeconds(time);
    }
}
