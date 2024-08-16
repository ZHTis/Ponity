
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisonDetection : MonoBehaviour
{
    public GameObject cube;
    private bool hasContact;
    public List<int> uuidOfCatchedCoins;
    public Variants variants;

    MeshCollider cubearea ;
    Vector3 cubePos;
    Vector3 cubesize;

    void Awake(){
      hasContact = false;
      uuidOfCatchedCoins = variants.uuidOfCatchedCoins;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(hasContact){CheckPoint();}
       
    }

    void OnCollisionEnter(Collision other){
        //Debug.Log(other.gameObject.name);
        if( other.gameObject.name == "CubeTar"){
            //Debug.Log("Collided");
            hasContact = true;
        }}
    
    void CheckPoint(){
        cubearea = cube.GetComponent<MeshCollider>();
        cubePos = cubearea.transform.position;
        cubesize = cubearea.bounds.size;

        Collider[] overlappingColliders = Physics.OverlapBox(cubePos, cubesize*0.1f);
        foreach (Collider collider in overlappingColliders){
            //Debug.Log(collider.tag);
            if(collider.tag == "coin")
            {
            int id = collider.gameObject.GetInstanceID();
            if (!uuidOfCatchedCoins.Contains(id))
            {
              uuidOfCatchedCoins.Add(id);
              Debug.Log(id);
            }
          
            }
           
          
        }
        
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(cubePos, cubesize*0.1f);
    }

}






