using System.Numerics;
using Tobii.Research;

public class EyeCenter 
{
    public Vector2 LeftEyeGaze;
    public Vector2 RightEyeGaze; //on screen 
    public float LeftEyePupilSize;
    public float RightEyePupilSize;// in mm
    public int SystemTimeStamp;
    public int TrailTag;
    public int FrameTag;
    public Vector4 Validity;//(lG,RG,LP,RP)
}
