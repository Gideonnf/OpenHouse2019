using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorRandomiser : MonoBehaviour
{
    System.Random rnd = new System.Random();

    [Header("Monitor Settings")]
    [Tooltip("Store the materials to change the monitor color ")]
    [SerializeField]
    Material[] arr_monitorMaterials;

    [SerializeField]
    Material _default;

    GameObject go_Monitor;

    [SerializeField]
    GameObject go_TablePuzzle;

    public enum monitor_states
    {
        MS_NONE,
        MS_0121,
        MS_1203,
        MS_2021,
        NUM_OF_STATES
    };
    public monitor_states n_monitorStates;

    int[,] arr_monitorColourCombi;
    int[,] arr_monitorColourBlitzCombi;

    float f_ctimer = 0;
    int n_citer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        arr_monitorColourCombi = new int[,] { { 0, 1, 2, 1}, { 1, 2, 0, 3}, {2, 0, 2, 1 } };

        go_Monitor = this.gameObject;

        n_monitorStates = (monitor_states)rnd.Next(0, (int)monitor_states.NUM_OF_STATES);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (n_monitorStates == 0)
            go_Monitor.SetActive(false);
        else
        {
            f_ctimer += Time.deltaTime * 1;

            if (f_ctimer > 1)
            {
                if (n_citer >= 5)
                    n_citer = 0;
                
                if (n_citer == 0)
                {
                    go_Monitor.GetComponent<Renderer>().materials = new Material[1] { _default };
                }
                else
                    go_Monitor.GetComponent<Renderer>().materials = new Material[1] { arr_monitorMaterials[arr_monitorColourCombi.GetRow((int)n_monitorStates - 1)[n_citer - 1]] };

                f_ctimer = 0;
                ++n_citer;
            }
        }

        //TODO: SET PUZZLE ACTIVE TO RUN PUZZLE LOOP
        for (int i = 0; i < go_TablePuzzle.transform.childCount - 1; ++i)
        {
            if (go_TablePuzzle.transform.GetChild(i).gameObject.GetComponent<MonitorObject>())
            {
                if (go_TablePuzzle.transform.GetChild(i).gameObject.GetComponent<MonitorObject>().correctCoaster)
                    continue;
                else
                    return;
            }

            //TODO: COMPLETE PUZZLE HERE
            PuzzleLightManager.GetInstance().nextLight();
        }
    }
}
