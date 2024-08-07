
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;


public class TobiiHandler : MonoBehaviour
{
    public GameObject cursor;
    public GameObject SizeLeft;
    public GameObject SizeRight;
    public Camera maincamera;
    public behaviorCenter behaviorC;
    public PupilData LeftPupilData;
    public PupilData RightPupilData;
    public GazePoint LeftGaze;
    public GazePoint RightGaze;
    IEyeTracker Fourc;
    RectTransform canvas;
    public List<EyeFormat> eyeDataToSave;
    public EyeFormat perEye;
    public int  TrialTag=0;
    public int  FrameTag=0;
    public TrialState trialState;


    void Awake(){
        Debug.Log("Awake:"+ Display.displays.Length);
        eyeDataToSave = new List<EyeFormat>();
        canvas = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        trialState.FrameTag =0;

    }

    // Start is called before the first frame update
    void Start()
    {
        cursor.transform.localScale = new Vector3(1f, 1f, 1f) ;
        ProGetDevice(); 
        Subscribe();
    }

   
    void Update()
    {
      GazePlot();
      //Refresh tags
      if(trialState.ifTrialAltered == true){
        trialState.FrameTag +=1;
      }
      if(trialState.ifTrialAltered ==false){
        trialState.FrameTag =0;
      }
    
    }


    private void GazePlot(){
        if(LeftPupilData != null && RightPupilData != null){
        SizeLeft.GetComponent<RectTransform>().localScale = 
            new Vector3(LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter) *0.5f;
        SizeRight.GetComponent<RectTransform>().localScale = 
            new Vector3(RightPupilData.PupilDiameter, RightPupilData.PupilDiameter, RightPupilData.PupilDiameter) *0.5f;
       
        float x = 0.5f * (LeftGaze.PositionOnDisplayArea.X + RightGaze.PositionOnDisplayArea.X);
        float y = 0.5f * (LeftGaze.PositionOnDisplayArea.Y + RightGaze.PositionOnDisplayArea.Y);
        //Debug.Log("gaze: "+new Vector2(x,y));
       
        
        float width = canvas.rect.width;
        float height = canvas.rect.height;
        float cursorX = width * x;
        float cursorY = height * y;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(cursorX, cursorY);
        //Debug.Log("cursor"+cursor.GetComponent<RectTransform>().anchoredPosition);
        }

    }

    void  GazeEventHandler (object sender , GazeDataEventArgs e)
    {
        //Debug.Log("sender: "+sender);
        LeftGaze = e.LeftEye.GazePoint;
        RightGaze = e.RightEye.GazePoint;
        LeftPupilData = e.LeftEye.Pupil;
        //Debug.Log("Got gaze data with:" + LeftGazePoint.PositionOnDisplayArea);
        //Debug.Log("Got pupil data with:" + LeftPupilData.PupilDiameter );
        RightPupilData = e.RightEye.Pupil;
        //Debug.Log("time: "+ e.SystemTimeStamp);
        perEye = new EyeFormat();
        Debug.Log("perEye: "+ perEye);
        perEye.LeftGazeX = LeftGaze.PositionOnDisplayArea.X;
        perEye.LeftGazeY = LeftGaze.PositionOnDisplayArea.Y;
        perEye.RightGazeX = RightGaze.PositionOnDisplayArea.X;
        perEye.RightGazeY = RightGaze.PositionOnDisplayArea.Y;
        perEye.LeftPupilSize = LeftPupilData.PupilDiameter;
        perEye.RightPupilSize = RightPupilData.PupilDiameter;
        perEye.SystemTimeStamp = e.SystemTimeStamp;
        perEye.Validity = string.Format("{0},{1},{2},{3}", LeftGaze.Validity.ToString(), RightGaze.Validity.ToString(),
             LeftPupilData.Validity.ToString(), LeftPupilData.Validity.ToString());
        perEye.TrailTag = trialState.TrialTag;
        perEye.FrameTag = trialState.FrameTag;
        eyeDataToSave.Add(perEye);
    }

    private void  ProGetDevice(){
        var eyetracker = EyeTrackingOperations.FindAllEyeTrackers();
        Debug.Log(eyetracker.Count);
        Fourc = eyetracker[0];
        Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", Fourc.Address, Fourc.DeviceName, Fourc.Model, Fourc.SerialNumber, Fourc.FirmwareVersion) );
        
    }
    
    void Subscribe(){
        
        Fourc.GazeDataReceived += GazeEventHandler;
    }


    void OnDestroy(){
        if(Fourc != null){
        Fourc.GazeDataReceived -= GazeEventHandler;
        }
        DataOutput dataOutpute = new DataOutput();
        dataOutpute.SaveData<EyeFormat>(eyeDataToSave, "/Resources/EyeData/", behaviorC.PlayerName);
        Debug.Log("framecount"+trialState.FrameTag);
    }
}
