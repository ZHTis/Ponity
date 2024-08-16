using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrialStateTypes  {Init, InputOccurred, End}





public class TrialFSM : MonoBehaviour
{
    public class StateParameter {}

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
