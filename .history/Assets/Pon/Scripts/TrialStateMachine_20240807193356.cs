using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrialStateTypes  {Init, InputOccurred, End}

public class TrialFSM
{
    private TState currentState;
    private Dictionary<TrialStateTypes, TState> stateMap ;

    public TrialFSM(){
        this.stateMap = new Dictionary<TrialStateTypes, TState>();
    }
    public void TransitionState(TrialStateTypes type){
        if (currentState != null){
            currentState.OnExit();
            currentState = stateMap[type];
            currentState.OnEnter();
        }
    }
}
