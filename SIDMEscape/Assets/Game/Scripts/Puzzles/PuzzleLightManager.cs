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

    List<GameObject> arr_Lights;

    int curLight = 0;

    bool blocker = false;

    // Start is called before the first frame update
    void Start()
    {
        arr_Lights = new List<GameObject>();
    }

    public void init()
    {
        int skipObj = 0;
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (this.transform.GetChild(i).gameObject.activeInHierarchy == false)
            {
                Destroy(this.transform.GetChild(i).gameObject);
                skipObj = i;
            }
        }

        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (i == skipObj)
                continue;

            arr_Lights.Add(this.transform.GetChild(i).gameObject);
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        arr_Lights[curLight].SetActive(true);
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
        if (!blocker && FindObjectOfType<GameManager>().getPuzzleSetStatus())
        {
            init();
            blocker = true;
        }
    }
}
