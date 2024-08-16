using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveHow : MonoBehaviour
{
    public GameObject pon;
    public GameObject target;
    public Vector3 ponvel;
alInput;


    void Awake()    {
        ponvel = new Vector3(1,0,0);
        
    }
    // Start is called before the first frame updatedadsawwsadddasdsadsdddadwcvdsddddddddd
    void Start()
    {
        pon.GetComponent<Rigidbody>().velocity  = ponvel;
    }

    // Update is called once per frame
    void Update()
    {
        TarHandler(5f);
    }

    void TarHandler(float speed){
        if( Input.GetKeyDown("a") ){
            target.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
        if(InputInput.GetKey("d")){}

        if(Input.GetKey("w") ){}

        if( Input.GetKey("s") ){
            
        }
    }
}
