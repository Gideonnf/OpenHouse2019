using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioObject
{
    public string name;
    public AudioClip audioFile;
    public bool isLooping = false;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
