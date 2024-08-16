using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrialStateTypes  {Init, InputOccurred, End}

public class TrialFSM
{
    private TState currentState;
    private Dictionary<TrialStateTypes, TState> stateMap = new Dictionary<TrialStateTypes, TState>();

    void Start() {
        stateMap.Add(TrialStateTypes.Init, new TrialBeginState());
    }

    void TransitionState(TrialStateTypes type){
        if (currentState != null){
            currentState.OnExit();
            currentState = stateMap[type];
            currentState.OnEnter();
        }
    }
}
