using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTutorial()
    { 
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


