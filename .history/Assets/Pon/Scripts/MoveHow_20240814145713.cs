using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHow : MonoBehaviour
{
    public GameObject pon;
    public GameObject target;
    public Vector3 vel;
    
    private float HorizontalInput; 
    private float VerticalInput;


    void Awake()    {
        vel = new Vector3(1,0,0);
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }
    // Start is called before the first frame update
    void Start()
    {
        pon.GetComponent<Rigidbody>().velocity  = vel;
    }

    // Update is called once per frame
    void Update()
    {
        TarHandler(1.0f);
    }

    void TarHandler(float speed){
        Vector3 movement = new Vector3(HorizontalInput, 0.0f, VerticalInput);
        target.GameObject.transform.position += movement * speed * Time.deltaTime;
    }
}
