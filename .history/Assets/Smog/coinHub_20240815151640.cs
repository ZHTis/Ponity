using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Editor;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;

    private GameObject coinPrefab ;
    private float vel_forward;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        vel_forward = Random.Range(0, 10f);
        coinPrefab.transform.LookAt(aim.transform);
        Debug.DrawLine(coinPrefab.transform.position, aim.transform.position, Color.red);
    }

    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;
        coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);
        
        //coinPrefab.GetComponent<Rigidbody>().velocity = coinPrefab.transform.forward * vel_forward*(1);
       Destroy(coinPrefab,1f);
    }
}
