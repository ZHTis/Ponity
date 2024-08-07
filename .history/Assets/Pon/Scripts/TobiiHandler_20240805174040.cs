using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using System.ComponentModel;

public class TobiiHandler : MonoBehaviour
{
    public GameObject cursor;
    public GameObject SizeLeft;
    public GameObject SizeRight;
    public Camera maincamera;
    public Canvas canvas;
    Tobii.Research.PupilData LeftPupilData;
    Tobii.Research.PupilData RightPupilData;
    Tobii.Research.GazePoint LeftGaze;
    Tobii.Research.GazePoint RightGaze;
    IEyeTracker Fourc;

    [Tooltip("Distance from screen to visualization plane in the World.")]




    // Start is called before the first frame update
    void Start()
    {
        cursor.transform.localScale = new Vector3(1f, 1f, 1f) * 0.2f;
        ProGetDevice(); 
        Subscribe();
        SetScreen();
    }

   
    void Update()
    {
        if(LeftPupilData != null && RightPupilData != null){
        SizeLeft.GetComponent<RectTransform>().localScale = 
            new Vector3(LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter) *0.5f;
        SizeRight.GetComponent<RectTransform>().localScale = 
            new Vector3(RightPupilData.PupilDiameter, RightPupilData.PupilDiameter, RightPupilData.PupilDiameter) *0.5f;
       
        float x = 0.5f * (LeftGaze.PositionOnDisplayArea.X + RightGaze.PositionOnDisplayArea.X);
        float y = 0.5f * (LeftGaze.PositionOnDisplayArea.Y + RightGaze.PositionOnDisplayArea.Y);
        Debug.Log(new Vector2(x,y));
        Debug.Log(cursor.GetComponent<RectTransform>().anchoredPosition);
        }
    }


    private  void  GazePos (object sender , GazeDataEventArgs e)
    {// eyedata: daze, pupil
        
        LeftGaze = e.LeftEye.GazePoint;
        RightGaze = e.RightEye.GazePoint;
        LeftPupilData = e.LeftEye.Pupil;
        //Debug.Log("Got gaze data with:" + LeftGazePoint.PositionOnDisplayArea);
        //Debug.Log("Got pupil data with:" + LeftPupilData.PupilDiameter );
        RightPupilData = e.RightEye.Pupil;
    }

    private void  ProGetDevice(){
        var eyetracker = EyeTrackingOperations.FindAllEyeTrackers();
        Debug.Log(eyetracker.Count);
        Fourc = eyetracker[0];
        Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", Fourc.Address, Fourc.DeviceName, Fourc.Model, Fourc.SerialNumber, Fourc.FirmwareVersion) );
        
    }
    private void SetScreen(){
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasRect.anchorMin = Vector2.zero;
        canvasRect.anchorMax = Vector2.one;
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;
    }

    void Subscribe(){
        
        Fourc.GazeDataReceived += GazePos;
    }


    void OnDestroy(){
        if(Fourc != null){
        Fourc.GazeDataReceived -= GazePos;
        }
     
    }
}
