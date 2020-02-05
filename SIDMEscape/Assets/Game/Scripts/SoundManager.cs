using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioObject
{
    public string name;
    public AudioClip audioFile;
    public bool isLooping = false;
    public bool isRepeatable = false;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    AudioSource audioSource;
    
    public List<AudioObject> ListOfAudioObjects = new List<AudioObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Plays the audio from the audio source in the game manager
    /// </summary>
    /// <param name="audioName"></param>
    /// <returns></returns>
    public bool playAudio(string audioName)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if(audio.name == audioName)
            {
                // If it is already playing
                //if (audioSource.clip == audio.audioFile)
                //{
                //    if (audio.isRepeatable == false)
                //        return false;
                //}

                audioSource.clip = audio.audioFile;
                audioSource.loop = audio.isLooping;
                audioSource.Play();
                return true;
            }
        }

        return false;
    }

    //public bool playAudioOnce(string audioName)
    //{
    //    foreach (AudioObject audio in ListOfAudioObjects)
    //    {
    //        if (audio.name == audioName)
    //        {
    //            audioSource.clip = audio.audioFile;
    //           // audioSource.loop = audio.isLooping;
    //            audioSource.Playonce
    //            return true;
    //        }
    //    }

    //    return false;

    //}

    /// <summary>
    /// ]Plays audio from a source outside of the game manager
    /// </summary>
    /// <param name="audioName"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public bool playAudio(string audioName, AudioSource source)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.name == audioName)
            {
                source.clip = audio.audioFile;
                source.loop = audio.isLooping;
                source.Play();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Pauses the audio
    /// </summary>
    /// <returns></returns>
    public bool PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Controlling external audio sources
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public bool PauseAudio(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Pause();
            return true;
        }

        return false;
    }
}
