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
        IniVel(3,3,3);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IniVel(float x, float y, float z){
        float vel_x = Random.Range(0, x);
        float vel_y = Random.Range(0, y);
        float vel_z = Random.Range(0, z);
        GetComponent<Rigidbody>().velocity = new Vector3(vel_x, vel_y, vel_z);
    }
}
