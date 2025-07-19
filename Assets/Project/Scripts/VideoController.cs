using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button backButton;

    public void PlayVideo(VideoClip clip)
    {
        videoPanel.SetActive(true);
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void CloseVideo()
    {
        videoPlayer.Stop();
        videoPanel.SetActive(false);
    }

    private void Start()
    {
        // Ensure panel is hidden at start
        videoPanel.SetActive(false);

        if (backButton != null)
            backButton.onClick.AddListener(CloseVideo);
    }
}
