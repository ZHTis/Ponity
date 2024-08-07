using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHub : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator Spawn()
    {
        Instantiate(prefab, new Vector3(15,2,2), Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }
}
