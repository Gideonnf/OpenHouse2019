using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = new GameManager();

    private GameManager() { }

    public static GameManager GetInstance()
    {
        return instance;
    }
    #endregion

    bool b_blitzMode;

    public bool getBlitzMode()
    {
        return b_blitzMode;
    }
    public void setBlitzMode(bool _value)
    {
        b_blitzMode = _value;
    }

    // Use this for initialization
    void Start()
    {
        b_blitzMode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
