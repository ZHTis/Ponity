using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Tutorial : MonoBehaviour
{

    private bool withMarker= false;//true;
    [SerializeField] private GameObject prefab;
    public ponCharacter ponCharacter;
    public targetCharacter targetCharacter;
    public camShelfCharacter camShelfCharacter;
    public behaviorCenter behaviorC;

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
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
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


    IEnumerator StartTutorial()
    { 
        ratio = new float[] {1,0.1f,0.15f,0.2f,0.25f,0.3f,0.35f,0.4f,0.5f,0.6f};
        camIDList = new int[] {1,2,3};
        camShelfCharacter.radius = 8f;
        ponCharacter.vel_x = 5;
        ponCharacter.vel_y = -4;
        targetDistance =new float[]{1.1f,1.15f,1.2f,1.3f,1.7f};
        camNeck = new float[]{3.5f,2,0} ;
        targetCharacter.isParallelToViewCanvas = false; 
        Physics.gravity = new Vector3(0, 0, 0);

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
    StartCoroutine(Grey());
    }
                
    EnterBreak();StartCoroutine(exitBreak());
    }

}


