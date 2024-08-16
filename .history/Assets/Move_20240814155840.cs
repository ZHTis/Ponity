using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    private float HorizontalInput; 
    private float VerticalInput;

    // Start is called before the first frame update
    void Start()
    {HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
       Vector3 movement = new Vector3(HorizontalInput,0f, VerticalInput);
        transform.Translate(movement* speed * Time.deltaTime) ;
        if (Input.GetKey("a")||Input.GetKey("d")||Input.GetKey("w")||Input.GetKey("s")){
            Denug.Log(Input.GetAxis)
        }
    }
}
