using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ponCharacter ponCharacter;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(ponCharacter.vel_x,ponCharacter.vel_y*(-1),0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
