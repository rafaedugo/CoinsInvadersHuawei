using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System;
public class Videos : MonoBehaviour
{
    public TweenedPopups tween;
    private VideoPlayer videoPlayer;
    public static int clipNmbr;
    [SerializeField] private VideoClip[] spanishClips;
    [SerializeField] private VideoClip[] englishClips;
    [SerializeField] private GameObject[] panels;
    private string scene;
    private int continueCount;
    private int maxCount;
    private int showingCanvasNum;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDisable() => videoPlayer.loopPointReached -= OnVideoEnd;

    private void Start() => SetVideoClip();

    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        FindObjectOfType<ChangeScene>().Change(scene);
        enabled = false;
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began|| Input.GetKeyDown(KeyCode.Space))
        {
            Continue();
            //FindObjectOfType<ChangeScene>().Change(scene);
            //enabled = false;
        }      
        
    }

    private void SetVideoClip()
    {
        VideoClip[] clips = new VideoClip[5];
        switch (SaveSystem.data.language)
        {
            case 0: clips = englishClips; break;
            case 1: clips = spanishClips; break;
        }
        switch (clipNmbr)
        {
            case 1:
                videoPlayer.clip = clips[1];
                scene = "Videos";
                clipNmbr = 2;
                showingCanvasNum = 0;
                maxCount = 1;
                ActivePanel(showingCanvasNum);

                break;
            case 2:
                videoPlayer.clip = clips[2];
                scene = "Level0";
                showingCanvasNum = 1;
                maxCount = 2;
                ActivePanel(showingCanvasNum);
                break;
            case 3:
                videoPlayer.clip = clips[3];
                scene = "Level1";
                showingCanvasNum = 3;
                maxCount = 2;
                ActivePanel(showingCanvasNum);
                break;
            case 4:
                videoPlayer.clip = clips[4];
                scene = "Level2";
                showingCanvasNum = 5;
                maxCount = 3;
                ActivePanel(showingCanvasNum);
                break;
            case 5:
                videoPlayer.clip = clips[5];
                scene = "Level3";
                showingCanvasNum = 8;
                maxCount = 2;
                ActivePanel(showingCanvasNum);
                break;
        }
        /*if(videoPlayer.clip == null)
        {
            OnVideoEnd(videoPlayer);
        }*/
    }


    private void Continue()
    {
        continueCount++;
        showingCanvasNum++;
        if (continueCount <  maxCount)
        {
            ActivePanel(showingCanvasNum);
        }
        else
        {
            FindObjectOfType<ChangeScene>().Change(scene);
            enabled = false;
        }
    }

    private void ActivePanel(int num)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i != num)
            {
                tween.DisappearPopUps(panels[i].transform);
                panels[i].SetActive(false);
            }
            else
            {
                panels[i].SetActive(true);
                tween.AppearPopUps(panels[i].transform);
            }
        }
    }
}
