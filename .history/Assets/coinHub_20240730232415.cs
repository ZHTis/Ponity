using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Spawn()
    {
        Instantiate(prefab, new Vector3(15,2,2), Quaternion.identity);
    }
}
