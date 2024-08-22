using UnityEngine;
using System;
using System.Collections.Generic;

public class MouseEvents : MonoBehaviour
{
    bool isRecording;
    public Variants variants;
    List<MouseEventsFormat> mouseData;

    void Start()
    {
        mouseData = new List<MouseEventsFormat>();
    }

    void Update()
    {
        isRecording = variants.isRecording;
        Debug.Log("isRecording: "+isRecording);
     if(isRecording)
        {
        if(Input.GetMouseButton(0)){MouseEventsFormat mouseEventsFormat = new MouseEventsFormat();
            {mouseEventsFormat.timestamp =  new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
             mouseEventsFormat.mousePOs = new System.Numerics.Vector2(Input.mousePosition.x,Input.mousePosition.y);
             Debug.Log("mouseEventsFormat.mousePOs: "+mouseEventsFormat.mousePOs);}
            mouseData.Add(mouseEventsFormat);}
        }
        if(Input.GetMouseButtonUp(0)){MouseEventsFormat mouseEventsFormat = new MouseEventsFormat();
            {mouseEventsFormat.timestamp =  new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            mouseEventsFormat.buttonUP = true;
            mouseData.Add(mouseEventsFormat);
            Debug.Log("butonUP");
            } }
    }

    void OnDestroy(){
        DataOutput dataOutput = new DataOutput();
        dataOutput.SaveData<MouseEventsFormat>(mouseData, "/Resources/Smog/smogMouseData/","smogMouse");
    }
}


public class MouseEventsFormat{
    public long timestamp;
    public System.Numerics.Vector2 mousePOs ; 
    public bool buttonUP;
}