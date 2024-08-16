
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;


    private float vel_forward;
    public Vector3 vector;


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
        //Debug.DrawLine(Cannon.transform.position, aim.transform.position, Color.red, 10f);
        vector = -Cannon.transform.position + aim.transform.position;
    }


    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;
        Vector3 vector_forward = new Vector3(vector.x,0,vector.z);

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);

        coinPrefab.GetComponent<Rigidbody>().velocity = 
           vector_forward.normalized * vel_forward ;
        Debug.DrawLine(Cannon.transform.position, 
            Cannon.transform.position + vector_forward, Color.red, 10f);
       Destroy(coinPrefab,1f);
    }
}
