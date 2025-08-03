using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.IO;

public class ProfileLoader : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI studentNameText;
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
        string studentID = ProfileManager.Instance.SelectedStudentID;
        Debug.Log($"🔍 ProfileLoader Start() — Student ID: {studentID}");

        if (string.IsNullOrEmpty(studentID))
        {
            Debug.LogWarning("⚠️ No student ID found in ProfileManager.");
            return;
        }

        LoadProfile(studentID);

        if (videoBackButton != null)
            videoBackButton.onClick.AddListener(CloseVideoPanel);

        if (videoPanel != null)
            videoPanel.SetActive(false);
    }

    public void LoadProfile(string id)
    {
        Debug.Log($"📄 LoadProfile() called with ID: {id}");

        StudentData data = studentDatabase.GetStudentByID(id);

        if (data == null)
        {
            Debug.LogWarning($"❌ No student found with ID: {id}");
            return;
        }

        Debug.Log($"✅ Found student: {data.studentName}");

        studentNameText.text = data.studentName;
        studentPortrait.texture = data.studentPortrait;

        SetupButton(interviewButton, data.interviewVideoFileName);
        SetupButton(compilationButton, data.projectCompilationVideoFileName);
    }

    void SetupButton(Button button, string videoFileName)
    {
        if (button == null)
        {
            Debug.LogWarning("⚠️ Button reference is missing.");
            return;
        }

        if (!string.IsNullOrEmpty(videoFileName))
        {
            Debug.Log($"🎬 Setting up button for video: {videoFileName}");

            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                Debug.Log($"▶️ Button clicked — video: {videoFileName}");
                PlayVideoFromStreamingAssets(videoFileName);
            });
        }
        else
        {
            Debug.LogWarning("⚠️ Video file name is empty. Disabling button.");
            button.gameObject.SetActive(false);
        }
    }

    void PlayVideoFromStreamingAssets(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogWarning("⚠️ Video file name is null or empty.");
            return;
        }

        if (videoPlayer == null)
        {
            Debug.LogWarning("⚠️ VideoPlayer is not assigned.");
            return;
        }

        if (videoPanel == null)
        {
            Debug.LogWarning("⚠️ VideoPanel is not assigned.");
            return;
        }

        if (!fileName.EndsWith(".mp4"))
            fileName += ".mp4";

        string fileCheckPath = Path.Combine(Application.streamingAssetsPath, fileName);
        string fullPath = fileCheckPath;

#if UNITY_IOS
        fullPath = "file://" + fullPath;
#endif

        if (!File.Exists(fileCheckPath))
        {
            Debug.LogError($"❌ Video file not found at: {fileCheckPath}");
            return;
        }

        Debug.Log($"✅ Found video file: {fileCheckPath}");
        Debug.Log($"Attempting to play video from: {fullPath}");

        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = fullPath;

        // 🔍 TEMP: Render directly to camera for visibility
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.targetCameraAlpha = 1f;

        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoPanel.SetActive(true);

        videoPlayer.Prepare();
        Debug.Log("🔄 videoPlayer.Prepare() called");
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("✅ Video prepared and now playing.");
        vp.Play();
    }

    void CloseVideoPanel()
    {
        if (videoPlayer != null)
            videoPlayer.Stop();

        if (videoPanel != null)
            videoPanel.SetActive(false);
    }
}
