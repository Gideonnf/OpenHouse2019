using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiseEnPlace : MonoBehaviour
{
    Vector3[,] v3_placePos;

    [SerializeField]
    GameObject go_Monitor;
    [SerializeField]
    GameObject[] go_Objects;
    [SerializeField]
    GameObject[] go_Places;

    // Start is called before the first frame update
    void Start()
    {
        v3_placePos = new Vector3[,] { {new Vector3(1.391f, -0.156f, -2.37f), new Vector3(0.436f, -0.156f, -2.464f), new Vector3(0.782f, -0.156f, -2.623f) },
                                                               {new Vector3(1.652f, -0.156f, -2.526f), new Vector3(0.975f, -0.156f, -2.464f), new Vector3(0.545f, -0.156f, -2.392f) },
                                                               {new Vector3(0.545f, -0.156f, -2.358f), new Vector3(0.973f, -0.156f, -2.589f), new Vector3(1.56f, -0.156f, -2.352f) }};

        for (int j = 0; j < go_Places.Length; ++j)
        {
            if ((int)go_Monitor.GetComponent<MonitorRandomiser>().n_monitorStates == 0)
                break;

            go_Places[j].transform.localPosition = v3_placePos[(int)go_Monitor.GetComponent<MonitorRandomiser>().n_monitorStates - 1, j];
        }

        for (int i = 0; i < go_Objects.Length; ++i)
        {
            go_Objects[i].transform.localPosition = new Vector3(Random.Range(0.24f, 1.8f), go_Objects[i].transform.position.y, Random.Range(-2.707f, -2.3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
