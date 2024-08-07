using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using Newtonsoft.Json;
using System.IO;
public class TobiiHandler : MonoBehaviour
{
    public GameObject cursor;
    public GameObject SizeLeft;
    public GameObject SizeRight;
    public Camera maincamera;

    Tobii.Research.PupilData LeftPupilData;
    Tobii.Research.PupilData RightPupilData;
    Tobii.Research.GazePoint LeftGaze;
    Tobii.Research.GazePoint RightGaze;
    IEyeTracker Fourc;
    RectTransform canvas;
    public List<EyeFormat> eyeDataToSave;
    public EyeFormat perEye;
    public int  TrialTag;
    public int  FrameTag;



    void Awake(){
        eyeDataToSave = new List<EyeFormat>();
        canvas = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();

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

    private  void  GazeEventHandler (object sender , GazeDataEventArgs e)
    {
        
        LeftGaze = e.LeftEye.GazePoint;
        RightGaze = e.RightEye.GazePoint;
        LeftPupilData = e.LeftEye.Pupil;
        //Debug.Log("Got gaze data with:" + LeftGazePoint.PositionOnDisplayArea);
        //Debug.Log("Got pupil data with:" + LeftPupilData.PupilDiameter );
        RightPupilData = e.RightEye.Pupil;
        //Debug.Log("time: "+ e.SystemTimeStamp);

        perEye.LeftGaze = new Vector2(LeftGaze.PositionOnDisplayArea.X,LeftGaze.PositionOnDisplayArea.Y);
        perEye.RightGaze = new Vector2(RightGaze.PositionOnDisplayArea.X,RightGaze.PositionOnDisplayArea.Y);
        perEye.LeftPupilSize = LeftPupilData.PupilDiameter;
        perEye.RightPupilSize = RightPupilData.PupilDiameter;
        perEye.SystemTimeStamp = e.SystemTimeStamp;
        perEye.TrailTag = TrialTag;
        perEye.FrameTag = FrameTag;
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

    void SaveData(){
         Debug.Log("eyeDataleft: "+ eyeDataToSave);
        string jsonData  = JsonConvert.SerializeObject(eyeDataToSave);
       Debug.Log("json: "+jsonData);
       Debug.Log(Application.dataPath + "/Resources");
       if (!Directory.Exists(Application.dataPath + "/Resources/EyeData")){
           Directory.CreateDirectory(Application.dataPath + "/Resources/EyeData");
       }
       File.WriteAllText
        (Application.dataPath + "/Resources/EyeData/" + System.DateTime.Now.ToString()  + ".json", 
        jsonData);
    }


    void OnDestroy(){
        if(Fourc != null){
        Fourc.GazeDataReceived -= GazeEventHandler;
        }

        SaveData();
    }
}
