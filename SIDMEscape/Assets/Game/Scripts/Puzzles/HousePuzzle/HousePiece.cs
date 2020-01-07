using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.localPosition.x < 1 && this.transform.localPosition.y < 1 && this.transform.localPosition.z < 1) &&
            (this.transform.localPosition.x > -1 && this.transform.localPosition.y > -1 && this.transform.localPosition.z > -1))
        {
            if ((this.transform.localEulerAngles.x < 5 && this.transform.localPosition.y < 5 && this.transform.localPosition.z < 5) &&
            (this.transform.localPosition.x > 355 && this.transform.localPosition.y > 355 && this.transform.localPosition.z > 355))
            {
                this.transform.localPosition = Vector3.zero;
                this.transform.localEulerAngles = Vector3.zero;
            }
        }
    }
}
