
public  interface TState 
{
    void OnEnter(){}
    void OnUpdate(){}
    void OnExit(){}
}


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


public class TrailInputOccuredState : TState
{
    private TrialFSM manager;
    private StateParameter stateParameter;

    public TrailInputOccuredState(TrialFSM manager){
        this.stateParameter = manager.stateParameter;
        this.manager = manager;
    }

    // Start is called before the first frame update
    public void OnEnter()
    {  }
    public void OnUpdate()
    {  }
    public void OnExit()
    {  }

}




public class TrialEndState : TState
{
    private TrialFSM manager;
    private StateParameter stateParameter;

    public TrialEndState(TrialFSM manager){
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