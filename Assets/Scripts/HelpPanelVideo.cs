using UnityEngine;
using UnityEngine.Video;

public class HelpPanelVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    SlidePanels slide;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        slide = FindObjectOfType<SlidePanels>();
        videoPlayer.loopPointReached += slide.LeftSlide;
    }
    private void OnDisable()
    {
        videoPlayer.loopPointReached -= slide.LeftSlide;
    }
    private void OnEnable()
    {
        videoPlayer.Play();
    }
    //private void Update()
    //{
    //    if (transform.parent.localPosition.x != 0)
    //        videoPlayer.Stop();
    //    if (transform.parent.localPosition.x > -1920 && videoPlayer.isPlaying == false)
    //        videoPlayer.Play();
    //}
}