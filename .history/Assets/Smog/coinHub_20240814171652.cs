using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("coin(Clone)"),3f);
    }

    private void Spawn()
    {
        Instantiate(prefab, new Vector3(50,2,2), Quaternion.identity);
       
    }
}