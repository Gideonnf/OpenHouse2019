using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

[System.Serializable]
public class ChestAnswers
{
    public int[] AnswerArrays = new int[4];
}

public class ChestCombiManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_clock; // Clock GO for time to set combi
    [SerializeField]
    GameObject go_drawerObj; //drawer object to affect
    [SerializeField]
    GameObject go_Text; // Reference to the text screen in the numpad
    //[SerializeField]
    //float inputTimer;

    int[,] arr_chestCombi;
    int[,] arr_chestBlitzCombi;

    bool Completed = false;
    //float elapsedTime = 0;
    public List<ChestAnswers> chestAnswerList = new List<ChestAnswers>();

    public List<int> arr_testingCombi; // input buffer to compare code

    // Start is called before the first frame update
    void Start()
    {
        //combination passwords
        arr_chestBlitzCombi = new int[,] { { 1, 5, 2, 5 }, { 1,1, 1, 2 }, { 2, 1, 3, 8 } };
        arr_chestCombi = new int[,] { { 1, 5, 2, 5 }, { 1, 1, 1, 2 }, { 2, 1, 3, 8 } };

        

        arr_testingCombi = new List<int>();

        UpdateScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (Completed)
            return;

       // elapsedTime += Time.deltaTime;


        if (arr_testingCombi.Count == 4)
        {
            if(CompareVariables())
            {
                go_drawerObj.GetComponentInChildren<VRControllables.Base.Slider.Controllable_Slider>().isLocked = false; //unlock drawer

                PuzzleLightManager.Instance.nextLight(); //puzzle complete, set next light
               // SoundManager.instance.playAudio("PuzzleComplete");
                Completed = true;

                arr_testingCombi.Clear();
            }
            else
            {
                arr_testingCombi.Clear(); //if wrong, clear combination
            }
            //if (!GameManager.Instance.getBlitzMode()) //checking if full game
            //{
            //    if (arr_testingCombi.ToArray().SequenceEqual(arr_chestCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates))) //check if arrays are equal
            //    { 
            //        go_drawerObj.GetComponentInChildren<VRControllables.Base.Slider.Controllable_Slider>().isLocked = false; //unlock drawer

            //        PuzzleLightManager.Instance.nextLight(); //puzzle complete, set next light

            //        Completed = true;

            //        arr_testingCombi.Clear();
            //    }
            //    else
            //    {
            //        arr_testingCombi.Clear(); //if wrong, clear combination
            //    }
            //}
            //else
            //{
            //    if (arr_testingCombi.ToArray().SequenceEqual(arr_chestBlitzCombi.GetRow((int)go_clock.GetComponent<ClockRandomiser>().n_clockStates))) //check if arrays are equal
            //    {
            //        go_drawerObj.GetComponent<VRControllables.Base.Slider.Controllable_Slider>().isLocked = false; //unlock drawer

            //        PuzzleLightManager.Instance.nextLight(); //puzzle complete, set next light
            //    }
            //    else
            //    {
            //        arr_testingCombi.Clear(); //if wrong, clear combination
            //    }
            //}
        }
    }

    public bool CompareVariables()
    {
        for(int x = 0; x < chestAnswerList.Count; x++)
        {
            bool correctAnswer = true;

            for (int i = 0; i < arr_testingCombi.Count; i++)
            {
                if (arr_testingCombi[i] != chestAnswerList[x].AnswerArrays[i])
                {
                    correctAnswer = false;
                    break;
                }
            }

            if (correctAnswer == true)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateScreen()
    {
        //int count = 0;
        string numPad = "";

        for (int i = 0; i < 4; ++i)
        {
            // If the count is less than the number of number sin the arr_testing
            if (i < arr_testingCombi.Count)
            {
                numPad += arr_testingCombi[i].ToString();
            }
            else
            {
                numPad += "_";
            }
        }
        go_Text.GetComponent<TextMeshProUGUI>().text = numPad;
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
