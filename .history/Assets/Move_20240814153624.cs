using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    private float HorizontalInput; 
    private float VerticalInput;

    // Start is called before the first frame update
    void Start()
    {HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
       Vector3 movement = new Vector3(HorizontalInput,0f, VerticalInput);
        transform.Translate(movement* speed * Time.deltaTime) ;
    }
}
