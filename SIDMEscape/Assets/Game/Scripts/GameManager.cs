using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    bool b_blitzMode;
    [SerializeField]
    GameObject go_blitz;
    [SerializeField]
    GameObject go_full;

    System.Random rnd = new System.Random();
    [SerializeField]
    List<GameObject> goArr_puzzleManagers;

    bool puzzleSetter = false;

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
        goArr_puzzleManagers = new List<GameObject>();
        b_blitzMode = false;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleSetter)
        {
            if (SceneManager.GetActiveScene().name == "Game" /*full*/ )
            {
                GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
                foreach (GameObject go in gos)
                {
                    if (go.layer >= 8 && go.layer <= 10) //add all layers
                    {
                        go.SetActive(false);
                        goArr_puzzleManagers.Add(go);
                    }
                }

                int prevNum = 0;
                for (int i = 0; i < 2; ++i)
                {
                    int hitlayer;
                    do
                    {
                        hitlayer = rnd.Next(8, 11);
                    } while (prevNum == hitlayer);

                    foreach (GameObject temp in goArr_puzzleManagers)
                    {
                        if (temp.layer == hitlayer)
                        {
                            temp.SetActive(true);
                            //goArr_puzzleManagers.Remove(temp);
                        }
                    }
                    prevNum = hitlayer;
                }

                puzzleSetter = true;
            }
            else if (SceneManager.GetActiveScene().name == "Blitz")
            {

                puzzleSetter = true;
            }
        }


        //NOTHING BELOW THIS LINE
        if (go_blitz == null || go_full == null)
        {
            return;
        }

        if (go_blitz.GetComponent<VRControllables.Base.Controllable_Movables>().isGrabbed)
        {
            b_blitzMode = true;
            SceneChanger.GetInstance().SceneBlitz();
        }
        else if (go_full.GetComponent<VRControllables.Base.Controllable_Movables>().isGrabbed)
        {
            SceneChanger.GetInstance().SceneFull();
        }
    }
}

public static class ArrayExt
{
    public static T[] GetRow<T>(this T[,] array, int row)
    {
        if (!typeof(T).IsPrimitive)
            throw new InvalidOperationException("Not supported for managed types.");

        if (array == null)
            throw new ArgumentNullException("array");

        int cols = array.GetUpperBound(1) + 1;
        T[] result = new T[cols];

        int size;

        if (typeof(T) == typeof(bool))
            size = 1;
        else if (typeof(T) == typeof(char))
            size = 2;
        else
            size = Marshal.SizeOf<T>();

        Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

        return result;
    }
}