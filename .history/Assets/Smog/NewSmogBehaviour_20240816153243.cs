using System.Collections;
using Terresquall;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class NewSmogBehaviour : MonoBehaviour
{
    public GameObject cube;
    public GameObject cannon;
    public Vector3[] cannonPos;

    private int bowlFilledWith;
    private bool bowlFullfilled;

    private MeshFilter meshFilter;
    private SphereCollider coinArea;
    Bounds bounds ;

    void Awake(){
        cannonPos = new Vector3[]{new Vector3(-17,0.9f,5),
                new Vector3(-18,0.9f,-6),
                new Vector3(-15,0.9f,-6)};

        meshFilter = cube.GetComponentInChildren<MeshFilter>();
        
    }
    // Start is called before the first frame updatSS
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = VirtualJoystick.GetAxis("Horizontal")*(-1);
        float x = VirtualJoystick.GetAxis("Vertical")*(1f);
        cube.transform.position += 2 * new Vector3(x,0,y) * Time.deltaTime;
        
        bounds = meshFilter.mesh.bounds;
        Debug.Log(bounds);
        Coin[] coins =  GameObject.FindObjectsOfType<Coin>();
        Debug.Log(coins.Length);
        if(bounds.Intersects(coinArea.bounds)){
            Debug.Log("bowlIntruded");
        }
    }


    IEnumerator Thisisit(){
        for(int j=0; j<cannonPos.Length; j++){
            cannon.transform.position = cannonPos[j];
            yield return null;  //WaitUntil(()=>bowlFullfilled);
        }
    }


}
