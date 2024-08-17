using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinhubT : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public Variants variants;

    private float vel_forward;
    private float vel_up;
    public Vector3 vector;
    public float spawInterval = 2.5f;
    private bool spawn;
    public GameObject aim;


    // Start is called before the first frame update
    void Start()
    {
        spawInterval = 2.5f;
        InvokeRepeating("Spawn", 0f, spawInterval);
    }

    // Update is called once per frame
    void Update()
    {
        spawn =variants.spawn;
        Debug.Log(spawn);
        vel_forward = Random.Range(9, 10f);
        vel_up = Random.Range(5, 7f);
        vector = -Cannon.transform.position + aim.transform.position;
    }

        private void Spawn()
    {
        if(spawn==true){
        Vector3 iniPos = Cannon.transform.position;
        Vector3 vector_forward = new Vector3(vector.x,0,vector.z);

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);

        coinPrefab.GetComponent<Rigidbody>().velocity = 
           vector_forward.normalized * vel_forward + Vector3.up* vel_up; ;
        Debug.DrawLine(Cannon.transform.position, 
            Cannon.transform.position + vector_forward*0.5f, Color.red, spawInterval);
       Destroy(coinPrefab, spawInterval);}
       else{}
        
    }
}
