using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "StudentData", menuName = "ISA/Student Data")]
public class StudentData : ScriptableObject
{
    public string studentID;
    public string studentName;
    public Texture studentPortrait;

    // Video file names (must match files in StreamingAssets)
    public string interviewVideoFileName;
    public string projectCompilationVideoFileName;
}
