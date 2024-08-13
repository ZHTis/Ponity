using Tobii.Research;
using UnityEngine;

public class EyeFormat
{
    public float LeftGazeX ;
    public float LeftGazeY ;
    public float RightGazeX;
    public float RightGazeY; //on screen 
    public float LeftPupilSize;
    public float RightPupilSize;// in 
    public long SystemTimeStamp;
    public int TrailTag;
    public int FrameTag;
    public bool isGrey;
    public string Validity;//(lG,RG,LP,RP)
}
