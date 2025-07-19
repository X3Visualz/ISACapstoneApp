using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class ProfileLoader : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI studentName;
    public RawImage studentPortrait;
    public Button interviewButton;
    public Button compilationButton;

    [Header("Video Panel")]
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button videoBackButton;

    [Header("Data")]
    public StudentDatabase studentDatabase;

    void Start()
    {
        if (ProfileManager.Instance == null)
        {
            Debug.LogError("ProfileManager.Instance is null. Make sure ProfileManager exists in the Home scene and is set to DontDestroyOnLoad.");
            return;
        }

        string selectedID = ProfileManager.Instance.SelectedStudentID;
        Debug.Log("Loading profile for ID: " + selectedID);
        LoadProfile(selectedID);

        if (videoBackButton != null)
            videoBackButton.onClick.AddListener(CloseVideoPanel);

        if (videoPanel != null)
            videoPanel.SetActive(false);
    }

    public void LoadProfile(string id)
    {
        Debug.Log("Running LoadProfile with ID: " + id);

        if (studentDatabase == null)
        {
            Debug.LogError("StudentDatabase reference is null! Assign it in the inspector.");
            return;
        }

        StudentData data = studentDatabase.GetStudentByID(id);

        if (data == null)
        {
            Debug.LogWarning("No student found with ID: " + id);
            return;
        }

        // Debug logs
        Debug.Log("Student Name Loaded: " + data.studentName);
        Debug.Log("Student Portrait Texture: " + (data.studentPortrait != null ? data.studentPortrait.name : "NULL"));

        // Assign to UI
        if (studentName != null)
            studentName.text = data.studentName;
        else
            Debug.LogWarning("studentName UI reference is not set.");

        if (studentPortrait != null)
            studentPortrait.texture = data.studentPortrait;
        else
            Debug.LogWarning("studentPortrait UI reference is not set.");

        // Interview button
        if (interviewButton != null)
        {
            interviewButton.onClick.RemoveAllListeners();
            interviewButton.onClick.AddListener(() => PlayVideo(data.interviewVideo));
        }

        // Compilation button
        if (compilationButton != null)
        {
            compilationButton.onClick.RemoveAllListeners();
            compilationButton.onClick.AddListener(() => PlayVideo(data.projectCompilationVideo));
        }
    }

    void PlayVideo(VideoClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("VideoClip is null. Cannot play video.");
            return;
        }

        if (videoPlayer == null || videoPanel == null)
        {
            Debug.LogWarning("VideoPlayer or VideoPanel reference is missing.");
            return;
        }

        videoPlayer.Stop();
        videoPlayer.clip = clip;
        videoPlayer.Play();
        videoPanel.SetActive(true);
    }

    void CloseVideoPanel()
    {
        if (videoPlayer != null)
            videoPlayer.Stop();

        if (videoPanel != null)
            videoPanel.SetActive(false);
    }
}
