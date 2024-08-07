using System.Numerics;
using Tobii.Research;

public class EyeFormat
{
    public Vector2 LeftGaze { get; set; }
    public Vector2 RightGaze; //on screen 
    public float LeftPupilSize;
    public float RightPupilSize;// in mm
    public int SystemTimeStamp;
    public int TrailTag;
    public int FrameTag;
    public Vector4 Validity;//(lG,RG,LP,RP)
}
