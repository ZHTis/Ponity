using UnityEngine;


[CreateAssetMenu]
public class TrialState : ScriptableObject
{
    
    public int TrialTag;
    public int  FrameTag;

    public bool ifTrialAltered;
    public bool frameReadyToReset;
}