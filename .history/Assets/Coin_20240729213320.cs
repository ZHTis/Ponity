using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ponCharacter ponCharacter = new ponCharacter();
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(15,2,0);
        Vector3 aimPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
