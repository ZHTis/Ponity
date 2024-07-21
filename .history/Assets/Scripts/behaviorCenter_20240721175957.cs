using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class behaviorCenter : ScriptableObject
{
   public int trial;
   public int session;
   public int session_interlude;
   public int repeatCount;

   [Range(0f,1.3f)]
    public float ratio;
   public int correctCount;
   [Range(0f,1f)]
   public float correctRate;
   public bool isCorrect;

   public string filePath;

   public string PlayerName;

   public string choice;
   public float touchTimefromInit;
   public float touchTimefromPause;
   public float initTime;

    //run before all trials
   public void Reset()
   {
       trial = 0;
       correctCount = 0;
       correctRate = 0f;
       choice = "";
       touchTimefromInit = 0f;
       touchTimefromPause = 0f;
       initTime = 0f;
       isCorrect = false;
       filePath = PlayerName + DateTime.Now.ToString("-MM-dd-HH-mm-ss-yyyy") + ".csv";
   }

   //run after each choice
   public void OnValidate(){
    correctRate = (float)correctCount / (float)trial;
    isCorrect=false;
    touchTimefromInit = 0f;
    touchTimefromPause = 0f;
    choice = "";}
}
