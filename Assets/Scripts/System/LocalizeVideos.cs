using UnityEngine;
using UnityEngine.Video;

public class LocalizeVideos : MonoBehaviour
{
    private VideoPlayer currentVideoPlayer;
    public VideoClip Spanish;
    public VideoClip English;

    private void Awake() => currentVideoPlayer = GetComponent<VideoPlayer>();

    private void OnEnable()
    {
        switch (SaveSystem.data.language)
        {
            case 0: currentVideoPlayer.clip = English; break;
            case 1: currentVideoPlayer.clip = Spanish; break;
        }
    }
}