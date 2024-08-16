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
    private Vector2 point1;
    private Vector2 point2;

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        vel_forward = Random.Range(0, 10f);
        point1 = new Vector2(Cannon.transform.position.x, Cannon.transform.position.y);
        point2 = new Vector2(aim.transform.position.x, aim.transform.position.y);
    }

    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;
        
        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);

        coinPrefab.transform.LookAt(aim.transform);
        Debug.DrawLine(coinPrefab.transform.position, aim.transform.position, Color.red);
        coinPrefab.GetComponent<Rigidbody>().velocity = coinPrefab.transform.forward * vel_forward*(1);

       Destroy(coinPrefab,1f);
    }
}
