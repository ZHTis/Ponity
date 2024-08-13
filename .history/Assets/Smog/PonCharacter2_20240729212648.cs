using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ponCharacter2 : ScriptableObject
{
   [Range(0f, 17f)]
   [Tooltip("vel_x")]
    public float vel_x;
    [Range(-7f, 7f)]
    [Tooltip("vel_y")]
    public float vel_y;
    public bool withMarker;

   public bool isKinematic;
    public Vector3 IniPosition;

    public Vector3 kinematicPosition;
    public bool kinematicPosGiven;
    public float kinematicTime;
    public bool pauseBeforeFire;
}