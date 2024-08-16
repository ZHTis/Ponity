using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrialStateTypes  {Init, InputOccurred, End}

public class StateParameter {}




public class TrialFSM : MonoBehaviour
{
    public StateParameter stateParameter;
    private TState currentState;
    private Dictionary<TrialStateTypes, TState> stateMap ;
    

    public void Start(){
        
    }

    public void TransitionState(TrialStateTypes type){
        if (currentState != null){
            currentState.OnExit();
            currentState = stateMap[type];
            currentState.OnEnter();
        }
    }
}
