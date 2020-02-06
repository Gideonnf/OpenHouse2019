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

    public bool EndScene = false;

    public Animator transitionAnim;

    bool endPuzzle = false;
    float endTimer = 0.0f;

   // public int nextScene;

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

        if (EndScene)
            return;

        int skipObj = 0;
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            //if light false; if set false by gamemanager means not meant to be ran
            if (this.transform.GetChild(i).gameObject.activeInHierarchy == false)
            {
                //destroy
                Destroy(this.transform.GetChild(i).gameObject);
                //skipObj = i;
            }
            else
            {
                arr_Lights.Add(this.transform.GetChild(i).gameObject);
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //add the rest of the lights and set false
        //for (int i = 0; i < this.transform.childCount; ++i)
        //{
        //    if (i == skipObj)
        //        continue;

        //    arr_Lights.Add(this.transform.GetChild(i).gameObject);
        //    this.transform.GetChild(i).gameObject.SetActive(false);
        //}

        arr_Lights[curLight].SetActive(true);
    }

    /// <summary>
    /// Switch lights
    /// </summary>
    public void nextLight()
    {
        if (EndScene)
            return;

        arr_Lights[curLight].SetActive(false);
        SoundManager.instance.playAudio("PuzzleComplete");
        if (curLight + 1 >= this.transform.childCount)
        {
            //can end the game here
            endPuzzle = true;
            endTimer = 10.0f;
            //nextScene = 2;
            return;
        }
        else
            ++curLight;

        arr_Lights[curLight].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (EndScene)
            return;

        // If the puzzles finished
        if (endPuzzle)
        {
            endTimer -= Time.deltaTime;
            // When hte timer is done, start the transition
            // so that the player doesnt transition right after the end of the puzzle
            if (endTimer <= 0.0f)
            {
                transitionAnim.SetTrigger("FadeOut");
                GameManager.Instance.nextScene = 2;
            }
        }

        //run once
        if (!blocker && GameManager.Instance.getPuzzleSetStatus())
        {
            init();
            blocker = true;
        }
    }
}
