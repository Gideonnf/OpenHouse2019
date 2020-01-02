using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region Singleton
    private static SceneChanger instance = new SceneChanger();

    private SceneChanger() { }

    public static SceneChanger GetInstance()
    {
        return instance;
    }
    #endregion


    public void SceneMenu()
    {
        //ON ENTER CODE HERE
        //Camera.allCameras[0].Reset();
        SceneManager.LoadScene("Alwin-merge", LoadSceneMode.Single);
    }

    public void SceneFull()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void SceneBlitz()
    {
        SceneManager.LoadScene("Blitz", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}