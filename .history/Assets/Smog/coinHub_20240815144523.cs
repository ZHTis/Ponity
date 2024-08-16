using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
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
        float pos_x = Random.Range(0, 2f);
        float pos_y = Random.Range(0, 2f);
        float pos_z = Random.Range(0, 2f);

        Vector3 iniPos = Cannon.transform.position;

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);
       Destroy(coinPrefab,1f);
    }
}
