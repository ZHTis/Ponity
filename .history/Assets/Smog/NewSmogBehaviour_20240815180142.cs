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
        float y = VirtualJoystick.GetAxis("Horizontal")*(-1);
        float x = VirtualJoystick.GetAxis("Vertical")*(-1f);
        cube.transform.position += new Vector3(x,0,y) * Time.deltaTime;
    }
}
