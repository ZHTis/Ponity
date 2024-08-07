using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ponCharacter ponCharacter;
    public Camera gameObject;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 aimPos = gameObject.transform.position;
        transform.LookAt(aimPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
