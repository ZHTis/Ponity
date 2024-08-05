using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;

public class TobiiHandler1 : MonoBehaviour
{
    public GameObject cursor;
    public GameObject SizeLeft;
    public GameObject SizeRight;
    Vector3 worldPos;
    public Camera maincamera;
    Tobii.Research.PupilData LeftPupilData;
    Tobii.Research.PupilData RightPupilData;

    IEyeTracker Fourc;

    [Tooltip("Distance from screen to visualization plane in the World.")]
	public float VisualizationDistance = 30f;




    // Start is called before the first frame update
    void Start()
    {
        cursor.transform.localScale = new Vector3(1f, 1f, 1f) * 0.2f;
        ProGetDevice(); 
        Subscribe();
    }

   
    void Update()
    {
        UsingGamingtoShowGazePosition();
        SizeLeft.transform.localScale = new Vector3(LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter);
        SizeRight.transform.localScale = new Vector3(RightPupilData.PupilDiameter, RightPupilData.PupilDiameter, RightPupilData.PupilDiameter);
    }


    void UsingGamingtoShowGazePosition(){
        Tobii.Gaming.GazePoint gazePoint = Tobii.Gaming.TobiiAPI.GetGazePoint();
        //Debug.Log(gazePoint);
        if(gazePoint.IsValid){
        Vector3 screenPos = gazePoint.Screen;
        screenPos += (transform.forward * VisualizationDistance);
        worldPos = maincamera.ScreenToWorldPoint(screenPos);
        cursor.transform.position = worldPos;
        //Debug.Log("worldPos"+  worldPos);
        }
    }
    private  void  GazePos (object sender , GazeDataEventArgs e)
    {// eyedata: daze, pupil
        
        Tobii.Research.GazePoint LeftGazePoint = e.LeftEye.GazePoint;
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
       //to-do: set screren
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
