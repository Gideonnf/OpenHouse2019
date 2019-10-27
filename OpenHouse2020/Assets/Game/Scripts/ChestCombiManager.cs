using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCombiManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_clock; // Clock GO for time to set combi

    int[,] arr_chestCombi;

    // Start is called before the first frame update
    void Start()
    {
        arr_chestCombi = new int[3, 4]{{5, 6, 5, 3 }, {2, 6, 3, 6 }, {1, 7, 4, 8}};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
