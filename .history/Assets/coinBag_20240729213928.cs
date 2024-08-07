using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBag : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
       Instantiate(prefab, new Vector3(15,2,2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
