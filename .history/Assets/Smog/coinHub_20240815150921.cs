using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Editor;
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
        vel_forward = Random.Range(0, 10f);
    }

    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);
        coinPrefab.transform.LookAt(aim.transform);
        coinPrefab.GetComponent<Rigidbody>().velocity = 
            coinPrefab.transform.forward * vel_forward*(-1);
       Destroy(coinPrefab,1f);
    }
}
