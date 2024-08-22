using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using Newtonsoft.Json;
using System.IO;
using System;


public class TobiiHandler1 : MonoBehaviour
{
    public GameObject cursor;
    public GameObject cursor2;
    public GameObject[] cursori;
    public GameObject[] cursor2i;
    public GameObject pon;
    public GameObject[] ponEdge;
    public GameObject[] markers;
    public GameObject[] markers1;

    public SmogFrameData dotSmog;
    public List<SmogFrameData> smogFrameToSave;

    public GameObject SizeLeft;
    public GameObject SizeRight;
    public Camera maincamera;
    public Camera maincameraMain;

    public PupilData LeftPupilData;
    public PupilData RightPupilData;
    public GazePoint LeftGaze;
    public GazePoint RightGaze;
    IEyeTracker Fourc;
    RectTransform canvas;
    public List<EyeFormat> eyeDataToSave;
    public EyeFormat perEye;
    public Variants smogVariants;
    private Tobii.Research.Unity.GazeTrail _gazeTrail;


    void Awake(){
        _gazeTrail = Tobii.Research.Unity.GazeTrail.Instance;

        Debug.Log("Awake:"+ Display.displays.Length);
        eyeDataToSave = new List<EyeFormat>();
        canvas = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        smogFrameToSave = new List<SmogFrameData>();
        pon = Instantiate(cursor, cursor.transform.position, Quaternion.identity);
        pon.transform.SetParent(cursor.transform.parent);

        cursori = new GameObject[8];
        for (int i = 0; i < 8; i++){
            cursori[i] = Instantiate(cursor, cursor.transform.position, Quaternion.identity);
            cursori[i].transform.SetParent(cursor.transform.parent);
        }
        cursor2i = new GameObject[3];
        for (int i = 0; i < 3; i++){
            cursor2i[i] = Instantiate(cursor2, cursor2.transform.position, Quaternion.identity);
            cursor2i[i].transform.SetParent(cursor2.transform.parent);
        }
        ponEdge = new GameObject[6];
        for(int i = 0; i < 6; i++)
        {ponEdge[i] = Instantiate(cursor, cursor.transform.position, Quaternion.identity);
        ponEdge[i].transform.SetParent(cursor.transform.parent);}

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
        dotSmog = new SmogFrameData();
        {dotSmog.frameTag = smogVariants.frameID;
        dotSmog.timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

        dotSmog.targetpos = new List<System.Numerics.Vector2>();
        dotSmog.cannonpos = new List<System.Numerics.Vector2>();
        for (int i = 0; i < 8; i++){
            Dots(cursori[i], markers[i]);
            Vector2 pos= RectTransformUtility.WorldToScreenPoint(maincameraMain, markers[i].transform.position);
            System.Numerics.Vector2 pos2 = new System.Numerics.Vector2(pos.x, pos.y);
            dotSmog.targetpos.Add(pos2);  
        }
        for (int i = 0; i < 3; i++){
            Dots(cursor2i[i], markers1[i]);
            Vector2 pos= RectTransformUtility.WorldToScreenPoint(maincameraMain, markers1[i].transform.position);
            System.Numerics.Vector2 pos2 = new System.Numerics.Vector2(pos.x, pos.y);
            dotSmog.cannonpos.Add(pos2);
        }
        

        dotSmog.ponPos = new List<System.Numerics.Vector2>();
        GameObject PonClone = GameObject.Find("coin(Clone)");
        if(PonClone != null){
            for (int i = 0; i < 6; i++){
                GameObject ponedgeobj = PonClone.transform.GetChild(i).gameObject;
                Vector2 ponpos = RectTransformUtility.WorldToScreenPoint
                (maincameraMain,ponedgeobj.transform.position);
                ponEdge[i].GetComponent<RectTransform>().anchoredPosition= ponpos;
                dotSmog.ponPos.Add(new System.Numerics.Vector2(ponpos.x, ponpos.y) );
            }

            Vector2 ponposc =   RectTransformUtility.WorldToScreenPoint
            (maincameraMain, PonClone.transform.position);
            pon.GetComponent<RectTransform>().anchoredPosition= ponposc;
            dotSmog.ponPos.Add(new System.Numerics.Vector2(ponposc.x, ponposc.y) );
        }
        }
        smogFrameToSave.Add(dotSmog);

        if(LeftPupilData != null){GazePlot();}
    }

     void Dots(GameObject cursor, GameObject marker1)
    {
        Vector2 pos1= RectTransformUtility.WorldToScreenPoint(maincameraMain, marker1.transform.position);
        cursor.GetComponent<RectTransform>().anchoredPosition = pos1;
        //Debug.Log($"{marker1.gameObject.name}"+pos1);
    }

    private void GazePlot(){
        if(LeftPupilData.Validity == Validity.Valid && RightPupilData.Validity == Validity.Valid){
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
        perEye.SystemTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        perEye.Validity = string.Format("{0},{1},{2},{3}", LeftGaze.Validity.ToString(), RightGaze.Validity.ToString(),
             LeftPupilData.Validity.ToString(), LeftPupilData.Validity.ToString());
        perEye.TrailTag = 0;
        perEye.FrameTag = smogVariants.frameID;
        eyeDataToSave.Add(perEye);
    }

    private void  ProGetDevice(){
        var eyetracker = EyeTrackingOperations.FindAllEyeTrackers();
        Debug.Log(eyetracker.Count);
        if(eyetracker.Count > 0){
            Fourc = eyetracker[0];
            Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", Fourc.Address, Fourc.DeviceName, Fourc.Model, Fourc.SerialNumber, Fourc.FirmwareVersion) );
        }
        
        
    }
    
    void Subscribe(){
        if(Fourc != null){
             Fourc.GazeDataReceived += GazeEventHandler;
        }
       
    }


    void OnDestroy(){
        if(Fourc != null){
        Fourc.GazeDataReceived -= GazeEventHandler;
        }
        DataOutput dataOutpute = new DataOutput();
        dataOutpute.SaveData<EyeFormat>(eyeDataToSave, "/Resources/smogEyeData/","smogEye");

        DataOutput frameData = new DataOutput();
        frameData.SaveData<SmogFrameData>(smogFrameToSave, "/Resources/smogFrameData/","smogFrame");
    }
}
