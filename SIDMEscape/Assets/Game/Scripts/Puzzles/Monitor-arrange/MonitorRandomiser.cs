﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorRandomiser : MonoBehaviour
{
    #region Singleton
    private static MonitorRandomiser _instance;

    public static MonitorRandomiser Instance { get { return _instance; } }


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

        go_Monitor = this.gameObject;

        n_monitorStates = (monitor_states)rnd.Next(0, (int)monitor_states.NUM_OF_STATES);
    }
    #endregion

    System.Random rnd = new System.Random(); //init randomiser C# style

    [Header("Monitor Settings")]
    //[Tooltip("Store the materials to change the monitor color ")]
    //[SerializeField]
    //Material[] arr_monitorMaterials;
    //[SerializeField]
    //Material _default;

    GameObject go_Monitor;

    [SerializeField]
    GameObject go_TablePuzzle;

    bool Completed = false;

    public enum monitor_states
    {
        MS_0121,
        MS_1203,
        MS_2021,
        NUM_OF_STATES
    };
    public monitor_states n_monitorStates;

    //int[,] arr_monitorColourCombi;
    //int[,] arr_monitorColourBlitzCombi;

    //float f_ctimer = 0;
    //int n_citer = 0;

    // Update is called once per frame
    void Update()
    { 
        //commented out cause this does the colour changing
        //f_ctimer += Time.deltaTime * 1; //timer for per second to change the colour

        ////cycling through colours
        //if (f_ctimer > 1)
        //{
        //    if (n_citer >= 5)
        //        n_citer = 0;

        //    if (n_citer == 0)
        //    {
        //        go_Monitor.GetComponent<Renderer>().materials = new Material[1] { _default };
        //    }
        //    else //get the monitor material from the colour combi array
        //        go_Monitor.GetComponent<Renderer>().materials = new Material[1] { arr_monitorMaterials[arr_monitorColourCombi.GetRow((int)n_monitorStates)[n_citer - 1]] }; //n_citer - 1 cause 0 wil be default

        //    f_ctimer = 0;
        //    ++n_citer;
        //}

        //checking if correct coaster
        for (int i = 0; i < go_TablePuzzle.transform.childCount - 1; ++i)
        {
            if (go_TablePuzzle.transform.GetChild(i).gameObject.GetComponent<MonitorObject>())
            {
                if (go_TablePuzzle.transform.GetChild(i).gameObject.GetComponent<MonitorObject>().correctCoaster)
                    continue; //if there is a correct coaster, check the rest
                else
                    return; //so long there is a false, puzzle is incomplete
            }

        }

        if (Completed == false)
        {
            //puzzle done, set next puzzle
            PuzzleLightManager.Instance.nextLight();
            Completed = true;
        }

    }
}
