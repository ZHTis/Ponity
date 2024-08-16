using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailInputOccuredState : TState
{
    private TrialFSM manager;
    private StateParameter stateParameter;

    public TrailInputOccuredState(TrialFSM manager){
        this.stateParameter = manager.stateParameter;
        this.manager = manager;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
