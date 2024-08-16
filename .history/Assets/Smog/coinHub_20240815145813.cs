using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;

    private float vel_forward;


    // Start is called before the first frame update
    void Start()
    {
        vel_forward = 10f;
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
        coinPrefab.transform.LookAt(aim.transform);
        coinPrefab.GetComponent<Rigidbody>().velocity = Vector3.forward * vel_forward;
       Destroy(coinPrefab,1f);
    }
}
