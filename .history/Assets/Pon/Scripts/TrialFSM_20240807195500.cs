using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrialStateTypes  {Init, InputOccurred, End}

public class StateParameter {
    
}




public class TrialFSM : MonoBehaviour
{
    public StateParameter stateParameter;
    private TState currentState;
    private Dictionary<TrialStateTypes, TState> stateMap ;


    public void Start(){
        stateMap.Add(TrialStateTypes.Init, new TrialBeginState(this));
        stateMap.Add(TrialStateTypes.InputOccurred, new TrailInputOccuredState(this));
        stateMap.Add(TrialStateTypes.End, new TrialEndState(this));
    }

    public void TransitionState(TrialStateTypes type){
        if (currentState != null){
            currentState.OnExit();
            currentState = stateMap[type];
            currentState.OnEnter();
        }
    }
}
