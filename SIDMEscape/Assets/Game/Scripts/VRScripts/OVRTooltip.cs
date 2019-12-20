using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// For displaying a world space canvas/UI Canvas of information
/// Place this on objects that you want to have tooltip for
/// </summary>
public class OVRTooltip : MonoBehaviour
{
    [Tooltip("Reference to the main VR Camera")]
    VRPlayerManager OVRPlayerReference;
    [Header("Tooltip Configuration")]
    [Tooltip("World space Canvas tool tip prefab")]
    public GameObject tooltipCanvas;
    [Tooltip("Tooltip Text to be displayed")]
    [TextArea(15, 20)]
    public string toolTip = "";

    private GameObject localTooltipReference;
    // Start is called before the first frame update
    void Start()
    {
        OVRPlayerReference = VRPlayerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool CreateCanvasTooltip()
    {
        if (OVRPlayerReference == null)
        {
            return false;
        }

        GameObject toolTipCanvas = GameObject.Instantiate(tooltipCanvas);
        toolTipCanvas.GetComponentInChildren<TextMeshProUGUI>().text = toolTip;

        // Try to create it and store in the player reference
        if(OVRPlayerReference.CreateWorldTooltipCanvas(toolTipCanvas))
        {
            return true;
        }
        else
        {
            Destroy(toolTipCanvas);
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if it is a hand
        if (other.gameObject.tag == "Hands")
        {
            if(CreateCanvasTooltip())
            {
                localTooltipReference = OVRPlayerReference.GetWorldTipCanvas();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hands")
        {
            // If its null means that it isnt created yet because theres already an existing tool tip
            if (localTooltipReference == null && OVRPlayerReference.GetWorldTipCanvas() == null)
            {
                // Try to create it
                if (CreateCanvasTooltip())
                {
                    // Store it
                    localTooltipReference = OVRPlayerReference.GetWorldTipCanvas();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if it is a hand
        if (other.gameObject.tag == "Hands")
        {
            // if there isnt even any lcoal tool tip then it means that it belongs to another tool tip object
            if (localTooltipReference == null)
                return;

            // Check if it is the same first
            // Just for safety
            if (localTooltipReference == OVRPlayerReference.GetWorldTipCanvas())
            {
                // Destroy it
                if (OVRPlayerReference.DeleteWorldTipCanvas())
                {
                    // Set this back to null
                    localTooltipReference = null;
                }
            }
        }
    }
}
