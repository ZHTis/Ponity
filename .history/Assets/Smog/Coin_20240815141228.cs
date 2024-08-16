using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ponCharacter ponCharacter;
    public Camera FPCamera;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 aimPos = FPCamera.transform.position;
        transform.LookAt(aimPos);
        GetComponent<Rigidbody>().velocity =  Vector3.left * 10;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
