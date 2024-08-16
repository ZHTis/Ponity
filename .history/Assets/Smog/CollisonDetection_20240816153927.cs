using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetection : MonoBehaviour
{
    public GameObject cube;
    private MeshFilter meshFilter;
    Bounds bounds ;

    void Awake(){
        meshFilter = cube.GetComponentInChildren<MeshFilter>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
