using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLightManager : MonoBehaviour
{
    #region Singleton
    private static PuzzleLightManager instance = new PuzzleLightManager();

    private PuzzleLightManager() { }

    public static PuzzleLightManager GetInstance()
    {
        return instance;
    }
    #endregion

    GameObject[] arr_Lights;

    int curLight = 0;

    bool blocker = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void nextLight()
    {
        arr_Lights[curLight].SetActive(false);

        ++curLight;
        arr_Lights[curLight].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (blocker)
        {
            arr_Lights = new GameObject[this.transform.childCount];

            for (int i = 0; i < this.transform.childCount; ++i)
            {
                arr_Lights[i] = this.transform.GetChild(i).gameObject;
            }

            arr_Lights[curLight].SetActive(true);
        }
    }
}
