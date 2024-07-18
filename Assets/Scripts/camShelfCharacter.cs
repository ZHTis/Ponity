using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class camShelfCharacter : ScriptableObject
{
    public Vector3 location;
    public int numberOfCams = 12;

    [Range(0, 12)]
    [Tooltip("camID")]
    public int camID ;
    
    [Range(5, 12)]
    [Tooltip("radius")]
    public float radius =10;
    public List<Vector3> _camerapos = new List<Vector3>();

    [Range(0f, 5f)]
    public float h;

    [Range(0f,5f )]
    [Tooltip("neck")]
    public float neck = 1.5f;
    public bool cam_as_origin = true;

    public Vector3 location_2;
}
