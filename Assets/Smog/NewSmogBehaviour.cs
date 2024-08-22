using System.Collections;
using Terresquall;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;


public class NewSmogBehaviour : MonoBehaviour
{
    public GameObject cube;
    public GameObject cannon;
    public Camera camerame;

    public Vector3[] cannonPos;
    public Vector3[] cubePos;

    public Variants smogVariants;
    public DataOutput dataOutput;

    private List<frameStateFormat> frameStateToSave;
    public int count ;
    public List<int> uuidOfCatchedCoins;
    public List<int> CatchedCoins;
    private List<TimeEvent> timeEvents;
    public bool spawn;
    private  Vector3 poscannon;
    private Vector3 poscube;
    private bool change;
    private Vector3 cameramePos;
     private frameStateFormat frameState;



    void Awake()
    {
        smogVariants.Reset();
        cannonPos = new Vector3[]{new Vector3(-17,0.9f,5),
                new Vector3(-18,0.9f,-6),
                new Vector3(-15,0.9f,-6)};

        cubePos = new Vector3[]{new Vector3(-22,0.4f,2.2f),
                new Vector3 (-28,0.4f,-0.4f)};
        cameramePos=camerame.transform.position;

        smogVariants.uuidOfCatchedCoins = new List<int>();
        uuidOfCatchedCoins = smogVariants.uuidOfCatchedCoins;
        count = 0;
        timeEvents = new List<TimeEvent>();
        dataOutput = new DataOutput();
        change = false;
        frameStateToSave = new List<frameStateFormat>();

        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true;
      
    }

    // Start is called before the first frame updatSS
    void Start()
    {
        StartCoroutine(Thisisit());
        smogVariants.frameID = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        smogVariants.frameID ++;

        if(change!=true){smogVariants.isRecording=true;
        float y = VirtualJoystick.GetAxis("Horizontal")*(-1);
        float x = VirtualJoystick.GetAxis("Vertical")*(1f);
        cube.transform.position += 2 * new Vector3(x,0,y) * Time.deltaTime;
       
        CatchedCoins = uuidOfCatchedCoins.Distinct().ToList();
        count = CatchedCoins.Count ;
        Debug.Log("count:"+ count);}
        
        if(change==true ){cannon.transform.position = Vector3.MoveTowards(cannon.transform.position,poscannon, 1.5f*Time.deltaTime);
        cube.transform.position = Vector3.MoveTowards(cube.transform.position,poscube, 1.5f*Time.deltaTime);
        smogVariants.isRecording=false;}

        //save 
        frameState= new frameStateFormat();
        { frameState.frameId = smogVariants.frameID;
        if(GameObject.Find("coin(Clone)")!=null) {
            GameObject coin = GameObject.Find("coin(Clone)");
            frameState.coinPosX = coin.transform.position.x;
            frameState.coinPosY = coin.transform.position.y;
            frameState.coinPosZ = coin.transform.position.z;
           
        }
        frameState.cubePosX = cube.transform.position.x;
        frameState.cubePosY = cube.transform.position.y;
        frameState.cubePosZ = cube.transform.position.z; 
        frameState.virtualJoystick =new System.Numerics.Vector2(VirtualJoystick.GetAxis("Horizontal"),VirtualJoystick.GetAxis("Vertical"));
        frameState.timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }
        frameStateToSave.Add(frameState);
        //save ended
    }


    void DefineScene(int i){
        switch(i){
            case 0:
                poscannon= cannonPos[0] ;
                poscube =cubePos[1];
                break;
            case 1:
                poscannon=cannonPos[1];
                poscube =cubePos[1];
                break;
            case 2:
                poscannon=cannonPos[2];
                poscube =cubePos[0];
                break;
        }}


    IEnumerator Thisisit(){
        yield return new WaitForSeconds(3f); 
        for(int j=0; j<3; j++){
            DefineScene(j);
            smogVariants.spawn = false;
            change = true;
            smogVariants.spawn = true;  
            yield return new WaitUntil(() => cannon.transform.position == poscannon);
            change = false;
        
            yield return new WaitForSeconds(0.5f); 
            
            TimeEvent timeEvent = new TimeEvent($"cannon_{j}_start",
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds());
            Debug.Log(timeEvent.eventName + " " + timeEvent.timestamp);
            timeEvents.Add(timeEvent);
            yield return new WaitUntil(() => count == 3);

            count = 0;   smogVariants.uuidOfCatchedCoins.Clear();
            TimeEvent timeEvent2 = new TimeEvent($"cannon_{j}_completed", 
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds());
            timeEvents.Add(timeEvent2);
            smogVariants.spawn = false;
            yield return new WaitForSeconds(1f); 
        }
    }

    void OnDestroy(){
        smogStateFormat smogState = new smogStateFormat();
       {smogState.timeEvents_name = new List<string>();
       smogState.timeEvents_timestamp = new List<long>(); 
       smogState.cameraPos =cameramePos; }

        for(int i = 0; i < timeEvents.Count; i++){
            smogState.timeEvents_name.Add(timeEvents[i].eventName); 
            smogState.timeEvents_timestamp.Add(timeEvents[i].timestamp);
            Debug.Log(timeEvents[i].eventName + " " + timeEvents[i].timestamp); 
            smogState.cannonPos = cannonPos.ToList();
            smogState.cubePos = cubePos.ToList();
            }

        //Debug.Log(timeEvents.Count);
        dataOutput.SaveDataSimple(smogState, "/Resources/smog/state/", "smogState");
        dataOutput.SaveData<frameStateFormat>(frameStateToSave, "/Resources/smog/frameInfo/", "frameState");
        smogVariants.isRecording = false;
}

}
