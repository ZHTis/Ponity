using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class pathSimulator : MonoBehaviour
{
    public targetCharacter targetCharacter;
    [SerializeField] private GameObject _pon;
    public ponCharacter _ponCharacter;
    public behaviorCenter  behaviorCenter;

    //only for this script:
    private LineRenderer _line;
    private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private List<Vector3> points;
    private Vector3 lastLowestPoint = Vector3.zero;
    private int positionCount;
    private Vector3[] positions;
    private float simulateOffset=0.7f;

    // Start is called before the first frame update
    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        CreatePhysicsScene();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SimulateTrajectory(_pon, new Vector3 (0,0,0),new Vector3 (_ponCharacter.vel_x,0,0),_ponCharacter.withMarker);
        giveHitPoints();
    }

    private void CreatePhysicsScene() 
    {
        _simulationScene = SceneManager.CreateScene("Simulate", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
        foreach (Transform obj in _obstaclesParent) 
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position+Vector3.up*simulateOffset, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            
        }
        
    }

    public void SimulateTrajectory(GameObject prefab, Vector3 pos, Vector3 vel, bool lineisVIsible = true) 
    {
        var ghostObj = Instantiate(prefab, pos,Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        ghostObj.GetComponent<Rigidbody>().velocity = vel;
        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) 
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
            _line.enabled = lineisVIsible;

            if (Time.fixedDeltaTime * i == behaviorCenter.ratio || _ponCharacter.kinematicPosGiven == false)
            {
                _ponCharacter.kinematicPosition = ghostObj.transform.position;  
                _ponCharacter.kinematicPosGiven = true;
                //Debug.Log("kinematicPosition: " + _ponCharacter.kinematicPosition);
            }
        }

        Destroy(ghostObj.gameObject);
    }



    void giveHitPoints()
    {
        points = new List<Vector3>();
        positionCount = _line.positionCount; 
        positions = new Vector3[positionCount];
        _line.GetPositions(positions);
        for (int i = 1; i < positions.Length; i++)
        {
            if (points.Count < 200)
            {
                //Debug.Log("logging " + i + ": " + positions[i].y);
                points.Add(positions[i]);
               // Debug.Log(points.Count());
            }
        }

        // find the vector in <points> that have the smallest y.
        Vector3 lowestPoint = points.OrderBy(p => p.y).First();
        if(lastLowestPoint==lowestPoint){}
        else {
            lastLowestPoint = lowestPoint;
            //Debug.Log("lowestPoint: " + lowestPoint);
            targetCharacter.location = lowestPoint-Vector3.up*simulateOffset;
        }

    }

}
