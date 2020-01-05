using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(GameManager.GetInstance().nextScene);
    }
}
