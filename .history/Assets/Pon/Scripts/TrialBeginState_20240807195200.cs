using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialBeginState : TState
{
    private TrialFSM manager;
    private StateParameter stateParameter;

    public TrialBeginState(TrialFSM manager){
        this.stateParameter = manager.stateParameter;
        this.manager = manager;
    }

    public void OnEnter()
    {  }
    public void OnUpdate()
    {  }
    public void OnExit()
    {  }

}
