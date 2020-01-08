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
        CS_1525,
        CS_1112,
        CS_2138,
        NUM_OF_STATES
    };
    public Clock_States n_clockStates;

    // Start is called before the first frame update
    void Start()
    {
        n_clockStates = (Clock_States)rnd.Next(0, (int)Clock_States.NUM_OF_STATES);
        go_Clock = this.gameObject;

        go_Clock.GetComponent<Renderer>().materials = new Material[1] { arr_clockMaterials[(int)n_clockStates] }; //set clock material
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
