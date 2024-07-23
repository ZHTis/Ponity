using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRig : MonoBehaviour
{
    public Camera currentCamera, newCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        // Set the new camera to the same position and properties as the enabled camera
        newCamera.transform.position = currentCamera.transform.position;
        newCamera.transform.rotation = currentCamera.transform.rotation;
        newCamera.fieldOfView = currentCamera.fieldOfView;
        newCamera.nearClipPlane = currentCamera.nearClipPlane;
        newCamera.farClipPlane = currentCamera.farClipPlane;
    }
}
