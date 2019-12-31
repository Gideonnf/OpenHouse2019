using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ClockRandomiser : MonoBehaviour
{
    System.Random rnd = new System.Random();

    [SerializeField]
    Material[] arr_clockMaterials;
    GameObject go_Clock;

    public enum Clock_States
    {
        CS_0325,
        CS_1112,
        CS_2008,
        NUM_OF_STATES
    };
    public Clock_States n_clockStates;

    // Start is called before the first frame update
    void Start()
    {
        go_Clock = this.gameObject;

        //n_clockStates = (Clock_States)rnd.Next(0, (int)Clock_States.NUM_OF_STATES);

        Debug.Log(n_clockStates);

        go_Clock.GetComponent<Renderer>().materials = new Material[1] { arr_clockMaterials[(int)n_clockStates] };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
