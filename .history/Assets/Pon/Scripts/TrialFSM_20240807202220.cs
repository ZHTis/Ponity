using System.Collections.Generic;
using UnityEngine;


public enum TrialStateTypes  {Init, InputOccurred, End}

public class StateParameter: behaviorCenter {
    public int TrialTag;
    public int  FrameTag;
    public bool frameReadyToReset;
}




public class TrialFSM 
{
    public StateParameter stateParameter;
    private TState currentState;
    private Dictionary<TrialStateTypes, TState> stateMap ;


    public void Start(){
        stateMap.Add(TrialStateTypes.Init, new TrialBeginState(this));
        stateMap.Add(TrialStateTypes.InputOccurred, new TrailInputOccuredState(this));
        stateMap.Add(TrialStateTypes.End, new TrialEndState(this));
        Debug.Log("TrialFSM Start");
     
    }

    public void TransitionState(TrialStateTypes type){
        if (currentState != null){
            currentState.OnExit();
            currentState = stateMap[type];
            currentState.OnEnter();
        }
    }
}
