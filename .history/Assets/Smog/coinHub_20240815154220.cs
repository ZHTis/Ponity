
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Cannon;
    public GameObject aim;


    private float vel_forward;
    private Vector2 point1;
    private Vector2 point2;
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
        point1 = new Vector2(Cannon.transform.position.x, Cannon.transform.position.y);
        point2 = new Vector2(aim.transform.position.x, aim.transform.position.y);
        Vector2 iniVel_forward = (point2 - point1).normalized * vel_forward;
        vector = new Vector3(iniVel_forward.x, iniVel_forward.y, 0);
    }


    private void Spawn()
    {
        Vector3 iniPos = Cannon.transform.position;
        
        GameObject coinPrefab = Instantiate(prefab, iniPos, Quaternion.identity);

        coinPrefab.GetComponent<Rigidbody>().velocity = 
           vector + Vector3.up*2f;

       Destroy(coinPrefab,1f);
    }
}
