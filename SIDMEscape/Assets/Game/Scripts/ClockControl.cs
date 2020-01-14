using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockControl : MonoBehaviour
{
    [Tooltip("Reference for the TMPro Component")]
    public TextMeshProUGUI textReference;
    [Tooltip("Timer for the round")]
    public float roundTime = 120.0f;

    public GameObject ClockReference;
    ClockRandomiser clockRandom;
    // Start is called before the first frame update
    void Start()
    {
        clockRandom = ClockReference.GetComponentInChildren<ClockRandomiser>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(clockRandom.n_clockStates)
        {
            case ClockRandomiser.Clock_States.CS_1112:
                {
                    textReference.text = "11:12";
                }
                break;
            case ClockRandomiser.Clock_States.CS_1525:
                {
                    textReference.text = "15:25";

                }
                break;
            case ClockRandomiser.Clock_States.CS_2138:
                {
                    textReference.text = "21:38";
                }
                break;
        }
        //roundTime -= Time.deltaTime;
        //string minutes = "";
        //string seconds = "";
        //if (roundTime > 0)
        //{
        //     minutes = Mathf.Floor(roundTime / 60).ToString("00");
        //     seconds = (roundTime % 60).ToString("00");

        //    // Special formatting when seconds is at 60
        //    if (seconds == "60")
        //    {
        //        seconds = "00";
        //        float fMinute = Mathf.Floor(roundTime / 60);
        //        fMinute += 1;
        //        minutes = fMinute.ToString("00");
        //    }

        //    textReference.text = string.Format("{0}:{1}", minutes, seconds);
        //}
        //else
        //{
        //    // Time is up, its set to 0, 0
        //    minutes = "00";
        //    seconds = "00";

        //    textReference.text = string.Format("{0}:{1}", minutes, seconds);

        //}


    }
}
