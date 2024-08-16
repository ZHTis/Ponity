using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Newtonsoft.Json;
using System;
using UnityEngine.Apple.ReplayKit;


public class NewBehaviourScript : MonoBehaviour
{
    public Camera mainCamera ;
    public Camera Camera2 ;
    public Camera Camera3 ;
    public Camera Camera4 ;

    private bool withMarker=false;//true;
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
        behaviorC.Reset();
        trialDataList = new List<ExpSaveFormat>();
        ponCharacter.withMarker = withMarker;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DebugCharacter());

        EnterBreak();
        StartCoroutine(exitBreak());
        StartCoroutine(ThisIsIt(1));
    }

   void EnterBreak()
   {
        Camera2.enabled = true;
        mainCamera.enabled = false;
   }
   void Grey(){

   }

   IEnumerator exitBreak(){
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        Camera2.enabled = false;
        mainCamera.enabled = true;
   }

   void FinalBreak(){
       Camera3.enabled = true;
       Camera2.enabled = false;
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
                targetCharacter.makeInvisible = true; // make target invisible
                //Debug.Log(targetCharacter.makeInvisible);
            }

            behaviorC.OnValidate();
            //Debug.Log("correctRate: " + behaviorC.correctRate);
        }

        if(mainCamera.enabled == true &&
                Input.GetKeyDown(KeyCode.Space) && 
                keyPressed==false &&
                abortAllowed==true)
                {
            behaviorC.choice = "abort";
            keyPressed = true;
            Debug.Log("Abort");
            behaviorC.touchTimefromInit = Time.time-behaviorC.initTime;
            if(ponCharacter.kinematicTime!=0){behaviorC.touchTimefromPause = Time.time-ponCharacter.kinematicTime;}
            
            behaviorC.OnValidate();
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
                ratio = new float[] {0.1f,0.15f,0.2f,0.25f,0.3f,0.35f,0.4f,0.5f,0.6f};
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
                ratio = new float[] {0.1f,0.15f,0.2f,0.25f,0.3f,0.35f,0.4f,0.5f,0.6f};
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
   
   void Shuffle<T>(T[] array){
    int n = array.Length;
    System.Random random = new System.Random();

            for (int i = 0; i < n - 1; i++)
            {
                int j =random.Next(i, n);

                if (j != i)
                {
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
   }

   void if_Shuffle<T>(T[] list, bool shuffle){

    if(shuffle){Shuffle(list);}
    if(!shuffle){}
   }

   /// <summary>
   /// main trial loop
   /// </summary>
   /// <returns></returns>
    IEnumerator ThisIsIt(int how=0)
    {
        float waitbeforechoice = 1.0f;
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
                } EnterBreak(); }}
                break;
            
            case 1:
                
                abortAllowed = true;
                sessionList = new int[] {3,4};
       

                for (int s = 0; s < sessionList.Length; s++){
                    defineSession(sessionList[s]);behaviorC.session = sessionList[s];

                    if_Shuffle<int>(camIDList, ifShuffle);
                    if_Shuffle<float>(camNeck, ifShuffle); //if change sessions in a decending order

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
                }
                if(keyPressed == true){
                    Replay = 0;
                }
                 //record
                createExpDataSlot(out trialData);trialDataList.Add(trialData);
                //
                yield return new WaitForSeconds(0.5f);
                DestroyPrefab(GameObject.Find("Pon(Clone)")); 

                trialState.frameReadyToReset = true;
                } } } }
                EnterBreak();StartCoroutine(exitBreak());
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
        dataOutput.SaveData<ExpSaveFormat>(trialDataList, "/Resources/", behaviorC.PlayerName); 
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




