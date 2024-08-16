using UnityEngine;


[CreateAssetMenu]
public class TrialState : ScriptableObject
{
    
    public int TrialTag;
    public int  FrameTag;

    public bool frameReadyToReset;


}

public class InitTrialState : TrialState
{
    
}

public class TrialhasInputState : TrialState
{
    
}

public class TrialEndState : TrialState
{
    
}
