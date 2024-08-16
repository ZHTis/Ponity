using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);
       Destroy(coinPrefab,1f);
    }
}
