using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VRControllables.Base;

/// <summary>
/// For displaying a world space canvas/UI Canvas of information
/// Place this on objects that you want to have tooltip for
/// </summary>
public class OVRTooltip : MonoBehaviour
{
    [Tooltip("Reference to the main VR Camera")]
    VRPlayerManager OVRPlayerReference;
    [Tooltip("Reference to the object VR Interactable script")]
    Controllable_Movables VRMovableReference;
    [Tooltip("Scale of canvas in object mode")]
    float objectModeScale = 0.004f;
    [Tooltip("Scale of canvas in camera mode")]
    float cameraModeScale = 0.0006f;
    [Tooltip("To track if the canvas was closed by the player")]
    bool isClosed = false;

    [Header("Tooltip Configuration")]
    [Tooltip("If it is attached to the object or player camera")]
    public bool isObjectTooltip = true;
    [Tooltip("World space Canvas tool tip prefab")]
    public GameObject tooltipCanvas;
    [Tooltip("Tooltip Text to be displayed")]
    [TextArea(6, 8)]
    public string toolTip = "";

    private GameObject localTooltipReference;
    // Start is called before the first frame update
    void Start()
    {
        OVRPlayerReference = VRPlayerManager.Instance;
        VRMovableReference = GetComponent<Controllable_Movables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (VRMovableReference == null)
        {
            Debug.LogError("VRMovableReference not found");
            return;
        }

        // If it is grabbed
            // If there isnt a tool tip created yet
        if (localTooltipReference == null)
        {
            if (VRMovableReference.isGrabbed && !isClosed)
            {
                CreateCanvasTooltip();
            }
        }
        else
        {
          
            // If it is in object tool tip mode, the canvas has to rotate towards the camera
            if (isObjectTooltip)
            {
                // Looking Function
                {
                    // Only need the X and Z positions
                    Vector3 targetPosition = OVRPlayerReference.mainCameraReference.transform.position;
                    targetPosition.y = localTooltipReference.transform.position.y;

                    //Turn to the target position
                    localTooltipReference.transform.LookAt(targetPosition, localTooltipReference.transform.up);
                }

                // Moving Function
                {
                    Vector3 targetPosition = this.transform.position;
                    targetPosition += Vector3.up * 0.25f;

                    //Vector3 distance = targetPosition - localTooltipReference.transform.position;

                   // if (Vector3.Distance(localTooltipReference.transform.position, targetPosition) > 0.1f)
                  //  {
                        localTooltipReference.transform.position = Vector3.Slerp(localTooltipReference.transform.position, targetPosition, 1.0f);
                    //}
                }
                // If it is no longer being grabbed
                if (!VRMovableReference.isGrabbed)
                {
                    Destroy(localTooltipReference);
                    localTooltipReference = null;
                }
                else if (OVRInput.Get(OVRInput.Button.One))
                {
                    Destroy(localTooltipReference);
                    localTooltipReference = null;
                    isClosed = true;
                }
            }
            // If it is camera mode
            else
            {
                // If it is no longer being grabbed
                if (!VRMovableReference.isGrabbed)
                {
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
                else if (OVRInput.Get(OVRInput.Button.One))
                {
                    // Check if it is the same first
                    // Just for safety
                    if (localTooltipReference == OVRPlayerReference.GetWorldTipCanvas())
                    {
                        // Destroy it
                        if (OVRPlayerReference.DeleteWorldTipCanvas())
                        {
                            // Set this back to null
                            localTooltipReference = null;
                            isClosed = true;
                        }
                    }
                }

            }
        }
        
    }

    protected bool CreateCanvasTooltip()
    {
        if (OVRPlayerReference == null)
        {
            return false;
        }

        GameObject toolTipCanvas = GameObject.Instantiate(tooltipCanvas);
        toolTipCanvas.GetComponentInChildren<TextMeshProUGUI>().text = toolTip;

        if (isObjectTooltip)
        {
            toolTipCanvas.transform.position = this.transform.position + new Vector3(0, 0.25f, 0);
           // toolTipCanvas.transform.SetParent(this.transform);
            //toolTipCanvas.transform.localScale = new Vector3(objectModeScale, objectModeScale, objectModeScale);
            localTooltipReference = toolTipCanvas;
            return false;
        }
        // If its meant for VR Camera
        else
        {
            // Try to create it and store in the player reference
            if (OVRPlayerReference.CreateWorldTooltipCanvas(toolTipCanvas))
            {
                localTooltipReference = OVRPlayerReference.GetWorldTipCanvas();
                return true;
            }
            else
            {
                // Destroy it since it cant be created
                Destroy(toolTipCanvas);
                return false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //// Check if it is a hand
        //if (other.gameObject.tag == "Hands")
        //{
            
        //    if (!isObjectTooltip)
        //    {
        //        if (CreateCanvasTooltip())
        //        {
        //            localTooltipReference = OVRPlayerReference.GetWorldTipCanvas();
        //        }
        //    }
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Hands")
        //{
        //    // If its null means that it isnt created yet because theres already an existing tool tip
        //    if (localTooltipReference == null && OVRPlayerReference.GetWorldTipCanvas() == null)
        //    {
        //        // Try to create it
        //        if (CreateCanvasTooltip())
        //        {
        //            // Store it
        //            localTooltipReference = OVRPlayerReference.GetWorldTipCanvas();
        //        }
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //// Check if it is a hand
        //if (other.gameObject.tag == "Hands")
        //{
        //    // if there isnt even any lcoal tool tip then it means that it belongs to another tool tip object
        //    if (localTooltipReference == null)
        //        return;

        //    // Check if it is the same first
        //    // Just for safety
        //    if (localTooltipReference == OVRPlayerReference.GetWorldTipCanvas())
        //    {
        //        // Destroy it
        //        if (OVRPlayerReference.DeleteWorldTipCanvas())
        //        {
        //            // Set this back to null
        //            localTooltipReference = null;
        //        }
        //    }
        //}
    }
}
