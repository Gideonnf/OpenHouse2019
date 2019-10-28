using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCombiManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_clock; // Clock GO for time to set combi

    int[,] arr_chestCombi;

    public List<int> arr_testingCombi;

    // Start is called before the first frame update
    void Start()
    {
        arr_chestCombi = new int[3, 4] { { 5, 6, 5, 3 }, { 2, 6, 3, 6 }, { 1, 7, 4, 8 } };
        arr_testingCombi = new List<int>(4);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    //Check for a match with the specified name on any GameObject that collides with your GameObject
    //    if (collision.gameObject.name == "")
    //    {

    //    }
    //}
}
