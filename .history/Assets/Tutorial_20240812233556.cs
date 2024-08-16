using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Apple.ReplayKit;
public class Tutorial : MonoBehaviour
{

    private bool withMarker= false;//true;
    [SerializeField] private GameObject prefab;
    public ponCharacter ponCharacter;
    public targetCharacter targetCharacter;
    public camShelfCharacter camShelfCharacter;
    public behaviorCenter behaviorC;


    private float spawnInterval ;
    public enum rightChoice  {a,l}
    private bool keyPressed =false;
    private float timer;
    public Camera mainCamera ;
    public Camera Camera2 ;


     private int repeatCount;
    private float[] ratio;
    private int[] ponVel_x;
    private int[] ponVel_y;
    private float[]targetDistance ;
    private float[] camNeck;
    private int[] camIDList;
    private bool abortAllowed=false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Grey());
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
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

 void Fire(float DestroyTime = 1.1f, bool realFire = true)
    {
       if(realFire == true)
       {
        behaviorC.trial += 1;
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
   

    IEnumerator StartTutorial()
    { 
        float waitbeforechoice = 2f;
        ratio = new float[] {1,0.6f};
        camIDList = new int[] {1,2};
        camShelfCharacter.radius = 8f;
        ponCharacter.vel_x = 5;
        ponCharacter.vel_y = 0;
        targetDistance =new float[]{1.2f};
        camNeck = new float[]{2} ;
        Physics.gravity = new Vector3(0, -9.8f, 0);
        targetCharacter.isParallelToViewCanvas = false; 
      

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

    DestroyPrefab(GameObject.Find("Pon(Clone)")); 
    StartCoroutine(Grey());
    }}

       void DestroyPrefab(GameObject prefab)
    {   if (prefab != null){
        Destroy(prefab);}
    }

    IEnumerator Grey(){
       mainCamera.enabled = false;
    
       Camera2.enabled = false;
       yield return new WaitForSeconds(0.8f);

        mainCamera.enabled = true;
    }

 
}


