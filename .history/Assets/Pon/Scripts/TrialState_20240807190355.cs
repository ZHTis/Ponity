using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class TrialState : ScriptableObject
{
    
    public int TrialTag;
    public int  FrameTag;

    public bool frameReadyToReset;
    
    public virtual void OnEnter(){}
    public virtual void OnUpdate(){}
    public virtual void OnExit(){}
}

public class InitTrialState : TrialState
{
    OnEnterState(){

    }
}


public class TrialhadInputState : TrialState
{
    
}

public class TrialEndState : TrialState
{
    
}
