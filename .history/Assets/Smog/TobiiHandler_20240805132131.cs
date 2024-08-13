using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;

public class TobiiHandler : MonoBehaviour
{
    public GameObject cursor;
    public GameObject Size;
    Vector3 worldPos;
    public Camera maincamera;
    Tobii.Research.PupilData LeftPupilData;

    IEyeTracker Fourc;

    [Tooltip("Distance from screen to visualization plane in the World.")]
	public float VisualizationDistance = 10f;




    // Start is called before the first frame update
    void Start()
    {
        cursor.transform.localScale = new Vector3(1f, 1f, 1f);
        ProGetDevice(); 
        Subscribe();
    }

   
    void Update()
    {
        UsingGamingtoShowGazePosition();
        Size.transform.localScale = new Vector3(LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter, LeftPupilData.PupilDiameter);
    }


    void UsingGamingtoShowGazePosition(){
        Tobii.Gaming.GazePoint gazePoint = Tobii.Gaming.TobiiAPI.GetGazePoint();
        //Debug.Log(gazePoint);
        if(gazePoint.IsValid){
        Vector3 screenPos = gazePoint.Screen;
        screenPos += (transform.forward * VisualizationDistance);
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        cursor.transform.position = worldPos;
        //Debug.Log("worldPos"+  worldPos);
        }
    }
    private  void  GazePos (object sender , GazeDataEventArgs e)
    {// eyedata: daze, pupil
        
        Tobii.Research.GazePoint LeftGazePoint = e.LeftEye.GazePoint;
        LeftPupilData = e.LeftEye.Pupil;
        Debug.Log("Got gaze data with:" + LeftGazePoint.PositionOnDisplayArea);
        Debug.Log("Got pupil data with:" + LeftPupilData.PupilDiameter );
        
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