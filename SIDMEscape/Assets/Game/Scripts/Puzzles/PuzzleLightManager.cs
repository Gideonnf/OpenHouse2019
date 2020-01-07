using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLightManager : MonoBehaviour
{
    #region Singleton
    private static PuzzleLightManager _instance;

    public static PuzzleLightManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    List<GameObject> arr_Lights;

    int curLight = 0;

    bool blocker = false;

    // Start is called before the first frame update
    void Start()
    {
        arr_Lights = new List<GameObject>();
    }
    
    /// <summary>
    /// Destroy light that is not meant to be there
    /// Add lights that are and switch them off
    /// </summary>
    public void init()
    {
        //find the light whose puzzle is inactive
        int skipObj = 0;
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            //if light false; if set false by gamemanager means not meant to be ran
            if (this.transform.GetChild(i).gameObject.activeInHierarchy == false)
            {
                //destroy
                Destroy(this.transform.GetChild(i).gameObject);
                skipObj = i;
            }
        }

        //add the rest of the lights and set false
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (i == skipObj)
                continue;

            arr_Lights.Add(this.transform.GetChild(i).gameObject);
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        arr_Lights[curLight].SetActive(true);
    }

    /// <summary>
    /// Switch lights
    /// </summary>
    public void nextLight()
    {
        arr_Lights[curLight].SetActive(false);

        ++curLight;
        arr_Lights[curLight].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //run once
        if (!blocker && GameManager.Instance.getPuzzleSetStatus())
        {
            init();
            blocker = true;
        }
    }
}
