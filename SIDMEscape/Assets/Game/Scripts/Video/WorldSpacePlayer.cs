using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class WorldSpacePlayer : MonoBehaviour
{
    public VideoClip[] videoClips;

    private VideoPlayer videoPlayer;
    private int videoClipIndex = 0;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.clip = videoClips[videoClipIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
}
