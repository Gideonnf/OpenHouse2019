using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class WorldSpacePlayer : MonoBehaviour
{
    [Header("Video Player Settings")]
    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    private int videoClipIndex = 0;

    [Header("Audio Settings")]
    GameObject playerCamera;
    [Tooltip("Audio source ")]
     AudioSource audio;


    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        videoClipIndex = (int)MonitorRandomiser.Instance.n_monitorStates;

        if (GameManager.Instance)
            if (GameManager.Instance.getBlitzMode())
                videoClipIndex = 3;

        videoPlayer.targetTexture.Release();
        videoPlayer.clip = videoClips[videoClipIndex];
        playerCamera = VRPlayerManager.Instance.mainCameraReference;

    }

    // Update is called once per frame
    void Update()
    {
        // If the audio is playing
        if (audio.isPlaying)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.transform.position);

            //Debug.Log("Distance to audio Source : " + distance);

            // Cant go below 1
            if (distance < 1)
                distance = 1;

            audio.volume = 1 / distance;
        }
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
