using Terresquall;
using UnityEngine;

public class NewSmogBehaviour : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = VirtualJoystick.GetAxis("Horizontal");
        cube.transform.position += new Vector3(x,0,0) * Time.deltaTime;
    }
}
