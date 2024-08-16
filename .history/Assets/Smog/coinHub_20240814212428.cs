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
        
    }

    private void Spawn()
    {
        float vel_x = Random.Range(0, 10f);
        float vel_y = Random.Range(0, 10f);
        float vel_z = Random.Range(0, 10f);
    GameObject coinPrefab = Instantiate(prefab, new Vector3(vel_x, vel_y, vel_z), Quaternion.identity);
       Destroy(coinPrefab,1f);
    }
}
