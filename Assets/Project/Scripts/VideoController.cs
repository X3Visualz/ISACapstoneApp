using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class VideoController : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button backButton;

    private void Start()
    {
        videoPanel.SetActive(false);

        if (backButton != null)
            backButton.onClick.AddListener(CloseVideo);
    }

    public void PlayVideoFromStreamingAssets(string fileName)
    {
        if (string.IsNullOrEmpty(fileName) || videoPlayer == null)
        {
            Debug.LogWarning("Invalid video file name or missing video player.");
            return;
        }

        if (!fileName.EndsWith(".mp4"))
            fileName += ".mp4";

        string path = Path.Combine(Application.streamingAssetsPath, fileName);

#if UNITY_IOS
        path = "file://" + path;
#endif

        Debug.Log("Playing video from: " + path);

        videoPlayer.Stop();
        videoPlayer.clip = null; // 💡 Clear old clip
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = path;
        videoPanel.SetActive(true);

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (vp) => vp.Play();
    }

    public void CloseVideo()
    {
        if (videoPlayer != null)
            videoPlayer.Stop();

        if (videoPanel != null)
            videoPanel.SetActive(false);
    }
}
