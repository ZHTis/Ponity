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
        
    }

    void TarHandler(float speed){
        
    }
}
