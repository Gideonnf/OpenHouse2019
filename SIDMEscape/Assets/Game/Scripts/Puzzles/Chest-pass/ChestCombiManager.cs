using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChestCombiManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_clock; // Clock GO for time to set combi
    [SerializeField]
    GameObject go_drawerObj; //drawer object to affect

    int[,] arr_chestCombi;
    int[,] arr_chestBlitzCombi;

    public List<int> arr_testingCombi; // input buffer to compare code

    // Start is called before the first frame update
    void Start()
    {
        arr_chestBlitzCombi = new int[,] { { 0, 3, 2, 5 }, { 1, 1, 1, 2 }, { 2, 0, 0, 8 } };
        //go_drawerObj.GetComponent<VRControllables.Base.Slider.Controllable_Slider>().isLocked = false;

        arr_chestCombi = new int[, ] { { 5, 6, 5, 3 }, { 2, 6, 3, 6 }, { 1, 7, 4, 8 } };
        arr_testingCombi = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arr_testingCombi.Count == 4)
        {
            if (!GameManager.GetInstance().getBlitzMode())
            {
                if (arr_testingCombi.ToArray().SequenceEqual(arr_chestCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates)))
                {
                    //Debug.LogError("Do not panic, it works");
                    go_drawerObj.GetComponent<VRControllables.Base.Slider.Controllable_Slider>().isLocked = false;

                    PuzzleLightManager.GetInstance().nextLight();
                }
                else
                {
                    arr_testingCombi.Clear();
                }
            }
            else
            {
                if (arr_testingCombi.ToArray().SequenceEqual(arr_chestBlitzCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates - 1)))
                {
                    Debug.LogError("Do not panic, it works");
                }
                else
                {
                    arr_testingCombi.Clear();
                }
            }

        }
    }

    /// <summary>
    /// Call this function in the lever object
    /// </summary>
    /// <param name="triggerID"></param>
    public void LeverTrigger(int triggerID)
    {
        switch (triggerID)
        {

        }
    }

}
