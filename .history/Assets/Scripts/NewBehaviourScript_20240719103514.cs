using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO; 
using Newtonsoft.Json;
using System;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.Experimental.GlobalIllumination;


public class NewBehaviourScript : MonoBehaviour
{
    public Camera mainCamera ;
    public Camera Camera2 ;
    private string player = "test";
    private bool withMarker= false;//true;
    [SerializeField] private GameObject prefab;
    public ponCharacter ponCharacter;
    public targetCharacter targetCharacter;
    public camShelfCharacter camShelfCharacter;
    public behaviorCenter behaviorC;
    private List<ExpSaveFormat> trialDataList;
    private float spawnInterval = 0.5f;
    public enum rightChoice  {a,l}
    private bool keyPressed =false;
    private float timer;
    private ExpSaveFormat trialData;


/// <summary>
/// Parameter space
/// </summary>
    private int[] repeat = Enumerable.Range(1, 3).ToArray();
    private float[] ratio = {0.3f, 0.2f, 0.1f};
    private int[] ponVel;
    private float[]targetDistance ;
    private float[] camNeck;


    void Awake(){
      Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        behaviorC.Reset();
        behaviorC.PlayerName = player;

        trialDataList = new List<ExpSaveFormat>();

        ponCharacter.withMarker = withMarker;

        //StartCoroutine(DebugCharacter());

        EnterBreak();
        StartCoroutine(exitBreak());
        StartCoroutine(ThisIsIt());
    }

   void EnterBreak()
   {
        Camera2.enabled = true;
        mainCamera.enabled = false;
   }

   IEnumerator exitBreak(){
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Camera2.enabled = false;
        mainCamera.enabled = true;
   }

    void Update()
    {
        timer += Time.deltaTime;
        claimAnswer();
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L)) && keyPressed==false && mainCamera.enabled == true) 
        {
            behaviorC.choice = Input.inputString.ToString();
            keyPressed = true;
            behaviorC.touchTime = Time.time-behaviorC.initTime;

            //Debug.Log("rightChoice: " + targetCharacter.rightChoice);
            //Debug.Log("choice: " + behaviorC.choice);

            if (StringComparer.OrdinalIgnoreCase.Equals
                (behaviorC.choice, targetCharacter.rightChoice) == true)
            {
                behaviorC.correctCount += 1;
                behaviorC.isCorrect = true;
                targetCharacter.makeInvisible = true;
                //Debug.Log(targetCharacter.makeInvisible);
            }

            ///record trial data
            createExpDataSlot(out trialData);
            trialDataList.Add(trialData);
            //Debug.Log(JsonConvert.SerializeObject(trialData));
            /// end record

            behaviorC.OnValidate();
            //Debug.Log("correctRate: " + behaviorC.correctRate);
        }
       

    }


  void Fire(float DestroyTime = 1.1f, bool realFire = true)
    {
       GameObject ponPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
       if(realFire == true)
       {
        behaviorC.trial += 1;
       keyPressed = false;
       ponCharacter.kinematicPosGiven = false;
       behaviorC.initTime = Time.time;
       }
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
            if (session == 1) {
                camShelfCharacter.camID = 1;
                camShelfCharacter.radius = 5.5f;
                ponVel = Enumerable.Range(1, 7).OrderByDescending(x => x).ToArray();
                targetDistance =new float[]{1.1f,1.3f,1.6f,1.7f,1.8f};
                camNeck = new float[]{5,3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = true;
                // 135
            }

            if (session == 2) {
                camShelfCharacter.camID = 2;
                camShelfCharacter.radius = 5.5f;
                ponVel = Enumerable.Range(3, 7).OrderByDescending(x => x).ToArray();
                targetDistance =new float[]{1.1f,1.3f,1.6f,1.7f,1.8f};
                camNeck = new float[]{5,3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = true;
                // 135
            }

            if (session == 3) {
                camShelfCharacter.camID = 3;
                camShelfCharacter.radius = 8f;
                ponVel = Enumerable.Range(2, 4).OrderByDescending(x => x).ToArray();
                targetDistance =new float[]{1.1f,1.2f,1.3f,1.4f,1.5f};
                camNeck = new float[]{3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = false;
                // 120
            }

            if (session == 4) {
                camShelfCharacter.camID = 1;
                camShelfCharacter.radius = 5.5f;
                ponVel = Enumerable.Range(1, 9).OrderByDescending(x => x).ToArray();
                targetDistance =new float[]{1.1f,1.3f,1.6f,1.7f,1.8f};
                camNeck = new float[]{5,3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = false;
            }

            if (session == 5) {
                camShelfCharacter.camID = 2;
                camShelfCharacter.radius = 5.5f;
                ponVel = Enumerable.Range(3, 9).OrderByDescending(x => x).ToArray();
                targetDistance =new float[]{1.1f,1.3f,1.6f,1.7f,1.8f};
                camNeck = new float[]{5,3.5f,2,0} ;
                targetCharacter.isParallelToViewCanvas = false;
            }
    }
   
   

   /// <summary>
   /// main trial loop
   /// </summary>
   /// <returns></returns>
    IEnumerator ThisIsIt()
    { 
        spawnInterval = 1.5f;
        
        for(int i = behaviorC.session; i < 6; i++)
        {
            behaviorC.session = i;
            Debug.Log("Session: " + i);
            defineSession(behaviorC.session);

            for(int p = 0; p < repeat.Length; p++){

            for(int m= behaviorC.session_interlude; m < camNeck.Length; m++)
            {
                camShelfCharacter.neck = camNeck[m];
                behaviorC.session_interlude = m;

                StartCoroutine(exitBreak());
                yield return new WaitUntil(() => mainCamera.enabled == true);
            
            for (int j = 0; j < targetDistance.Length; j++){
                for (int k = 0; k < ratio.Length; k++){
                    for (int l = 0; l < ponVel.Length; l++){   
            ponCharacter.vel_x = ponVel[l];
            targetCharacter.distance = targetDistance[j];
            behaviorC.ratio = ratio[k];
            targetRandomize();  
            ponCharacter.isKinematic = false;
            Fire(spawnInterval-0.5f);
            timer = 0f;
            yield return new WaitUntil(() => timer > spawnInterval || keyPressed == true);
            yield return new WaitForSeconds(0.5f);
            DestroyPrefab(GameObject.Find("Pon(Clone)")); }

            }
            } EnterBreak(); //ratio*vel*diastance = break, then repeat
   
            
        }}

    }
    
    }

    void DestroyPrefab(GameObject prefab)
    {   if (prefab != null){
        Destroy(prefab);}
    }

    void OnDestroy()
    {
       string jsonData  = JsonConvert.SerializeObject(trialDataList);
       Debug.Log(jsonData);
       if (!Directory.Exists(Application.dataPath + "/Resources")){
           Directory.CreateDirectory(Application.dataPath + "/Resources");
       }
       File.WriteAllText
        (Application.dataPath + "/Resources/"  + behaviorC.filePath + ".json", 
        jsonData);
    
    }
    
    public ExpSaveFormat createExpDataSlot(out ExpSaveFormat trialData)
    {  ExpSaveFormat expData= new ExpSaveFormat();

       {expData.trial = behaviorC.trial;
       expData.choice = behaviorC.choice;
       expData.isCorrect = behaviorC.isCorrect;
        expData.touchTime= behaviorC.touchTime;
        expData.initTime = behaviorC.initTime;

        expData.ratio = behaviorC.ratio;
        expData.vel = ponCharacter.vel_x;
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



