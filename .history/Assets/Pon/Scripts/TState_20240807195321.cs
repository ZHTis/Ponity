
public  interface TState 
{
    void OnEnter(){}
    void OnUpdate(){}
    void OnExit(){}
}


public class TrialEndState : TState
{
    private TrialFSM manager;
    private StateParameter stateParameter;

    public TrialEndState(TrialFSM manager){
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