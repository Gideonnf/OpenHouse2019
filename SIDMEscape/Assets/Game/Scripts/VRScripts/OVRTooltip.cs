using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For displaying a world space canvas/UI Canvas of information
/// </summary>
public class OVRTooltip : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if it is a hand
        if (other.gameObject.tag == "Hands")
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if it is a hand
        if (other.gameObject.tag == "Hands")
        {

        }
    }
}
