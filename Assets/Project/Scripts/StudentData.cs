using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Student", menuName = "Student/Profile")]
public class StudentData : ScriptableObject
{
    public string studentID;
    public string studentName;
    public Texture studentPortrait; // Used with RawImage
    public VideoClip interviewVideo;
    public VideoClip projectCompilationVideo; // <-- Keep original field name
}
