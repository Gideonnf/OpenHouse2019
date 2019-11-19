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

    public bool b_blitzMode { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
