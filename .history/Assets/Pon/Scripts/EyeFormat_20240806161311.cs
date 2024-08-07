using Tobii.Research;
using UnityEngine;

public class EyeFormat
{
    public Vector2 LeftGaze ;
    public Vector2 RightGaze; //on screen 
    public float LeftPupilSize;
    public float RightPupilSize;// in mm
    public long SystemTimeStamp;
    public int TrailTag;
    public int FrameTag;
    public string Validity;//(lG,RG,LP,RP)
}
