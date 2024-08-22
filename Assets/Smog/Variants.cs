using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Variants : ScriptableObject
{
    public List<int> uuidOfCatchedCoins;
    public bool spawn;
    public int frameID;
    public bool isRecording;

    public void Reset(){
        uuidOfCatchedCoins = new List<int>();
        spawn = false;
        frameID = 0;
    } 

}
