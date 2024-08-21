
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;


public class TobiiHandler : MonoBehaviour
{
    public GameObject cursor;
    public GameObject[] cursori;
    public GameObject SizeLeft;
    public GameObject SizeRight;
    public Camera maincamera;
    public Camera maincameraMain;
    public behaviorCenter behaviorC;
    public PupilData LeftPupilData;
    public PupilData RightPupilData;
    public GazePoint LeftGaze;
    public GazePoint RightGaze;
    IEyeTracker Fourc;
    RectTransform canvas;
    public List<EyeFormat> eyeDataToSave;
    public EyeFormat perEye;

    public TrialState trialState;

    
    public GameObject[] markers;




     void Dots(GameObject cursor, GameObject marker1)
    {
        Vector2 pos1= RectTransformUtility.WorldToScreenPoint(maincameraMain, marker1.transform.position);
        cursor.GetComponent<RectTransform>().anchoredPosition = pos1;
        Debug.Log($"{marker1.gameObject.name}"+pos1);
    }


    void Awake(){
        Debug.Log("Awake:"+ Display.displays.Length);
        eyeDataToSave = new List<EyeFormat>();
        canvas = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        trialState.FrameTag =0;
        cursori = new GameObject[8];
        for (int i = 0; i < 8; i++){
            cursori[i] = Instantiate(cursor, cursor.transform.position, Quaternion.identity);
        }
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
        //Dots(cursor,markers[0]);
        for (int i = 0; i < 8; i++){
            Dots(cursori[i], markers[i]);
        }
      //GazePlot();
      //Refresh tags
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
        perEye.isGrey = trialState.grey;
        eyeDataToSave.Add(perEye);
    }

    private void  ProGetDevice(){
        var eyetracker = EyeTrackingOperations.FindAllEyeTrackers();
        Debug.Log(eyetracker.Count);
        Fourc = eyetracker[0];
        Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", Fourc.Address, Fourc.DeviceName, Fourc.Model, Fourc.SerialNumber, Fourc.FirmwareVersion) );
        DisplayArea displayArea = Fourc.GetDisplayArea();
        Debug.Log($"bottomleft: ,{ displayArea.BottomLeft.X}, topright: , {displayArea.TopRight.X}");
    }
    
    void Subscribe(){
        
        Fourc.GazeDataReceived += GazeEventHandler;
    }


    void OnDestroy(){
        if(Fourc != null){
        Fourc.GazeDataReceived -= GazeEventHandler;
        }
        DataOutput dataOutpute = new DataOutput();
        dataOutpute.SaveData<EyeFormat>(eyeDataToSave, "/Resources/PonEyeData/", behaviorC.PlayerName);
        Debug.Log("framecount"+trialState.FrameTag);
    }
}
