using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{

    public Camera mainCamera ;
    public Camera Camera2 ;
    public Camera Camera3 ;
    public Camera Camera4 ;

    private bool withMarker= false;//true;
    [SerializeField] private GameObject prefab;
    public ponCharacter ponCharacter;
    public targetCharacter targetCharacter;
    public camShelfCharacter camShelfCharacter;
    public behaviorCenter behaviorC;
    private List<ExpSaveFormat> trialDataList;
    private float spawnInterval ;
    public enum rightChoice  {a,l}
    private bool keyPressed =false;
    private float timer;
    private ExpSaveFormat trialData;
    public TrialState trialState;
    private bool ifShuffle = false;
    private Text text;
    


/// <summary>
/// Parameter space
/// </summary>
    private int repeatCount;
    private float[] ratio;
    private int[] ponVel_x;
    private int[] ponVel_y;
    private float[]targetDistance ;
    private float[] camNeck;
    private int[] camIDList;
    private bool abortAllowed=false;
    private int[] sessionList;



    void Awake(){
        Cursor.visible = false;
        text= Camera2.GetComponentInChildren<Text>();
        behaviorC.Reset();
        trialDataList = new List<ExpSaveFormat>();
        ponCharacter.withMarker = withMarker;
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DebugCharacter());

        EnterBreak("Press Enter when you are ready, ^_^");
        StartCoroutine(exitBreak());
        StartCoroutine(ThisIsIt(2));
    }

   void EnterBreak(string message)
   {
        text.text = message;
        Camera2.enabled = true;
        mainCamera.enabled = false;
        Camera4.enabled = false;
   }
   IEnumerator Grey(){
       mainCamera.enabled = false;
       Camera4.enabled = true;
       Camera3.enabled = false;
       Camera2.enabled = false;
       trialState.frameReadyToReset = true; trialState.grey = true;Debug.Log("frameReadyToResetGrey");
       yield return new WaitForSeconds(0.8f);
       Camera4.enabled = false;
       Camera2.enabled = false;
        mainCamera.enabled = true;
        trialState.frameReadyToReset = true; trialState.grey = false;Debug.Log("frameReadyToReset");
   }

   IEnumerator exitBreak(){
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Return));
        StartCoroutine(Grey());
         
   }

   void FinalBreak(){
       Camera3.enabled = true;
       Camera2.enabled = false;
       Camera4.enabled = false;
       mainCamera.enabled = false;

   }
   
       
    void Update()
    {
    

        if( trialState.frameReadyToReset == false){
        trialState.FrameTag +=1;
      }
      if(trialState.frameReadyToReset == true){
          trialState.FrameTag =0;
          trialState.frameReadyToReset =  false;
          }
    
        timer += Time.deltaTime;
        claimAnswer();

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L)) && keyPressed==false && mainCamera.enabled == true) 
        {
            behaviorC.choice = Input.inputString.ToString();
            //Debug.Log(Input.inputString.ToString());
            keyPressed = true;
            behaviorC.touchTimefromInit = Time.time-behaviorC.initTime;
            if(ponCharacter.kinematicTime!=0){behaviorC.touchTimefromPause = Time.time-ponCharacter.kinematicTime;}

            //Debug.Log("rightChoice: " + targetCharacter.rightChoice);
            //Debug.Log("choice: " + behaviorC.choice);

            if (StringComparer.OrdinalIgnoreCase.Equals
                (behaviorC.choice, targetCharacter.rightChoice) == true)
            {
                behaviorC.correctCount += 1;
                behaviorC.isCorrect = true;
               // targetCharacter.makeInvisible = true; // make target invisible
                //Debug.Log(targetCharacter.makeInvisible);

            }
            createExpDataSlot(out trialData); trialDataList.Add(trialData);  behaviorC.OnValidate();
        }

        if(mainCamera.enabled == true &&
                Input.GetKeyDown(KeyCode.Space) && 
                keyPressed==false &&
                abortAllowed==true)
                {
            behaviorC.choice = "abort";
            keyPressed = true;
            //Debug.Log("Abort");
            behaviorC.touchTimefromInit = Time.time-behaviorC.initTime;
            if(ponCharacter.kinematicTime!=0){behaviorC.touchTimefromPause = Time.time-ponCharacter.kinematicTime;}
            createExpDataSlot(out trialData); trialDataList.Add(trialData);  behaviorC.OnValidate();
        }
                

    }


  void Fire(float DestroyTime = 1.1f, bool realFire = true)
    {
       if(realFire == true)
       {
        behaviorC.trial += 1;
        trialState.TrialTag = behaviorC.trial;
       keyPressed = false;
       ponCharacter.kinematicPosGiven = false;
       behaviorC.initTime = Time.time;
       ponCharacter.kinematicTime = 0f;
       }

       GameObject ponPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
       Destroy(ponPrefab, DestroyTime );
    }

    void targetRandomize(bool debugMode = false)
    {
        if(debugMode == true){
            targetCharacter.flaseChoiceIsLeft = 1;
            targetCharacter.Reset();
            
        }
        else{
        System.Random random = new System.Random();
        targetCharacter.flaseChoiceIsLeft =  random.Next(0,2) *2-1;
        targetCharacter.Reset();  }
    }
        
    void claimAnswer()
    {
       if(targetCharacter.flaseChoiceIsLeft != 1 && camShelfCharacter.camID >6)
       { targetCharacter.rightChoice = rightChoice.a.ToString();}   

       if(targetCharacter.flaseChoiceIsLeft == 1 && camShelfCharacter.camID >6)
       { targetCharacter.rightChoice = rightChoice.l.ToString();}   


     // view from another side
        if(targetCharacter.flaseChoiceIsLeft == 1 && camShelfCharacter.camID <6)
       { targetCharacter.rightChoice = rightChoice.l.ToString();}    

       if(targetCharacter.flaseChoiceIsLeft != 1 && camShelfCharacter.camID <6)
       { targetCharacter.rightChoice = rightChoice.a.ToString();}  

       else{}
    }
   
   
   /// <summary>
   /// change session parameter here
   /// </summary>
   /// <param name="session"></param>
   void defineSession(int session)
   {
    switch(session)
    {
        case 0:
            camShelfCharacter.camID = 1;
            camShelfCharacter.radius = 8f;
            ponVel_x = new int[]{5,7};
            ponVel_y = new int[]{0,2};
            targetDistance =new float[]{1.1f,1.7f,2f};
            camNeck = new float[]{3.5f,2,0} ;
            targetCharacter.isParallelToViewCanvas = false;
            ratio = new float[] {1.2f, 0.3f, 0.2f}; 
            Physics.gravity = new Vector3(0, -9.8f, 0);
            break;

        case 1:
            camShelfCharacter.camID = 2;
            camShelfCharacter.radius = 8f;
            ponVel_x = new int[]{5,7};
            ponVel_y = new int[]{0,2};
            targetDistance =new float[]{1.1f,1.7f,2f};
            camNeck = new float[]{3.5f,2,0} ;
            targetCharacter.isParallelToViewCanvas = false;
            ratio = new float[] {1.2f, 0.3f, 0.2f};
            Physics.gravity = new Vector3(0, -9.8f, 0);
            break;

        case 2:
            camShelfCharacter.camID = 3;
            camShelfCharacter.radius = 8f;
            ponVel_x = new int[]{5,7};
            ponVel_y = new int[]{0,2};
            targetDistance =new float[]{1.1f,1.3f,1.7f};
            camNeck = new float[]{3.5f,2,0} ;
            targetCharacter.isParallelToViewCanvas = false;
            ratio = new float[] {1.2f, 0.3f, 0.2f};
            Physics.gravity = new Vector3(0, -9.8f, 0);
            break;

        case 3:
                ratio = new float[] {1,0.1f,0.15f,0.2f,0.25f,0.3f,0.35f,0.4f,0.5f,0.6f};
                camIDList = new int[] {1,2,3};
                camShelfCharacter.radius = 8f;
                ponCharacter.vel_x = 5;
                ponCharacter.vel_y = 2;
                targetDistance =new float[]{1.1f,1.15f,1.2f,1.3f,1.7f};
                camNeck = new float[]{3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = false; 
                Physics.gravity = new Vector3(0, -9.8f, 0);
                break;
        case 4:
                ratio = new float[] {1,0.1f,0.15f,0.2f,0.25f,0.3f,0.35f,0.4f,0.5f,0.6f};
                camIDList = new int[] {1,2,3};
                camShelfCharacter.radius = 8f;
                ponCharacter.vel_x = 5;
                ponCharacter.vel_y = -4;
                targetDistance =new float[]{1.1f,1.15f,1.2f,1.3f,1.7f};
                camNeck = new float[]{3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = false; 
                Physics.gravity = new Vector3(0, 0, 0);
                break;  

    }
    }
   
   void Shuffle<T>(List<T> list){
    int n = list.Count;
    System.Random random = new System.Random();

            for (int i = 0; i < n - 1; i++)
            {
                int j =random.Next(i, n);

                if (j != i)
                {
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
   }

   void if_Shuffle<T>(List<T> list, bool shuffle){

    if(shuffle){Shuffle(list);}
    if(!shuffle){}
   }

   /// <summary>
   /// main trial loop
   /// </summary>
   /// <returns></returns>
    IEnumerator ThisIsIt(int how=0)
    {
        float waitbeforechoice = 2f;
        int Replay=0;
        switch(how)//history lept
        {
            case 0:
                abortAllowed=false;
                repeatCount = 0;  
                int sessionLenth = 3;
                

                for(int i = behaviorC.session; i <= sessionLenth; i++)
                {
                behaviorC.repeatCount=repeatCount;
                if(i==sessionLenth){Debug.Log("repeat");
                    repeatCount++;Debug.Log("repeatCount: " + repeatCount);
                    i=0;}
                if(repeatCount>= 10){Debug.Log("completed all trials"); FinalBreak();break;}

                behaviorC.session = i;
                Debug.Log("Session: " + i);
                defineSession(behaviorC.session); 

                for(int m= behaviorC.session_interlude; m <= camNeck.Length; m++)    //3
                {
                    if(m==camNeck.Length){Debug.Log("complete interlude");m=0;behaviorC.session_interlude = 0; 
                    break;}
                    camShelfCharacter.neck = camNeck[m];
                    behaviorC.session_interlude = m;
                    StartCoroutine(exitBreak());
                    yield return new WaitUntil(() => mainCamera.enabled == true);
            
                for (int j = 0; j < targetDistance.Length; j++){       //3
                Debug.Log("targetDistance: " + targetDistance[j]);
                    for(int y = 0; y < ponVel_y.Length; y++){        //2
                        for (int k = 0; k < ratio.Length; k++){           //3
                            for (int l = 0; l < ponVel_x.Length; l++){   //2
                            
                ponCharacter.vel_y = ponVel_y[y];
                ponCharacter.vel_x = ponVel_x[l];
                targetCharacter.distance = targetDistance[j];
                behaviorC.OnValidate();
                behaviorC.ratio = ratio[k];
                targetRandomize();  
                ponCharacter.isKinematic = false;
                Fire(waitbeforechoice+ratio[k]);
                timer = 0f;
                yield return new WaitUntil(() => timer > waitbeforechoice+ratio[k]+0.5 || keyPressed == true);
                if(keyPressed != true){
                    behaviorC.choice = "abort";
                   
                    if(Replay <4){Replay += 1;
                    l--;}
                    else{Replay = 0;}
                }
                if(keyPressed == true){
                    Replay = 0;
                }
                 //record
                createExpDataSlot(out trialData);trialDataList.Add(trialData);
                //
                yield return new WaitForSeconds(0.5f);

                DestroyPrefab(GameObject.Find("Pon(Clone)")); }}

                }
                } EnterBreak(""); }}
                break;
            
            case 1:
            // sequtially-run design
                abortAllowed = true;
                sessionList = new int[] {3,4};
       

                for (int s = 0; s < sessionList.Length; s++){
                    defineSession(sessionList[s]);behaviorC.session = sessionList[s];

                    if_Shuffle(camIDList.ToList(), ifShuffle);
                    if_Shuffle(camNeck.ToList(), ifShuffle); //if change sessions in a decending order

                    for (int i = 0; i < camIDList.Length; i++){
                    for (int j = 0; j < camNeck.Length; j++){
                        for (int d = 0; d < targetDistance.Length; d++)
                        {
                            camShelfCharacter.camID = camIDList[i];
                            camShelfCharacter.neck = camNeck[j];
                            targetCharacter.distance = targetDistance[d];

                for (int r=0; r<ratio.Length; r++)
                {
                behaviorC.ratio = ratio[r];
                targetRandomize();  
                yield return new WaitUntil(() => mainCamera.enabled == true);
                Fire(ratio[r]+waitbeforechoice);
                timer = 0f;
                yield return new WaitUntil(() => timer > waitbeforechoice+ratio[r]+0.5 || keyPressed == true);
                if(keyPressed != true){
                    behaviorC.choice = "abort";
                   
                    if(Replay <4){Replay += 1;
                    r--;}
                    else{Replay = 0;}
                    createExpDataSlot(out trialData); trialDataList.Add(trialData);  behaviorC.OnValidate();
                }
                if(keyPressed == true){
                    Replay = 0;
                }
                
                yield return new WaitForSeconds(0.5f);
                DestroyPrefab(GameObject.Find("Pon(Clone)")); 

                trialState.frameReadyToReset = true;
                StartCoroutine(Grey());
                
                } } } }
                EnterBreak("休息好了Enter开始继续测试 ^_^");StartCoroutine(exitBreak());
                }
                FinalBreak();
                break;
        
            case 2:
            //fully randomized design
                abortAllowed = true;
                sessionList = new int[] {3,4};

                for (int s = 0; s < sessionList.Length; s++){
                //camlist
                //camNeck
                //targetDistance
                //ratio
                defineSession(sessionList[s]);
                var combinations =  camIDList.SelectMany(neck => camNeck,(id,neck)=>new {id,neck})
                    .SelectMany(f => targetDistance,(f,tar)=>new {f,tar})
                    .SelectMany(e => ratio,(e,ratio)=>new {e,ratio});
                var combinationList = new List<float[]>();
                foreach (var combination in combinations){
                    var it = new [] {combination.ratio,
                        combination.e.tar, 
                        combination.e.f.id, 
                        combination.e.f.neck};
                    combinationList.Add(it);
                    //Debug.Log(it[0] + " , " + it[1] + " , " + it[2] + " , " + it[3]);
                    }
                if_Shuffle(combinationList,true);
                Debug.Log($"sesssion{sessionList[s]} trails count: "+combinationList.Count);
                //had shuffled
                //start running
                for(int i= 0; i < combinationList.Count; i++){
                    //Debug.Log(combination[0]+" , "+combination[1]+" , "+combination[2]+" , "+combination[3]);
              
                    var combination = combinationList[i];
                    camShelfCharacter.camID = (int)combination[2];
                    camShelfCharacter.neck = combination[3];  
                    targetCharacter.distance = combination[1];
                    behaviorC.ratio = combination[0];
                    targetRandomize();
                    yield return new WaitUntil(() => mainCamera.enabled == true);
                    Fire(combination[0]+waitbeforechoice);
                    timer = 0f;
                    yield return new WaitUntil(() => timer > waitbeforechoice+combination[0]|| keyPressed == true);

                    if(keyPressed != true){
                        behaviorC.choice = "abort";
                         if(Replay <2){Replay += 1;
                            i--;
                         }
                    else{Replay = 0;}
                    //Debug.Log(Replay+ "--" + combination[0]+" , "+combination[1]+" , "+combination[2]+" , "+combination[3]);
                    createExpDataSlot(out trialData); trialDataList.Add(trialData);  behaviorC.OnValidate();
                    }
                    if(keyPressed == true){
                        Replay = 0;
                    }
                DestroyPrefab(GameObject.Find("Pon(Clone)")); 

                trialState.frameReadyToReset = true;
                
                  if(behaviorC.trial %50==1 && behaviorC.trial>1)
                {// take a break
                    string[] lines = new string[]
                        {$"你的正确率目前是{behaviorC.correctRate}",
                        "休息一下",
                        "休息好了Enter开始继续测试 ^_^"};
                    string message = string.Join("\n", lines);
                    EnterBreak(message);
                    yield return new WaitUntil(() => Input.GetKey(KeyCode.Return));
                }
                StartCoroutine(Grey());

                }
    
                }
                FinalBreak();
                break;
        }
    }

    void DestroyPrefab(GameObject prefab)
    {   if (prefab != null){
        Destroy(prefab);}
    }

    void OnDestroy()
    {
        DataOutput dataOutput = new DataOutput();
        dataOutput.SaveData<ExpSaveFormat>(trialDataList, "/Resources/Pon/", behaviorC.PlayerName);
        
    }
    
    public ExpSaveFormat createExpDataSlot(out ExpSaveFormat trialData)
    {  ExpSaveFormat expData= new ExpSaveFormat();

       {
        expData.trial = behaviorC.trial;
       expData.session= behaviorC.session;
       expData.choice = behaviorC.choice;
       expData.isCorrect = behaviorC.isCorrect;
        expData.touchTimefromInit= behaviorC.touchTimefromInit;
        expData.touchTimefromPause = behaviorC.touchTimefromPause;
        expData.correctRate = behaviorC.correctRate;

        expData.initTime = behaviorC.initTime;
        expData.pauseTime = ponCharacter.kinematicTime;
        expData.ratio = behaviorC.ratio;
        expData.vel_x = ponCharacter.vel_x;
        expData.vel_y = ponCharacter.vel_y;
        expData.camID = camShelfCharacter.camID;
        expData.camShelfRadius = camShelfCharacter.radius;
        expData.camNeck = camShelfCharacter.neck;
        expData.targetsDistance = targetCharacter.distance;
        expData.isParallelToViewCanvas = targetCharacter.isParallelToViewCanvas;}

        return trialData = expData;
        }
    
    IEnumerator DebugCharacter()
    { ///for debug use only
       
        spawnInterval = 1.5f;
        targetCharacter.isParallelToViewCanvas = true;
        mainCamera.enabled = true;
        Camera2.enabled = false;
    
            while (true) {
    
            targetRandomize();  
            ponCharacter.isKinematic = false;
            Fire(spawnInterval-0.5f);
            timer = 0f;
            yield return new WaitUntil(() => timer > spawnInterval || keyPressed == true);
            yield return new WaitForSeconds(0.5f);
            DestroyPrefab(GameObject.Find("Pon(Clone)")); 
            }
    }



}



