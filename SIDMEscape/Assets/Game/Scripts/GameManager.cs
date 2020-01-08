using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


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

    [SerializeField]
    bool b_blitzMode;
    [SerializeField]
    GameObject go_blitz;
    [SerializeField]
    GameObject go_full;

    System.Random rnd = new System.Random();
    [SerializeField]
    List<GameObject> goArr_puzzleManagers;

    public int nextScene;
    public Animator animator;

    bool puzzleSetter = false;

    public bool getPuzzleSetStatus()
    {
        return puzzleSetter;
    }

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

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //run this only once
        if (!puzzleSetter)
        {
            if (!b_blitzMode /*full*/ )
            {
                GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
                foreach (GameObject go in gos)
                {
                    if (go.layer >= 8 && go.layer <= 11) //add all layers
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
                        hitlayer = rnd.Next(8, 12);
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
            else /*if (SceneManager.GetActiveScene().name == "Blitz")*/
            {
                GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
                foreach (GameObject go in gos)
                {
                    if (go.layer >= 8 && go.layer <= 11) //add all GO in layers 8-10
                    {
                        go.SetActive(false); //set them all false
                        goArr_puzzleManagers.Add(go);
                    }
                }

                int prevNum = 0;
                for (int i = 0; i < 2; ++i) // set 2 puzzles active
                {
                    int hitlayer; //current layer
                    do
                    {
                        hitlayer = rnd.Next(8, 11);
                    } while (prevNum == hitlayer); //make sure current layer not same as previous layer

                    //set the GO of those in the layer, true
                    foreach (GameObject temp in goArr_puzzleManagers)
                    {
                        if (temp.layer == hitlayer)
                        {
                            temp.SetActive(true);
                        }
                    }

                    prevNum = hitlayer;
                }

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
            animator.SetTrigger("FadeOut");

            nextScene = 2;
        }
        else if (go_full.GetComponent<VRControllables.Base.Controllable_Movables>().isGrabbed)
        {
            animator.SetTrigger("FadeOut");

            nextScene = 1;
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