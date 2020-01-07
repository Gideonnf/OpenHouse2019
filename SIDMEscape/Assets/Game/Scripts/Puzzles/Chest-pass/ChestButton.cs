using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestButton : MonoBehaviour
{
    public bool bOnOff = false;
    public int buttonNum = 0;

    bool bOnce = false;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hands"))
            bOnOff = !bOnOff; //change colour of button for visual feedback

        if (bOnOff && this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Count < 4)
        {
            Debug.Log("Num Pressed :" + this.transform.GetSiblingIndex());
            this.gameObject.GetComponent<Renderer>().material.color = Color.green; //set colour that shows its pressed
            this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Add(buttonNum); //add number to list
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red; //set colour that shows its cancelled
            this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Remove(buttonNum); //remove number from list
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            bOnOff = !bOnOff; //change colour of button for visual feedback
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bOnOff)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red; //change colour of button for visual feedback
        }
    }
}
    