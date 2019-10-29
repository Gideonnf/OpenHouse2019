using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestButton : MonoBehaviour
{
    public bool bOnOff = false;

    bool bOnce = false;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Hands"))
            bOnOff = !bOnOff;

        if (bOnOff && this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Count < 4)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Add(this.transform.GetSiblingIndex());
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Remove(this.transform.GetSiblingIndex());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bOnce)
        {
            if (bOnOff && this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Count < 4)
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Add(this.transform.GetSiblingIndex() + 1);
                bOnce = true;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.transform.parent.GetComponent<ChestCombiManager>().arr_testingCombi.Remove(this.transform.GetSiblingIndex() + 1);
            }
        }
    }
}
