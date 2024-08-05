using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class targetCharacter : ScriptableObject
{
    public Vector3 location;
    public Vector3 location_2;
    
    [Range(1.1f, 5f)]
    [Tooltip("distance of choices")]
    public float distance=1;
    public int flaseChoiceIsLeft;
    public string rightChoice;

    public bool makeInvisible;

    public bool setNow;
    public bool isParallelToViewCanvas;
    public void Reset()
    {
        makeInvisible = false;
        setNow = true;
    }

}
