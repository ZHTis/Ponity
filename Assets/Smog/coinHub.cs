
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;
    public Variants variants;


    private float vel_forward;
    private float vel_up;
    public Vector3 vector;
    public float spawInterval;
   private bool spawn;


    void Awake(){
        spawInterval = 3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, spawInterval);
    }

    // Update is called once per frame
    void Update()
    {
        spawn =variants.spawn;
        vel_forward = Random.Range(9, 10f);
        vel_up = Random.Range(5, 7f);
        vector = -Cannon.transform.position + aim.transform.position;
    }


    private void Spawn()
    {
        if(spawn == false){}
        else{
        Vector3 iniPos = Cannon.transform.position;
        Vector3 vector_forward = new Vector3(vector.x,0,vector.z);

        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);

        coinPrefab.GetComponent<Rigidbody>().velocity = 
           vector_forward.normalized * vel_forward + Vector3.up* vel_up; ;
        Debug.DrawLine(Cannon.transform.position, 
            Cannon.transform.position + vector_forward*0.5f, Color.red, spawInterval);
       Destroy(coinPrefab, spawInterval);
        }
        
    }


}
