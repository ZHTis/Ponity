using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialBeginStates : TState
{
    private TrialFSM manager;
    public TrialBeginStates(TrialFSM manager){
        this.manager = manager;
    }

    public void OnEnter()
    {  }
    public void OnUpdate()
    {  }
    public void OnExit()
    {  }

}
